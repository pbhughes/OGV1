using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Expression.Encoder.Live;
using Profiles = Microsoft.Expression.Encoder.Profiles;
using Encoder = Microsoft.Expression.Encoder;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace OpenGOVideo
{
    public partial class Main : Form
    {
        public delegate void delStatuschanged(string message);
        public delegate void delTimerTick(object state);
        public delegate void delAgendaAction();
        public delegate void delStopJob();
        public delegate void delJobStatus(object sender, EncodeStatusEventArgs e);

        #region Events

        public static event delStatuschanged StatusChanged;

        #endregion

        #region WIN API references
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion


        #region Member Variables and Fields



        private Thread th;
        private TreeNode sourceNode;
        private DateTime?  dtBefore;
        private const string WindowTitle = "OpenGOVideo (Alpha 1.1)";
        private SettingsCache settings;
        private string _lastHash = null;
        private const int WM_LBUTTONDBLCLK = 201;
        TimerCallback timerDelegate;
        private System.Threading.Timer tmrTime1;

        



        private ManualResetEvent WAITFORCONNECTION;

        #endregion


        #region Functions and Methods

        public Main()
        {
            //this.Hide();
            //th = new Thread(new ThreadStart(ShowSplash));
            //th.Start();
            //Thread.Sleep(300);
            InitializeComponent();
            

        }
        void CacheManager_SettingChange()
        {
            try
            {
                settings = CacheManager.ReadSettings<SettingsCache>();
                
                SetEncoderOptions();
                SetupDeviceSource(null);
                StartPreview();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        void job_Status(object sender, EncodeStatusEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    delJobStatus x = new delJobStatus( job_Status);
                    object[] parameters = {sender,e};
                    this.BeginInvoke(x,parameters);
                }
                else
                {
                    switch (e.Status)
                    {
                        case EncodeStatus.EncodingError:
                            MessageBox.Show("An error has occurred - recording has been stopped.  " + e.Message);
                            this.StopJob();
                            SetBothFileAndVideoStatus(Color.Red);
                            NotifyStatusChange("An error has occured.");
                            break;
                        case EncodeStatus.PipelineRebuildStarted:
                            Meeting.Current.Status = RecordingStatus.NotReady;
                            SetBothFileAndVideoStatus(Color.Red);
                            NotifyStatusChange("Not Ready.");
                            break;
                        case EncodeStatus.PipelineRebuildFinished:
                            Meeting.Current.Status = RecordingStatus.FileOnly;
                            SetVidoeStatus(Color.Red);
                            SetFileStatus(Color.Green);
                            NotifyStatusChange("File recording only.");
                            break;
                        case EncodeStatus.PublishingPointConnecting:
                            Meeting.Current.Status = RecordingStatus.Connecting;
                            SetVidoeStatus(Color.Yellow);
                            NotifyStatusChange("Connecing to Publishing Point.");
                            break;
                        case EncodeStatus.PublishingPointOpened:
                            Meeting.Current.Status = RecordingStatus.Ready;
                            WAITFORNEWFILEHANDLE.Set();
                            SetVidoeStatus(Color.Green);
                            NotifyStatusChange("Video Streaming Ready.");
                            break;
                        case EncodeStatus.PublishingPointError:
                            if (this.chkstream.InvokeRequired)
                            {
                                //MessageBox.Show("oops its running on a different thread.");
                                //this.Invoke(
                            }
                            Meeting.Current.Status = RecordingStatus.NotReady;
                            SetBothFileAndVideoStatus(Color.Red);
                            chkstream.Checked = false;
                            NotifyStatusChange("Publishing Point Error.");
                            try
                            {
                                WAITFORNEWFILEHANDLE.Set();
                                //StopJob();
                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message;
                            }
                            break;
                    }
                }
            }

            catch (Exception ex)
            {

                int x = 0;
            }
       
            
        }

        private static void NotifyStatusChange(string message)
        {
            if (StatusChanged != null)
                StatusChanged(message);
        }


        void SetBothFileAndVideoStatus(Color color)
        {
            SetVidoeStatus(color);
            SetFileStatus(color);
        }

        private delegate void DelSetColor(Color color);

        void SetVidoeStatus(Color color)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelSetColor( SetVidoeStatus ),color);
            }
            else
            {

                cmdVideoStatus.BackColor = color;
                switch (color.Name)
                {
                    case "Green":
                        this.cmdVideoStatus.Text = "Video Ready";
                        break;
                    case "Red":
                        cmdVideoStatus.Text = "Video Not Ready";
                        break;
                    case "Yellow":
                        cmdVideoStatus.Text = "Video Pending";
                        break;
                }
            }

        }

        void SetFileStatus(Color color)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelSetColor(SetVidoeStatus), color);

            }
            else
            {
                this.cmdFileStreaming.BackColor = color;
                switch (color.Name)
                {
                    case "Green":
                        cmdFileStreaming.Text = "File Ready";
                        break;
                    case "Red":
                        cmdFileStreaming.Text = "File Not Ready";
                        break;
                    case "Yellow":
                        cmdFileStreaming.Text = "File Pending";
                        break;
                }
            }
        }

        void job_AcquireCredentials(object sender, AcquireCredentialsEventArgs e)
        {
            Meeting.Current.Status = RecordingStatus.AuthRequired;
            MessageBox.Show("Failed to authorize with publishing point. Double-check your credentials and try again.");
            Console.WriteLine("Publish point auth failed");
        }
        // Copy all children and add them to a new node
        // TODO: Maybe promote this to an extension method?
        private TreeNode children(TreeNode parent)
        {
            TreeNode node = new TreeNode(parent.Text, parent.ImageIndex, parent.SelectedImageIndex)
            {
                ForeColor = parent.ForeColor,
                BackColor = parent.BackColor,
                ToolTipText = sourceNode.ToolTipText,
                Name = parent.Name,
                Tag = parent.Tag
            };

            foreach (TreeNode child in parent.Nodes)
                node.Nodes.Add(children(child));

            return node;
        }
        //used to convert the current tree strut to agenda list to save
        public void AgendaFromTree()
        {
            Meeting.Current.Agendatemp = new BindingList<AgendaItem>();
            foreach (TreeNode child in treeview.Nodes)
                Meeting.Current.AgendaChild(child);

            // treeview.Nodes.Clear();
            Meeting.Current.Agenda = Meeting.Current.Agendatemp;
            Meeting.Current.Agendatemp = new BindingList<AgendaItem>();
            //this.treeview.Nodes.AddRange(Meeting.Current.LoadTreeFromAgenda(Meeting.Current.Agenda.Where(a => a.parent_id == 0).ToArray<AgendaItem>()));
            // if (treeview.Nodes.Count > 0)
            // {
            treeview.ExpandAll();
            //     treeview.SelectedNode = treeview.Nodes[0];
            //}
        }
        private void Form_exit(object sender, EventArgs e)
        {
            //AppQuit();
        }
        private void ApplyTimeStamp()
        {
            if (dtBefore != null)
            {
                // Create the timestamp now
                // Retrieve a WMEncStatistics object.
                /*
                IWMEncStatistics Stats =Meeting.Current.Encoder.Statistics;
                IWMEncFileArchiveStats FileStats = (IWMEncFileArchiveStats)Stats.FileArchiveStats;
                */


                Timestamp ts = new Timestamp();

                TimeSpan position = (DateTime.Now - (DateTime)dtBefore);
                ts.frame = (long)((double)position.TotalSeconds * 29.97);
                ts.position = position;



                /*// Don't do anything if its not capturing
                //if (!capture.Capturing || treeview.SelectedNode == null)
                {
                    MessageBox.Show("You can only timestamp a Item when the video is runnning");
                    treeview.ExpandAll();
                    treeview.SelectedNode = treeview.Nodes[0];
                    return;
                }*/

                // Add timestamp to AgendaItem
                int ai_id = (int)treeview.SelectedNode.Tag;
                Meeting.Current.Agenda.Where(i => i.id == ai_id).First().timestamp = ts;
                string title = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().title;

                // Update the tree
                treeview.SelectedNode.Text = "[" + ts.position.ToPrettyTimeStamp() + "]  " + title;


                treeview.SelectedNode.ForeColor = Color.White;
                treeview.SelectedNode.BackColor = Color.Blue;


                // Update the button
                //btnAddTimestamp.Text = "Restamp selected item";

                // Select next item in the tree
                try
                {
                    TreeNode nextNode = treeview.Nodes.Find(
                        (Convert.ToInt32(treeview.SelectedNode.Name) + 1).ToString()
                    , true).First();

                    treeview.SelectedNode = treeview.Nodes.Find((ai_id + 1).ToString(), true).First();
                }
                catch (Exception ex)
                {
                    treeview.SelectedNode = null;
                }

                
            }
            else
            {
                MessageBox.Show("You can only timestamp a Item when the video is runnning");
                treeview.ExpandAll();
                treeview.SelectedNode = treeview.Nodes[0];
            }
        }
        private void videoon()
        {
            try
            {

                /*
                // Add an audio source and a video source.
                IWMEncSourceGroupCollection SrcGrpColl = Meeting.Current.Encoder.SourceGroupCollection;
                IWMEncSourceGroup SrcGrp = SrcGrpColl.Add("SG_1");
                 Meeting.Current.SrcAud = SrcGrp.AddSource(WMENC_SOURCE_TYPE.WMENC_AUDIO);
               Meeting.Current.SrcVid = (IWMEncVideoSource2)SrcGrp.AddSource(WMENC_SOURCE_TYPE.WMENC_VIDEO);

                // Specify a source file.
                Meeting.Current.SrcAud.SetInput(Properties.Settings.Default.Audio, "Device","");
                Meeting.Current.SrcVid.SetInput(Properties.Settings.Default.Video, "Device","");
                



                // Specify an output file.
             //   IWMEncFile File = Encoder.File;
               // File.LocalFileName = "C:\\OutputFile.wmv";

                // Select a profile from the collection and set it into the source group.
                IWMEncProfileCollection ProColl = Meeting.Current.Encoder.ProfileCollection;
                IWMEncProfile Pro;
                for (int i = 0; i < ProColl.Count; i++)
                {
                    Pro = ProColl.Item(i);
                    if (Pro.Name == "Windows Media Video 8 for Local Area Network (384 Kbps)")
                    {
                        SrcGrp.set_Profile(Pro);
                        break;
                    }
                }

                // Create two IWMEncDataViewCollection objects--one for the preview
                // collection and one for the postview collection.
                IWMEncDataViewCollection DVColl_preview = Meeting.Current.SrcVid.PreviewCollection;
                //IWMEncDataViewCollection DVColl_postview = SrcVid.PostviewCollection;

                // Create two WMEncDataView objects--one for previewing and one for postviewing.
                WMEncDataView Preview = new WMEncDataView();
                

                // Add the WMEncDataView objects to the collection.
                int lpreviewStream = DVColl_preview.Add(Preview);
                //int lpostviewStream = DVColl_postview.Add(Postview);

                // Start encoding.
                Meeting.Current.Encoder.PrepareToEncode(true);

                Meeting.Current.Encoder.Start();
                if (Properties.Settings.Default.Input != "none")
                {
                    IWMEncInputCollection InputColl;
                    InputColl = Meeting.Current.SrcVid.EnumerateInputs();
                    for (Int16 x = 0; x < InputColl.Count; x++)
                    {
                        if (InputColl.Item(x) == Properties.Settings.Default.Input)
                        {
                            Meeting.Current.SrcVid.Input = x;
                            break;
                        }
                    }
                }

                // Display the preview in a frame named Panel_Preview.
                Preview.SetViewProperties(lpreviewStream, (int)Panel_Preview.Handle);
                Preview.StartView(lpreviewStream);
                */
                
            }

            catch (Exception ex)
            {  // TODO: Handle exceptions. 
            }

        }
        private void StartJob(object sender, EventArgs e)
        {
            try
            {
                if (Meeting.Current.Job.IsCapturing)
                    throw new ApplicationException("Unable to start - capture currently in progress.");

                if (chkfile.Checked == true)
                {
                    FileInfo f = new FileInfo(Meeting.Current.VideoFilename);
                    if (f.Extension != ".wmv")
                        Meeting.Current.VideoFilename = Meeting.Current.VideoFilename + ".wmv";

                }

                dtBefore = DateTime.Now;
                dropcapStatusLabel.Text = "0 Dropped / 0 Captured";
                txtDuration.Text = "";

            
                recordingStatusLabel.Text =
                    "Status: "
                    + ((chkfile.Checked) ? " Recording to " + Meeting.Current.VideoFilename : "")
                ;
                dropcapStatusLabel.Text = "0 Dropped / 0 Captured";

                if (!chkstream.Checked)
                {
                    ((WindowsMediaPublishingPointOutputFormat)Meeting.Current.Job.OutputFormat).PublishingPoint = null;

                }
               
                this.SetEncoderOptions(sender, e);

                DisableEditSession();

                Meeting.Current.Job.StartEncoding();

                timer1.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                this.cmdStartRecording.Enabled = false;
                this.cmdStopRecording.Enabled = true;
                this.cmdStopRecording.Select();
            }
        }
        private void SetupDeviceSource(Object State)
        {
            LiveDevice audio = null;
            LiveDevice video = null;

            if (settings == null)
            {
                settings = CacheManager.ReadSettings<SettingsCache>();
            }



            if (Meeting.Current.Job.DeviceSources.Count < 1)
            {
                foreach (LiveDevice x in Meeting.Current.Job.VideoDevices)
                {
                    if (x.Name == settings.PreferedVideoDeviceName)
                    {
                        //found audio device
                        video = x;
                    }

                }

                foreach(LiveDevice y in Meeting.Current.Job.AudioDevices)
                {
                    if (y.Name == settings.PreferedAudioDeviceName)
                    {
                        //found vidoe device
                        audio = y;
                    }
                }


                if (video == null || audio == null)
                {
                    frmSettings frm = new frmSettings();
                    if (frm.ShowDialog() == DialogResult.Cancel)
                        ;
                    else
                    {
                        settings = CacheManager.ReadSettings<SettingsCache>();
                        SetupDeviceSource(null);
                    }

                }

                Meeting.Current.Job.AddDeviceSource(video, audio);
                Meeting.Current.Job.ActivateSource(Meeting.Current.Job.DeviceSources[0]);

            }
           
        }
        private void StopJob()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delStopJob(StopJob));
            }
            else
            {
                try
                {

                    // ShowDropped();
                    timer1.Stop();

                    Meeting.Current.Job.StopEncoding();


                    dtBefore = null;

                    EnableEditSession();

                    Meeting.Current.Status = RecordingStatus.Stopped;
                }
                catch (Exception)
                {

                    MessageBox.Show("Unable to properly stop this job please restart the application.", "OpenGOVideo", MessageBoxButtons.OK);
                }
                finally
                {
                    this.cmdStartRecording.Enabled = true;
                    this.cmdStopRecording.Enabled = false;
                    this.cmdStopRecording.Select();
                }
            }
                
          
        }
        // Show the time elapsed for this recording
        private void ShowDuration()
        {
            TimeSpan duDuration = DateTime.Now - (DateTime)dtBefore;
            txtDuration.Text = duDuration.ToPrettyTimeStamp();
        }
        private void ShowDropped()
        {
           dropcapStatusLabel.Text = String.Format("{0} Dropped / {1} Captured", Meeting.Current.Job.NumberOfDroppedSamples, Meeting.Current.Job.NumberOfEncodedSamples);
        }
        //private void treeview_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    if (treeview.SelectedNode == null)
        //    {
        //        cmdTimeStamp.Enabled = false;
        //        this.cmdAdd.Enabled = false;
        //        cmdDelete.Enabled = false;
        //        this.cmdEditAgendaItem.Enabled = false;
        //        cmdTimeStamp.Text = "Select item to timestamp";
        //    }
        //    else
        //    {
        //        cmdTimeStamp.Enabled = true;
        //        cmdAdd.Enabled = true;
        //        cmdDelete.Enabled = true;
        //        cmdEditAgendaItem.Enabled = true;

        //        int ai_id = (int)treeview.SelectedNode.Tag;
        //        AgendaItem ai = Meeting.Current.Agenda.Where(i => i.id == ai_id).First();

        //        cmdTimeStamp.Text = (ai.timestamp == null) ? "Timestamp selected item" : "Re-stamp selected item";
        //    }
        //}
        private static void EditSession()
        {
            try
            {
                System.Windows.Forms.Screen scr = System.Windows.Forms.Screen.PrimaryScreen;

                Form edittimestamps = new EditTimestamps();
                edittimestamps.StartPosition = FormStartPosition.CenterScreen;

                edittimestamps.Size = new Size(scr.WorkingArea.Width - 100, scr.WorkingArea.Height - 100);

                edittimestamps.ShowDialog();
                edittimestamps.Activate();   
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Error editing this session item: {0}", ex.Message));
            }

            
        }
        private void respondToOrgChanged(object sender, EventArgs e)
        {
            this.Text = WindowTitle + " - " + Meeting.Current.Org.Name;
            Meeting.Current.VideoFilename =Path.Combine( Meeting.Current.FilePath, Meeting.Current.Org.GetVideoFileName(".wmv"));
        }
        // sender = (string)newFilePath
        private void respondToFilePathChanged(object sender, EventArgs e)
        {
            this.curPathStatusLabel.Text = "Current Path: " + (string)sender;
        }
        private void updateFieldsBasedOnStatus(object sender, EventArgs e)
        {
            var status = Meeting.Current.Status;

            /*
            if (status == RecordingStatus.Recording)
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Name == "tabRecordingControls")
                    {
                        //tabPage.Show();
                        tabPage.Select();
                    }
                    else
                    {
                        ((Control)tabPage).Enabled = false;
                    }
                }
            }
            else
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                    ((Control)tabPage).Enabled = true;
            }
            */

            switch (status)
            {
                case RecordingStatus.AuthRequired:
                    break;

                case RecordingStatus.Ready:
                    break;

                case RecordingStatus.Connecting:
                    break;

                case RecordingStatus.NotReady:
                    break;

                case RecordingStatus.Recording:
                    break;

                case RecordingStatus.Stopped:
                    this.RecheckRecordingStatus();
                    break;

                case RecordingStatus.Unknown:
                    break;
            }
        }
        private void RecheckRecordingStatus()
        {
            //TODO: Add some real validation stuff
            Meeting.Current.Status = RecordingStatus.Ready;
        }
        private void AppQuit()
        {
            try
            {
                // Check to see if recording/capture is in progress
                if(Meeting.Current.Status == RecordingStatus.Recording || Meeting.Current.Job.IsCapturing)
                    throw new ApplicationException("Cannot quit - recording in progress.");

                          
                // Everything looks good, so let's save our settings and close this app!
                Properties.Settings.Default.File = chkfile.Checked;
                Properties.Settings.Default.Stream = chkstream.Checked;
                Properties.Settings.Default.Save();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StartPreview()
        {
            // If anything goes wrong, turn the preview off
           
                if (chkPreview.Checked)
                {
                    if (Meeting.Current.Job.DeviceSources.Count == 0 || Meeting.Current.Job.DeviceSources[0].VideoDevice == null)
                        throw new ApplicationException("Please select a video device first.");

                    HandleRef h = new HandleRef(Panel_Preview, Panel_Preview.Handle);
                    PreviewWindow prev = new PreviewWindow(h);
                   

                    Meeting.Current.Job.DeviceSources[0].PreviewWindow = prev;
                    Meeting.Current.Job.DeviceSources[0].PreviewWindow.SetSize(Panel_Preview.Size);

                    {
                        Visible = true;
                    };
                }
                else
                {
                    if (Meeting.Current.Job.DeviceSources.Count != 0)
                        Meeting.Current.Job.DeviceSources[0].PreviewWindow = null;
                }
            
           
        }

        private void ShowSplash()
        {
            LoadingSplash ls = new LoadingSplash();
            ls.ShowDialog();
            //Thread.Sleep(500);
        }

        private void SetPublishPoint()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadedSetPublishingPoint));
           

        }

        private void ThreadedSetPublishingPoint(object data)
        {
            if (Meeting.Current.Org.LiveStream == null)
                throw new ApplicationException("Please configure your publish point first.");

            System.Security.SecureString password = new System.Security.SecureString();
            foreach (char letter in Meeting.Current.Org.LiveStream.Password)
                password.AppendChar(letter);

            Meeting.Current.Job.OutputFormat = new WindowsMediaPublishingPointOutputFormat()
            {
                PublishingPoint = new Uri(
                    Meeting.Current.Org.LiveStream.PublishPoint
                    + System.IO.Path.GetFileNameWithoutExtension(Meeting.Current.VideoFilename)
                ),
                UserName = Meeting.Current.Org.LiveStream.Username,
                Password = password
            };

            this.SetEncoderOptions(null, null);
            try
            {
                Meeting.Current.Job.PreConnectPublishingPoint();

            }
            catch (Exception ex)
            {

                int x = 0;
            }
        }
        //private bool InitDeviceSources(object sender, EventArgs e)
        //{
        //    // If capture is in progress, set device back to current device and halt the init process
        //    // This should NEVER happen if the tab is locked during recording, but you never know what may happen.
        //    if (Meeting.Current.Job.IsCapturing || Meeting.Current.Status == RecordingStatus.Recording)
        //    {
        //        //Meeting.Current.Job.StopEncoding();
        //        if (((ComboBox)sender).Name == "cmbVideoDevice")
        //            ((ComboBox)sender).SelectedItem = Meeting.Current.Job.DeviceSources[0].VideoDevice;
        //        else
        //            ((ComboBox)sender).SelectedItem = Meeting.Current.Job.DeviceSources[0].AudioDevice;
        //        return false;
        //    }

        //    try
        //    {
        //        if (Meeting.Current.Job.DeviceSources.Count > 0)
        //            Meeting.Current.Job.RemoveDeviceSource(Meeting.Current.Job.DeviceSources[0]);

        //        //Meeting.Current.Job.AddDeviceSource(
        //        //    (LiveDevice)cmbVideoDevice.SelectedItem,
        //        //    (LiveDevice)cmbAudioDevice.SelectedItem
        //        //);

        //        //chkPreview_CheckedChanged(sender, e);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        private void btnAdvancedDeviceConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (Meeting.Current.Job.DeviceSources.Count == 0)
                    throw new ApplicationException("Please select a device source first.");
                /*
                if (cmbAudioDevice.SelectedItem != Meeting.Current.Job.DeviceSources[0].AudioDevice)
                    throw new ApplicationException("Please apply your changes before tweaking advanced device settings.");
                if (cmbVideoDevice.SelectedItem != Meeting.Current.Job.DeviceSources[0].VideoDevice)
                    throw new ApplicationException("Please apply your changes before tweaking advanced device settings.");
                */

                ConfigurationDialog[] dialogs = Meeting.Current.Job.DeviceSources[0].GetSupportedConfigurationDialogs();
                if (dialogs == null || dialogs.Length == 0)
                    throw new ApplicationException("No advanced config options available for the selected devices.");

                AdvancedDeviceConfig dcform = new AdvancedDeviceConfig()
                {
                    Visible = false
                };
                DialogResult dr = dcform.ShowDialog(this);
                //dcform.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetEncoderOptions()
        {
            ApplyEncoderOptions();
        }
        private void SetEncoderOptions(object sender, EventArgs e)
        {
            ApplyEncoderOptions();
        }
        private void ApplyEncoderOptions()
        {
            Meeting.Current.Job.OutputFormat.VideoProfile = new Profiles.MainVC1VideoProfile()
            {
                Bitrate = new Profiles.ConstantBitrate(settings.BitRate),
                FrameRate = Convert.ToDouble(settings.FrameRate),
                Size = settings.VideoSize
            };
            Meeting.Current.Job.OutputFormat.AudioProfile = new Profiles.WmaAudioProfile()
            {

            };
        }
        private void EnableAgendaButtons()
        {
            cmdSaveAgenda.Enabled = true;
            cmdEditAgendaItem.Enabled = true;
            cmdEditSession.Enabled = true;
            cmdTimeStamp.Enabled = true;
            cmdAddItem.Enabled = true;
            cmdDeleteAgendaItem.Enabled = true;
        }
        private void DisableAgendaButtons()
        {
            cmdSaveAgenda.Enabled = false;
            cmdEditAgendaItem.Enabled = false;
            cmdEditSession.Enabled = false;
            cmdTimeStamp.Enabled = false;
            cmdAddItem.Enabled = false;
            cmdDeleteAgendaItem.Enabled = false;
        }
        private void EnableTimeStamp()
        {
            cmdTimeStamp.Enabled = true;
        }
        private void EnableEditSession()
        {
            cmdEditSession.Enabled = true;
            mnuEditBoard.Enabled = true;
        }
        private void DisableEditSession()
        {
            cmdEditSession.Enabled = false;
            mnuEditBoard.Enabled = false;
        }

        private void DisableTimeStamp()
        {
            cmdTimeStamp.Enabled = false;
        }
        private void QuickSave()
        {
            // Check if agenda contains any items
            if (Meeting.Current.Agenda.Count == 0)
                throw new ApplicationException("Can't save an empty agenda!");

            if (!File.Exists(Meeting.Current.MeetingFilename))
            {
                VerboseSave();
                return;
            }
            string filename = Meeting.Current.MeetingFilename;
            TreeNode[] output = new TreeNode[treeview.Nodes.Count];

            int x = 0;
            foreach (TreeNode child in treeview.Nodes)
                output[x++] = child;

            AgendaFromTree();
            Meeting.Current.AgendaTree = output;

            if (!Meeting.Current.SaveToXML(filename))
                throw new ApplicationException("An error prevented the meeting file from being saved.");


        }
        private void VerboseSave()
        {
            // Check if agenda contains any items
            if (Meeting.Current.Agenda.Count <= 0)
                return;


            // Initialize the save dialog box
            saveFileDialog1.InitialDirectory = Meeting.Current.FilePath;
            saveFileDialog1.FileName = Meeting.Current.MeetingFilename;


            // If the user didn't cancel it, then proceed with saving
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;


            string filename = saveFileDialog1.FileName;
            Meeting.Current.MeetingFilename = Path.Combine(Meeting.Current.FilePath, filename);

            TreeNode[] output = new TreeNode[treeview.Nodes.Count];

            int x = 0;
            foreach (TreeNode child in treeview.Nodes)
                output[x++] = child;

            AgendaFromTree();
            Meeting.Current.AgendaTree = output;

            if (!Meeting.Current.SaveToXML(filename))
                throw new ApplicationException("An error prevented the meeting file from being saved.");

            // Everything saved just fine!
            MessageBox.Show("Meeting file saved!");
        }
        public bool IsAgendaFileDirty()
        {
            bool retval;
            string source = Meeting.Current.GetAgendaXML();
            string currentHash = ComputeHash(source);
            retval = !(_lastHash == currentHash);
            return retval;
        }
        private string ComputeHash(string Input)
        {
            string hash = null;
            byte[] bytInput = System.Text.Encoding.ASCII.GetBytes(Input);
            byte[] hashedInput = new MD5CryptoServiceProvider().ComputeHash(bytInput);

            hash = ByteArrayToString(hashedInput);

            return hash;

        }

        private string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        #endregion


        #region Event Handlers

        // TODO: Maybe we can refactor this method?
        private void Form_Foccused(object sender, EventArgs e)
        {
            //has the tree changed if so update
            if (Meeting.Current.change)
            {
                Meeting.Current.change = false;
                if (treeview.Nodes != null)
                {
                    treeview.Nodes.Clear();
                    this.treeview.Nodes.AddRange(Meeting.Current.LoadTreeFromAgenda(Meeting.Current.Agenda.Where(a => a.parent_id == 0).ToArray<AgendaItem>()));
                    if (treeview.Nodes.Count > 0)
                    {
                        treeview.ExpandAll();
                        treeview.SelectedNode = treeview.Nodes[0];
                    }
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        // TODO: Fix spacing/indentation
        private void btnAddTimestamp_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyTimeStamp();
                QuickSave();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        private void mnuEditSettings_Click(object sender, EventArgs e)
        {
            

            try
            {
                //Form settings = new Settings()
                //{
                //    Visible = false
                //};
                //settings.Activate();
                //settings.ShowDialog(this);
                frmSettings frm = new frmSettings();
                frm.ShowDialog();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

                //disable all state changing controls
                mnuFile.Enabled = false;
                mnuEdit.Enabled = false;
                chkstream.Enabled = false;

                if (File.Exists(Meeting.Current.VideoFilename))
                {
                    if (MessageBox.Show("A video file exists for this agenda. Continuing will overwrite it. Continue?", "OpenGOVideo",
                                                    MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                if (Meeting.Current.Agenda.Count == 0)
                {
                    MessageBox.Show("You do not have an agenda file loaded. You can only record video.");
                }

                
                if (chkfile.Checked & !chkstream.Checked)
                {
                   
                        StartJob(sender, e);
                        return;

                 
                }

                //user just wants to stream
                if (chkstream.Checked & !chkfile.Checked)
                {
                    if (Meeting.Current.Status == RecordingStatus.Ready)
                    {
                        StartJob(sender, e);
                        SetupWebLink();
                        return;
                    }
                }


                //user want to save to file and stream to server
                if (chkfile.Checked & chkstream.Checked)
                {
                    if (Meeting.Current.Status == RecordingStatus.Ready)
                    {
                        StartJob(sender, e);
                        SetupWebLink();
                        return;
                    }
                }

                if (chkstream.Checked)
                {
                }

                //MessageBox.Show(string.Format("Video file will be saved here: {0}", Meeting.Current.FilePath), "Info", MessageBoxButtons.OK);


                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void SetupWebLink()
        {
            lblLink.Links.Clear();
            lblLink.Text = "Video Stream";
            string url = Meeting.Current.Org.LiveStream.PublishPoint + Path.GetFileNameWithoutExtension(Meeting.Current.VideoFilename);
            lblLink.Links.Add(new LinkLabel.Link(0,lblLink.Text.Length,url));
            lblLink.Visible = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //enable all state changing controls
                mnuFile.Enabled = true;
                mnuEdit.Enabled = true;
                chkstream.Enabled = true;

                this.Cursor = Cursors.AppStarting;

                if (Meeting.Current.Agenda.Count > 0)
                {
                    QuickSave();

                    // Not the most exhaustive check, make sure they have a host configured to start the publish. 
                    if (!string.IsNullOrEmpty(Meeting.Current.Org.FTPServer.Host))
                    {
                        mnuPublishToFTP.Enabled = true;
                    }
                }

                StopJob();
                lblLink.Visible = false;
                recordingStatusLabel.Text = "Status: Inactive";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DisableTimeStamp();
                this.Cursor = Cursors.Default;
            }

        }


        // Determine what node in the tree we are dropping on to (target),
        // copy the drag source (sourceNode), make the new node and delete
        // the old one.
        private void treeview_DragDrop(object sender, DragEventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Are you sure you want to move the selected item " + sourceNode.Text + "?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                Point pos = treeview.PointToClient(new Point(e.X, e.Y));

                TreeNode targetNode = treeview.GetNodeAt(pos);
                TreeNode nodeCopy;

                // TODO: Refactor this into something much simpler
                if (targetNode.Parent != sourceNode)
                {
                    if (targetNode != null)
                    {
                        nodeCopy = new TreeNode(sourceNode.Text, sourceNode.ImageIndex, sourceNode.SelectedImageIndex)
                        {
                            Name = sourceNode.Name,
                            ToolTipText = sourceNode.ToolTipText,
                            ForeColor = sourceNode.ForeColor,
                            BackColor = sourceNode.BackColor,
                            Tag = sourceNode.Tag
                        };

                        foreach (TreeNode child in sourceNode.Nodes)
                        {
                            // nodeCopy.Nodes.Add(new TreeNode(child.Text, child.ImageIndex, child.SelectedImageIndex));
                            nodeCopy.Nodes.Add(children(child));
                        }


                        if (sourceNode.Index > targetNode.Index)
                        {
                            if (targetNode.Parent == null)
                            {
                                treeview.Nodes.Insert(targetNode.Index, nodeCopy);
                                treeview.Nodes.Remove(sourceNode);
                            }
                            else
                            {
                                targetNode.Parent.Nodes.Insert(targetNode.Index, nodeCopy);
                                sourceNode.Remove();
                            }
                        }
                        else
                        {
                            if (targetNode.Parent == null)
                            {
                                treeview.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                                treeview.Nodes.Remove(sourceNode);
                            }
                            else
                            {
                                targetNode.Parent.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                                sourceNode.Remove();
                            }
                        }

                        treeview.Update();
                        treeview.Invalidate();
                        treeview.ExpandAll();

                        AgendaFromTree();
                    }
                }
            }
        }

        private void btnexpand_Click(object sender, EventArgs e)
        {
            if (treeview.Nodes.Count > 0)
            {
                treeview.ExpandAll();
                treeview.SelectedNode = treeview.Nodes[0];
            }
        }

        private void btncollaps_Click(object sender, EventArgs e)
        {
            if (treeview.Nodes != null)
                treeview.CollapseAll();
        }


        private void treeview_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sourceNode = (TreeNode)e.Item;
            DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);

        }

        private void treeview_Dragover(object sender, DragEventArgs e)
        {
            Point pos = treeview.PointToClient(new Point(e.X, e.Y));
            //treeview.SelectedNode = treeview.GetNodeAt(pos);
            // Set a constant to define the autoscroll region

            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = treeview.PointToClient(Cursor.Position);

            // See if we need to scroll up or down
            if ((pt.Y + scrollRegion) > treeview.Height)
            {
                // Call the API to scroll down
                SendMessage(treeview.Handle, (int)277, (int)1, 0);
            }
            else if (pt.Y < (treeview.Top + scrollRegion))
            {
                // Call thje API to scroll up
                SendMessage(treeview.Handle, (int)277, (int)0, 0);
            }
        }


        // Define the event that occurs while the dragging happens
        private void treeview_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Main_Load(object sender, EventArgs e)
        {

            try
            {
                this.Hide();

                this.WindowState = FormWindowState.Maximized;
                this.Text = WindowTitle;
                CacheManager.SettingChange += new SettingsChangedEvent(CacheManager_SettingChange);

                // TODO: Investigate if we should allow this functionality or not:
                //chkfile.Checked = Properties.Settings.Default.File;
                //chkstream.Checked = Properties.Settings.Default.Stream;

                //treeview.Width = this.Width / 2 - 100;
                //treeview.Location = new Point(this.Location.X + this.Width-treeview.Width-75 , treeview.Location.Y);
                //treeview.Height = this.Height - 300;
                //treeview.Width = this.Width / 2-10;
                //treeview.Location = int.Parse(this.Location.X.ToString) - this.Width + treeview.Width;
                //Panel_Preview.Width = this.Width / 2-10;

                Thread.Sleep(100);

                // Bind some events
                Meeting.Current.Job.AcquireCredentials += new EventHandler<AcquireCredentialsEventArgs>(job_AcquireCredentials);
                Meeting.Current.Job.Status += new EventHandler<EncodeStatusEventArgs>(job_Status);
                Meeting.Current.onStatusChanged += new EventHandler(updateFieldsBasedOnStatus);
                Meeting.Current.onOrgChanged += new EventHandler(respondToOrgChanged);
                Meeting.Current.onFilePathChanged += new EventHandler(respondToFilePathChanged);

                // Update the "current path" status bar label
                this.respondToFilePathChanged(Meeting.Current.FilePath, null);

                //read the cache file or display settings)
                settings = CacheManager.ReadSettings<SettingsCache>();
                if (settings == null)
                {
                    frmSettings settingsFrm = new frmSettings();
                    DialogResult response = settingsFrm.ShowDialog();
                    while (response != DialogResult.OK)
                    {

                        if (response == DialogResult.Cancel)
                        {
                            if (MessageBox.Show("Cannot continue without valid settings.  Close the apllication?", "OpenGOVideo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                break;
                            else
                                response = settingsFrm.ShowDialog();
                        }


                    }
                    if (response == DialogResult.Cancel)
                    {

                        this.Close();
                        return;
                    }

                }
                settings = CacheManager.ReadSettings<SettingsCache>();
                SetupDeviceSource(null);


                // Attempt to load the device sources

                if (Meeting.Current.Job.IsCapturing)
                    Meeting.Current.Job.StopEncoding();


                if (Meeting.Current.Job.VideoDevices.Count == 0)
                {
                    //menuFile.Enabled = false;
                    cmdStartRecording.Enabled = false;
                    cmdStopRecording.Enabled = false;

                }
                else
                {
                    mnuFile.Enabled = true;
                    cmdStartRecording.Enabled = false;
                    cmdStopRecording.Enabled = false;

                }



                Thread.Sleep(100);
                this.Show();
                this.Enabled = true;
                if(th != null)
                    th.Abort();

                frmSelectOrganization frmOrg = new frmSelectOrganization();

                DialogResult dr = frmOrg.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    AppQuit();
                    Application.Exit();
                }

                if (frmOrg.OrgCount <= 1)
                    mnuEditBoard.Enabled = false;
                else
                    mnuEditBoard.Enabled = true;



                if (Meeting.Current.Job.DeviceSources.Count > 0)
                {
                    StartPreview();
                    cmdStartRecording.Enabled = true;

                }
                this.mnuPublishToFTP.Enabled = true;

                WAITFORNEWFILEHANDLE = new ManualResetEvent(false);


                ConfigurePublishingPoint();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
            }



        }

   
        private void ConfigurePublishingPoint()
        {

            frmWait frmw = new frmWait();

            //setup publishing point
            SetPublishPoint();

            frmw.TopMost = true;
            frmw.StartPosition = FormStartPosition.Manual;
            frmw.Location = new Point(this.Location.X + 100, this.Location.Y + 100);
            frmw.ShowDialog();

            WAITFORNEWFILEHANDLE.WaitOne(30000);

            if (frmw.DialogResult == DialogResult.Cancel)
            {
                //chkstream.Checked = false;
            }
            frmw.Close();
        }

     
        private void btnexpand_Click_1(object sender, EventArgs e)
        {
            if (treeview.Nodes.Count > 0)
            {
                treeview.ExpandAll();
                treeview.SelectedNode = treeview.Nodes[0];
            }
        }

        private void btncollaps_Click_1(object sender, EventArgs e)
        {
            if (treeview.Nodes != null)
                treeview.CollapseAll();
        }

        private void cmdEditSession_Click(object sender, EventArgs e)
        {
            try
            {
                EditSession();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            /*  treeview.Width = this.Width / 2-100;
              treeview.Location = new Point(this.Location.X + this.Width - treeview.Width-85 , treeview.Location.Y);
              treeview.Height = this.Height - 200; 
               Panel_Preview.Width = this.Width / 2-50;
             */

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            Meeting.Current.parentnode = treeview.SelectedNode;
            Meeting.Current.newnode = new TreeNode();

            Form additem = new AddItem();
            additem.ShowDialog();

            treeview.SelectedNode.Nodes.Add(Meeting.Current.newnode);
        }

        private void btndelete_Click_2(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to remove the selected item " + treeview.SelectedNode.Text + " and everything under it?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                treeview.SelectedNode.Remove();
                AgendaFromTree();
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            //has the tree changed if so update
            if (Meeting.Current.change)
            {
                Meeting.Current.change = false;
                if (treeview.Nodes != null)
                {
                    treeview.Nodes.Clear();
                    this.treeview.Nodes.AddRange(Meeting.Current.LoadTreeFromAgenda(Meeting.Current.Agenda.Where(a => a.parent_id == 0).ToArray<AgendaItem>()));
                    if (treeview.Nodes.Count > 0)
                    {
                        treeview.ExpandAll();
                        treeview.SelectedNode = treeview.Nodes[0];
                    }
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Meeting.Current.Status == RecordingStatus.Recording)
                {
                    MessageBox.Show("Please stop recording before closing.", "OpenGOVideo", MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }

                //nothing to save
                if (Meeting.Current.Agenda.Count == 0)
                    return;

                if (!IsAgendaFileDirty())
                    return;
                else
                {
                    if (MessageBox.Show("Do you want to save your changes?", "OpenGOVideo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {

                    }
                    
                }

                if (File.Exists(Meeting.Current.MeetingFilename))
                    QuickSave();
                else
                    VerboseSave();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                EditItemThread();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(string.Format("Unable to edit agenda item: {0}",ex.Message));
            }
         
        }

        private void EditItemThread()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    delAgendaAction x = new delAgendaAction(EditItemThread);
                    this.Invoke(x);
                }
                else
                {
                    Meeting.Current.parentnode = treeview.SelectedNode;

                    Form edititem = new EditItem();
                    edititem.ShowDialog();

                    treeview.SelectedNode.ToolTipText = Meeting.Current.parentnode.ToolTipText;
                    treeview.SelectedNode.Name = Meeting.Current.parentnode.Name;
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(string.Format("Error editing the agenda item: {0}",ex.Message));
            }
           
        }

        private void btnPreConnect_Click(object sender, EventArgs e)
        {

        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                StartPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to show preview. " + ex.Message);
                chkPreview.Checked = false;
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            try
            {

                if (Meeting.Current.Agenda.Count > 0)
                {
                    DialogResult ask =
                            MessageBox.Show("You have an agenda file loaded.  Continuing will overwrite it.  Continue?", "OpenGOVideo",
                                                MessageBoxButtons.YesNo);

                    if (ask == DialogResult.Yes)
                    {
                        treeview.Nodes.Clear();
                        Meeting.Current.Agenda.Clear();
                        _agendaText = string.Empty;
                    }
                    else
                        return;
                }

                if (System.IO.Directory.Exists(Meeting.Current.FilePath))
                    openFileDialog1.InitialDirectory = Meeting.Current.FilePath;
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    string filename = openFileDialog1.FileName;
                    treeview.Nodes.Clear();

                 
                    bool editSession =  Meeting.Current.LoadFromFile(filename);
                    if (Meeting.Current.Agenda.Count > 0)
                        _lastHash = ComputeHash(Meeting.Current.GetAgendaXML());
                    treeview.Nodes.AddRange(Meeting.Current.AgendaTree);
                    treeview.ExpandAll();
                    treeview.SelectedNode = treeview.Nodes[0];
                    EnableAgendaButtons();
                    if (File.Exists(Meeting.Current.VideoFilename) && editSession)
                    {
                        EditSession();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DisableAgendaButtons();
            }

        }

        private void mnuEditOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                //dont change boards while recording
                if (Meeting.Current.Status == RecordingStatus.Recording || Meeting.Current.Job.IsCapturing)
                    throw new ApplicationException("Unable to switch organization while recording is in progress.");

                //if agenda file is dirty prompt to save
                if (IsAgendaFileDirty())
                    VerboseSave();

                //clear the current agenda file
                Meeting.Current.Agenda.Clear();
                treeview.Nodes.Clear();

                
                var so = new frmSelectOrganization();
                so.ShowDialog();

                MessageBox.Show(string.Format("Your board has changed to {0}", Meeting.Current.Org.Board));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error selecting new organization", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlLink_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLink_Click(object sender, EventArgs e)
        {
            try
            {
                string url = string.Format("{0}{1}", Meeting.Current.Org.LiveStream.PublishPoint,
                                        Path.GetFileNameWithoutExtension(Meeting.Current.VideoFilename));

                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Attempt to open the web browser failed.");
            }
        }

        private void Panel_Preview_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdTimeStamp_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyTimeStamp();
                QuickSave();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void menuFileNewAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                ObtainNewAgendaFile();
                EnableAgendaButtons();
            }
            catch (Exception ex)
            {
                DisableAgendaButtons();
                MessageBox.Show("Unable to get agenda file from server please check your connections.");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void cmdNewAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                ObtainNewAgendaFile();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }

        }

        private void cmdSaveAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                VerboseSave();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            try
            {

                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void chkstream_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkstream.Checked)
                {
                    cmdStartRecording.Enabled = false;
                    ConfigurePublishingPoint();
                }
                else
                {
                    Meeting.Current.Job.StopEncoding();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error establishing a connection to the publishing server.", "OpenGOVideo", MessageBoxButtons.OK);
            }
            finally
            {
                cmdStartRecording.Enabled = true;
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void mnuPublishToFTP_Click(object sender, EventArgs e)
        {

            // This was there before, but in order to make it more friendly, have an automated publish option based on their settings. We could always 
            // validate their settings and allow them to change them or do a manual download. 

            // Show the Auto Publish form
            using (frmAutoPublish autoPublishForm = new frmAutoPublish())
            {
                DialogResult dr = autoPublishForm.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    mnuPublishToFTP.Enabled = true;
                }
            }

            //if (/*Meeting.Current.AgendaModified*/true)
            //{
            //    //DialogResult dr = MessageBox.Show("You have unsaved changes to your agenda.  Would you like to save now?", "Unsaved changes to agenda", MessageBoxButtons.YesNoCancel);
            //    DialogResult dr = MessageBox.Show("Would you like to save the agenda before continuing?", "Save changes to agenda?", MessageBoxButtons.YesNoCancel);
            //    switch (dr)
            //    {
            //        case DialogResult.Yes:
            //            mnuFileSave_Click(sender, e);
            //            break;

            //        case DialogResult.No:
            //            break;

            //        case DialogResult.Cancel:
            //            return;
            //            break;
            //    }
            //}
            //// Publish via FTP button
            //Form publish = new Publish();
            //publish.ShowDialog();
            //publish.Activate();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //ThreadPool.QueueUserWorkItem(new WaitCallback(tmrTime1_Tick));

            if (Meeting.Current.Status == RecordingStatus.Recording)
            {
                ShowDropped();
                ShowDuration();
            }
            else if (Meeting.Current.Job.IsCapturing)
            {
                dtBefore = DateTime.Now;
                Meeting.Current.Status = RecordingStatus.Recording;
            }
            else
            {
                //tmrTime1.Enabled = false;
            }

        }

        private void tmrTime1_Tick(object state)
        {
      
               
            
        }
        #endregion

        private void cmdMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                MoveAgendaItemUp();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to move node ups.");
            }
            finally
            {
                
                treeview.Refresh();
                this.Cursor = Cursors.Default;
            }
            
        }

        private void MoveAgendaItemUp()
        {
            if (this.InvokeRequired)
            {
                delAgendaAction x = new delAgendaAction(MoveAgendaItemUp);
                this.Invoke(x);
            }
            else
            {

                if (treeview.SelectedNode != null)
                {
                    if (treeview.SelectedNode.Parent == null && treeview.SelectedNode.PrevNode == null)
                    {
                        ;
                    }
                    else
                    {

                        TreeNode temp = treeview.SelectedNode;
                        if (treeview.SelectedNode.PrevNode != null)
                        {
                            TreeNode prev = treeview.SelectedNode.PrevNode;
                            if (treeview.SelectedNode.Parent != null)
                            {
                                treeview.SelectedNode.Parent.Nodes.Remove(temp);
                            }
                            else
                            {
                                treeview.Nodes.Remove(temp);
                            }

                            prev.Nodes.Add(temp);
                            prev.Expand();
                        }
                        else
                        {
                            if (treeview.SelectedNode.Parent.Parent != null)
                            {
                                TreeNode newParent = treeview.SelectedNode.Parent.Parent;
                                int parentIndex = temp.Parent.Index;
                                treeview.SelectedNode.Parent.Nodes.Remove(temp);
                                newParent.Nodes.Insert(parentIndex, temp);

                            }
                            else
                            {
                                int parentIndex = temp.Parent.Index;
                                treeview.SelectedNode.Parent.Nodes.Remove(temp);
                                treeview.Nodes.Insert(parentIndex, temp);
                            }

                        }

                        treeview.SelectedNode = temp;


                    }
                }
            }
        }

        private void cmdMoveNodeDown_Click(object sender, EventArgs e)
        {
            try
            {
                MoveAgendaItemDownThread();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to move node down.");
            }
        }

        private void MoveAgendaItemDownThread()
        {
            if (this.InvokeRequired)
            {
                delAgendaAction x = new delAgendaAction(MoveAgendaItemDownThread);
                this.Invoke(x);
            }
            else
            {
                if (treeview.SelectedNode != null)
                {
                    if (treeview.SelectedNode.Parent == null && treeview.SelectedNode.NextNode == null)
                    {
                        ;
                    }
                    else
                    {

                        TreeNode temp = treeview.SelectedNode;
                        if (treeview.SelectedNode.NextNode != null)
                        {
                            TreeNode next = treeview.SelectedNode.NextNode;
                            if (treeview.SelectedNode.Parent != null)
                            {
                                treeview.SelectedNode.Parent.Nodes.Remove(temp);
                            }
                            else
                            {
                                treeview.Nodes.Remove(temp);
                            }

                            next.Nodes.Insert(0, temp);
                            next.Expand();
                        }
                        else
                        {
                            if (treeview.SelectedNode.Parent == null)
                                ;
                            else
                            {

                                if (treeview.SelectedNode.Index == treeview.SelectedNode.Parent.Nodes.Count - 1)
                                {
                                    //move to the next node
                                    int index = treeview.SelectedNode.Parent.Index + 1;
                                    if (treeview.SelectedNode.Parent.Parent == null)
                                    {
                                        treeview.SelectedNode.Remove();
                                        treeview.Nodes.Insert(index, temp);
                                    }
                                    else
                                    {
                                        treeview.SelectedNode.Remove();
                                        treeview.SelectedNode.Parent.Nodes.Insert(index, temp);
                                    }
                                }
                                else
                                {
                                    treeview.SelectedNode.Parent.Nodes.Remove(temp);
                                    treeview.SelectedNode.Parent.Nodes.Insert(treeview.SelectedNode.Parent.Nodes.Count, temp);
                                }
                            }
                        }

                        treeview.SelectedNode = temp;
                    }
                }
            }
        }

        private void SwapNode(TreeNode source, TreeNode target)
        {

            if (source.Parent == null && source.PrevNode == null)
                return;

            TreeNodeCollection col;
            if (source.Parent != null)
            {
                col = source.Parent.Nodes;
            }
            else
            {
                col = treeview.Nodes;
            }

            
            TreeNode temp = source;
            col.Remove(source);
            col.Insert(target.Index, temp);
            treeview.Refresh();
            treeview.SelectedNode = source;
        }

      

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemThread();

            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Unable to add items. Error {0}", ex.Message));
            }
        }

        private void AddItemThread()
        {
            if (this.InvokeRequired)
            {
                delAgendaAction del = new delAgendaAction(AddItemThread);
                this.Invoke(del);
            }
            else
            {
                if (treeview.SelectedNode == null)
                {
                    MessageBox.Show("Please select a node to incidate where to insert the item.");

                }
                else
                {
                    AgendaItem agi = new AgendaItem();
                    agi.id = Meeting.Current.lastGivenID + 1;
                    agi.title = "";
                    agi.desc = "";

                    Meeting.Current.Agenda.Add(agi);

                    Meeting.Current.lastGivenID = Meeting.Current.lastGivenID++;

                    TreeNode tn = new TreeNode();
                    Meeting.Current.parentnode = tn;
                    tn.Tag = agi.id;
                    tn.Text = agi.title;
                    treeview.SelectedNode.Nodes.Add(tn);
                    treeview.Refresh();

                    Form edititem = new EditItem();
                    DialogResult res = edititem.ShowDialog();

                    if (res == DialogResult.Cancel)
                        tn.Remove();

                    treeview.SelectedNode.Expand();
                    treeview.Refresh();
                }
            }
        }

        private void cmdDeleteAgendaItem_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteAgendaItemThread();
            }
            catch (Exception  ex)
            {

                MessageBox.Show(string.Format("Error while trying to delete agenda item: {0}", ex.Message));

            }
        }

        private void DeleteAgendaItemThread()
        {
            if (this.InvokeRequired)
            {
                delAgendaAction x = new delAgendaAction(DeleteAgendaItemThread);
                this.Invoke(x);
            }
            else
            {
                if (treeview.SelectedNode == null)
                {
                }
                else
                {
                    int ai_id = int.Parse(treeview.SelectedNode.Tag.ToString());
                    AgendaItem dead = Meeting.Current.Agenda.Where(i => i.id == ai_id).First();
                    Meeting.Current.Agenda.Remove(dead);
                    treeview.SelectedNode.Remove();
                    treeview.Refresh();
                }
            }


        }

 

        #region Message Loop

        //protected override void WndProc(ref Message m)
        //{
        //    Debug.WriteLine(m.Msg.ToString());
        //    switch (m.Msg)
        //    {
        //        case 528:
        //            return;
        //    }
        //    base.WndProc(ref m);
        //}

        #endregion

    }
}
