using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace OpenGOVideo
{

    public delegate void SettingsChangedEvent();


    public class CacheManager
    {

        public const string SETTINGFILENAME = "settings_cache.xml";
        public static event SettingsChangedEvent SettingChange;

     
  

        public static void CacheSettings<T>(T CachedData)
        {
            XmlSerializer xser = new XmlSerializer(CachedData.GetType());
            string filePath = Path.Combine(OpenGovStatics.AppDataPath(), SETTINGFILENAME);
            MemoryStream ms = new MemoryStream();
            xser.Serialize(ms, CachedData);
            ms.Flush();
            string serializeData = ASCIIEncoding.ASCII.GetString(ms.GetBuffer());
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(serializeData);
            xdoc.Save(filePath);

            OnSettingChange();
        }

        public static T ReadSettings<T>() where T : class
        {
            FileStream fin = null;
            try
            {
                string filePath = Path.Combine(OpenGovStatics.AppDataPath(), SETTINGFILENAME);
                if (!File.Exists(filePath))
                    return null;

                fin = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fin);
                string objectCode = sr.ReadToEnd();

                object intermediate = DeserializeObject(objectCode, typeof(T));
                return (T)intermediate;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(fin != null)
                    fin.Close();
            }

            
        }

        private static object DeserializeObject(string ObjectData, Type T)
        {
            XmlSerializer xser = new XmlSerializer(T);
            StringReader sr = new StringReader(ObjectData);
            object obj = xser.Deserialize(sr);
            return obj;
        }

        public static void OnSettingChange()
        {
            if (SettingChange != null)
                SettingChange();

        }

            

    }
}
