using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

public static class AppUtils
{
    public static string GetAppPath()
    {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}

