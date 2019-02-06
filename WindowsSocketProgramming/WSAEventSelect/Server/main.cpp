// SimpleFastSocketServer.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"
#include <iostream>
#include "SimpleServer.h"


int _tmain(int argc, _TCHAR* argv[])
{
	SimpleServer Server;
	Server.SocketInit();

	if( NETWORK_ERROR_NONE != Server.Init( true, 2, 16840 ) )
	{
		std::cout << "ERROR - 소켓 초기화 실패!!!" << std::endl;
		return 0;
	}

	Server.StartServer( 10101 );
	std::cout << "서버 시작!!!" << std::endl;
	SimpleServer::NetworkProc( &Server );


	Server.SocketTerminate();
	return 0;
}
