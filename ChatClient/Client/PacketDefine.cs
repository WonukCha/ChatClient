using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Client
{
	public class PacketDefine
    {
		public const UInt16 HEDER_SIZE = 13;
		public const UInt16 CHAT_SIZE = 255 + 1;
		public const UInt16 NAME_SIZE = 255 + 1;
		public const UInt16 ID_SIZE = 255 + 1;
		public const UInt16 PW_SIZE = 255 + 1;


		public enum COMPRESS_TYPE : byte
		{
			NONE = 0,
			ZLIB,
			END
		};

		public enum USER_STATUS_INFO : byte
		{
			NONE = 0,
			DISCONECT,
			CONNECT,
			LOBBY,
			ROOM,
		};

		public enum PACKET_ID : UInt16
		{
			NONE,
			SYSYEM_DISCONNECT = 100,
			SYSYEM_CONNECT,

			LOGIN_REQUEST = 200,
			LOGIN_RESPONSE,

			ALL_USER_CHAT_REQUEST = 300,
			ALL_USER_CHAT_RESPONSE,
			ALL_USER_CHAT_NOTIFY,

			ROOM_ENTER_REQUEST,
			ROOM_ENTER_RESPONSE,

			ROOM_LEAVE_REQUEST,
			ROOM_LEAVE_RESPONSE,

			ROOM_CHAT_REQUEST,
			ROOM_CHAT_RESPONSE,
			ROOM_CHAT_NOTIFY,
			END
		};
	}
	
}
