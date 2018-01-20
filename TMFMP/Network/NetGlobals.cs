using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMFMP.PluginInterfaces;
using System.Net;
using System.Net.Sockets;
using StudioForge.Engine.Net;
using StudioForge.TotalMiner.API;
using TMFMP.Network.Data;
namespace TMFMP.Network
{
    public static class NetGlobals
    {
        public static string IP = "localhost";//"74.208.169.59";
        public static int Port = 5786;
        public static ITMNetworkManager NetworkManager;
        public static ITMPluginNet NetPlugin;
        public static PluginNetworkSession NetSession;
        public static TcpClient CurrentConnection;

        public static Queue<Packet> TotalMinerConnectionPacketBuffer_In;
        public static Queue<Packet> TotalMinerConnectionPacketBuffer_Out;

        public static Queue<Packet> InternalConnectionPacketBuffer_In;
        public static Queue<Packet> InternalConnectionPacketBuffer_Out;

        public static NetworkGamer MyGamer;

        public static bool EnableSendingTMData = true;
        public static bool EnableSendingInternalData = true;

        public static void InitNetGlobals()
        {
            NetGlobals.TotalMinerConnectionPacketBuffer_In = new Queue<Packet>(8);
            NetGlobals.TotalMinerConnectionPacketBuffer_Out = new Queue<Packet>(8);
            NetGlobals.InternalConnectionPacketBuffer_In = new Queue<Packet>(8);
            NetGlobals.InternalConnectionPacketBuffer_Out = new Queue<Packet>(8);
        }
        

    }
}
