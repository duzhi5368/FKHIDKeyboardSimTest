#include "stdafx.h"
#include <stdio.h> 

#pragma comment(lib, "hidparse.lib")
#pragma comment(lib, "hidclass.lib")
#pragma comment(lib, "hid.lib")
#pragma comment(lib, "setupapi.lib")

extern "C" 
{
	#include "vmulticlient.h"
}

extern "C"
{
	__declspec(dllexport) void __stdcall MyHelloWorld()
	{
		printf ("Hello World !\n");
	}
}

extern "C" _declspec(dllexport) int __stdcall MySum(int a, int b)
{
	return a + b;
}