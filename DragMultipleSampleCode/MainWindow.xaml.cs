using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Net.Sockets;
using System.Net;

namespace SampleCode
{
    public partial class MainWindow : Window
    {
        #region Data Members
        int q = 1;
        /// <summary>
        /// Set to 'true' when the left mouse-button is down.
        /// </summary>
        private bool isLeftMouseButtonDownOnWindow = false;
        private bool isrightmousedownonwindow = false;
        /// <summary>
        /// Set to 'true' when dragging the 'selection rectangle'.
        /// Dragging of the selection rectangle only starts when the left mouse-button is held down and the mouse-cursor
        /// is moved more than a threshold distance.
        /// </summary>
        private bool isDraggingSelectionRect = false;

        /// <summary>
        /// Records the location of the mouse (relative to the window) when the left-mouse button has pressed down.
        /// </summary>
        private Point origMouseDownPoint;

        /// <summary>
        /// The threshold distance the mouse-cursor must move before drag-selection begins.
        /// </summary>
        private static readonly double DragThreshold = 5;

        /// <summary>
        /// Set to 'true' when the left mouse-button is held down on a rectangle.
        /// </summary>
        private bool isLeftMouseDownOnRectangle = false;

        private bool isRightMouseDownOnRectangle = false;

        /// <summary>
        /// Set to 'true' when the left mouse-button and control are held down on a rectangle.
        /// </summary>
        private bool isLeftMouseAndControlDownOnRectangle = false;

        /// <summary>
        /// Set to 'true' when dragging a rectangle.
        /// </summary>
        private bool isDraggingRectangle = false;

        #endregion Data Members

        public MainWindow()
        {
            InitializeComponent();
           // pic1.Source = new BitmapImage(new Uri("Tulips.jpg", UriKind.Relative));
        }

        /// <summary>
        /// Convenient accessor for the view model.
        /// </summary>
        private ViewModel ViewModel
        {
            get
            {
                return (ViewModel) this.DataContext;
            }
        }

        /// <summary>
        /// Event raised when the Window has loaded.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
            // Display help text for the sample app.
            //

           
            listBox.Focus();
        }

