
#include <opencv/cv.h>
#include <opencv/highgui.h>

#include "package_bgs/pt/PixelBasedAdaptiveSegmenter.h"

#include "package_tracking/BlobTracking.h"

#include "package_analysis/VehicleCouting.h"

#include "NetworkConnect.h"

using namespace std;

string picName;




ref class network{
public:
	static TcpListener^ tcpListener = gcnew TcpListener(10);

	static void Listeners()
	{
		System::String^ picname;
		int i = 1;
		Bitmap^ x;

		Socket^ socketForClient = tcpListener->AcceptSocket();
		if (socketForClient->Connected)
		{
			Console::WriteLine("Client:" + socketForClient->RemoteEndPoint + " now connected to server.");
			NetworkStream^ networkStream = gcnew NetworkStream(socketForClient);
			StreamWriter^ streamWriter = gcnew StreamWriter(networkStream);
			StreamReader^ streamReader = gcnew StreamReader(networkStream);



			System::String^ theString = streamReader->ReadLine();
			Console::WriteLine("Message recieved by client:" + socketForClient->RemoteEndPoint +" " +theString);
			BinaryFormatter^ fo = gcnew BinaryFormatter();
			while (true){
				try
				{
					
					Thread::Sleep(100);
					//streamWriter.WriteLine(theString);
					//streamWriter.Flush();
					picname = gcnew System::String(picName.c_str());;
					
					if (File::Exists(picname)){
						x = gcnew Bitmap(picname);

						fo->Serialize(networkStream, x);
						//i++;
						
					}
					theString = streamReader->ReadLine();
					
				}
				catch (Exception^ e) { /*break;*//*theString = streamReader->ReadLine();*/ }

				if (theString == "exit")
					break;
			}
			streamReader->Close();
			networkStream->Close();
			streamWriter->Close();
			//}

		}

		Console::WriteLine("Client:" + socketForClient->RemoteEndPoint + " closed");
		socketForClient->Close();
		//Console.WriteLine("Press any key to exit from server program");

		Console::ReadKey();
	}



};





int main(int argc, char **argv)
{
	int counter = 0;


	Thread^ r, ^ m;
	Bitmap^ x;

	//int i = 1;
	//bool connected = false;

	//Network::NetworkConnect^ net = gcnew Network::NetworkConnect();
	////net->connect();
	//Network::serialize^ sero = gcnew Network::serialize();


	/*r = gcnew Thread(gcnew ThreadStart(net, &Network::NetworkConnect::connect));
	r->Start();*/




	network^ net = gcnew network();
	net->tcpListener->Start();
	int numberOfClientsYouNeedToConnect = 5;


	for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
	{
		Thread^ newThread = gcnew Thread(gcnew ThreadStart(net->Listeners));
		newThread->Start();
	}

	CvCapture *capture = 0;
	//capture = cvCaptureFromAVI("./dataset/video.avi");
	//capture = cvCaptureFromFile("E:/highway2.mp4");
	//capture=cvCaptureFromFile("E:/Project/dataset/test44.mp4");
	capture = cvCreateCameraCapture(-1);
	if (!capture){
		std::cerr << "Cannot open video!" << std::endl;
		return 1;
	}

	char link[512];

	int i = 1;
	//string picName;
	System::String^ picName2;


	int resize_factor = 100; // 50% of original image

	IplImage *frame_aux = cvQueryFrame(capture);
	IplImage *frame = cvCreateImage(cvSize((int)((frame_aux->width*resize_factor) / 100), (int)((frame_aux->height*resize_factor) / 100)), frame_aux->depth, frame_aux->nChannels);

	/* Background Subtraction Methods */
	IBGS *bgs;



	/*** PT Package (adapted from Hofmann) ***/
	bgs = new PixelBasedAdaptiveSegmenter;


	/* Blob Tracking */
	cv::Mat img_blob;
	BlobTracking* blobTracking;
	blobTracking = new BlobTracking;

	/* Vehicle Counting Algorithm */
	VehicleCouting* vehicleCouting;
	vehicleCouting = new VehicleCouting;

	int key = 0;
	while (key != 'q')
	{
		frame_aux = cvQueryFrame(capture);
		if (!frame_aux) break;

		cvResize(frame_aux, frame);

		cv::Mat img_input(frame);
		cvNamedWindow("input", CV_WINDOW_NORMAL);
		//cvNamedWindow("PBAS", CV_WINDOW_NORMAL);
		//cvNamedWindow("Blob Tracking", CV_WINDOW_NORMAL);
		//cvNamedWindow("bla", CV_WINDOW_NORMAL);
		cv::imshow("input", img_input);


		// bgs->process(...) method internally shows the foreground mask image
		cv::Mat img_mask;
		bgs->process(img_input, img_mask);

		if (!img_mask.empty())
		{
			// Perform blob tracking
			blobTracking->process(img_input, img_mask, img_blob);
			cv::imshow("bla", img_blob);

			//i = 0;
			//if (i == 0)
			//	i++;
			//else if (i == 1)
			//	i = 2;
			//else
			//	i = 1;


			picName = "E:/server/" + to_string(i);
			picName += ".jpg";
			/*if (counter > 0)
				File::Delete("E:/server/pic.jpg");*/
			//picName = "E:/server/pic.jpg";

			imwrite(picName, img_blob);

			/*if (counter >1){
				string neg = "E:/server/" + to_string(i - 1);
				neg += ".jpg";
				System::String^ neg2 = gcnew System::String(neg.c_str());
				File::Delete(neg2);

				}*/

			/*picName2 = gcnew System::String(picName.c_str());
			x = gcnew Bitmap(picName2);*/


			/*if (Network::make){

				sero->ser(net->s, x);
			}*/

				i++; counter++;

				//killProcessByName("E:/server/pic.jpg");


				// Perform vehicle counting
				vehicleCouting->setInput(img_blob);
				vehicleCouting->setTracks(blobTracking->getTracks());
				vehicleCouting->process();
			}

			key = cvWaitKey(1);
		}

		delete vehicleCouting;
		delete blobTracking;
		delete bgs;

		cvDestroyAllWindows();
		cvReleaseCapture(&capture);
		//r->Abort();
		return 0;
	}



