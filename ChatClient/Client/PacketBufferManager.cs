using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Client.Packet;

namespace ChatClient.Client
{
    class PacketBufferManager : BufferManager
    {
        Int16 mHeaderSize = 0;
        byte[] heder = null;
        public bool Init(int bufferSize, int maxPacketSize, Int16 headerSize)
        {
            mHeaderSize = headerSize;
            heder = new byte[bufferSize];
            return base.Init(bufferSize, maxPacketSize);
        }
        public PacketData ReadPacket()
        {
            PacketData packetData = new PacketData();

            if (Size() > mHeaderSize)
            {
                Read(heder, 0, mHeaderSize, false);
                packetData.PacketID = BitConverter.ToInt16(heder, 0);
                packetData.DataSize = BitConverter.ToInt16(heder, 2);
                packetData.DataSize -= mHeaderSize;
                packetData.Type = (SByte)heder[4];
                if(packetData.DataSize <= Size())
                {
                    Read(heder, 0, mHeaderSize);
                    byte[] packet = new byte[packetData.DataSize];
                    Read(packet,0, packetData.DataSize);
                    packetData.BodyData = packet;
                }
            }
            return packetData;
        }
    }
}
