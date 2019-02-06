#include <iostream>
#include "..\include\TcpSocket.h" 
#include "..\Server\Packet.h"





class SimpleClient : public TcpSocket 
{
public:
	SimpleClient() {}
	virtual ~SimpleClient() {}
};