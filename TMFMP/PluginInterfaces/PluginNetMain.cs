using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using TMFMP.PluginInterfaces;
using TMFMP.Network;
namespace TMFMP
{
    public class PluginNetMain : ITMPluginNet
    {
        public ITMNetworkManager GetNetworkManager()
        {
            return (NetGlobals.NetworkManager = new PluginNetworkManager());
        }
    }
}
