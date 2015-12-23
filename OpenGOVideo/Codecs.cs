using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Profiles = Microsoft.Expression.Encoder.Profiles;

namespace OpenGOVideo
{
    public class Codec
    {
        public string Name;
        public Profiles.ProfileBase Profile;
        public int MaxBitrate;
        public override string ToString()
        {
            return this.Name;
        }
    }

    public static class Codecs
    {
        public static Codec[] Video = new Codec[]
        {
            /*new Codec()
            {
                Name = "VC-1 Advanced",
                Profile = new Profiles.AdvancedVC1VideoProfile(),
                MaxBitrate = 135000
            },*/
            new Codec()
            {
                Name = "VC-1 Main",
                Profile = new Profiles.MainVC1VideoProfile(),
                MaxBitrate = 20000
            }/*,
            new Codec()
            {
                Name = "VC-1 Simple",
                Profile = new Profiles.SimpleVC1VideoProfile(),
                MaxBitrate = 384
            }*/
        };
        public static Codec[] Audio = new Codec[]
        {
            new Codec()
            {
                Name = "WMA",
                Profile = new Profiles.WmaAudioProfile(),
                MaxBitrate = 320
            }
        };
    }
}
