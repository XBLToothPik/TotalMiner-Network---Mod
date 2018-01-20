using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class ScreenManager
    {
        private object _BaseScreenMangerObject;
        public object BaseScreenMangerObject
        {
            get
            {
                return this._BaseScreenMangerObject;
            }
        }
        public Type BaseScreenManagerObjectType
        {
            get
            {
                return this._BaseScreenMangerObject.GetType();
            }
        }

        public static ScreenManager CreateFromScreenManagerObject(object obj)
        {
            ScreenManager toRet = new ScreenManager();
            toRet._BaseScreenMangerObject = obj;
            return toRet;
        }
        public static ScreenManager CreateFromTotalMinerAssembly(Assembly tmAsm)
        {
            ScreenManager toRet = new ScreenManager();
            Type tmGameType = tmAsm.GetType("StudioForge.TotalMiner.TotalMinerGame");
            FieldInfo instField = tmGameType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            object instValue = instField.GetValue(null);
            Type instType = instValue.GetType();
            Type gameWithScreenManager = instType.BaseType;
            PropertyInfo screenManagerProperty = gameWithScreenManager.GetProperty("ScreenManager", BindingFlags.Public | BindingFlags.Instance);
            object screenManagerValue = screenManagerProperty.GetValue(instValue, null);
            return ScreenManager.CreateFromScreenManagerObject(screenManagerValue);
        }

        public void AddScreen(StudioForge.Engine.GameState.GameScreen screen, Microsoft.Xna.Framework.PlayerIndex? index)
        {
            TM.Reflection.TMReflection.InvokeMethod(this._BaseScreenMangerObject, "AddScreen", BindingFlags.Public | BindingFlags.Instance, new object[] { screen, index });

        }
    }
}
