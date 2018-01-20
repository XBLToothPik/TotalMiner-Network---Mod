using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class MapTM
    {
        private object _BaseMapTMObject;
        public object BaseMapTMObject
        {
            get
            {
                return this._BaseMapTMObject;
            }
        }
        public Type BaseMapTMObjectType
        {
            get
            {
                return this._BaseMapTMObject.GetType();
            }
        }
        public static MapTM GetFromGameInstance(GameInstance inst)
        {
            MapTM newMap = new MapTM();

            FieldInfo mapField = inst.BaseGameObjectType.GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
            object mapObject = mapField.GetValue(inst.BaseGameInstancebject);
            newMap._BaseMapTMObject = mapObject;

            return newMap;
        }
    }
}
