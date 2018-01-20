using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMFMP.TMInternal
{
    public class MapModel
    {
        private object _BaseMapModelObject;
        public object BaseMapModelObject
        {
            get
            {
                return this._BaseMapModelObject;
            }
        }
        public Type BaseMapModelType
        {
            get
            {
                return this._BaseMapModelObject.GetType();
            }
        }

        public static MapModel CreateFromInternalObject(object obj)
        {
            MapModel toRet = new MapModel();
            toRet._BaseMapModelObject = obj;
            return toRet;
        }
    }
}