        /// <summary>
        /// Event raised when the mouse is pressed down on a rectangle.
        /// </summary>
        /// 


        
        private void Rectangle_leftMouseDown(object sender, MouseButtonEventArgs e)
        {
           

            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;
            
            isLeftMouseDownOnRectangle = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                //
                // Control key was held down.
                // This means that the rectangle is being added to or removed from the existing selection.
                // Don't do anything yet, we will act on this later in the MouseUp event handler.
                //
                //isLeftMouseAndControlDownOnRectangle = true;
               // System.Diagnostics.Process.Start(@"C:\Users\Moh_Hossam\Desktop\ImageCropper\ImageCropper\ImageCropper\bin\Debug\ImageCropper.EXE");

            }
            else
            {
                //
                // Control key is not held down.
                //
               // isLeftMouseAndControlDownOnRectangle = false;

                if (this.listBox.SelectedItems.Count == 0)
                {
                    //
                    // Nothing already selected, select the item.
                    //
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
                else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                {
                    
                    rectangleViewModel.Width = rectangleViewModel.Width + 30;
                    rectangleViewModel.Height = rectangleViewModel.Height + 30;
                    //rectangleViewModel.X =  700;
                    //rectangleViewModel.Y =  300;

                    //for (int q = 0; q < 11; q++)
                    //{


                    //    rectangleViewModel.Image = "C:\\Users\\OAM\\Desktop\\" + q + ".jpg";
                    //    // listBox.Focus();
                    //    if (q == 10)
                    //        q = 1;
                    //}

                   
                    
                    // 
                    // Item is already selected, do nothing.
                    // We will act on this in the MouseUp if there was no drag operation.
                    //
                }
                else
                {
                    //
                    // Item is not selected.
                    // Deselect all, and select the item.
                    //
                    this.listBox.SelectedItems.Clear();
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
            }

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }
        private void Rectangle_rightMouseDown(object sender, MouseButtonEventArgs e)
        {


            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            isRightMouseDownOnRectangle = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                //
                // Control key was held down.
                // This means that the rectangle is being added to or removed from the existing selection.
                // Don't do anything yet, we will act on this later in the MouseUp event handler.
                //
                //isLeftMouseAndControlDownOnRectangle = true;
            }
            else
            {
                //
                // Control key is not held down.
                //
                // isLeftMouseAndControlDownOnRectangle = false;

                if (this.listBox.SelectedItems.Count == 0)
                {
                    //
                    // Nothing already selected, select the item.
                    //
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
                else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                {
                    if (rectangleViewModel.Width<= 50 )
                    {
                        return;
                    }
                    else
                    {
                        rectangleViewModel.Width = rectangleViewModel.Width - 30;
                        rectangleViewModel.Height = rectangleViewModel.Height - 30;
                    }//rectangleViewModel.X =  700;
                    //rectangleViewModel.Y =  300;

                    // 
                    // Item is already selected, do nothing.
                    // We will act on this in the MouseUp if there was no drag operation.
                    //
                }
                else
                {
                    //
                    // Item is not selected.
                    // Deselect all, and select the item.
                    //
                    this.listBox.SelectedItems.Clear();
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
            }

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }

        /// <summary>
        /// Event raised when the mouse is released on a rectangle.
        /// </summary>
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isLeftMouseDownOnRectangle)
            {
                var rectangle = (FrameworkElement)sender;
                var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;
                if (rectangleViewModel.Y > 400)
                {
                    ViewModel.Rectangles.Remove(rectangleViewModel);
                }
                if (rectangleViewModel.X > 700)
                {
                   //System.Diagnostics.Process.Start(@"C:\Users\OAM\Desktop\ImageCropper\ImageCropper\ImageCropper\bin\Debug\ImageCropper.EXE");
                
         //            Socket sck;
         //sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
         //   IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
         //   try
         //   {
         //       sck.Connect(localEndPoint);
         //   }
         //   catch
         //   {
         //       Console.Write("Unable to connect to remote end point!\r\n");
                
         //   }
         //   Console.Write("Enter Text: ");
         //   string text = rectangleViewModel.Image;
         //   byte[] data = Encoding.ASCII.GetBytes(text);

         //   sck.Send(data);
         //   Console.Write("Data Sent!\r\n");
         //   Console.Write("Press any key to continue...");
         //   Console.Read();
         //   sck.Close();
        
                  }
                if (!isDraggingRectangle)
                {
                    //
                    // Execute mouse up selection logic only if there was no drag operation.
                    //
                    if (isLeftMouseAndControlDownOnRectangle)
                    {
                        //
                        // Control key was held down.
                        // Toggle the selection.
                        //
                        if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                        {
                            //
                            // Item was already selected, control-click removes it from the selection.
                            //
                            this.listBox.SelectedItems.Remove(rectangleViewModel);
                        }
                        else
                        {
                            // 
                            // Item was not already selected, control-click adds it to the selection.
                            //
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }
                    else
                    {
                        //
                        // Control key was not held down.
                        //
                        if (this.listBox.SelectedItems.Count == 1 &&
                            this.listBox.SelectedItem == rectangleViewModel)
                        {
                            //
                            // The item that was clicked is already the only selected item.
                            // Don't need to do anything.
                            //
                        }
                        else
                        {
                            //
                            // Clear the selection and select the clicked item as the only selected item.
                            //
                            this.listBox.SelectedItems.Clear();
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }
                }

                rectangle.ReleaseMouseCapture();
                isLeftMouseDownOnRectangle = false;
                isLeftMouseAndControlDownOnRectangle = false;

                e.Handled = true;
            }

            isDraggingRectangle = false;
        }

        /// <summary>
        /// Event raised when the mouse is moved over a rectangle.
        /// </summary>
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingRectangle)
            {
                //
                // Drag-move selected rectangles.
                //
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;

                origMouseDownPoint = curMouseDownPoint;

                foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
                {
                    rectangle.X += dragDelta.X;
                    rectangle.Y += dragDelta.Y;
                }
            }
            else if (isLeftMouseDownOnRectangle)
            {
                //
                // The user is left-dragging the rectangle,
                // but don't initiate the drag operation until
                // the mouse cursor has moved more than the threshold value.
                //
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    //
                    // When the mouse has been dragged more than the threshold value commence dragging the rectangle.
                    //
                    isDraggingRectangle = true;
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Event raised when the user presses down the left mouse-button.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //
                //  Clear selection immediately when starting drag selection.
                //
                listBox.SelectedItems.Clear();

                isLeftMouseButtonDownOnWindow = true;
                origMouseDownPoint = e.GetPosition(this);

                this.CaptureMouse();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Event raised when the user releases the left mouse-button.
        /// </summary>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                bool wasDragSelectionApplied = false;

                if (isDraggingSelectionRect)
                {
                    //
                    // Drag selection has ended, apply the 'selection rectangle'.
                    //


                    isDraggingSelectionRect = false;
                    ApplyDragSelectionRect();

                    e.Handled = true;
                    wasDragSelectionApplied = true;
                }

                if (isLeftMouseButtonDownOnWindow)
                {
                    isLeftMouseButtonDownOnWindow = false;
                    this.ReleaseMouseCapture();

                    e.Handled = true;
                }

                if (!wasDragSelectionApplied)
                {
                    //
                    // A click and release in empty space clears the selection.
                    //
                    listBox.SelectedItems.Clear();
                }
            }
        }

        /// <summary>
        /// Event raised when the user moves the mouse button.
        /// </summary>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingSelectionRect)
            {
                //
                // Drag selection is in progress.
                //
                Point curMouseDownPoint = e.GetPosition(this);
                UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);

