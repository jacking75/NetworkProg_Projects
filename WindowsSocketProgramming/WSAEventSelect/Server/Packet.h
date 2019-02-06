
const int MAX_CHAT_DATA = 1024;

struct PACKET_COMMAND 
{
	enum TYPE
	{
		CMD_REQ_LOGIN = 0
		, CMD_RESP_LOGIN = 1
		, CMD_REQ_CHAT = 2 
		, CMD_RESP_CHAT = 3
	};
};

#pragma pack(1)

struct CMD_REQ_CHAT : public PACKET_HEADER
{
	char szName[32];
	char szChatData[MAX_CHAT_DATA];
};

struct CMD_RESP_CHAT : public PACKET_HEADER
{
	char szName[32];
	char szChatData[MAX_CHAT_DATA];
};

#pragma pack()