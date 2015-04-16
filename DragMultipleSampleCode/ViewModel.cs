using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Threading;
using System.IO;
using System.Drawing;

namespace SampleCode
{
    /// <summary>
    /// A simple example of a view-model.  
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        #region Data Members

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        private  ObservableCollection<RectangleViewModel> rectangles = new ObservableCollection<RectangleViewModel>();
      //  private ObservableCollection<RectangleViewModel> temporary = new ObservableCollection<RectangleViewModel>();
        #endregion Data Members

        BitmapImage y = new BitmapImage(new Uri("E:/aaa/1.jpg"));
        ImageSource x = new BitmapImage(new Uri("E:/aaa/3.jpg"));
        //void lf(RectangleViewModel r1)
        //{
        //    while (true)
        //    {

        //        r1.Image = r1.cont();

        //    }
        //}
        Thread tt;




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





        public ViewModel()
        {
            //
            // Populate the view model with some example data.
            //
            string path = "E:/client/xx.jpg";
            string path2 = "E:/client/xx2.jpg";
            string path3 = "E:/client/xx3.jpg";
            int i = 1;
            //var r1 = new RectangleViewModel() ;
           
             var r1 = new RectangleViewModel(310, 110, 70, 70,y, 0);

             

            rectangles.Add(r1);
            r1.connect();

            

            r1 = new RectangleViewModel(310, 110, 70, 70, r1.Image, 0);
            
            
            //r1.send();
            //var r2 = new RectangleViewModel(365, 110, 70, 70,r1.Image, 0);
           // rectangles.Add(r2);
            //r2.connect();
            //r2.send();
            //var r3 = new RectangleViewModel(420, 110, 70, 70, "C:\\Users\\OAM\\Desktop\\Train004\\002.tif", 0);
            //rectangles.Add(r3);
            //var r4 = new RectangleViewModel(475, 110, 70, 70, "C:\\Users\\OAM\\Desktop\\Train004\\002.tif", 0);
            //rectangles.Add(r4);
            //var r5 = new RectangleViewModel(230, 10, 50, 50);
            //rectangles.Add(r5);
            //var r6 = new RectangleViewModel(285, 10, 50, 50);
            //rectangles.Add(r6);
            //var r7 = new RectangleViewModel(340, 10, 50, 50);
            //rectangles.Add(r7);
            //var r8 = new RectangleViewModel(395, 10, 50, 50);
            //rectangles.Add(r8);
            //var r9 = new RectangleViewModel(450, 10, 50, 50);
            //rectangles.Add(r9);
            //var r10 = new RectangleViewModel(505, 10, 50, 50);
            ////rectangles.Add(r10);
            //var r11 = new RectangleViewModel(10, 65, 50, 50);
            //rectangles.Add(r11);
            //var r12 = new RectangleViewModel(65, 65, 50, 50);
            //rectangles.Add(r12);
            //var r13 = new RectangleViewModel(120, 65, 50, 50);
            //rectangles.Add(r13);
            //var r14 = new RectangleViewModel(175, 65, 50, 50);
            //rectangles.Add(r14);
            //var r15 = new RectangleViewModel(230, 65, 50, 50);
            //rectangles.Add(r15);
            //var r16 = new RectangleViewModel(285, 65, 50, 50);
            //rectangles.Add(r16);
            //var r17 = new RectangleViewModel(340, 65, 50, 50);
            //rectangles.Add(r17);
            //var r18 = new RectangleViewModel(395, 65, 50, 50);
            //rectangles.Add(r18);
            //var r19 = new RectangleViewModel(450, 65, 50, 50);
            //rectangles.Add(r19);
            //var r20 = new RectangleViewModel(505, 65, 50, 50);
            //rectangles.Add(r20);
            //var r21 = new RectangleViewModel(10, 120, 50, 50);
            //rectangles.Add(r21);
            //var r22 = new RectangleViewModel(65, 120, 50, 50);
            //rectangles.Add(r22);
            //var r23 = new RectangleViewModel(120, 120, 50, 50);
            //rectangles.Add(r23);
            //var r24 = new RectangleViewModel(175, 120, 50, 50);
            //rectangles.Add(r24);
            //var r25 = new RectangleViewModel(230, 120, 50, 50);
            //rectangles.Add(r25);
            //var r26 = new RectangleViewModel(285, 120, 50, 50);
            //rectangles.Add(r26);
            //var r27 = new RectangleViewModel(340, 120, 50, 50);
            //rectangles.Add(r27);
            //var r28 = new RectangleViewModel(395, 120, 50, 50);
            //rectangles.Add(r28);
            //var r29 = new RectangleViewModel(450, 120, 50, 50);
            //rectangles.Add(r29);
            //var r30 = new RectangleViewModel(505, 120, 50, 50);
            //rectangles.Add(r30);
            //var r31 = new RectangleViewModel(10, 175, 50, 50);
            //rectangles.Add(r31);
            //var r32 = new RectangleViewModel(65, 175, 50, 50);
            //rectangles.Add(r32);
            //var r33 = new RectangleViewModel(120, 175, 50, 50);
            //rectangles.Add(r33);
            //var r34 = new RectangleViewModel(175, 175, 50, 50);
            //rectangles.Add(r34);
            //var r35 = new RectangleViewModel(230, 175, 50, 50);
            //rectangles.Add(r35);
            //var r36 = new RectangleViewModel(285, 175, 50, 50);
            //rectangles.Add(r36);
            //var r37 = new RectangleViewModel(340, 175, 50, 50);
            //rectangles.Add(r37);
            //var r38 = new RectangleViewModel(395, 175, 50, 50);
            //rectangles.Add(r38);
            //var r39 = new RectangleViewModel(450, 175, 50, 50);
            //rectangles.Add(r39);
            //var r40 = new RectangleViewModel(505, 175, 50, 50);
            //rectangles.Add(r40);
            //var r41 = new RectangleViewModel(10, 230, 50, 50);
            //rectangles.Add(r41);
            //var r42 = new RectangleViewModel(65, 230, 50, 50);
            //rectangles.Add(r42);
            //var r43 = new RectangleViewModel(120, 230, 50, 50);
            //rectangles.Add(r43);
            //var r44 = new RectangleViewModel(175, 230, 50, 50);
            //rectangles.Add(r44);
            //var r45 = new RectangleViewModel(230, 230, 50, 50);
            //rectangles.Add(r45);
            //var r46 = new RectangleViewModel(285, 230, 50, 50);
            //rectangles.Add(r46);
            //var r47 = new RectangleViewModel(340, 230, 50, 50);
            //rectangles.Add(r47);
            //var r48 = new RectangleViewModel(395, 230, 50, 50);
            //rectangles.Add(r48);
            //var r49 = new RectangleViewModel(450, 230, 50, 50);
            //rectangles.Add(r49);
            //var r50 = new RectangleViewModel(505, 230, 50, 50);
            //rectangles.Add(r50);
            //var r51 = new RectangleViewModel(10, 285, 50, 50);
            //rectangles.Add(r51);
            //var r52 = new RectangleViewModel(65, 285, 50, 50);
            //rectangles.Add(r52);
            //var r53 = new RectangleViewModel(120, 285, 50, 50);
            //rectangles.Add(r53);
            //var r54 = new RectangleViewModel(175, 285, 50, 50);
            //rectangles.Add(r54);
            //var r55 = new RectangleViewModel(230, 285, 50, 50);
            //rectangles.Add(r55);
            //var r56 = new RectangleViewModel(285, 285, 50, 50);
            //rectangles.Add(r56);
            //var r57 = new RectangleViewModel(340, 285, 50, 50);
            //rectangles.Add(r57);
            //var r58 = new RectangleViewModel(395, 285, 50, 50);
            //rectangles.Add(r58);
            //var r59 = new RectangleViewModel(450, 285, 50, 50);
            //rectangles.Add(r59);
            //var r60 = new RectangleViewModel(505, 285, 50, 50);
            //rectangles.Add(r60);


        }

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get
            {
                return rectangles;
            }
            set
            {
                rectangles = value;
            }
        }
        /*public void insertrextangel(int a)
        {
            rectangles.RemoveAt(a);

        }*/

        //public ObservableCollection<RectangleViewModel> Temporary
        //{
        //    get
        //    {
        //        return temporary;
        //    }
        //    set
        //    {
        //        temporary = value;
        //    }
        //}

       /* public void myownfunction()
        {
            rectangles.Clear();
        }*/

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        private void OnPropertyChanged(string name)
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
