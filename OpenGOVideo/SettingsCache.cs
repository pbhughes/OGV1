using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace OpenGOVideo
{
    [Serializable()]
    public class SettingsCache
    {
        public SettingsCache()
        {
            IsDirty = false;
        }

        public string PreferedVideoDeviceName;
        public string PreferedAudioDeviceName;
        public int BitRate;
        public decimal FrameRate;
        public Size VideoSize;
        
        

        public DateTime LastAccess { set; get; }
        
        [XmlIgnore()]
        public bool IsDirty;


    }
}
