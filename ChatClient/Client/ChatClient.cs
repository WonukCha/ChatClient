using ChatClient.Client.Packet;
using ChatClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ChatClient.Client.PacketDefine;

namespace ChatClient
{

    public delegate void DOnConnect(string ip, int port);
    public delegate void DOnDisconnect();
    public delegate void DOnReceive(byte[] bytes, int size);
    public delegate void DOnSend(byte[] bytes, int size);
    public delegate void DReceiveChat(string user, string chat);
    public delegate void DResponse();
    public delegate void DSystemInfo(string msg);
    public delegate void DUserType(USER_STATUS_INFO type);

    class ChatClient : TcpSocketClient
    {
        public event DOnConnect _eOnConnect;
        public event DOnDisconnect _eOnDisconnect;
        public event DOnReceive _eOnReceive;
        public event DOnSend _eOnSend;
        public event DReceiveChat _eReceiveChat;
        public event DSystemInfo _eSystemInfo;
        public event DUserType _eUserType;


        private PacketBufferManager _packetBufferManager = new PacketBufferManager();
        private Thread _receiveThread = null;
        private bool _runReceiveThread = false;

        Dictionary<PacketDefine.PACKET_ID, Action<PacketData>> packetFuncDic = new Dictionary<PacketDefine.PACKET_ID, Action<PacketData>>();

        UInt16 _roomNumber = 0;

        public ChatClient()
        {
            InitPacketFuncDic();

            _packetBufferManager.Init(1024,500,13);
            _runReceiveThread = true;
            _receiveThread = new Thread(ReceiveProcess);
            _receiveThread.Name = "receiveThread";
            _receiveThread.Start();
        }
        ~ChatClient()
        {
            
        }

        public void Dispose()
        {
            _runReceiveThread = false;
            if (_receiveThread != null)
            {
                _receiveThread.Join();
            }
            base.Dispose();
        }

