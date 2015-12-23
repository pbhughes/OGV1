using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGOVideo
{
    public class Timestamp
    {
        //public int id { get; private set; }
        //public string caption { get; set; }
        public long frame { get; set; }
        public TimeSpan position { get; set; }
    }
}
