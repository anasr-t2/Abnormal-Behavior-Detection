#pragma once


#using <mscorlib.dll>
#using <System.dll>
#using <system.drawing.dll>


using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Net::Sockets;


using namespace System::IO;
using namespace System::Net;
using namespace System::Text;
using namespace System::Threading;
using namespace System::Globalization;

using namespace System::Runtime::Serialization::Formatters::Binary;
using namespace System::Drawing;


#include <iostream>


namespace Network{

	bool make = false;

	ref class NetworkConnect
	{
	public:
		TcpListener^ listener;
		Socket^ soc;
		Stream^ s;

		void connect(){

			std::cout << "server started" << std::endl;
			listener = gcnew TcpListener(50001);
			listener->Start();
			soc = listener->AcceptSocket();

			s = gcnew NetworkStream(soc);
			Console::WriteLine("connected");
			make = true;



		}


	};



	ref class serialize{
	public:
		BinaryFormatter^ formatter = gcnew BinaryFormatter();

		void ser(Stream^ s, Bitmap^ x){
			
				formatter->Serialize(s, x);
			
			
		}



	};



}