                e.Handled = true;
            }
            else if (isLeftMouseButtonDownOnWindow)
            {
                //
                // The user is left-dragging the mouse,
                // but don't initiate drag selection until
                // they have dragged past the threshold value.
                //
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    //
                    // When the mouse has been dragged more than the threshold value commence drag selection.
                    //
                    isDraggingSelectionRect = true;
                    InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Initialize the rectangle used for drag selection.
        /// </summary>
        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);

            dragSelectionCanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Update the position and size of the rectangle used for drag selection.
        /// </summary>
        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            double x, y, width, height;

            //
            // Determine x,y,width and height of the rect inverting the points if necessary.
            // 

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            //
            // Update the coordinates of the rectangle used for drag selection.
            //
            Canvas.SetLeft(dragSelectionBorder, x);
            Canvas.SetTop(dragSelectionBorder, y);
            dragSelectionBorder.Width = width;
            dragSelectionBorder.Height = height;
        }

        /// <summary>
        /// Select all nodes that are in the drag selection rectangle.
        /// </summary>
        /// 
        private void ApplyDragSelectionRect()
        {
            dragSelectionCanvas.Visibility = Visibility.Collapsed;

            double x = Canvas.GetLeft(dragSelectionBorder);
            double y = Canvas.GetTop(dragSelectionBorder);
            double width = dragSelectionBorder.Width;
            double height = dragSelectionBorder.Height;
            Rect dragRect = new Rect(x, y, width, height);

            //
            // Inflate the drag selection-rectangle by 1/10 of its size to 
            // make sure the intended item is selected.
            //
            dragRect.Inflate(width / 10, height / 10);

            //
            // Clear the current selection.
            //
           listBox.SelectedItems.Clear();

            //
            // Find and select all the list box items.
            //
           
           foreach (RectangleViewModel rectangleViewModel in this.ViewModel.Rectangles)
            {
                Rect itemRect = new Rect(rectangleViewModel.X, rectangleViewModel.Y, rectangleViewModel.Width, rectangleViewModel.Height);
                if (dragRect.Contains(itemRect))
                {
                    listBox.SelectedItems.Add(rectangleViewModel);
                    rectangleViewModel.Width = rectangleViewModel.Width + 20;
                    rectangleViewModel.Height = rectangleViewModel.Height + 20;
                    //rectangleViewModel.X = rectangleViewModel.X + 300;
                    //rectangleViewModel.Y = rectangleViewModel.Y + 200;
                   
                }
                
            }

          
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Rectangle_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            isLeftMouseDownOnRectangle = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                //
                // Control key was held down.
                // This means that the rectangle is being added to or removed from the existing selection.
                // Don't do anything yet, we will act on this later in the MouseUp event handler.
                //
                isLeftMouseAndControlDownOnRectangle = true;
            }
            else
            {
                //
                // Control key is not held down.
                //
                isLeftMouseAndControlDownOnRectangle = false;

                if (this.listBox.SelectedItems.Count == 0)
                {
                    //
                    // Nothing already selected, select the item.
                    //
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
                else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                {

                    // rectangleViewModel.Width = rectangleViewModel.Width + 30;
                    //  rectangleViewModel.Height = rectangleViewModel.Height + 30;
                    //  
                    //if (rectangleViewModel.X > 700)
                    //{
                    //    ViewModel.Rectangles.Remove(rectangleViewModel);
                    //}  
                   rectangleViewModel.Angel = rectangleViewModel.Angel + 100;

                  //  rectangleViewModel.Image = "C:\\Users\\OAM\\Desktop\\" + q + ".jpg";
                    //// 
                    // q++;

                    // Item is already selected, do nothing.
                    // We will act on this in the MouseUp if there was no drag operation.

                    //
                }
               
            }

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }



        private void leftMouseDown(object sender, MouseButtonEventArgs e)
        {


            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            isLeftMouseDownOnRectangle = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                //
                // Control key was held down.
                // This means that the rectangle is being added to or removed from the existing selection.
                // Don't do anything yet, we will act on this later in the MouseUp event handler.
                //
                //isLeftMouseAndControlDownOnRectangle = true;
            }
            else
            {
                //
                // Control key is not held down.
                //
                // isLeftMouseAndControlDownOnRectangle = false;

                if (this.listBox.SelectedItems.Count == 0)
                {
                    //
                    // Nothing already selected, select the item.
                    //
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
                else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                {
               
                    rectangleViewModel.Width = rectangleViewModel.Width + 30;
                    rectangleViewModel.Height = rectangleViewModel.Height + 30;
        
                    //rectangleViewModel.X =  700;
                    //rectangleViewModel.Y =  300;

                    //for (int q = 0; q<11 ;q++ )
                    //{


                    //    rectangleViewModel.Image = "C:\\Users\\OAM\\Desktop\\"+q+".jpg";

                    //}



                    // 
                    // Item is already selected, do nothing.
                    // We will act on this in the MouseUp if there was no drag operation.
                    //
                }
                else
                {
                    //
                    // Item is not selected.
                    // Deselect all, and select the item.
                    //
                    this.listBox.SelectedItems.Clear();
                    this.listBox.SelectedItems.Add(rectangleViewModel);
                }
            }

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }

        private void listBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //RectangleViewModel m = new RectangleViewModel();
            //m.t.Abort();


            foreach (RectangleViewModel rectangle in this.listBox.Items)
            {
                rectangle.t.Abort();
                rectangle.streamWriter.WriteLine("exit");
                rectangle.streamWriter.Flush();

                rectangle.networkStream.Close();
            }


        }
        
    }
}
