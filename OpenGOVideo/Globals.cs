using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenGOVideo
{
    class OpenGovStatics
    {
        public static string FilePath()
        {
            string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OpenGOVideo");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            return _path;
        }

        public static string AppDataPath()
        {
            string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"OpenGOVideo");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            return _path;
        }

    }
}
