using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpPcap;
using System.Threading;

namespace VNESConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitialTests();
            StartEquipments();

            Console.Write("Hit 'Enter' to exit...");
            Console.ReadLine();

        }

        private static void InitialTests()
        {
            CaptureDeviceList devList = VNESLib.Util.VNESUtils.GetAllCaptureDevices();

            if (devList != null)
            {
                foreach (ICaptureDevice item in devList)
                {
                    Console.WriteLine("{0}\n", item.ToString());
                }

                ICaptureDevice capDevice = VNESLib.Util.VNESUtils.GetCaptureDeviceByGuid("1FF52D36-D991-471F-BEDC-785AEDC621BA");

                if (capDevice != null)
                {
                    Console.WriteLine("My CaptureDevice is:\n{0}", capDevice.ToString());
                }
            }
        }

        private static void StartEquipments()
        {
            byte[] ip = { 10, 0, 0, 1 };

            VNESLib.VNESBase equip = new VNESLib.VNESBase(new System.Net.IPAddress(ip), -1, System.Net.Sockets.ProtocolType.Unknown);

            ICaptureDevice capDevice = VNESLib.Util.VNESUtils.GetCaptureDeviceByGuid("1FF52D36-D991-471F-BEDC-785AEDC621BA");
            if (capDevice!=null)
	        {
                equip.StartEquipment(capDevice);

                Console.WriteLine("Press any key to stop equipment.");
                while ( false==Console.KeyAvailable )
                {
                    Thread.Sleep(500);                
                }
                
                equip.StopEquipment();
	        }
        }
    }
}
