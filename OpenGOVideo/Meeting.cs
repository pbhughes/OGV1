using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Runtime.InteropServices;
using Microsoft.Expression.Encoder.Live;

namespace OpenGOVideo
{
    public class Meeting
    {
        // Implements the Singleton design pattern
        private static Meeting current;
        public static Meeting Current
        {
            get
            {
                if (current == null)
                    current = new Meeting();
                return current;
            }
        }

        // Encoder job
        public LiveJob Job = new LiveJob();

        // Info about PublishPoint and FTP and stuff
        public event EventHandler onOrgChanged;
        private Organization _org = new Organization();
        public Organization Org
        {
            get
            {
                return this._org;
            }
            set
            {
                this._org = value;
                this.onOrgChanged(this, null);
            }
        }

        // Job Status
        //public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event EventHandler onStatusChanged;
        private RecordingStatus _status = RecordingStatus.NotReady;
        public RecordingStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
                this.onStatusChanged(this, null);
            }
        }
        
        // The last error encountered
        public Exception LastError { get; private set; }

        // List of AgendaItems, which may or may not have a Timestamp associated with them
        private BindingList<AgendaItem> _agenda = new BindingList<AgendaItem>();
        public BindingList<AgendaItem> Agenda
        {
            get
            {
                return this._agenda;
            }
            set
            {
                this._agenda = value;
                this.onAgendaChanged(this, null);
            }
        }
        public BindingList<AgendaItem> Agendatemp = new BindingList<AgendaItem>();
        public bool AgendaModified { get; private set; }
        public event EventHandler onAgendaChanged;
        
        // Used for binding AgendaItems to controls
        public BindingSource AgendaBindingSource = new BindingSource();
        
        // Agenda as a tree
        public TreeNode[] AgendaTree;

        //Used to Add Node
        public TreeNode newnode;
        public TreeNode parentnode;

        //used for edit window
        public Boolean change = false;
        public Boolean editwindow = false;
        public Point editwindowlocation;

        // Filename of video recording
        public string VideoFilename
        {
            get
            {
                return Job.OutputFileName;
            }
            set
            {
                Job.OutputFileName = value;
            }
        }
        public bool FileExists
        {
            get
            {
                FileInfo fi = new FileInfo(this.VideoFilename);
                if (!fi.Exists)
                    return false;
                return (fi.Length > 0);
            }
        }
        public string MeetingFilename;

        private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"OpenGovVideo");
        public event EventHandler onFilePathChanged;
        public string FilePath
        {
            get
            {
                return OpenGovStatics.FilePath();
            }
            
        }

        // Used for recursive stuff
        public int lastGivenID = 0;

      

        private string NewFileName(string Ext)
        {
           return this.Org.GetVideoFileName(Ext);
            
        }

        private string NewTimeStamp
        {
            get
            {
                return DateTime.Now.ToFileTime().ToString();
            }
        }

        //public bool LoadFromXML(string filename)
        //{
        //    try
        //    {
        //        XDocument xmldoc = XDocument.Load(filename);

        //        // Load the agenda items and build the tree
        //        this.Agenda.Clear();
        //        this.lastGivenID = 0;
                
        //        this.AgendaTree = this.LoadAgendaItems(xmldoc.XPathSelectElement("/meeting/agenda/items"), 0);
        //        this.AgendaModified = false;

        //        try
        //        {
        //            if (!File.Exists(this.VideoFilename = xmldoc.XPathSelectElement("/meeting/filename").Value))
        //                throw new ApplicationException("Unable to locate video file.");
        //        }
        //        catch (Exception ex) {
        //            this.LastError = ex;
        //            this.VideoFilename = Path.Combine(this.FilePath, this.NewFileName);
        //        }

        //        this.MeetingFilename = filename;

        //        this.lastGivenID = 0;

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        this.LastError = ex;
        //        return false;
        //    }
        //}
        public bool LoadFromXML(string XmlText, string FileName)
        {
            try
            {
                XDocument xmldoc = XDocument.Parse(XmlText);

                // Load the agenda items and build the tree
                this.Agenda.Clear();
                this.lastGivenID = 0;

                this.AgendaTree = this.LoadAgendaItems(xmldoc.XPathSelectElement("/meeting/agenda/items"), 0);
                this.AgendaModified = false;

               

                return SetMeetingFileNames(xmldoc, FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.LastError = ex;
                return false;
            }
        }

        
        public bool LoadFromFile(string FileName)
        {
            try
            {
                XDocument xmldoc = XDocument.Load(FileName);

                // Load the agenda items and build the tree
                this.Agenda.Clear();
                this.lastGivenID = 0;

                this.AgendaTree = this.LoadAgendaItems(xmldoc.XPathSelectElement("/meeting/agenda/items"), 0);
                this.AgendaModified = false;

                MeetingFilename = FileName;

                string targetVideoFile = xmldoc.XPathSelectElement("meeting/filename").Value;
                targetVideoFile = Path.Combine(FilePath, targetVideoFile) + ".wmv";

                if (File.Exists(targetVideoFile))
                {
                    if (MessageBox.Show("This agenda file has an associated video.  Do you want to edit it? Click Yes.", "OpenGOVideo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        VideoFilename = targetVideoFile;
                        return true;
                    }
                }

                SetMeetingFileNames(xmldoc);

                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.LastError = ex;
                return false;
            }
        }


        private bool SetMeetingFileNames(XDocument xmldoc)
        {
            string targetVideoFile = xmldoc.XPathSelectElement("meeting/filename").Value;
            targetVideoFile = Path.Combine(FilePath, targetVideoFile) + ".wmv";

            if (File.Exists(targetVideoFile))
            {
                

                if (MessageBox.Show("Video file already exists for this meeting.  Click OK to overwrite or CANCEL to pick a new file name.", "OpenGOVideo",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    VideoFilename = targetVideoFile;
                }
                else
                {
                    //pick a new file name
                    SaveFileDialog sav = new SaveFileDialog();
                    sav.InitialDirectory = FilePath;
                    sav.FileName = "";
                    sav.AddExtension = true;
                    sav.Filter = "Windows Media Files(*.wmv)|*.wmv";
                    sav.ValidateNames = true;
                    if (sav.ShowDialog() == DialogResult.OK)
                    {
                        VideoFilename = sav.FileName;
                        return true;
                    }
                    else
                        ;
                }


            }

            VideoFilename = targetVideoFile;
            return true;
        }
        private bool SetMeetingFileNames(XDocument xmldoc, string OgaFileName)
        {
            string targetVideoFile = xmldoc.XPathSelectElement("meeting/filename").Value;
            targetVideoFile = Path.Combine(FilePath, targetVideoFile);

            MeetingFilename = Path.Combine(FilePath, OgaFileName);

            if (File.Exists(MeetingFilename))
            {
                if (MessageBox.Show("Agenda file already exists for this meeting.  Click OK to overwrite or CANCEL to pick a new file name.", "OpenGOVideo",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ;//
                }
                else
                {
                    //pick a new file name
                    SaveFileDialog sav = new SaveFileDialog();
                    sav.InitialDirectory = FilePath;
                    sav.FileName = "";
                    sav.Filter = "Agenda Files(*.oga)|*.oga";
                    sav.AddExtension = true;
                    sav.ValidateNames = true;
                    if (sav.ShowDialog() == DialogResult.OK)
                    {
                        MeetingFilename = sav.FileName;
                        SaveToXML(MeetingFilename);
                        
                    }
                    else
                        ;
                }


            }
            else
            {
                SaveToXML(MeetingFilename);
            }

            if (File.Exists(targetVideoFile))
            {
                if (MessageBox.Show("Video file already exists for this meeting.  Click OK to overwrite or CANCEL to pick a new file name.", "OpenGOVideo",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    VideoFilename = targetVideoFile;
                }
                else
                {
                    //pick a new file name
                    SaveFileDialog sav = new SaveFileDialog();
                    sav.InitialDirectory = FilePath;
                    sav.FileName = "";
                    sav.ValidateNames = true;
                    sav.Filter = "Windows Media Files (*.wmv)|*.wmv";
                    sav.AddExtension = true;
                    if (sav.ShowDialog() == DialogResult.OK)
                    {
                        VideoFilename = sav.FileName;
                        return true;
                    }
                    else
                        ;
                }


            }
           
            VideoFilename = targetVideoFile;
            return true;
        }

        // Returns TreeNode array as needed by the tree thingy
        private TreeNode[] LoadAgendaItems(XElement rootNode, int parentID)
        {
            List<TreeNode> tmpTree = new List<TreeNode>();

            foreach (XElement item in rootNode.Elements("item"))
            {
                AgendaItem tmpAI = new AgendaItem
                {
                    id = ++this.lastGivenID,
                    parent_id = parentID,
                    title = item.Element("title").Value,
                    desc = (item.Element("desc")==null) ? "" : item.Element("desc").Value,
                    timestamp = null
                };

                // Add this item to the tree
                TreeNode tn = new TreeNode
                {
                    Tag = this.lastGivenID,
                    Text = item.Element("title").Value,
                    ToolTipText = tmpAI.desc,
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Name = lastGivenID.ToString()
                };

                // Does it have a timestamp?
                try
                {
                    XElement tsxml = item.Element("timestamp");
                    Timestamp ts = new Timestamp
                    {
                        frame = Convert.ToInt64(item.Element("timestamp").Element("frame").Value),
                        position = new TimeSpan(0,0,(int)Convert.ToDouble(item.Element("timestamp").Element("position").Value))
                    };
                    tmpAI.timestamp = ts;
                    tn.ForeColor = Properties.Settings.Default.TimeFcolor;
                    tn.BackColor = Properties.Settings.Default.Timebcolor;
                    tn.Text = "[" + ts.position.ToPrettyTimeStamp() + "]  " + tn.Text;
                }
                catch {}

                // Add to agenda
                this.Agenda.Add(tmpAI);

                // If it has children, process those next
                if (item.Element("items") != null)
                {
                    tn.Nodes.AddRange(this.LoadAgendaItems(item.Element("items"), lastGivenID));
                }

                tmpTree.Add(tn);

            }

            return tmpTree.ToArray();
        }

        public TreeNode[] LoadTreeFromAgenda(AgendaItem[] agendaItems)
        {
            List<TreeNode> tmpTree = new List<TreeNode>();

            foreach (AgendaItem ai in agendaItems)
            {
                // Add this item to the tree
                TreeNode tn = new TreeNode
                {
                    Tag = ai.id,
                    Text = ai.title,
                    ToolTipText = ai.desc,
                    Name = ai.id.ToString()
                };

                // Is it timestamped?
                if (ai.timestamp != null)
                {
                    tn.Text = "[" + ai.timestamp.position.ToPrettyTimeStamp() + "]  " + ai.title;

                    tn.ForeColor = Color.White;
                    tn.BackColor = Color.Blue;
                }

                // If it has children, process those next
                try
                {
                    AgendaItem[] children = this.Agenda.Where(a => a.parent_id == ai.id).OrderBy(a => a.id).ToArray<AgendaItem>();
                    if (children != null)
                        tn.Nodes.AddRange(this.LoadTreeFromAgenda(children));

                }
                catch { }

                tmpTree.Add(tn);
            }
            
            return tmpTree.ToArray();
        }
        public bool SaveToXML(string path)
        {
            // TODO
            //throw new Exception("please don't use this method as its untested!!!");

            try
            {
                // Convert AgendaTree to XElement
                XElement AgendaItems = new XElement("agenda");
                AgendaItems.Add(new XElement("items",
                    this.GetAgendaAsXML(
                        this.Agenda.Where(a => a.parent_id == 0).ToList<AgendaItem>()
                    )
                ));

                // Create the final XML document
                XDocument xdoc = new XDocument(
                    new XElement(
                        "meeting",
                        new XElement(
                            "filename",
                            new XText(Path.GetFileNameWithoutExtension(this.VideoFilename))
                        ),
                        AgendaItems
                    )
                );
                xdoc.Save(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool SaveToXML()
        {
            // TODO
            //throw new Exception("please don't use this method as its untested!!!");

            try
            {
                // Convert AgendaTree to XElement
                XElement AgendaItems = new XElement("agenda");
                AgendaItems.Add(new XElement("items",
                    this.GetAgendaAsXML(
                        this.Agenda.Where(a => a.parent_id == 0).ToList<AgendaItem>()
                    )
                ));

                // Create the final XML document
                XDocument xdoc = new XDocument(
                    new XElement(
                        "meeting",
                        new XElement(
                            "filename",
                            new XText(Path.GetFileNameWithoutExtension(this.VideoFilename))
                        ),
                        AgendaItems
                    )
                );
                xdoc.Save(Path.Combine(FilePath,MeetingFilename));
                this.AgendaModified = false;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string GetAgendaXML()
        {
            // TODO
            //throw new Exception("please don't use this method as its untested!!!");

          
                // Convert AgendaTree to XElement
                XElement AgendaItems = new XElement("agenda");
                AgendaItems.Add(new XElement("items",
                    this.GetAgendaAsXML(
                        this.Agenda.Where(a => a.parent_id == 0).ToList<AgendaItem>()
                    )
                ));

                // Create the final XML document
                XDocument xdoc = new XDocument(
                    new XElement(
                        "meeting",
                        new XElement(
                            "filename",
                            new XText(Path.GetFileNameWithoutExtension(this.VideoFilename))
                        ),
                        AgendaItems
                    )
                );

                return xdoc.ToString();
            
        }

        private XElement[] GetAgendaAsXML(List<AgendaItem> agendaItems)
        {
            // TODO
            //throw new Exception("please don't use this method as its untested!!!");

            List<XElement> xels = new List<XElement>();
            foreach (AgendaItem ai in agendaItems)
            {
                XElement tmp = new XElement("item");

                // Add the title, or skip if blank
                if(!String.IsNullOrEmpty(ai.title))
                    tmp.Add(new XElement("title", new XText(ai.title)));
                else
                    continue;

                // Add the description if exists
                if(!String.IsNullOrEmpty(ai.desc))
                    tmp.Add(new XElement("desc", new XText(ai.desc)));

                // Add the timestamp if exists
                if (ai.timestamp != null)
                {
                    XElement tmpTs = new XElement("timestamp");
                    if (ai.timestamp.frame != 0)
                        tmpTs.Add(new XElement("frame", new XText(ai.timestamp.frame.ToString())));
                    if (ai.timestamp.position != null)
                        tmpTs.Add(new XElement("position", new XText(ai.timestamp.position.TotalSeconds.ToString())));
                    tmp.Add(tmpTs);
                }

                if (this.Agenda.Count(a => a.parent_id == ai.id) > 0)
                {
                    tmp.Add(
                        new XElement("items", GetAgendaAsXML(this.Agenda.Where(a => a.parent_id==ai.id).ToList<AgendaItem>()))
                    );
                }
                xels.Add(tmp);
            }

            return xels.ToArray();
        }




        internal void AgendaChild(TreeNode cur)
        {
            int ai_id = (int)cur.Tag;

            int par = (cur.Parent== null) ? 0 : (int)cur.Parent.Tag;

            AgendaItem tmpAI = new AgendaItem
            {
                id = (int)cur.Tag,
                parent_id = par,
                title = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().title,
                desc = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().desc,
                timestamp = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().timestamp
            };

            Agendatemp.Add(tmpAI);
            
            foreach (TreeNode child in cur.Nodes)
                AgendaChild(child);

        }
        private void Meeting_onAgendaChanged(object sender, EventArgs e)
        {
            this.AgendaModified = true;
        }
    }
}