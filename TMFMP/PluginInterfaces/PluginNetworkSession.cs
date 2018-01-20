using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using StudioForge.Engine.Net;
using StudioForge.Engine.GamerServices;
using System.Net.Sockets;
using TMFMP.Network;

namespace TMFMP.PluginInterfaces
{
    public class PluginNetworkSession : INetworkSession
    {
        #region Events
        public event EventHandler<GameEventArgs> GameEnded;

        public event EventHandler<GameEventArgs> GameStarted;

        private event EventHandler<GamerEventArgs> _GamerJoined;
        public event EventHandler<GamerEventArgs> GamerJoined
        {
            add
            {
                foreach (NetworkGamer _gamer in NetGlobals.NetSession.AllGamers)
                    value(_gamer, new GamerEventArgs(_gamer));
                _GamerJoined += value;
            }
            remove
            {
                _GamerJoined -= value;
            }
        }
        
        public event EventHandler<GamerEventArgs> GamerLeft;
        public event EventHandler<NetworkSessionEndedEventArgs> SessionEnded;

        public void RaiseGameStarted()
        {
            if (this.GameStarted != null)
                this.GameStarted(this, new GameEventArgs());
        }
        public void RaiseGamerJoined(NetworkGamer gamer)
        {
            if (_GamerJoined != null)
                _GamerJoined(gamer, new GamerEventArgs(gamer));
        }
        public void RaiseGameEnded()
        {
            if (GameEnded != null)
                GameEnded(this, new GameEventArgs());
        }
        public void RaiseSessionEnded(NetworkSessionEndReason reason)
        {
            if (SessionEnded != null)
                SessionEnded(this, new NetworkSessionEndedEventArgs(reason));
        }
        public void RaiseGamerLeft(NetworkGamer gamer)
        {
            if (GamerLeft != null)
                GamerLeft(this, new GamerEventArgs(gamer));
        }
        #endregion

        #region Backing Vars
        private List<NetworkGamer> _AllGamers;
        private List<NetworkGamer> _LocalGamers;
        private List<NetworkGamer> _RemoteGamers;
        private NetworkGamer _myGamer;
        private NetworkGamer _Host;
        private NetworkSessionProperties _Properties;
        private NetworkSessionType _SessionType;
        private NetworkSessionState _SessionState;
        #endregion

        #region Accessor Vars
        public List<NetworkGamer> AllGamers
        {
            get 
            {
                return _AllGamers;
            }
            set
            {
                _AllGamers = value;
            }
        }
        public List<NetworkGamer> LocalGamers
        {
            get 
            {
                return _LocalGamers;
            }
            set
            {
                _LocalGamers = value;
            }
        }
        public List<NetworkGamer> RemoteGamers
        {
            get 
            {
                return _RemoteGamers; 
            }
            set
            {
                _RemoteGamers = value;
            }
        }

        public NetworkGamer MyGamer
        {
            get
            {
                return _myGamer;
            }
        }
        public NetworkGamer Host
        {
            get
            {
                return _Host;
            }
        }
        public bool IsDisposed
        {
            get 
            { 
                return false; 
            }
        }
        public bool IsHost
        {
            get 
            {
                return Host.ID == MyGamer.ID;
            }
        }
        public NetworkSessionProperties SessionProperties
        {
            get
            {
                return _Properties;
            }
        }
        public NetworkSessionState SessionState
        {
            get
            {
                return _SessionState;
            }
        }
        public NetworkSessionType SessionType
        {
            get
            {
                return _SessionType;
            }
        }

        #endregion

        #region CTORS
        public PluginNetworkSession(Gamer host, Gamer myGamer, NetworkSessionProperties properties, NetworkSessionType type, NetworkSessionState state)
        {
            _AllGamers = new List<NetworkGamer>();
            _LocalGamers = new List<NetworkGamer>();
            _RemoteGamers = new List<NetworkGamer>();

            _myGamer = new NetworkGamer(myGamer.ID, myGamer.Gamertag);
            _Host = new NetworkGamer(host.ID, host.Gamertag);
            _Host.AddGamerState(GamerStates.Host);

            _myGamer.AddGamerState(GamerStates.Local);

            _Properties = properties;
            _SessionType = type;
            _SessionState = state;

            if (IsHost)
            {
                _Host.AddGamerState(GamerStates.Local);
                LocalGamers.Add(_Host);
            }
            else
            {
                LocalGamers.Add(_myGamer);
                AllGamers.Add(_myGamer);
                RemoteGamers.Add(_Host);
                
            }
            AllGamers.Add(_Host);
        }
        #endregion

        #region Helper Methods
        public void SetSessionState(NetworkSessionState newState)
        {
            _SessionState = newState;
        }
        public void RemoveGamerByID(GamerID id)
        {
            NetworkGamer targetGamer = _AllGamers.Find(x => x.ID == id);
            if (targetGamer != null)
                this.RaiseGamerLeft(targetGamer);
            _AllGamers.RemoveAll(x => x.ID == id);
            _RemoteGamers.RemoveAll(x => x.ID == id);
        }
        public NetworkGamer FindGamerById(GamerID id)
        {
            if (id == 0)
                return null;
            for (int i = 0; i < _AllGamers.Count; i++)
                if (_AllGamers[i].ID.ID == id.ID)
                    return _AllGamers[i];
            return null;
        }
        #endregion

        #region Session Methods
        public void EndGame()
        {
        }

        public void StartGame()
        {
            this.GameStarted(this, new GameEventArgs());

            if (IsHost)
            {
                NetMethods.QueueSessionStateUpdate(NetworkSessionState.Playing);
            }
        }

        public void Update()
        {
            if (NetGlobals.CurrentConnection.IsConnected())
            {
                NetMethods.ParseIncomingData();
                NetMethods.SendInternalData();
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {

        }
        #endregion
    }
}
