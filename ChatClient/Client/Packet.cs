using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Client;

namespace ChatClient.Client.Packet
{
	struct PacketData
	{

		public Int16 DataSize;
		public Int16 PacketID;
		public SByte Type;
		public UInt64 tickCount;
		public byte[] BodyData;
	}

	//Todo : Set,Get, FromByte, ToByte
	class LoginRequest
	{
		byte[] id = new byte[PacketDefine.ID_SIZE];
		byte[] pw = new byte[PacketDefine.PW_SIZE];

		public bool SetIdPw(string id, string pw)
        {
			bool bResult = false;
			if(id.Length <= PacketDefine.ID_SIZE && pw.Length <= PacketDefine.PW_SIZE)
            {
				Encoding.UTF8.GetBytes(id).CopyTo(this.id, 0);
				Encoding.UTF8.GetBytes(id).CopyTo(this.pw, 0);
				bResult = true;
			}
			return bResult;
		}

		public byte[] ToBytes()
        {
			List<byte> data = new List<byte>();
			data.AddRange(this.id);
			data.AddRange(this.pw);
			return data.ToArray();
		}


	};
	class LoginResponse
	{
		Int16 result;

		public void FromBytes(byte[] bodyData,int offset)
        {
			result = BitConverter.ToInt16(bodyData,	offset);
        }
	};

	class AllUserChatRequest
	{
		byte[] checkCode = new byte[PacketDefine.NAME_SIZE];
		byte[] msg = new byte[PacketDefine.CHAT_SIZE];

		public bool SetIdPw(string checkCode, string msg)
		{
			bool bResult = false;
			if (checkCode.Length <= PacketDefine.NAME_SIZE && msg.Length <= PacketDefine.CHAT_SIZE)
			{
				Encoding.UTF8.GetBytes(checkCode).CopyTo(this.checkCode, 0);
				Encoding.UTF8.GetBytes(msg).CopyTo(this.msg, 0);
				bResult = true;
			}
			return bResult;
		}
		public byte[] ToBytes()
		{
			List<byte> data = new List<byte>();
			data.AddRange(this.checkCode);
			data.AddRange(this.msg);
			return data.ToArray();
		}
	}
	class AllUserChatResponse
	{

	};

	class RoomEnterRequest
	{
		UInt16 roomNumber;

		public bool SetRoomNumber(UInt16 roomNumber)
        {
			this.roomNumber = roomNumber;
			return true;
		}
		public byte[] ToBytes()
		{
			List<byte> data = new List<byte>();
			data.AddRange(BitConverter.GetBytes(roomNumber));
			return data.ToArray();
		}

	};

	class RoomEnterResponse
	{
		Int16 result = 0;
		public void FromBytes(byte[] bodyData, int offset)
		{
			result = BitConverter.ToInt16(bodyData, offset);
		}
	};

	class RoomLeaveRequest
	{
		UInt16 roomNumber;
		public bool SetRoomNumber(UInt16 roomNumber)
		{
			this.roomNumber = roomNumber;
			return true;
		}
		public byte[] ToBytes()
		{
			List<byte> data = new List<byte>();
			data.AddRange(BitConverter.GetBytes(roomNumber));
			return data.ToArray();
		}
	};

	class RoomLeaveResponse
	{
		Int16 result = 0;
		public void FromBytes(byte[] bodyData, int offset)
		{
			result = BitConverter.ToInt16(bodyData, offset);
		}
	};

	class RoomChatRequest
	{
		byte[] msg = new byte[PacketDefine.CHAT_SIZE];
		public bool SetValue(string msg)
		{
			bool bResult = false;
			if (msg.Length <= PacketDefine.CHAT_SIZE)
			{
				Encoding.UTF8.GetBytes(msg).CopyTo(this.msg, 0);
				bResult = true;
			}
			return bResult;
		}
		public byte[] ToBytes()
		{
			List<byte> data = new List<byte>();
			data.AddRange(msg);
			return data.ToArray();
		}
	};

	class RoomChatResponse
	{
		Int16 result = 0;
		public void FromBytes(byte[] bodyData, int offset)
		{
			result = BitConverter.ToInt16(bodyData, offset);
		}
	};

	class RoomChatNotify
	{
		byte[] id = new byte[PacketDefine.ID_SIZE];
		byte[] msg = new byte[PacketDefine.CHAT_SIZE];
		public void SetValue(string id, string msg)
		{
			Encoding.UTF8.GetBytes(id).CopyTo(this.id, 0);
			Encoding.UTF8.GetBytes(msg).CopyTo(this.msg, 0);
		}
		public byte[] ToBytes()
		{
			List<byte> data = new List<byte>();
			data.AddRange(id);
			data.AddRange(msg);
			return data.ToArray();
		}
	};
}
