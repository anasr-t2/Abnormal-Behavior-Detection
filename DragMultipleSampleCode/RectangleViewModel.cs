using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace SampleCode
{
    /// <summary>
    /// Defines the view-model for a simple displayable rectangle.
    /// </summary>
    public class RectangleViewModel : INotifyPropertyChanged
    {
        #region Data Members

        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double x = 0;

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double y = 0;

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        private double width = 0;

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        private double height = 0;

        /// <summary>
        /// The color of the rectangle.
        /// </summary>
        //private string image = "";
        private BitmapImage image = new BitmapImage();
        
        private double angel = 0;
        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        private System.Windows.Point connectorHotspot;

        

        #endregion Data Members

        #region network



        TcpClient socketForServer;
        public Bitmap processedimage;
        BinaryFormatter fo = new BinaryFormatter();

        public NetworkStream networkStream;
        StreamReader streamReader;
        public StreamWriter streamWriter;

       public Thread t;
       string picName;
       int i = 1;
        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public static void SaveClipboardImageToFile(string filePath, BitmapImage imagex)
        {
            var image = imagex;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        public void connect()
        {
            socketForServer = new TcpClient("localHost", 10);

            networkStream = socketForServer.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);


            

            streamWriter.WriteLine("  ");//bngrb nbaat bas
            streamWriter.Flush();


            t = new Thread(rec);
            t.Start();
            //rec();
            //BitmapImage y = new BitmapImage(new Uri("E:/aaa/3.jpg")); 
            //ImageSource x = y;
        }


        public void rec()
        {
            //NetworkStream networkStream = socketForServer.GetStream();
            //StreamReader streamReader = new StreamReader(networkStream);
            //StreamWriter streamWriter = new StreamWriter(networkStream);

            if (socketForServer.Connected)
            {

                while (true)
                {



                    processedimage = (Bitmap)fo.Deserialize(networkStream);
                    picName = "E:/client/" + i + ".jpg";
                    //processedimage.Save(picName);
                    //i++;
                     //cont();
                    //image = processedimage;
                    streamWriter.WriteLine("");//ghalat lazem nshofha baad keda
                    streamWriter.Flush();
                   
                   // ImageSourceConverter c = new ImageSourceConverter();
                    image = BitmapToImageSource(processedimage);

                   // SaveClipboardImageToFile(picName,image);
                    i++;

                }
            }

           
        }



        public ImageSource cont()
        {

            //  image = processedimage;

            return image;


        }


        #endregion network



        public RectangleViewModel()
        {

        }

        public RectangleViewModel(double x, double y, double width, double height, BitmapImage image, double angel)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.image = image;
            this.angel = angel;
        }
        
        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width == value)
                {
                    return;
                }

                width = value;

                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == value)
                {
                    return;
                }

                height = value;

                OnPropertyChanged("Height");
            }
        }

        /// <summary>
        /// The color of the item.
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                if (image == value)
                {
                    return;
                }

                image = value;

                OnPropertyChanged("image");
            }
        }

        public double Angel
        {
            get
            {
                return angel;
            }
            set
            {
                if (angel == value)
                {
                    return;
                }

                angel = value;

                OnPropertyChanged("angel");
            }
        }

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        public System.Windows.Point ConnectorHotspot
        {
            get
            {
                return connectorHotspot;
            }
            set
            {
                if (connectorHotspot == value)
                {
                    return;
                }

                connectorHotspot = value;

                OnPropertyChanged("ConnectorHotspot");
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 'PropertyChanged' event that is raised when the value of a property of the view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
