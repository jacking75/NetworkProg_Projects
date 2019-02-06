#include <iostream>
#include "..\include\TcpSocket.h" 
#include "Packet.h"


class SimpleServer : public TcpSocket 
{
public:
	SimpleServer() {}
	virtual ~SimpleServer() {}

	virtual int OnAccept( const int nIndex )
	{
		int nSockObjIndex = TcpSocket::OnAccept( nIndex );

		std::cout << "OnAccept : SockObjIndex - " << nSockObjIndex << std::endl;
		return nSockObjIndex;
	}

	virtual int OnRead( const int nIndex )
	{
		int nSockObjIndex = TcpSocket::OnRead( nIndex );
		std::cout << "OnRead : SockObjIndex - " << nSockObjIndex << std::endl;

		PacketCMD* pCmd = PopCommand();
		if( NULL != pCmd )
		{
			if( pCmd->nCmdType == PACKET_COMMAND::CMD_REQ_CHAT )
			{
				CMD_REQ_CHAT* pChat = (CMD_REQ_CHAT*)pCmd->pData;
				std::cout << "From : " << pChat->szName << ",  Msg : " << pChat->szChatData << std::endl;

				CMD_RESP_CHAT SendChat;
				SendChat.usType = PACKET_COMMAND::CMD_RESP_CHAT;
				SendChat.usBodySize = sizeof(CMD_RESP_CHAT) - PACKET_HEADER_SIZE;
				sprintf( SendChat.szName, "[re]%s" , pChat->szName );
				sprintf( SendChat.szChatData, "%s" , pChat->szChatData );
				Send( nSockObjIndex, sizeof(CMD_RESP_CHAT), (char*)&SendChat );
			}

			delete pCmd;
		}

		return nSockObjIndex;
	}

	virtual int OnClose( const int nIndex )
	{
		int nSockObjIndex = TcpSocket::OnClose( nIndex );

		std::cout << "OnClose : SockObjIndex - " << nSockObjIndex << std::endl;
		return nSockObjIndex;
	}

};
