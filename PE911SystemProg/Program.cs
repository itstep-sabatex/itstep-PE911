using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PE911SystemProg
{
    class Program
    {
        static byte[] loadFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            byte[] buffer = new byte[(int)fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            return buffer;
        }

        static void Main(string[] args)
        {

            byte[] ass = loadFile(@"C:\Users\sabat\source\repos\PE911SystemProg\DLLImportDemo\bin\Debug\net5.0\DLLImportDemo.exe");
            var type = ass.GetType();
            var s = AppDomain.CurrentDomain.Load(ass);
            var p = s.GetType("DLLImportDemo.Program");
            var method = p.GetMethod("Main");
            var instance = Activator.CreateInstance(p);
            var result = method.Invoke(instance, new object[] { "arg 1","arg 2" });

            //var s = AppDomain.CurrentDomain.ExecuteAssembly(@"C:\Users\sabat\source\repos\PE911SystemProg\DLLImportDemo\bin\Debug\net5.0\DLLImportDemo.exe");


            using (Process process = new Process())
            {
                process.StartInfo.FileName = @"C:\Users\sabat\source\repos\PE911SystemProg\DLLImportDemo\bin\Debug\net5.0\DLLImportDemo.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.Start();
                var stream = process.StandardInput;
                stream.WriteLine("wewuew weewew  eweweew ewewew ");


            }

            Console.ReadKey();

        }
    }
}
