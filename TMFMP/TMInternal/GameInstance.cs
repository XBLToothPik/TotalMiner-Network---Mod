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
        public static GameInstance GetFromAPIGame(StudioForge.TotalMiner.API.ITMGame game)
        {
            GameInstance inst = new GameInstance();
            inst._BaseGameInstancebject = game;
            return inst;
        }
        public static GameInstance GetFromInternalObject(object internalObj)
        {
            GameInstance inst = new GameInstance();
            inst._BaseGameInstancebject = internalObj;
            return inst;
        }
    }
}
