using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMFMP.Network
{
    public struct Packet
    {
        public short Sender;
        public short Target;
        public byte[] Data;
    }
}
