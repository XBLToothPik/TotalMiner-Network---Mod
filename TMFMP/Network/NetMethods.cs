using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using StudioForge.Engine.Net;
using TMFMP.Network.Data;
namespace TMFMP.Network
{
    public static class NetMethods
    {
        public static void ParseIncomingData()
        {
            if (NetGlobals.CurrentConnection.IsConnected() && NetGlobals.CurrentConnection.Available > 0)
            {
                PacketReader reader = new PacketReader(NetGlobals.CurrentConnection.GetStream());
                while (NetGlobals.CurrentConnection.Available > 0)
                {
                    PacketType type = (PacketType)reader.ReadByte();
                    switch (type)
                    {
                        case PacketType.TMData:
                            ReadTotalMinerGameData(reader);
                            break;
                        case PacketType.Internal:
                            ReadInternalData(reader);
                            break;
                    }
                }
            }
            
        }
        
        private static void ReadTotalMinerGameData(PacketReader reader)
        {
            short sender = reader.ReadInt16();
            int len = reader.ReadInt32();
            byte[] _data = reader.ReadBytes(len);

            Packet packet = default(Packet);
            packet.Sender = sender;
            packet.Data = _data;
            NetGlobals.TotalMinerConnectionPacketBuffer_In.Enqueue(packet);
        }
        public static void SendTotalMinerGameData()
        {
            if (NetGlobals.TotalMinerConnectionPacketBuffer_Out.Count > 0 && NetGlobals.EnableSendingTMData)
            {
                lock (NetGlobals.TotalMinerConnectionPacketBuffer_Out)
                {
                    while (NetGlobals.TotalMinerConnectionPacketBuffer_Out.Count != 0 && NetGlobals.CurrentConnection.IsConnected() && NetGlobals.EnableSendingTMData)
                    {
                        //For a non-interrupted data packet (could be interrupted by NetSession.Update) we will
                        //Send ALL of this data at one single time
                        Packet nextPacket = NetGlobals.TotalMinerConnectionPacketBuffer_Out.Dequeue();

                        byte[] _packetType = new byte[] { (byte)PacketType.TMData };
                        byte[] _target = BitConverter.GetBytes(nextPacket.Target);
                        byte[] _len = BitConverter.GetBytes(nextPacket.Data.Length);
                        byte[] _data = nextPacket.Data;

                        byte[] _allData = new byte[_packetType.Length + _target.Length + _len.Length + _data.Length];

                        Buffer.BlockCopy(_packetType, 0, _allData, 0, _packetType.Length);
                        Buffer.BlockCopy(_target, 0, _allData, _packetType.Length, _target.Length);
                        Buffer.BlockCopy(_len, 0, _allData, _packetType.Length + _target.Length, _len.Length);
                        Buffer.BlockCopy(_data, 0, _allData, _packetType.Length + _target.Length + _len.Length, _data.Length);

                        NetGlobals.CurrentConnection.GetStream().Write(_allData, 0, _allData.Length);

                    }
                }
            }
        }

        private static void ReadInternalData(PacketReader reader)
        {
            short sender = reader.ReadInt16();
            CustomInternalPacket type = (CustomInternalPacket)reader.ReadByte();
            switch (type)
            {
                case CustomInternalPacket.PlayerUpdate:
                    ReadInternalData_PlayerUpdate(reader, sender);
                    break;
                case CustomInternalPacket.SessionUpdate:
                    ReadInternalData_SessionUpdated(reader, sender);
                    break;
                    
            }
        }
        private static void ReadInternalData_PlayerUpdate(PacketReader reader, short sender)
        {
            PlayerUpdateType type = (PlayerUpdateType)reader.ReadByte();
            switch (type)
            {
                case PlayerUpdateType.Join:
                    ReadInternalData_PlayerUpdate_PlayerJoined(reader, sender);
                    break;
                case PlayerUpdateType.Leave:
                    ReadInternalData_PlayerUpdate_PlayerLeave(reader, sender);
                    break;
            }
        }
        private static void ReadInternalData_PlayerUpdate_PlayerLeave(PacketReader reader, short sender)
        {
            short pid = reader.ReadInt16();
            if (NetGlobals.NetSession != null)
            {
                NetGlobals.NetSession.RemoveGamerByID(new StudioForge.Engine.GamerServices.GamerID(pid));
            }
        }
        private static void ReadInternalData_PlayerUpdate_PlayerJoined(PacketReader reader, short sender)
        {
            short pid = reader.ReadInt16();
            string pName = reader.ReadString();
            NetworkGamer newGamer = new NetworkGamer(new StudioForge.Engine.GamerServices.GamerID(pid), pName);
            NetGlobals.NetSession.AllGamers.Add(newGamer);
            NetGlobals.NetSession.RemoteGamers.Add(newGamer);
            NetGlobals.NetSession.RaiseGamerJoined(newGamer);
        }
        
