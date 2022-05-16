using System;
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
		char[] id;
		char[] pw;
	};
	class LoginResponse
	{
		Int16 result;
	};

	class AllUserChatRequest
	{
		char[] checkCode;
		char[] msg;
	}
	class AllUserChatResponse
	{

	};

	class RoomEnterRequest
	{
		UInt16 roomNumber;
	};

	class RoomEnterResponse
	{
		Int16 result = 0;
	};

	class RoomLeaveRequest
	{
		UInt16 roomNumber;
	};

	class RoomLeaveResponse
	{
		Int16 result = 0;
	};

	class RoomChatRequest
	{
		char[] msg;
	};

	class RoomChatResponse
	{
		Int16 result = 0;
	};

	class RoomChatNotify
	{
		char[] id;
		char[] msg;
	};
}
