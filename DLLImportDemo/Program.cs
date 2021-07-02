using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace DLLImportDemo
{
    public class Program
    {
        [DllImport("user32.dll",EntryPoint = "MessageBox")]
        public static extern int MessageBox(IntPtr hwnd, string text, string caption, uint uType = 0x02);


        static void Main(string[] args)
        {
            //IntPtr wHhd = Process.GetCurrentProcess().MainWindowHandle;
            //MessageBox(wHhd, "Hello world", "test message box", 0x00004000);
            Console.WriteLine("DLLImportDemo");
            var s = Console.ReadLine();
            
            Console.WriteLine(s);
            Thread.Sleep(5000);

        }
    }
}