        private static void ReadInternalData_SessionUpdated(PacketReader reader, short sender)
        {
            SessionUpdateType type = (SessionUpdateType)reader.ReadByte();
            switch (type)
            {
                case SessionUpdateType.StateUpdate:
                    ReadInternalData_SessionUpdated_SessionStateUpdate(reader, sender);
                    break;
                case SessionUpdateType.SessionEnd:
                    ReadInternalData_SessionUpdated_SessionEnded(reader, sender);
                    break;
            }
        }
        private static void ReadInternalData_SessionUpdated_SessionEnded(PacketReader reader, short sender)
        {
            NetworkSessionEndReason reason = (NetworkSessionEndReason)reader.ReadByte();
            if (NetGlobals.NetSession != null)
            {
                NetGlobals.NetSession.RaiseSessionEnded(reason);
            }
        }
        private static void ReadInternalData_SessionUpdated_SessionStateUpdate(PacketReader reader, short sender)
        {
            NetworkSessionState newState = (NetworkSessionState)reader.ReadByte();
            if (NetGlobals.NetSession != null)
            {
                NetGlobals.NetSession.ExtendedProperties.SessionState = newState;
            }
        }

        public static void SendInternalData()
        {
            if (NetGlobals.InternalConnectionPacketBuffer_Out.Count > 0 && NetGlobals.EnableSendingInternalData)
            {
                lock (NetGlobals.InternalConnectionPacketBuffer_Out)
                {
                    while (NetGlobals.TotalMinerConnectionPacketBuffer_Out.Count > 0 && NetGlobals.CurrentConnection.IsConnected() && NetGlobals.EnableSendingInternalData)
                    {
                        Packet nextPacket = NetGlobals.InternalConnectionPacketBuffer_Out.Dequeue();

                        byte[] _packetType = new byte[] { (byte)PacketType.Internal };
                        byte[] _target = BitConverter.GetBytes(nextPacket.Target);
                        byte[] _data = nextPacket.Data;

                        byte[] _allData = new byte[_packetType.Length + _target.Length + _data.Length];

                        Buffer.BlockCopy(_packetType, 0, _allData, 0, _packetType.Length);
                        Buffer.BlockCopy(_target, 0, _allData, _packetType.Length, _target.Length);
                        Buffer.BlockCopy(_data, 0, _allData, _packetType.Length + _target.Length, _data.Length);

                        NetGlobals.CurrentConnection.GetStream().Write(_allData, 0, _allData.Length);

                    }
                }
            }
        }

      

   

        #region Immediate Network methods
        public static void SendEndSession()
        {
            if (NetGlobals.NetSession != null && NetGlobals.CurrentConnection.IsConnected())
            {
               Packet newPacket = default(Packet);
               newPacket.Sender = 0;
               newPacket.Target = 0;
               newPacket.Data = new byte[2] { (byte)CustomInternalPacket.SessionUpdate, (byte)SessionUpdateType.SessionEnd };
               SendInternalPacketNow(newPacket);
            }
        }
        public static void SendSessionStateUpdate(NetworkSessionState newState)
        {
            if (NetGlobals.NetSession != null)
            {
                Packet updatePacket = default(Packet);
                updatePacket.Target = 0;
                updatePacket.Sender = 0;
                updatePacket.Data = new byte[3] { (byte)CustomInternalPacket.SessionUpdate, (byte)SessionUpdateType.StateUpdate, (byte)newState };
                SendInternalPacketNow(updatePacket);
            }
        }
        public static void SendInternalPacketNow(Packet rawData)
        {
            if (NetGlobals.NetSession != null && NetGlobals.CurrentConnection.IsConnected())
            {
                byte[] _packetType = new byte[] { (byte)PacketType.Internal };
                byte[] _target = BitConverter.GetBytes(rawData.Target);
                byte[] _data = rawData.Data;

                byte[] _allData = new byte[_packetType.Length + _target.Length + _data.Length];
                
                Buffer.BlockCopy(_packetType, 0, _allData, 0, _packetType.Length);
                Buffer.BlockCopy(_target, 0, _allData, _packetType.Length, _target.Length);
                Buffer.BlockCopy(_data, 0, _allData, _packetType.Length + _target.Length, _data.Length);
                NetGlobals.CurrentConnection.GetStream().Write(_allData, 0, _allData.Length);
            }
        }
        #endregion
    }
}
