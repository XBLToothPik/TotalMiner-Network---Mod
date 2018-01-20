using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TM.Reflection
{
    /// <summary>
    /// This class is mainly provided as a Helper-Provider class for general reflection in this mod
    /// </summary>
    public static class TMReflection
    {
        #region Variables
        private static Assembly _TotalMinerAssembly;
        public static Assembly TotalMinerAssembly
        {
            get
            {
                return TMReflection._TotalMinerAssembly;
            }
        }
        #endregion

        #region Initialization Methods
        public static void ProvideAssembly(Assembly asm)
        {
            TMReflection._TotalMinerAssembly = asm;
        }
        public static void ProvideAssembly(StudioForge.TotalMiner.API.ITMGame game)
        {
            TMReflection._TotalMinerAssembly = game.GetType().Assembly;
        }
        #endregion

        #region Helper Methods
        public static Assembly GetAssembly(object targetObject)
        {
            return targetObject.GetType().Assembly;
        }
        public static Type GetAsmType(string fullTypeName)
        {
            return TMReflection._TotalMinerAssembly.GetType(fullTypeName);
        }
        
        public static FieldInfo GetField(Type targetType, string name, BindingFlags bindings)
        {
            return targetType.GetField(name, bindings);
        }
        public static MethodInfo GetMethod(Type targetType, string name, BindingFlags bindings, Type[] paramTypes)
        {
            return targetType.GetMethod(name, bindings, null, paramTypes, null);
        }
        
        public static object CallMethod(object targetObject, string name, BindingFlags bindings, object[] prams)
        {
            Type[] _types = (prams == null || prams.Length == 0)
                ? Type.EmptyTypes 
                : prams.Select(x => x.GetType()).ToArray();
            return targetObject.GetType().GetMethod(name, bindings, null, _types, null).Invoke(targetObject, prams);
        }
        public static void InvokeMethod(object targetObject, string name, BindingFlags bindings, object[] prams)
        {
            Type[] _types = (prams == null || prams.Length == 0)
                ? Type.EmptyTypes
                : prams.Select(x => x.GetType()).ToArray();
            MethodInfo method = targetObject.GetType().GetMethod(name, bindings, null, _types, null);
            targetObject.GetType().GetMethod(name, bindings, null, _types, null).Invoke(targetObject, prams);
        }
        public static void InvokeMethod(object targetObject, string name, BindingFlags bindings)
        {
            targetObject.GetType().GetMethod(name, bindings, null, Type.EmptyTypes, null).Invoke(targetObject, null);
        }

        public static Type GetTypeWithLevel(object targetObject, int level)
        {
            Type _toRet = targetObject.GetType();
            for (int i = 0; i < level; i++)
                _toRet = _toRet.BaseType;
            return _toRet;
        }
        public static Type GetTypeWithLevel(Type topType, int level)
        {
            Type _toRet = topType;
            for (int i = 0; i < level; i++)
                _toRet = _toRet.BaseType;
            return _toRet;
        }
        #endregion

    }
}
