using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace OpenGOVideo
{
    public class Organization
    {
        public string Guid = new System.Guid().ToString();
        public string Name = "Unknown Organization";
        public string State = "";
        public string City = "";
        public string Board = "";
        public int offset = 2;
        public Livestream LiveStream;
        public FTPserver FTPServer;

     
        public override string ToString()
        {
            return this.Name;
        }

        public bool IsConfigured
        {
            get
            {
                return (LiveStream == null || FTPServer == null) ? false : true;
            }
        }

        public Organization() { }

        public Organization(string xml) : this(XElement.Parse(xml)) { }

        public string  DefaultPath { get; set; }

        public Organization(XElement org)
        {
            if (org.Elements().Where(o => o.Name == "guid").Count() == 1)
                this.Guid = org.Element("guid").Value;

            this.Name = org.Element("name").Value;
            this.State = org.Element("state").Value;
            this.City = org.Element("city").Value;
            this.Board = org.Element("board").Value;
            this.DefaultPath = org.Element("filelocation").Value;
            this.offset = int.Parse(org.Element("offset").Value);
            this.LiveStream = new Livestream
            {
                PublishPoint = new Uri(org.Element("livestream").Element("publishpoint").Value),
                Username = org.Element("livestream").Element("username").Value,
                Password = org.Element("livestream").Element("password").Value
            };
            this.FTPServer = new FTPserver
            {
                Host = org.Element("ftpserver").Element("host").Value,
                Dir = org.Element("ftpserver").Element("dir").Value,
                Username = org.Element("ftpserver").Element("username").Value,
                Password = org.Element("ftpserver").Element("password").Value
            };
        }

        public string GetVideoFileName(string Ext)
        {
            //RI_Saunderstown_CityCouncil_20100107_2130.wmv
            string tmp;

            if((Meeting.Current.VideoFilename != string.Empty) && (Meeting.Current.VideoFilename != null))
                return Meeting.Current.VideoFilename;

            if (Meeting.Current.Agenda.Count > 0)
            {
                //agenda file is loaded file name should be set
                return Meeting.Current.VideoFilename;
            }
            else
            {
                //agenda file is not loaded we have to ask for a file name
                return "video_1" + Ext;
            }
            
        }

        public string ToXML()
        {
            if (this.Guid == null)
                this.Guid = new Guid().ToString();
            return new XElement("org",
                new XElement("guid", this.Guid),
                new XElement("name", this.Name),
                new XElement("state", this.State),
                new XElement("town", this.City),
                new XElement("department", this.Board),
                new XElement("filelocation", this.DefaultPath),
                new XElement("offset",this.offset.ToString()),
                new XElement("livestream",
                    new XElement("publishpoint", this.LiveStream.PublishPoint.ToString()),
                    new XElement("username", this.LiveStream.Username),
                    new XElement("password", this.LiveStream.Password)
                ),
                new XElement("ftpserver",
                    new XElement("host", this.FTPServer.Host),
                    new XElement("dir", this.FTPServer.Dir),
                    new XElement("username", this.FTPServer.Username),
                    new XElement("password", this.FTPServer.Password)
                )
            ).ToString();
        }
    }

    public class Livestream
    {
        public Uri PublishPoint;
        public string Username;
        public string Password;
    }

    public class FTPserver
    {
        public string Host;
        public string Dir;
        public string Username;
        public string Password;
    }
}
