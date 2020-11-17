using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Monitor
{
    public class ScreenResolution
    {
        public static int Vertical { get { return VerticalRes(Screen.PrimaryScreen); } }
        public static int Horizontal { get { return HorizontalRes(Screen.PrimaryScreen); } }

        private static int VerticalRes(Screen screen)
        {
            var hdc = NativeMethods.CreateDC(screen.DeviceName, "", "", IntPtr.Zero);
            return NativeMethods.GetDeviceCaps(hdc, (int)DeviceCap.DESKTOPVERTRES);
        }

        private static int HorizontalRes(Screen screen)
        {
            var hdc = NativeMethods.CreateDC(screen.DeviceName, "", "", IntPtr.Zero);
            return NativeMethods.GetDeviceCaps(hdc, (int)DeviceCap.DESKTOPHORZRES);
        }
    }

    internal static class NativeMethods
    {

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, IntPtr devMode);


        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
    }

    internal enum DeviceCap
    {
        HORZRES = 8,
        VERTRES = 10,
        DESKTOPVERTRES = 117,
        LOGPIXELSY = 90,
        DESKTOPHORZRES = 118
    }
}
