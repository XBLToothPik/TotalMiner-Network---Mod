﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using StudioForge.Engine.Net;
using TMFMP.Network;
using System.Net;
using System.Net.Sockets;
using System.IO;
using TMFMP;
using TMFMP.IO;
using TMFMP.Network.Data;
using StudioForge.TotalMiner;
namespace TMFMP.PluginInterfaces
{
    public class PluginNetworkManager : ITMNetworkManager
    {
        #region Properties
        public INetworkSession Session
        {
            get
            {
                return NetGlobals.NetSession;
            }
        }
        #endregion

        #region CTORS
        public PluginNetworkManager()
        {
            NetGlobals.InitNetGlobals();
        }
        #endregion

        #region Session Methods
        public INetworkSession CreateSession(NetworkSessionType type, StudioForge.Engine.GamerServices.Gamer host, SessionProperties properties)
        {
            NetGlobals.MyGamer = new NetworkGamer(host.ID, host.Gamertag);
            int newSessionID = -1;
            if (NetworkConnectionUtils.SendCreateSession(LocalData.ServerIP, LocalData.ServerPort, properties, host.ID.ID, out newSessionID, out NetGlobals.CurrentConnection))
            {
                SessionPropertiesExtended newProp = new SessionPropertiesExtended();
                properties.Copy(newProp);
                PluginNetworkSession createdSession = (NetGlobals.NetSession = new PluginNetworkSession(host, host, newProp));
                return createdSession;
            }
            return null;
        }
        public INetworkSession JoinSession(IAvailableNetworkSession session, StudioForge.Engine.GamerServices.Gamer gamer)
        {
            NetGlobals.MyGamer = new NetworkGamer(gamer.ID, gamer.Gamertag);
            if (session is PluginAvailableNetworkSession)
            {
                EndSession();
                PluginAvailableNetworkSession targetSession = (PluginAvailableNetworkSession)session;
                List<NetworkGamer> _resultGamers = new List<NetworkGamer>();
                if (NetworkConnectionUtils.SendJoinSession(LocalData.ServerIP, LocalData.ServerPort, gamer, targetSession, out NetGlobals.NetSession, out _resultGamers, out NetGlobals.CurrentConnection))
                {
                    foreach (NetworkGamer _gamer in _resultGamers)
                    {
                        NetGlobals.NetSession.RemoteGamers.Add(_gamer);
                        NetGlobals.NetSession.AllGamers.Add(_gamer);
                    }
                    return NetGlobals.NetSession;
                }
            }
            return null;
        }
        public List<IAvailableNetworkSession> FindSessions(SessionMatching match)
        {
            

            List<IAvailableNetworkSession> sessions = new List<IAvailableNetworkSession>();

            NetworkConnectionUtils.SendGetSessions(LocalData.ServerIP, LocalData.ServerPort,  ref sessions);

            return sessions;
        }
        public void EndSession()
        {
            if (NetGlobals.NetSession != null)
            {
                if (NetGlobals.CurrentConnection.IsConnected())
                {
                    NetGlobals.CurrentConnection.Close();
                }
            }
        }
        #endregion

        #region Data Methods
        public bool ParseCustomPacket(PacketReader data, NetworkGamer sender)
        {
            CustomTMDataPacket packet = (CustomTMDataPacket)data.ReadByte();
            switch (packet)
            {
            }
            throw new ArgumentException("Invalid CustomTMDataPacket");
        }
        public bool ReadData(PacketReader data, out NetworkGamer sender)
        {
            data.BaseStream.SetLength(0);
            
            sender = null;
            NetMethods.ParseIncomingData();

            if (NetGlobals.TotalMinerConnectionPacketBuffer_In.Count > 0)
            {
                Packet packet = NetGlobals.TotalMinerConnectionPacketBuffer_In.Dequeue();
                sender = NetGlobals.NetSession.FindGamerById(new StudioForge.Engine.GamerServices.GamerID(packet.Sender));
                data.BaseStream.Write(packet.Data, 0, packet.Data.Length);
                data.BaseStream.Position = 0;
                return true;
            }

            return false;
        }
        public void SendData(PacketWriter data, SendDataOptions options, NetworkGamer recipient)
        {
            if (NetGlobals.CurrentConnection.IsConnected() && data.BaseStream.Length > 0L)
            {
                
                Packet newPacket = default(Packet);
                newPacket.Target = recipient == null ? (short)0 : recipient.ID.ID;
                newPacket.Data = data.BaseStream.GetStreamData();

                newPacket.Sender = NetGlobals.NetSession.MyGamer.ID.ID;
                NetGlobals.TotalMinerConnectionPacketBuffer_Out.Enqueue(newPacket);

                NetMethods.SendTotalMinerGameData();
            }
        }
        #endregion

    }

}
