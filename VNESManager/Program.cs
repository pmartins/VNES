using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace VNESManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ValidateArgs(args))
            {
                
            }
            else
            {
                PrintUsage();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();            
        }

        private static bool ValidateArgs(string[] args)
        {
            bool argsValid = false;

            if (args!=null)
            {
                
            }

            return argsValid;
        }
        
        private static void PrintUsage()
        {
            StringBuilder sbUsage = new StringBuilder();

            sbUsage.Append("USAGE: ").Append(Environment.NewLine);

            string executableName = Assembly.GetExecutingAssembly().Location;

            // Only supports Windows paths...
            int startIdx = executableName.LastIndexOf(@"\");

            if (startIdx>-1)
	        {
                executableName = executableName.Substring(startIdx+1);
	        }            

            sbUsage.Append(executableName);

            Console.WriteLine(sbUsage.ToString());
        }

    }
}
