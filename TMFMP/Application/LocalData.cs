using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public static class LocalData
{
    private static string FullFilePath;
    public static string PlayerName;
    public static string ServerIP;
    public static short ServerPort;

    public static void Init()
    {
        LocalData.FullFilePath = AppUtils.GetAppPath() + "//" + "localdata.txt";
        PlayerName = string.Empty;
        ServerIP = string.Empty;
        ServerPort = 0;
    }

    public static void LoadOrCreate()
    {
        string name = string.Empty;
        string ip = string.Empty;
        short port = 0;

        if (File.Exists(LocalData.FullFilePath))
        {
            using (Stream xIn = File.Open(LocalData.FullFilePath, FileMode.Open))
            {
                LoadSettings(xIn, out name, out ip, out port);
            }
        }
        else
        {
            CreateNew();
        }

        LocalData.PlayerName = name;
        LocalData.ServerIP = ip;
        LocalData.ServerPort = port;
    }

    public static void Save()
    {
        if (File.Exists(LocalData.FullFilePath))
        {
            using (Stream xOut = File.Open(LocalData.FullFilePath, FileMode.Open, FileAccess.Write, FileShare.Read))
            {
                using (StreamWriter writer = new StreamWriter(xOut))
                {
                    writer.WriteLine("# TMFMP by XBLToothPik");
                    writer.WriteLine("# These are comment lines");
                    writer.WriteLine("# Local Data File");
                    writer.WriteLine("# Do not modify this file");
                    writer.WriteLine();
                    writer.WriteLine("NAME:" + LocalData.PlayerName ?? "");
                    writer.WriteLine("IP:" + LocalData.ServerIP ?? "");
                    writer.WriteLine("PORT:" + LocalData.ServerPort);
                    writer.Flush();
                }
            }
        }
        else
        {
            CreateNew();
        }
    }
    private static void LoadSettings(Stream xIn, out string name, out string ip, out short port)
    {
        name = string.Empty;
        ip = string.Empty;
        port = 0;
        using (StreamReader reader = new StreamReader(xIn))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

          
                if (line.StartsWith("NAME:", StringComparison.InvariantCultureIgnoreCase))
                    name = line.Substring(5).Trim();
                if (line.StartsWith("IP:", StringComparison.InvariantCultureIgnoreCase))
                    ip = line.Substring(3).Trim();
                 if (line.StartsWith("PORT:"))
                    short.TryParse(line.Substring(5).Trim(), out port);

            }
        }
    }
    private static void CreateNew()
    {
        using (Stream defaultFile = AppUtils.GetResourceAsStream("defaultsettings.txt"))
        {
            using (Stream xOut = File.Create(LocalData.FullFilePath))
            {
                defaultFile.CopyTo(xOut);
            }
        }
    }
}

