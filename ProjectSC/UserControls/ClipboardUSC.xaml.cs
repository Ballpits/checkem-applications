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

namespace ProjectSC
{
    /// <summary>
    /// Interaction logic for ClipboardUSC.xaml
    /// </summary>
    public partial class ClipboardUSC : UserControl
    {
        public ClipboardUSC()
        {
            InitializeComponent();

            //canvasBoard.MouseWheel += new MouseWheelEventHandler(StackPanel_MouseWheel);
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        #region
        //private void StackPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (dPanel.Width >= dPanel.MinWidth && dPanel.Height >= dPanel.MinHeight)
        //    {
        //        if (e.Delta > 0)
        //        {
        //            dPanel.Height += 10;
        //            dPanel.Width += 10;
        //        }
        //        else
        //        {
        //            dPanel.Height -= 10;
        //            dPanel.Width -= 10;
        //        }
        //    }
        //    else
        //    {
        //        dPanel.Width = dPanel.MinWidth;
        //        dPanel.Height = dPanel.MinHeight;
        //    }
        //}
        #endregion

        private string GetMousePositionX(string s)
        {
            int comma = new int();
            char[] c = s.ToCharArray();

            for (int i = 0; i < s.Length; i++)
            {
                if (c[i] == ',')
                {
                    comma = i;

                    break;
                }
            }

            return s.Substring(0, comma);
        }
        private string GetMousePositionY(string s)
        {
            int comma = new int();
            char[] c = s.ToCharArray();

            for (int i = 0; i < s.Length; i++)
            {
                if (c[i] == ',')
                {
                    comma = i;

                    break;
                }
            }

            return s.Substring(comma + 1);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void pinner_MouseMove(object sender, MouseEventArgs e)
        {
            double firstPosX = Canvas.GetLeft(pinner);
            double firstPosY = Canvas.GetTop(pinner);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {

                Canvas.SetLeft(pinner, firstPosX + double.Parse(GetMousePositionX(Mouse.GetPosition(canvasBoard).ToString())) - Canvas.GetLeft(pinner));
            }

            //Canvas.SetTop(pinner, double.Parse(GetMousePositionY(Mouse.GetPosition(canvasBoard).ToString())));
        }
    }
}
