using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGOVideo
{
    public enum RecordingStatus
    {
        Connecting,
        Ready,
        AuthRequired,
        Unknown,
        Recording,
        Stopped,
        NotReady,
        FileOnly,
        StreamOnly/*,
        ReadyAndConnected*/
    }
}
