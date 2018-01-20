using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMFMP.Network
{
    public enum CustomInternalPacket
    {
        PlayerUpdate,
        SessionUpdate
    }
    public enum SessionUpdateType
    {
        StateUpdate,
        SessionEnd
    }
    public enum PlayerUpdateType
    {
        Join,
        Leave
    }

    public enum CustomTMDataPacket
    {

    }
    public enum PacketType
    {
        Internal,
        TMData
    }
    public enum YesNo
    {
        Yes,
        No
    }
    public enum Master_Server_Op_Out
    {
        Connect
    }
    public enum Master_Server_Op_In
    {
        Connect
    }
    public enum Master_Server_ConnectionType
    {
        CreateSession,
        JoinSession,
        GetSessions
    }
}
