using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using TMFMP.Network;
using System.Reflection;
namespace TMFMP
{
    class PluginProvider : ITMPluginProvider
    {
        public PluginProvider()
        {
            EmbeddedAssembly.Load("TMFMP.Resources.0Harmony.dll", "0Harmony.dll");
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }
        private Assembly ResolveAssembly(object sender, ResolveEventArgs e)
        {
            return EmbeddedAssembly.Get(e.Name);
        }
        public ITMPlugin GetPlugin()
        {
            return (Globals.MainPlugin ?? (Globals.MainPlugin = new PluginMain()));
        }

        public ITMPluginArcade GetPluginArcade()
        {
            return null;
        }

        public ITMPluginBlocks GetPluginBlocks()
        {
            return null;
        }

        public ITMPluginGUI GetPluginGUI()
        {
            return null;
        }
        public ITMPluginNet GetPluginNet()
        {
            return (NetGlobals.NetPlugin ?? (NetGlobals.NetPlugin = new PluginNetMain()));
        }
    }
}
