using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public delegate void DOnConnect(string ip, int port);
    public delegate void DOnDisconnect();
    public delegate void DOnReceive(byte[] bytes, int size);
    public delegate void DOnSend(byte[] bytes, int size);
    public delegate void DReceiveChat(string chat);
    public delegate void DResponse();


    internal class ChatClient : Client
    {
        public event DOnConnect eOnConnect;
        public event DOnDisconnect eOnDisconnect;
        public event DOnReceive eOnReceive;
        public event DOnSend eOnSend;
        public event DReceiveChat eReceiveCommand;


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

        }
        public override void OnSend(byte[] bytes, int size) 
        {
            if (eOnSend != null)
            {
                eOnSend(bytes, size);
            }
        }
        private void MainProcess()
        {

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
