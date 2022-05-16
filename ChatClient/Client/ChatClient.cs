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
                    if (packetBufferManager.Size() > 0)
                    {
                        PacketData packetData = packetBufferManager.ReadPacket();
                        wasWorked = true;
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
    }
}
