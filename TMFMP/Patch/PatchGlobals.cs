using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Harmony;
namespace TMFMP.Patch
{
    public static class PatchGlobals
    {
        public static HarmonyInstance HarmonyInst;
        public static void PatchGame()
        {
            //PatchGlobals.HarmonyInst = HarmonyInstance.Create("pik.totalminer.tmfmp");
           // PatchGlobals.HarmonyInst.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
