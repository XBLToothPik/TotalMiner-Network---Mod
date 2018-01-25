using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner.API;
using StudioForge.Engine.Core;
using StudioForge.Engine.Game;
using StudioForge.Engine.GameState;
using StudioForge.Engine.GamerServices;
using System.IO;
using TM.Reflection;
using TMFMP.TMInternal;
using TMFMP;
using TMFMP.Network;
using StudioForge.BlockWorld;
using StudioForge.Engine.Net;

public static class Globals
{
    public static object LockSemaphore = new object();
    public static ITMGame Game;
    public static ITMPlugin MainPlugin;
    public static ITMPluginManager PluginManager;
    public static ITMPlayer MyPlayer;

    public static string Test_LocalPName;
    public static void LoadLocalData()
    {
        string ap = AppUtils.GetAppPath();
        string loc = ap + "//" + "localdata.txt";
        if (File.Exists(loc))
        {
            string newName = string.Empty;
            using (Stream xIn = File.Open(loc, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(xIn))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                            continue;
                        if (line.StartsWith("name:"))
                        {
                            newName = line.Substring(5);
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(newName))
            {
                StudioForge.Engine.Services.ExceptionReporter.ReportExceptionCaught(-32, new Exception("Invalid localdata.txt.  Contact XBLToothPik"));
            }
            else
                Globals.Test_LocalPName = newName;
        }
        else
            StudioForge.Engine.Services.ExceptionReporter.ReportExceptionCaught(-32, new Exception("localdata.txt Not Found.  Contact XBLToothPik"));
    }

    public static string InitPath;

    public static TMFMP.TMInternal.NpcManager InternalNPCManager;
    public static TMFMP.TMInternal.GameInstance InternalGameInstance;

    public static void Log(string data)
    {
        //lock (Globals.LockSemaphore)
        //{
        //    if (!File.Exists("logger.txt"))
        //        File.Create("logger.txt").Close();
        //    File.AppendAllLines("logger.txt", new string[] { data });
        //}
    }

}

