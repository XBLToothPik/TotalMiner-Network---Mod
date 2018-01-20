using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
namespace TMFMP.TMInternal
{
    public class NetworkManager
    {
        public object _InternalNetworkManagerObject;
        public object InternalNetworkManagerObject
        {
            get
            {
                return this._InternalNetworkManagerObject;
            }
        }
        public Type InternalNetworkManagerObjectType
        {
            get
            {
                return this._InternalNetworkManagerObject.GetType();
            }
        }


        public static NetworkManager GetFromTotalMinerGame(TotalMinerGame game)
        {
            NetworkManager newManager = new NetworkManager();
            FieldInfo networkManagerField = game.InternalTotalMinerGameType.GetField("networkManager", BindingFlags.NonPublic | BindingFlags.Instance);
            newManager._InternalNetworkManagerObject = networkManagerField.GetValue(game.InternalTotalMinerObject);
            return newManager;
        }

        public static NetworkManager GetFromGameInstance(GameInstance inst)
        {
            Type instType = inst.BaseGameObjectType;
            FieldInfo networkManagerField = instType.GetField("networkManager", BindingFlags.NonPublic | BindingFlags.Instance);
            NetworkManager newManager = new NetworkManager();
            newManager._InternalNetworkManagerObject = networkManagerField.GetValue(inst.BaseGameInstancebject);
            return newManager;
        }
    }
}
