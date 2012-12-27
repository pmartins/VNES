using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using PacketDotNet;
using SharpPcap;

namespace VNESLib.Util
{
    public class VNESUtils
    {

        public static CaptureDeviceList GetAllCaptureDevices()
        {
            try
            {
                return CaptureDeviceList.Instance;
            }
            catch (Exception ex)
            {                
                //throw;
            }
            return null;
        }

        public static ICaptureDevice GetCaptureDeviceByGuid(string guid)
        {
            ICaptureDevice capDevice = null;
            CaptureDeviceList devList = GetAllCaptureDevices();
            
            if (devList!=null)
            {
                capDevice = devList.Where(c => c.Name.Contains(guid)).FirstOrDefault();
            }

            return capDevice;
        }
        
        public static string ConvertByteArrayToString(byte[] data)
        {
            StringBuilder sbString = new StringBuilder();

            if (data!=null)
            {
                foreach (byte item in data)
                {
                    sbString.Append( Convert.ToString(item).PadLeft(2, '0') );
                }                
            }

            return sbString.ToString();        
        }

    }
}
