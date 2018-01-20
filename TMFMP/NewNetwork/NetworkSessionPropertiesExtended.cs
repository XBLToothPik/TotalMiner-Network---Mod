using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.Engine.Net;
namespace TMFMP.Network
{
    public class NetworkSessionPropertiesExtended : NetworkSessionProperties
    {
        public string HostName { get; set; }
        public short HostGID { get; set; }
        public int EXEVersion { get; set; }
        public int SessionID { get; set; }
    }
}
