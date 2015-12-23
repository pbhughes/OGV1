using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGOVideo
{
    class FrameRate
    {
        public string Name
        {
            get
            {
                return this.Value + " fps";
            }
        }
        public decimal Value;
        public FrameRate(decimal val)
        {
            this.Value = val;
        }
    }
}
