using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SharpPcap;
using PacketDotNet;
using System.Threading;
using PacketDotNet.Utils;

namespace VNESLib
{
    /// <summary>
    /// VNESBase is the base class that should be extended by all the specific equipments.
    /// </summary>
    public class VNESBase
    {
        private const int ReadTimeOutMs = 1000;
        private IPAddress ip = null;
        private int port = -1;
        private ProtocolType protocol = ProtocolType.Unknown;
        private bool isWorking = false;
        private string filter = null;
        private ICaptureDevice equipNetworkDevice = null;
            
        /// <summary>
        /// Parameterless constructor instantiation not allowed
        /// </summary>
        private VNESBase()
        { 

        }

        public VNESBase(IPAddress ip, int port, ProtocolType protocol)
        {
            this.ip = ip;
            this.port = port;
            this.protocol = protocol;
            this.filter = this.BuildFilter();
        }

        public bool StartEquipment(ICaptureDevice device)
        {
            bool startSuccess = false;

            if (device!=null)
            {
                equipNetworkDevice = device;

                device.Open(DeviceMode.Promiscuous, ReadTimeOutMs);
                isWorking = true;

                device.Filter = this.filter;

                Thread thread = new Thread(new ParameterizedThreadStart(this.DoCapture));
                thread.Start(device);
            }

            return startSuccess;        
        }

        private void DoCapture(object deviceObj)
        {
            ICaptureDevice device = deviceObj as ICaptureDevice;

            if ( device!=null )
            {
                RawCapture capture = null;

                while (isWorking)
                {
                    capture = device.GetNextPacket();
                    if (capture!=null)
                    {
                        this.ProcessData(capture.Data);
    

                    }                    

                    capture = null;
                }

                device.Close();    
            }
        }

        public bool StopEquipment()
        {
            bool stopSuccess = false;

            isWorking = false;

            // TODO: check that the device has terminated the capture
            //Thread.Sleep( ReadTimeOutMs );
            //if ( device.Started==false )
            //Thread.Sleep( ReadTimeOutMs );

            stopSuccess = true;
            Console.WriteLine("Equipment stopped...");

            return stopSuccess;        
        }

        public virtual int ProcessData(byte[] data)
        {
            Console.WriteLine("ProcessData: {0}", VNESLib.Util.VNESUtils.ConvertByteArrayToString(data));

            int rcvdByteCount = 0;
            if (data!=null)
            {
                rcvdByteCount = data.Length;
            }

            return rcvdByteCount;
        }

        public virtual byte[] SendData(byte[] data)
        {
            //TcpPacket pong = new TcpPacket(ByteArraySegment);

            //pong.Header = new byte[] { 
            //    0x00, 0x00, 0x00, 0x00,
            //    0x00, 0x00, 0x00, 0x00,
            //    data
            //    };
            //this.equipNetworkDevice.SendPacket(
            return null;
        }

        /// <summary>
        /// <see cref="http://www.winpcap.org/docs/docs_412/html/group__language.html"/>
        /// </summary>
        /// <returns></returns>
        private string BuildFilter()
        {
            StringBuilder sb = new StringBuilder();


            if (this.ip != null)
            {
                sb.Append("(dst host ").Append(this.ip.ToString()).Append(")");
            }

            if (this.port > 0)
            {
                if (sb.Length>0)
                {
                    sb.Append(" and ");
                }
                sb.Append("(dst port").Append(this.port).Append(")");
            }

            if (this.protocol != ProtocolType.Unknown)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }

                sb.Append(this.GetIpProtocolFilter());
            }

            return sb.ToString();
        }

        private string GetIpProtocolFilter()
        {
            StringBuilder protoFilterSb = new StringBuilder();

            // TODO: IPV6 not handled here
            if (this.protocol!=ProtocolType.Unknown)
            {
                protoFilterSb.Append("(ip proto ");

                if (this.protocol == ProtocolType.Tcp || this.protocol == ProtocolType.Udp || this.protocol == ProtocolType.Icmp)
                {
                    protoFilterSb.Append(@"\");
                }

                protoFilterSb.Append(this.protocol.ToString()).Append(")");
            }
            
            return protoFilterSb.ToString();
        }
    }
}