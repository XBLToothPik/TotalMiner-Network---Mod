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
        private SessionPropertiesExtended _ExtendedProperties;
        //private NetworkSessionProperties _SessionProperties;
        private NetworkSessionType _SessionType;
        private QualityOfService _Quality;



        public PluginAvailableNetworkSession(SessionPropertiesExtended extendedProperties)
        {
            this._ExtendedProperties = extendedProperties;
            _Quality = new QualityOfService();
            _SessionType = NetworkSessionType.PlayerMatch;
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


        public NetworkSessionType SessionType
        {
            get 
            {
                return _SessionType;
            }
        }

        object IAvailableNetworkSession.SessionProperties
        {
            get 
            {
                return _ExtendedProperties;
            }
        }
        public SessionPropertiesExtended ExtendedProperties
        {
            get
            {
                return _ExtendedProperties;
            }
        }
    }
}
