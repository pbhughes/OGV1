using System;
using System.IO;

namespace OpenGOVideo
{
    public static class Extensions
    {
        public static string ToPrettyTimeStamp(this TimeSpan duration)
        {
            return
                duration.Hours.ToString() + ":" +
                duration.Minutes.ToString().PadLeft(2, '0') + ":" +
                duration.Seconds.ToString().PadLeft(2, '0')
            ;
        }
        public static bool PathIsWriteable(string path)
        {
            string test = Path.GetFullPath(path) + ".writecheck";
            try
            {
                File.WriteAllText(test, "test");
            }
            catch (Exception ex)
            {
                return false;
            }
            File.Delete(test);
            return true;
        }
    }
}
