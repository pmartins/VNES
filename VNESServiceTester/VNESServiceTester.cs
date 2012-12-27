using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VNESServiceTester
{
    public partial class VNESServiceTester : ServiceBase
    {
        public VNESServiceTester()
        {
            InitializeComponent();
        }

        public void Start()
        { 
            this.OnStart( new string[0] );
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
