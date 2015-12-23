using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Text;
using System.Xml;
using System.Data;
using System.Diagnostics;


namespace AgendaServices
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/",Description="Provides methods to store agenda files and provides the shell for a new agenda document.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StorageService : System.Web.Services.WebService
    {

        EventLog _Log = new EventLog("Application",".","Application");
        public AuthenTokenHeader AuthenticToken;



        [WebMethod(Description="Saves an agenda (OGA) file. Payload is the string contents of the OGA file, File Name is the name given to the file when on disk")]
        [AuthentExtendedAttribute]
        [SoapHeader("AuthenticToken",Direction=SoapHeaderDirection.In,Required=true)]
        public bool SaveAgendaFile(string Payload, string FileName)
        {
            try
            {
                string pathToFolder = Server.MapPath(AuthenticToken.Path);
                if(!Directory.Exists(pathToFolder))
                    throw new FileNotFoundException("Storage repository not configured.");

                if (File.Exists(Path.Combine(pathToFolder,FileName)))
                    throw new InvalidOperationException(string.Format("File {0} already exists", FileName));

                if (FileName.Substring(FileName.Length - 3, 3).ToLower() != "oga")
                    throw new InvalidOperationException("File extension must be .OGA");

                FileStream fout = new FileStream(Path.Combine(pathToFolder,FileName),FileMode.Create, FileAccess.Write);
                TextWriter tw = new StreamWriter(fout);
                tw.Write(Payload);
                tw.Close();
                return true;
                
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            finally
            {
                ;
            }
           


        }


        [WebMethod(Description = "Gets a blank agenda shell from the file system as opposed to the database.")]
        public string GetAgendaFileFromWebServer(string City, String State, string Board, string FileName)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                string retval;
                string mapPath;
                mapPath = GetPathToBoardFolder(City, State, Board);
                mapPath = Path.Combine(mapPath, FileName);
                xdoc.Load(mapPath);
                retval = xdoc.OuterXml.ToString();
                return retval;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            finally
            {
                xdoc = null;
            }
        }

        private string GetPathToBoardFolder(string City, String State, string Board)
        {
            string retval = string.Empty;
            retval = Server.MapPath(@"AgendaFiles");
            retval = Path.Combine(retval, State);
            retval = Path.Combine(retval, City);
            retval = Path.Combine(retval, "Boards");
            retval = Path.Combine(retval, Board);
            return retval;
        }

        [WebMethod(Description = "Get a list of all available agenda files per organization.")]
        public List<OPGOV.AgendaServices.Objects.AgendaFile> GetAvailableAgendaFiles(string City, String State, string Board)
        {
            List<OPGOV.AgendaServices.Objects.AgendaFile> fileNames = new List<OPGOV.AgendaServices.Objects.AgendaFile>();
            string mapPath;
            mapPath = GetPathToBoardFolder(City,State,Board);
            string[] load = Directory.GetFiles(mapPath,"*.oga");
            IComparer fileDateCompare = new OPGOV.AgendaServices.CompareByFileDate();
            Array.Sort(load, fileDateCompare);
            
            foreach (string s in load)
            {
                string dte = File.GetCreationTime(s).ToString();
                string name = Path.GetFileName(s);
                OPGOV.AgendaServices.Objects.AgendaFile file = new OPGOV.AgendaServices.Objects.AgendaFile();
                file.Date = dte;
                file.FileName = name;
                fileNames.Add(file);
                
            }

            return fileNames;

        }

        [WebMethod(Description = "Get a list of all available agenda files by using the meeting date from inside the file.")]
        public List<OPGOV.AgendaServices.Objects.AgendaFile> GetAvailableAgendaFilesAndDates(string City, String State, string Board)
        {
            List<OPGOV.AgendaServices.Objects.AgendaFile> fileNames = new List<OPGOV.AgendaServices.Objects.AgendaFile>();
            string mapPath;
            mapPath = GetPathToBoardFolder(City, State, Board);
            string[] load = Directory.GetFiles(mapPath, "*.oga");
            IComparer fileDateCompare = new OPGOV.AgendaServices.CompareByFileDate();
            Array.Sort(load, fileDateCompare);

            foreach (string s in load)
            {
                string dte = string.Empty;
                string name = Path.GetFileName(s);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(Path.Combine(mapPath,s));
                XmlNode meetingDate = xdoc.SelectSingleNode("meeting/meetingdate");
                if (meetingDate == null)
                    dte = File.GetLastWriteTime(s).ToShortDateString();
                else
                    dte = meetingDate.InnerText.ToString();


                OPGOV.AgendaServices.Objects.AgendaFile file = new OPGOV.AgendaServices.Objects.AgendaFile();
                file.Date = dte;
                file.FileName = name;
                fileNames.Add(file);

            }

            return fileNames;

        }

        private void LogException(Exception ex)
        {
            string message = string.Format("Message: {0}", ex.Message);
            _Log.WriteEntry(message, EventLogEntryType.Error,1);
        }

        private bool Authenticate(string UserID, string Password)
        {
            if (UserID == "TestUser")
                if (Password == "mep-005a")
                    return true;
            return false;
        }

        
    }
}
