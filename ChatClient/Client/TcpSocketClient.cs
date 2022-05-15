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
    class TcpSocketClient : IDisposable
    {
        private string lastErrorMsg;

        private const int READ_TIMEOUT_SEC = 3;
        private const int MAX_QUEUE_SIZE = 100;
        private const int MAX_BUFFER_SIZE = 1024;

        private TcpClient tcpClient = new TcpClient();
        private NetworkStream stream = null;
        private bool runSendThread = false;
        private bool runReceiveThread = false;
        private Thread receiveThread = null;
        private Thread sendThread = null;

        private ConcurrentQueue<byte[]> receiveQueue = new ConcurrentQueue<byte[]>();
        private ConcurrentQueue<byte[]> sendQueue = new ConcurrentQueue<byte[]>();

        public virtual void OnConnect(string ip, int port) {}
        public virtual void OnDisconnect() { }
        public virtual void OnReceive(byte[] bytes,int size) { }
        public virtual void OnSend(byte[] bytes, int size) { }

        public TcpSocketClient()
        {
            StartThread();
        }
        ~TcpSocketClient()
        {
        }
        public void Dispose()
        {
            StopThread();
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                if (tcpClient.Connected == false)
                {
                    IPAddress serverIP = IPAddress.Parse(ip);
                    int serverPort = port;
                    tcpClient = new TcpClient();
                    tcpClient.Connect(new IPEndPoint(serverIP, serverPort));
                    stream = tcpClient.GetStream();
                    stream.ReadTimeout = 1000 * READ_TIMEOUT_SEC; // 10초
                    OnConnect(ip, port);
                    return true;
                }
                else
                {
                    return false;
                }
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
            stream.Close();
            tcpClient.Close();

            receiveQueue = new ConcurrentQueue<byte[]>();
            sendQueue = new ConcurrentQueue<byte[]>();
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

            runSendThread = true;
            sendThread = new Thread(SendThreadProc);
            sendThread.Name = "SendThread";
            sendThread.Start();
        }
        private void StartReceiveThread()
        {
            if (tcpClient == null)
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
                    if(stream != null && stream.CanRead && tcpClient.Connected)
                    {
                        nbytes = stream.Read(outbuf, 0, outbuf.Length);
                        if (nbytes == 0)
                        {
                            Disonnect();
                            OnDisconnect();
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
                            OnReceive(outbuf, nbytes);
                        }
                    }
                    else
                    {
                        Thread.Sleep(16);
                    }

                }
                catch (Exception ex)
                {
                    lastErrorMsg = ex.Message;
                    Disonnect();
                    OnDisconnect();
                }
            }
        }
        private void SendThreadProc()
        {
            while (runSendThread)
            {
                if(sendQueue.Count() > 0 && stream !=null && stream.CanWrite)
                {
                    byte[] buf = null;
                    sendQueue.TryDequeue(out buf);
                    stream.Write(buf,0, buf.Length);
                    OnSend(buf, buf.Length);
                }
                else
                {
                    Thread.Sleep(16);
                }
            }
        }
    }
}
