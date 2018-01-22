using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using StudioForge.Engine.Net;
using System.IO;
using TMFMP.PluginInterfaces;
using StudioForge.Engine.GamerServices;
using StudioForge.TotalMiner;
namespace TMFMP.Network
{
    public static class NetworkConnectionUtils
    {
        public static bool SendJoinSession(string ip, int port, Gamer myGamer, PluginAvailableNetworkSession targetSession, out PluginNetworkSession resultSession, out List<NetworkGamer> existingRemotes, out TcpClient resultConnection)
        {
            resultSession = null;
            resultConnection = null;
            existingRemotes = null;
            try
            {
                TcpClient connection = new TcpClient();
                connection.Connect(ip, port);

                if (connection.IsConnected())
                {
                    BinaryWriter writer = new BinaryWriter(connection.GetStream());
                    BinaryReader reader = new BinaryReader(connection.GetStream());

                    writer.Write((byte)Master_Server_Op_Out.Connect);
                    writer.Write((byte)Master_Server_ConnectionType.JoinSession);
                    writer.Write(targetSession.ExtendedProperties.SessionID);
                    writer.Write(myGamer.ID.ID);
                    writer.Write(myGamer.Gamertag);
                    writer.Flush();

                    Master_Server_Op_In opIn = (Master_Server_Op_In)reader.ReadByte();
                    if (opIn == Master_Server_Op_In.Connect)
                    {
                        Master_Server_ConnectionType type = (Master_Server_ConnectionType)reader.ReadByte();
                        if (type == Master_Server_ConnectionType.JoinSession)
                        {
                            YesNo good = (YesNo)reader.ReadByte();
                            if (good == YesNo.Yes)
                            {
                                int numPlayers = reader.ReadInt32();
                                existingRemotes = new List<NetworkGamer>(numPlayers);
                                for (int i = 0; i < numPlayers; i++)
                                {
                                    short pid = reader.ReadInt16();
                                    string pName = reader.ReadString();
                                    bool isHost = reader.ReadBoolean();
                                 
                                    if (!isHost)
                                    {
                                        NetworkGamer newGamer = new NetworkGamer(new GamerID(pid), pName);
                                        existingRemotes.Add(newGamer);
                                    }
                                    
                                }
                                resultSession = new PluginNetworkSession(new StudioForge.Engine.GamerServices.Gamer(new GamerID(targetSession.ExtendedProperties.HostID), targetSession.ExtendedProperties.HostName), myGamer, targetSession.ExtendedProperties);
                                resultConnection = connection;
                                return true;
                            }
                        }
                    }
                    connection.Close();
                }
                
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool SendGetSessions(string ip, int port, ref List<IAvailableNetworkSession> sessionsFound)
        {
            try
            {
                TcpClient connection = new TcpClient();
                connection.Connect(ip, port);
                if (connection.IsConnected())
                {
                    BinaryWriter writer = new BinaryWriter(connection.GetStream());
                    BinaryReader reader = new BinaryReader(connection.GetStream());

                    writer.Write((byte)Master_Server_Op_Out.Connect);
                    writer.Write((byte)Master_Server_ConnectionType.GetSessions);
                   // writer.Write(exeVersion);
                    writer.Flush();

                    Master_Server_Op_In opIn = (Master_Server_Op_In)reader.ReadByte();
                    if (opIn == Master_Server_Op_In.Connect)
                    {
                        Master_Server_ConnectionType conType = (Master_Server_ConnectionType)reader.ReadByte();
                        if (conType == Master_Server_ConnectionType.GetSessions)
                        {
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                SessionPropertiesExtended prop = SessionPropertiesExtended.Read(reader);
                                PluginAvailableNetworkSession newSession = new PluginAvailableNetworkSession(prop);
                                sessionsFound.Add(newSession);
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool SendCreateSession(string ip, int port, SessionProperties properties, short hostGID, out int newSessionID, out TcpClient newConnection)
        {
            newSessionID = -1;
            try
            {
                TcpClient connection = new TcpClient();
                connection.Connect(ip, port);
                if (connection.IsConnected())
                {
                    BinaryWriter writer = new BinaryWriter(connection.GetStream());
                    BinaryReader reader = new BinaryReader(connection.GetStream());
                    writer.Write((byte)Master_Server_Op_Out.Connect);
                    writer.Write((byte)Master_Server_ConnectionType.CreateSession);

                    SessionPropertiesExtended newProp = new SessionPropertiesExtended()
                    {
                        HostID = hostGID,
                        NetType = NetworkSessionType.PlayerMatch
                    };
                    properties.Copy(newProp); 
                    newProp.Write(writer);

                    Master_Server_Op_In opIn = (Master_Server_Op_In)reader.ReadByte();
                    if (opIn == Master_Server_Op_In.Connect)
                    {
                        Master_Server_ConnectionType type = (Master_Server_ConnectionType)reader.ReadByte();
                        if (type == Master_Server_ConnectionType.CreateSession)
                        {
                            YesNo valid = (YesNo)reader.ReadByte();
                            if (valid == YesNo.Yes)
                            {
                                newSessionID = reader.ReadInt32();
                                newConnection = connection;
                            }
                            else
                            {
                                newConnection = null;
                                connection.Close();
                            }
                            return valid == YesNo.Yes 
                                ? true 
                                : false;
                        }
                    }
                    connection.Close();
                }
                newConnection = null;
                return false;
            }
            catch
            {
                newConnection = null;
                return false;
            }
        }
    }
}
