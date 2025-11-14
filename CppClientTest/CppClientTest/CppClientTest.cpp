// CppClientTest.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <WinSock2.h>
//#include <ws2def.h>
#include <WS2tcpip.h>
#include <iostream>
#include <conio.h>

int main()
{
	DWORD dw = 5;
	bool sss = dw & 4;
	
	WSADATA wsaData;
	//WORD wVersionRequeted = Ma
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
	{
		return 0;
	}

	auto hSocket = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	SOCKADDR_IN addr;
	memset(&addr, 0, sizeof(addr));
	addr.sin_family = AF_INET;
	//addr.sin_addr.S_un.S_addr = inet_addr("imwasmdp.sk.com");
	//addr.sin_addr.S_un.S_addr = inet_addr("210.211.68.236");
	//inet_pton()
	//auto ret = InetPtonA(AF_INET, "imwasmdp.sk.com", &addr.sin_addr);
	char ip[16] = { 0, };
	int port = 0;
	auto ret = InetPtonA(AF_INET, ip, &addr.sin_addr);

	addr.sin_port = htons(port);
	//std::cout << "Connect Try" << ip << ":" << port << std::endl;
	auto sock = connect(hSocket, (sockaddr*)&addr, sizeof(sockaddr));

	char result[32] = { 0, };
	strcpy_s(result, 32, (sock < 0) ? "Failed" : "Success");

	char buff[128] = { 0, };
	sprintf_s(buff, 128, "Connect try (%s:%d) => %s", ip, port, result);
	std::cout << buff << std::endl;

    std::cout << "Hello World!\n";
}

// 프로그램 실행: <Ctrl+F5> 또는 [디버그] > [디버깅하지 않고 시작] 메뉴
// 프로그램 디버그: <F5> 키 또는 [디버그] > [디버깅 시작] 메뉴

// 시작을 위한 팁: 
//   1. [솔루션 탐색기] 창을 사용하여 파일을 추가/관리합니다.
//   2. [팀 탐색기] 창을 사용하여 소스 제어에 연결합니다.
//   3. [출력] 창을 사용하여 빌드 출력 및 기타 메시지를 확인합니다.
//   4. [오류 목록] 창을 사용하여 오류를 봅니다.
//   5. [프로젝트] > [새 항목 추가]로 이동하여 새 코드 파일을 만들거나, [프로젝트] > [기존 항목 추가]로 이동하여 기존 코드 파일을 프로젝트에 추가합니다.
//   6. 나중에 이 프로젝트를 다시 열려면 [파일] > [열기] > [프로젝트]로 이동하고 .sln 파일을 선택합니다.
