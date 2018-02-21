using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class TotalMinerGame
    {
        public object _InternalTotalMinerObject;
        public object InternalTotalMinerObject
        {
            get
            {
                return this._InternalTotalMinerObject;
            }
        }
        public static void AddNotification(Assembly tmAsm, string msg, bool ignoreGuide)
        {
            Type tmGameType = tmAsm.GetType("StudioForge.TotalMiner.TotalMinerGame");
            FieldInfo instField = tmGameType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            object instValue = instField.GetValue(null);
            Type instType = instValue.GetType();
            MethodInfo addNotificationMethod = instType.GetMethod("AddNotification", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(bool) }, null);
            addNotificationMethod.Invoke(instValue, new object[] { msg, ignoreGuide });
        }
        public static void ClearNotifications(Assembly tmAsm)
        {
            Type tmGameType = tmAsm.GetType("StudioForge.TotalMiner.TotalMinerGame");

            FieldInfo instField = tmGameType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            object instValue = instField.GetValue(null);

            FieldInfo notificationRendererField = instValue.GetType().GetField("notificationRenderer", BindingFlags.NonPublic | BindingFlags.Instance);
            object notificationRendererObject = notificationRendererField.GetValue(instValue);
            throw new NotImplementedException();
        }
    }
}