        public override void OnConnect(string ip, int port) 
        {
            if(_eOnConnect != null)
            {
                _eOnConnect(ip,port);
            }
        }
        public override void OnDisconnect() 
        {
            if (_eOnDisconnect != null)
            {
                _eOnDisconnect();
            }
        }
        public override void OnReceive(byte[] bytes, int size) 
        {
            if (_eOnReceive != null)
            {
                _eOnReceive(bytes, size);
            }
            _packetBufferManager.Write(bytes, 0, size);
        }
        public override void OnSend(byte[] bytes, int size) 
        {
            if (_eOnSend != null)
            {
                _eOnSend(bytes, size);
            }
        }
        private void SystemInfo(string msg)
        {
            if(_eSystemInfo != null)
            {
                _eSystemInfo(msg);
            }
        }
        private void UserType(USER_STATUS_INFO type)
        {
            if (_eUserType != null)
            {
                _eUserType(type);
            }
        }
        private void ReceiveProcess()
        {
            bool wasWorked = false;
            while (_runReceiveThread)
            {
                do
                {
                    if (_packetBufferManager.Size() > PacketDefine.HEDER_SIZE)
                    {
                        PacketData packetData = _packetBufferManager.ReadPacket();
                        wasWorked = true;
                        
                        packetFuncDic[(PacketDefine.PACKET_ID)packetData.PacketID](packetData);
                    }
                } while (false);
                
                if (wasWorked == false)
                {
                    Thread.Sleep(16);
                }
            }
        }
        public bool Login(string id, string pw)
        {
            LoginRequest loginRequest = new LoginRequest();
            if(loginRequest.SetIdPw(id, pw))
            {
                PacketData packetData;
                List<byte> datas = new List<byte>();
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.LOGIN_REQUEST)));
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + loginRequest.GetSize())));
                datas.AddRange(new byte[] { (byte)0 });
                datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
                datas.AddRange(loginRequest.ToBytes());
                SendData(datas.ToArray());
            }
            return true;
        }
        public bool LogOut()
        {
            return true;
        }
        public bool EnterRoom(uint roomNumber)
        {
            RoomEnterRequest roomEnterRequest = new RoomEnterRequest();
            roomEnterRequest.SetRoomNumber((ushort)roomNumber);

            PacketData packetData;
            List<byte> datas = new List<byte>();
            datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.ROOM_ENTER_REQUEST)));
            datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + roomEnterRequest.GetSize())));
            datas.AddRange(new byte[] { (byte)0 });
            datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
            datas.AddRange(roomEnterRequest.ToBytes());
            SendData(datas.ToArray());

            _roomNumber = (UInt16)roomNumber;
            return true;
        }

        public bool LeaveRoom()
        {
            RoomLeaveRequest roomLeaveRequest = new RoomLeaveRequest();
            roomLeaveRequest.SetRoomNumber(_roomNumber);

            PacketData packetData;
            List<byte> datas = new List<byte>();
            datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.ROOM_LEAVE_REQUEST)));
            datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + roomLeaveRequest.GetSize())));
            datas.AddRange(new byte[] { (byte)0 });
            datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
            datas.AddRange(roomLeaveRequest.ToBytes());
            SendData(datas.ToArray());
            return true;
        }
            public bool SendChat(string chat)
        {
            bool bResult = false;
            if(IsConnected())
            {
                RoomChatRequest roomChatRequest = new RoomChatRequest();
                roomChatRequest.SetValue(chat);

                List<byte> datas = new List<byte>();
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.ROOM_CHAT_REQUEST)));
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + roomChatRequest.GetSize())));
                datas.AddRange(new byte[] { (byte)0 });
                datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
                datas.AddRange(roomChatRequest.ToBytes());
                SendData(datas.ToArray());
                bResult = true;
            }
            else
            {
                bResult = false;
            }
            
            return bResult;
        }

        private void InitPacketFuncDic()
        {
            packetFuncDic.Add(PacketDefine.PACKET_ID.SYSYEM_DISCONNECT, PacketFunc_SystemDisconnect);
            packetFuncDic.Add(PacketDefine.PACKET_ID.SYSYEM_CONNECT, PacketFunc_SystemConnect);
            packetFuncDic.Add(PacketDefine.PACKET_ID.LOGIN_RESPONSE, PacketFunc_LoginResponse);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ALL_USER_CHAT_RESPONSE, PacketFunc_AllUserChatResponse);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ALL_USER_CHAT_NOTIFY, PacketFunc_AllUserChatNotify);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ROOM_ENTER_RESPONSE, PacketFunc_RoomEnterResponse);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ROOM_LEAVE_RESPONSE, PacketFunc_RoomLeaveResponse);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ROOM_CHAT_RESPONSE, PacketFunc_RoomChatResponse);
            packetFuncDic.Add(PacketDefine.PACKET_ID.ROOM_CHAT_NOTIFY, PacketFunc_RoomChatNotify);
        }

        private void PacketFunc_SystemDisconnect(PacketData packetData) 
        {
        }
        private void PacketFunc_SystemConnect(PacketData packetData) 
        {

        }
        private void PacketFunc_LoginResponse(PacketData packetData) 
        {
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.FromBytes(packetData.BodyData, 0);
            if(loginResponse.result == 1)
            {
                SystemInfo("Login Success");
                UserType(USER_STATUS_INFO.LOBBY);
            }
            else
            {
                SystemInfo("Login fail");
                UserType(USER_STATUS_INFO.CONNECT);
            }
        }
        private void PacketFunc_AllUserChatResponse(PacketData packetData) 
        {
            AllUserChatResponse allUserChatResponse = new AllUserChatResponse();
            SystemInfo("Receive All User Chat");

        }
        private void PacketFunc_AllUserChatNotify(PacketData packetData) 
        {
            RoomChatNotify roomChatNotify = new RoomChatNotify();
            if(roomChatNotify.FromBytes(packetData.BodyData,0))
            {
                if (_eReceiveChat != null)
                {
                    _eReceiveChat(roomChatNotify.GetId(), roomChatNotify.GetMsg());
                }
            }
        }
        private void PacketFunc_RoomEnterResponse(PacketData packetData) 
        {
            RoomEnterResponse roomEnterResponse = new RoomEnterResponse();
            if(roomEnterResponse.FromBytes(packetData.BodyData,0))
            {
                if(roomEnterResponse.result == 1)
                {
                    UserType(USER_STATUS_INFO.ROOM);
                }
            }
        }
        private void PacketFunc_RoomLeaveResponse(PacketData packetData) 
        {
            RoomLeaveResponse roomLeaveResponse = new RoomLeaveResponse();
            if(roomLeaveResponse.FromBytes(packetData.BodyData,0))
            {
                if(roomLeaveResponse.result == 1)
                {
                    UserType(USER_STATUS_INFO.LOBBY);
                }
            }
        }
        private void PacketFunc_RoomChatResponse(PacketData packetData) 
        {
            RoomChatResponse roomChatResponse = new RoomChatResponse();
            if(roomChatResponse.FromBytes(packetData.BodyData,0))
            {
                SystemInfo("Success RoomChatResponse");
            }

        }
        private void PacketFunc_RoomChatNotify(PacketData packetData) 
        {
            RoomChatNotify roomChatNotify =new RoomChatNotify();
            if( roomChatNotify.FromBytes(packetData.BodyData,0))
            {
                if (_eReceiveChat != null)
                {
                    _eReceiveChat(roomChatNotify.GetId(), roomChatNotify.GetMsg());
                }
            }
        }
    }
}
 