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
namespace TMFMP.Network
{
    public static class NetworkConnectionUtils
    {
        public static bool SendJoinSession(string ip, int port, int exeVersion, Gamer myGamer, PluginAvailableNetworkSession targetSession, out PluginNetworkSession resultSession, out List<NetworkGamer> existingRemotes, out TcpClient resultConnection)
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
                    writer.Write(targetSession.SessionID);
                    writer.Write(exeVersion);
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
                                resultSession = new PluginNetworkSession(new StudioForge.Engine.GamerServices.Gamer(new GamerID(targetSession.HostGID), targetSession.HostGamertag), myGamer, targetSession.SessionProperties, targetSession.SessionType, NetworkSessionState.Playing);
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
        public static bool SendGetSessions(string ip, int port, int exeVersion, ref List<IAvailableNetworkSession> sessionsFound)
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
                    writer.Write(exeVersion);
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
                                string hostName = reader.ReadString();
                                short hostGid = reader.ReadInt16();
                                int sessExeVersion = reader.ReadInt32();
                                int sessID = reader.ReadInt32();
                                int pCount = reader.ReadInt32();
                                NetworkSessionState state = (NetworkSessionState)reader.ReadByte();
                                NetworkSessionType netType = (NetworkSessionType)reader.ReadByte();
                                
                                if (sessExeVersion == exeVersion)
                                {
                                    NetworkSessionPropertiesExtended properties = new NetworkSessionPropertiesExtended();
                                    properties[0] = (sessExeVersion << 4) | (0 & 0xF); //TEMP
                                    properties[7] = pCount;
                                    properties.EXEVersion = sessExeVersion;
                                    properties.HostGID = hostGid;
                                    properties.HostName = hostName;
                                    properties.SessionID = sessID;
                                    PluginAvailableNetworkSession newSession = new PluginAvailableNetworkSession(properties);
                                    sessionsFound.Add(newSession);
                                }
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
        public static bool SendCreateSession(string ip, int port, int exeVersion, short hostGID, string hostName, NetworkSessionType sessionType, NetworkSessionState state, out int newSessionID, out TcpClient newConnection)
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
                    writer.Write(exeVersion);
                    writer.Write(hostGID);
                    writer.Write(hostName);
                    writer.Write((byte)sessionType);
                    writer.Write((byte)state);
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
