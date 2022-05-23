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
    public delegate void DReceiveChat(string chat);
    public delegate void DResponse();

    class ChatClient : TcpSocketClient
    {
        public event DOnConnect eOnConnect;
        public event DOnDisconnect eOnDisconnect;
        public event DOnReceive eOnReceive;
        public event DOnSend eOnSend;
        public event DReceiveChat eReceiveCommand;

        private PacketBufferManager packetBufferManager = new PacketBufferManager();
        private Thread receiveThread = null;
        private bool runReceiveThread = false;

        Dictionary<PacketDefine.PACKET_ID, Action<PacketData>> packetFuncDic = new Dictionary<PacketDefine.PACKET_ID, Action<PacketData>>();

        public ChatClient()
        {
            packetBufferManager.Init(1024,32,5);
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
            packetBufferManager.Write(bytes, 0, bytes.Length);
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
                //packetData.DataSize = 0;
                //packetData.PacketID = (Int16)PacketDefine.PACKET_ID.LOGIN_REQUEST;
                //packetData.Type = 0;
                //packetData.tickCount = (UInt64)Environment.TickCount;
                //packetData.BodyData = loginRequest.ToBytes();
                List<byte> datas = new List<byte>();
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.HEDER_SIZE + loginRequest.GetSize())));
                datas.AddRange(BitConverter.GetBytes((UInt16)(PacketDefine.PACKET_ID.LOGIN_REQUEST)));
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
            return true;
        }
        public bool SendChat(string chat)
        {
            return true;
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

            }
            else
            {

            }
        }
        private void PacketFunc_AllUserChatResponse(PacketData packetData) 
        {
            
        }
        private void PacketFunc_AllUserChatNotify(PacketData packetData) 
        {

        }
        private void PacketFunc_RoomEnterResponse(PacketData packetData) 
        {

        }
        private void PacketFunc_RoomLeaveResponse(PacketData packetData) 
        {

        }
        private void PacketFunc_RoomChatResponse(PacketData packetData) 
        {

        }
        private void PacketFunc_RoomChatNotify(PacketData packetData) 
        {

        }
    }
}
 