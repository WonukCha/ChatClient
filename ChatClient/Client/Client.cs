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
  

    internal class ChatClient
    {
        private TcpClient tcpClient = null;
        private NetworkStream stream = null;
        private bool run = false;
        private Thread receiveThread = null;
        private Thread sendThread = null;

        private const int MAX_QUEUE_SIZE = 100;
        private const int MAX_BUFFER_SIZE = 1024;

        private ConcurrentQueue<byte[]> receiveQueue = new ConcurrentQueue<byte[]>();
        private ConcurrentQueue<byte[]> sendQueue = new ConcurrentQueue<byte[]>();

        public ChatClient()
        {

        }
        ~ChatClient()
        {
            StopThread();
        }
        public bool Connect(string ip, int port)
        {
            //`if (IsTcpPortAvailable(port) == false)
            //`    return false;

            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }

            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            StartThread();
            return true;
        }
        public void Disonnect()
        {
            if (tcpClient != null)
            {
                StopThread();
                stream.Close();
                tcpClient.Close();
                tcpClient = null;
            }
        }
        public bool SendData(byte[] data)
        {
            if(sendQueue.Count() > MAX_QUEUE_SIZE)
            {
                return false;
            }
            else
            {
                sendQueue.Enqueue(data);
            }
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
        private void StopThread()
        {
            run = false;
            if (receiveThread != null)
            {
                receiveThread.Join();
            }
            if (sendThread != null)
            {
                sendThread.Join();
            }
        }
        private void StartThread()
        {
            StopThread();
            if(tcpClient != null)
            {
                if(tcpClient.Connected)
                {
                    run = true;
                    sendThread = new Thread(SendThreadProc);
                    sendThread.Name = "SendThread";
                    receiveThread = new Thread(ReceiveThreadProc);
                    receiveThread.Name = "ReceiveThread";

                    sendThread.Start();
                    receiveThread.Start();
                }
            }
        }
        private void ReceiveThreadProc()
        {
            int nbytes = 0;
            byte[] outbuf = new byte[MAX_BUFFER_SIZE];
            while (run)
            {
                nbytes = stream.Read(outbuf, 0, outbuf.Length);
                if (nbytes == 0)
                {
                    //TODO : StopSend 분리
                    run = false;
                    if (sendThread != null)
                    {
                        sendThread.Join();
                    }
                    break;
                }
                else
                {
                    if (receiveQueue.Count() < MAX_QUEUE_SIZE)
                    {
                        receiveQueue.Enqueue(outbuf);
                    }
                }
            }
        }
        private void SendThreadProc()
        {
            while (run)
            {
                if(sendQueue.Count() > 0)
                {
                    byte[] buf = null;
                    sendQueue.TryDequeue(out buf);
                    stream.Write(buf,0, buf.Length);
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
