using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class GameInstance
    {
        private object _BaseGameInstancebject;
        public object BaseGameInstancebject
        {
            get
            {
                return _BaseGameInstancebject;
            }
        }
        public Type BaseGameObjectType
        {
            get
            {
                return _BaseGameInstancebject.GetType();
            }
        }

        public MapRenderer MapRednerer;
        public NetworkManager NetworkManager;
        public MapTM Map;
        public VoxelModelManager VoxelModelManager;

        public bool IsEnabled
        {
            get
            {
                Type thisType = this.BaseGameObjectType;
                FieldInfo isEnabledField = thisType.GetField("IsEnabledField", BindingFlags.Public | BindingFlags.Instance);
                return (bool)isEnabledField.GetValue(this._BaseGameInstancebject);
            }
            set
            {
            
                Type thisType = this.BaseGameObjectType;
                FieldInfo isEnabledField = thisType.GetField("IsEnabledField", BindingFlags.Public | BindingFlags.Instance);
                isEnabledField.SetValue(this._BaseGameInstancebject, value);
            }
        }

        //public void AddPlayer(Player p)
        //{
        //    Type thisType = this.BaseGameObjectType;
        //    MethodInfo addPlayerMethod = thisType.GetMethod("AddPlayer", BindingFlags.Public | BindingFlags.Instance);
        //    addPlayerMethod.Invoke(this._BaseGameInstancebject, new object[] { p.InternalPlayerObject });
        //}
        public static GameInstance GetFromAPIGame(StudioForge.TotalMiner.API.ITMGame game)
        {
            GameInstance inst = new GameInstance();
            inst._BaseGameInstancebject = game;
            inst.MapRednerer = MapRenderer.GetFromGameInstance(inst);
            inst.NetworkManager = NetworkManager.GetFromGameInstance(inst);
            inst.Map = MapTM.GetFromGameInstance(inst);
            inst.VoxelModelManager = VoxelModelManager.CreateFromGameInstance(inst);
            return inst;
        }
        public static GameInstance GetFromInternalObject(object internalObj)
        {
            GameInstance inst = new GameInstance();
            inst._BaseGameInstancebject = internalObj;
            inst.MapRednerer = MapRenderer.GetFromGameInstance(inst);
            inst.NetworkManager = NetworkManager.GetFromGameInstance(inst);
            inst.Map = MapTM.GetFromGameInstance(inst);
            inst.VoxelModelManager = VoxelModelManager.CreateFromGameInstance(inst);
            return inst;
        }
    }
}
