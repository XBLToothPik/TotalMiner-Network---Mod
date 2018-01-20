using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using StudioForge.Engine.Net;
using System.Reflection;
using TMFMP.Network;
namespace TMFMP.PluginInterfaces
{
    
    public class PluginAvailableNetworkSession : IAvailableNetworkSession
    {
        private NetworkSessionPropertiesExtended _ExtendedProperties;
        private NetworkSessionProperties _SessionProperties;
        private NetworkSessionType _SessionType;
        private QualityOfService _Quality;

        private int _SessionID;
        private short _HostGID;
        private string _HostGamerTag;
        public string HostGamertag
        {
            get 
            {
                return _HostGamerTag;
            }
        }
        public short HostGID
        {
            get
            {
                return _HostGID;
            }
        }
        public int SessionID
        {
            get
            {
                return _SessionID;
            }
        }

        public PluginAvailableNetworkSession(NetworkSessionPropertiesExtended extendedProperties)
        {
            this._ExtendedProperties = extendedProperties;
            this._HostGamerTag = extendedProperties.HostName;
            this._SessionType = NetworkSessionType.PlayerMatch;
            this._Quality = new QualityOfService();
            this._SessionID = extendedProperties.SessionID;
            this._HostGID = extendedProperties.HostGID;

            this._SessionProperties = new NetworkSessionProperties();
            for (int i = 0; i < 8; i++)
                this._SessionProperties[i] = extendedProperties[i];
        }

        public double Ping
        {
            get 
            {
                return 29d;
            }
        }

        public QualityOfService QualityOfService
        {
            get 
            {
                return _Quality;
            }
        }

        public NetworkSessionProperties SessionProperties
        {
            get 
            {
                return _SessionProperties;
            }

        }

        public NetworkSessionType SessionType
        {
            get 
            {
                return _SessionType;
            }
        }

    }
}
