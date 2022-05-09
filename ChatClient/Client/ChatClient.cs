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

    internal class ChatClient : Client
    {
        public event DOnConnect eOnConnect;
        public event DOnDisconnect eOnDisconnect;
        public event DOnReceive eOnReceive;
        public event DOnSend eOnSend;
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
        public bool Login(string id, string pw)
        {
            return true;
        }
    }
}
