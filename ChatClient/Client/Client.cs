using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Client
    {
        private string lastErrorMsg;

        private const int READ_TIMEOUT_SEC = 10;
        private const int MAX_QUEUE_SIZE = 100;
        private const int MAX_BUFFER_SIZE = 1024;

        private TcpClient tcpClient = null;
        private NetworkStream stream = null;
        private bool runSendThread = false;
        private bool runReceiveThread = false;
        private Thread receiveThread = null;
        private Thread sendThread = null;

        private ConcurrentQueue<byte[]> receiveQueue = new ConcurrentQueue<byte[]>();
        private ConcurrentQueue<byte[]> sendQueue = new ConcurrentQueue<byte[]>();

        public virtual void OnConnect(string ip, int port) {}
        public virtual void OnDisconnect() { }
        public virtual void OnReceive(int nbytes) { }
        public virtual void OnSend(int nbytes) { }

        public Client()
        {

        }
        ~Client()
        {
            StopThread();
        }
        public bool Connect(string ip, int port)
        {
            try
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }
                IPAddress serverIP = IPAddress.Parse(ip);
                int serverPort = port;
                tcpClient = new TcpClient();
                tcpClient.Connect(new IPEndPoint(serverIP, serverPort));
                if (tcpClient == null || tcpClient.Connected == false)
                    return false;
                
                stream = tcpClient.GetStream();
                stream.ReadTimeout = 1000 * READ_TIMEOUT_SEC; // 10초
                StartThread();
                OnConnect(ip, port);
                return true;
            }
            catch(Exception ex)
            {
                lastErrorMsg = ex.Message;
                return false;
            }
        }
        public void Disonnect()
        {
            if (tcpClient == null)
                return;
            if (tcpClient.Connected == false)
                return;
            StopThread();
            stream.Close();
            tcpClient.Close();
            //C# !?!? um...
            receiveQueue = new ConcurrentQueue<byte[]>();
            sendQueue = new ConcurrentQueue<byte[]>();

            OnDisconnect();
        }
        public bool SendData(byte[] data)
        {
            if (tcpClient == null)
                return false;
            if (tcpClient.Connected == false)
                return false;
            if (sendQueue.Count() > MAX_QUEUE_SIZE)
                return false;

            sendQueue.Enqueue(data);
            return true;
        }
        public bool SendData(string str)
        {
            byte[] strByte = Encoding.UTF8.GetBytes(str);
            return SendData(strByte);
        }
        public byte[] ReadData()
        {
            byte[] data = null;
            while (receiveQueue.Count() > 0 && data == null)
            {
                receiveQueue.TryDequeue(out data);
            }
            return data;
        }
        public string GetLastError()
        {
            return lastErrorMsg;
        }
        private void StopThread()
        {
            StopReceiveThread();
            StopSendThread();
        }
        private void StopSendThread()
        {
            runSendThread = false;

            if (sendThread != null)
            {
                sendThread.Join();
            }
        }
        private void StopReceiveThread()
        {
            runReceiveThread = false;

            if (receiveThread != null)
            {
                receiveThread.Join();
            }
        }
        private void StartThread()
        {
            StartSendThread();
            StartReceiveThread();
        }
        private void StartSendThread()
        {
            if (tcpClient == null)
                return;
            if (tcpClient.Connected == false)
                return;

            runSendThread = true;
            sendThread = new Thread(SendThreadProc);
            sendThread.Name = "SendThread";

            sendThread.Start();
        }
        private void StartReceiveThread()
        {
            if (tcpClient == null)
                return;
            if (tcpClient.Connected == false)
                return;

            runReceiveThread = true;
            receiveThread = new Thread(ReceiveThreadProc);
            receiveThread.Name = "ReceiveThread";
            receiveThread.Start();
        }
        private void ReceiveThreadProc()
        {
            int nbytes = 0;
            byte[] outbuf = new byte[MAX_BUFFER_SIZE];
            while (runReceiveThread)
            {
                try
                {
                    nbytes = stream.Read(outbuf, 0, outbuf.Length);
                    if (nbytes == 0)
                    {
                        StopSendThread();
                        break;
                    }
                    else
                    {
                        if (receiveQueue.Count() < MAX_QUEUE_SIZE)
                        {
                            receiveQueue.Enqueue(outbuf);
                        }
                        else
                        {
                            lastErrorMsg = "MAX_QUEUE_SIZE_ERROR";
                        }
                        OnReceive(nbytes);
                    }
                }
                catch (Exception ex)
                {
                    lastErrorMsg = ex.Message;
                }

            }
        }
        private void SendThreadProc()
        {
            while (runSendThread)
            {
                if(sendQueue.Count() > 0)
                {
                    byte[] buf = null;
                    sendQueue.TryDequeue(out buf);
                    stream.Write(buf,0, buf.Length);
                    OnSend(buf.Length);
                }
                else
                {
                    Thread.Sleep(16);
                }
            }
        }
        private bool IsTcpPortAvailable(int tcpPort)
        {
            var ipgp = IPGlobalProperties.GetIPGlobalProperties();

            // Check ActiveConnection ports
            TcpConnectionInformation[] conns = ipgp.GetActiveTcpConnections();
            foreach (var cn in conns)
            {
                if (cn.LocalEndPoint.Port == tcpPort)
                {
                    return false;
                }
            }

            // Check LISTENING ports
            IPEndPoint[] endpoints = ipgp.GetActiveTcpListeners();
            foreach (var ep in endpoints)
            {
                if (ep.Port == tcpPort)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
