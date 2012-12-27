using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VNESServiceTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            VNESServiceTester svc = new VNESServiceTester();

            if (Environment.UserInteractive)
            {
                svc.Start();
                Console.WriteLine("Press ENTER key to stop service...");
                Console.ReadLine();
                svc.Dispose();
                svc.Stop();
            }
            else
            {
                ServiceBase.Run(svc);
            }
        }
    }
}
