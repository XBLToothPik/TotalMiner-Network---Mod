using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.Engine.Net;

namespace TMFMP.Events
{
    public static class NetEvents
    {
        public static void RaiseGetPlayersResult(List<NetworkGamer> players)
        {
            if (NetEvents.OnGetPlayersResult != null)
                NetEvents.OnGetPlayersResult(null, new OnGetPlayersResultArgs() { ResultGamers = players });
        }

        public static EventHandler<OnGetPlayersResultArgs> OnGetPlayersResult;
        public class OnGetPlayersResultArgs : EventArgs
        {
            public List<NetworkGamer> ResultGamers;
            public int Count
            {
                get
                {
                    return ResultGamers == null ? 0 : ResultGamers.Count;
                }
            }
        }
    }
}
