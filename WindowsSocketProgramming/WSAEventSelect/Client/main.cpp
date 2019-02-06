// SimpleFastSocketClient.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"
#include <iostream>
#include "SimpleClient.h"


int _tmain(int argc, _TCHAR* argv[])
{
	SimpleClient Client;
	Client.SocketInit();

	if( NETWORK_ERROR_NONE != Client.Init( false, 1, 16840 ) )
	{
		std::cout << "ERROR - 소켓 초기화 실패!!!" << std::endl;
		return 0;
	}

	char szIP[64] = "127.0.0.1";
	unsigned short nPort = 10101;
	std::cout << "Server IP : " << szIP << std::endl;
	std::cout << "Server Port : " << nPort << std::endl;

	unsigned short nRet = Client.Connect( szIP, nPort );
	if( NETWORK_ERROR_NONE == nRet )
	{
		std::cout << "클라이언트 접속 성공" << std::endl;
		SimpleClient::NetworkProc( &Client );
	}
	else
	{
		std::cout << "클라이언트 접속 실패 : " << nRet << std::endl;
	}

	Client.SocketTerminate();
	return 0;
}