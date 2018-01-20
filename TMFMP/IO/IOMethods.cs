using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace TMFMP.IO
{
    public static class IOMethods
    {
        public static byte[] GetStreamData(this Stream target)
        {
            target.Position = 0;
            byte[] _data = new byte[(int)target.Length];
            target.Read(_data, 0, _data.Length);
            return _data;
        }
    }
}
