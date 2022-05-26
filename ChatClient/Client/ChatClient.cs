using ChatClient.Client.Packet;
using ChatClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    public delegate void DOnConnect(string ip, int port);
    public delegate void DOnDisconnect();
    public delegate void DOnReceive(byte[] bytes, int size);
    public delegate void DOnSend(byte[] bytes, int size);
    public delegate void DReceiveChat(string user, string chat);
    public delegate void DResponse();

    class ChatClient : TcpSocketClient
    {
        public event DOnConnect eOnConnect;
        public event DOnDisconnect eOnDisconnect;
        public event DOnReceive eOnReceive;
        public event DOnSend eOnSend;
        public event DReceiveChat eReceiveChat;

        private PacketBufferManager packetBufferManager = new PacketBufferManager();
        private Thread receiveThread = null;
        private bool runReceiveThread = false;

        Dictionary<PacketDefine.PACKET_ID, Action<PacketData>> packetFuncDic = new Dictionary<PacketDefine.PACKET_ID, Action<PacketData>>();

        public ChatClient()
        {
            InitPacketFuncDic();

            packetBufferManager.Init(1024,500,13);
            runReceiveThread = true;
            receiveThread = new Thread(ReceiveProcess);
            receiveThread.Name = "receiveThread";
            receiveThread.Start();
        }
        ~ChatClient()
        {
            
        }

        public void Dispose()
        {
            runReceiveThread = false;
            if (receiveThread != null)
            {
                receiveThread.Join();
            }
            base.Dispose();
        }

        public override void OnConnect(string ip, int port) 
        {
            if(eOnConnect != null)
            {
                eOnConnect(ip,port);
            }
        }
        public override void OnDisconnect() 
        {
            if (eOnDisconnect != null)
            {
                eOnDisconnect();
            }
        }
        public override void OnReceive(byte[] bytes, int size) 
        {
            if (eOnReceive != null)
            {
                eOnReceive(bytes, size);
            }
            packetBufferManager.Write(bytes, 0, size);
        }
        public override void OnSend(byte[] bytes, int size) 
        {
            if (eOnSend != null)
            {
                eOnSend(bytes, size);
            }
        }
        private void ReceiveProcess()
        {
            bool wasWorked = false;
            while (runReceiveThread)
            {
                do
                {
                    if (packetBufferManager.Size() > PacketDefine.HEDER_SIZE)
                    {
                        PacketData packetData = packetBufferManager.ReadPacket();
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
            return true;
        }
        public bool SendChat(string chat)
        {
            //RoomChatRequest roomChatRequest = new RoomChatRequest();
            //roomChatRequest.SetValue(chat);
            //
            //List<byte> datas = new List<byte>();
            //datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.ROOM_CHAT_REQUEST)));
            //datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + roomChatRequest.GetSize())));
            //datas.AddRange(new byte[] { (byte)0 });
            //datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
            //datas.AddRange(roomChatRequest.ToBytes());
            //SendData(datas.ToArray());

            bool bResult = false;
            if(IsConnected())
            {
                AllUserChatRequest allUserChatRequest = new AllUserChatRequest();
                allUserChatRequest.SetCodeMsg("", chat);

                List<byte> datas = new List<byte>();
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.ALL_USER_CHAT_REQUEST)));
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + allUserChatRequest.GetSize())));
                datas.AddRange(new byte[] { (byte)0 });
                datas.AddRange(BitConverter.GetBytes((UInt64)(Environment.TickCount)));
                datas.AddRange(allUserChatRequest.ToBytes());
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
                //로그인 성공
            }
            else
            {
                //로그인 실패
            }
        }
        private void PacketFunc_AllUserChatResponse(PacketData packetData) 
        {
            AllUserChatResponse allUserChatResponse = new AllUserChatResponse();
            // 서버가 수신했다~
            if(eReceiveChat != null)
            {
                eReceiveChat("나얏", "송신성공");
            }
        }
        private void PacketFunc_AllUserChatNotify(PacketData packetData) 
        {
            RoomChatNotify roomChatNotify = new RoomChatNotify();
            if(roomChatNotify.FromBytes(packetData.BodyData,0))
            {
                // AllUserChatResponse 수신 성공!~
                if (eReceiveChat != null)
                {
                    eReceiveChat(roomChatNotify.GetId(), roomChatNotify.GetMsg());
                }
            }
            else
            {
                // 실패실패
            }
        }
        private void PacketFunc_RoomEnterResponse(PacketData packetData) 
        {
            RoomEnterResponse roomEnterResponse = new RoomEnterResponse();
            if(roomEnterResponse.FromBytes(packetData.BodyData,0))
            {
                // AllUserChatResponse 수신 성공!~
            }
            else
            {
                // 실패실패
            }
        }
        private void PacketFunc_RoomLeaveResponse(PacketData packetData) 
        {
            RoomLeaveResponse roomLeaveResponse = new RoomLeaveResponse();
            if(roomLeaveResponse.FromBytes(packetData.BodyData,0))
            {

            }
            else
            {

            }
        }
        private void PacketFunc_RoomChatResponse(PacketData packetData) 
        {
            RoomChatResponse roomChatResponse = new RoomChatResponse();
            if(roomChatResponse.FromBytes(packetData.BodyData,0))
            {

            }
            else
            {

            }

        }
        private void PacketFunc_RoomChatNotify(PacketData packetData) 
        {
            RoomChatNotify roomChatNotify =new RoomChatNotify();
            if( roomChatNotify.FromBytes(packetData.BodyData,0))
            {

            }
            else
            {

            }
        }
    }
}
 