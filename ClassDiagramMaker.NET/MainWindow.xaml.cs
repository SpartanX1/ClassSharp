using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDiagramMaker.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ClassBoxControl _currentControl = new ClassBoxControl();

        private void ButtonClass_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(sender as DependencyObject,"Class", DragDropEffects.Copy);
            }
        }

        private void CanvasBoard_Drop(object sender, DragEventArgs e)
        {
                if (sender != null)
                {
                        var classOutline = new ClassBoxControl(100,150);
                        Canvas.SetLeft(classOutline, e.GetPosition(CanvasBoard).X);
                        Canvas.SetTop(classOutline, e.GetPosition(CanvasBoard).Y);
                        CanvasBoard.Children.Add(classOutline);
                }
            }

        private void CanvasBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _currentControl != null)
            {
                Point point = e.GetPosition(CanvasBoard);
                if ((point.X - (_currentControl.ActualWidth/2) )>= 0 && (point.Y-(_currentControl.ActualHeight/2))>= 0)
                {
                    Canvas.SetTop(_currentControl, point.Y - (_currentControl.MinHeight / 2));
                    Canvas.SetLeft(_currentControl, point.X - (_currentControl.MinHeight / 2));
                }
            }
        }

        private void CanvasBoard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentControl = null;
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.SetCursor(Cursors.Hand);
        }

        private void Button_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                e.UseDefaultCursors = false;
                Mouse.SetCursor(Cursors.Hand);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

        private void CanvasBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = VisualTreeHelper.HitTest(CanvasBoard, e.GetPosition(CanvasBoard));
            DependencyObject obj = VisualTreeHelper.GetParent(result.VisualHit);
            while (obj != null)
            {
                if (obj is ClassBoxControl)
                {
                    _currentControl = obj as ClassBoxControl;
                    if (_currentControl.TemplatedParent != null)
                    {
                        _currentControl = _currentControl.TemplatedParent as ClassBoxControl;
                    }
                    break;
                }
                else
                    obj = VisualTreeHelper.GetParent(obj);
            }
        }
    }
}
