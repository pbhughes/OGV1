using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging; 
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Threading; 
using System.Windows.Forms;
using Ionic.Zip;

using Utilities.FTP; 

namespace OpenGOVideo
{
    public partial class frmAutoPublish : Form
    {
        private Thread      _uploadThread   = null;
        private FTPclient   _ftpClient      = null;

        //----------------------------------------------------------------------------------------------------------------------------
        //  frmAutoPublish
        /// <summary>
        /// Constructor
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        public frmAutoPublish()
        {
            InitializeComponent();

            _serverPromptLabel.Text = string.Format("Upload current agenda to Storage Services. ({0})",Meeting.Current.Org.FTPServer);
        }

        #region FORM BUTTON HANDLERS
        //----------------------------------------------------------------------------------------------------------------------------
        //  onUploadClicked
        /// <summary>
        /// The user clicked the upload button. Zip up the files and start the transfer. 
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void onUploadClicked(object sender, EventArgs e)
        {
            //validate oga file path textbox is not empty if checkbox checked
            if (this._uploadOgaCheckBox.Checked && this._ogaPathTextBox.Text == string.Empty)
            {
                MessageBox.Show("An agenda file must be selected if 'Publish agenda file' is checked", "Missing Agenda File");
                return;
            }
            else if (this._uploadOgaCheckBox.Checked && File.Exists(this._ogaPathTextBox.Text) == false)
            {
                MessageBox.Show("The selected agenda file does not exist on disk", "Agenda File Does Not Exist");
                return;
            }

            //validate wmv file path textbox is not empty if checkbox checked
            if (this._uploadWmvCheckBox.Checked && this._wmvPathTextBox.Text == string.Empty)
            {
                MessageBox.Show("A video file must be selected if 'Publish video file' is checked", "Missing Video File");
                return;
            }
            else if (this._uploadWmvCheckBox.Checked && File.Exists(this._wmvPathTextBox.Text) == false)
            {
                MessageBox.Show("The selected video file does not exist on disk", "Video File Does Not Exist");
                return;
            }

            //validate at least one file is present
            if (this._ogaPathTextBox.Text == string.Empty & this._wmvPathTextBox.Text == string.Empty)
            {
                MessageBox.Show("No files have been selected for publishing", "Empty Files");
                return;
            }

            _serverPromptLabel.Text = "Processing current agenda."; 
            _exitBtn.Enabled = false;
            _uploadBtn.Enabled = false;
            _uploadOgaCheckBox.Enabled = false;
            _locationOgaFileBtn.Enabled = false;
            _ogaPathTextBox.Enabled = false;
            _ogaFileLabel.Enabled = false;
            _uploadWmvCheckBox.Enabled = false;
            _locationWmvFileBtn.Enabled = false;
            _wmvPathTextBox.Enabled = false;
            _wmvFileLabel.Enabled = false;


            _uploadProgress.Visible = true;
            _uploadProgressLabel.Visible = true;
            _uploadProgressLabel.Visible = true; 

            FTPserver serverSettings = Meeting.Current.Org.FTPServer; 

            _ftpClient = new FTPclient(serverSettings.Host, serverSettings.Username, serverSettings.Password);
            _ftpClient.ReportProgress += new FTPclient.ReportProgressEventHandler(onFtpProgress);

            _uploadThread = new Thread(new ThreadStart(uploadFilesThreadRoutine));
            _uploadThread.IsBackground = false;
            _uploadThread.Name = "FTP Publish Thread";
            _uploadThread.Start(); 

        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  onExitClicked
        /// <summary>
        /// The user clicked the Exit button
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void onExitClicked(object sender, EventArgs e)
        {
            try
            {
                if (_ftpClient != null)
                    _ftpClient.ReportProgress -= onFtpProgress;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Close();
            }
        }

        #endregion 

        //----------------------------------------------------------------------------------------------------------------------------
        //  updateProgress
        /// <summary>
        /// Use this to do a thread safe update of the gui
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void uploadFilesThreadRoutine()
        {
            const string spaceReplacement = "%20";

            bool uploaded = false;
            string errorString = "Error communicating with server.";

            //this used to be ok because there was always a current meeting but since we are allowing the user
            //to browse for files to upload before they start recording - the meeting could be null
            //so just use the values from the textboxes

            //string zipFileName = Path.GetFileNameWithoutExtension(Meeting.Current.MeetingFilename) + "_TimeStamp_" + DateTime.Now.ToString("HHmmss_fff") + ".zip";
            //string zipPath = Path.Combine(Path.GetDirectoryName(Meeting.Current.MeetingFilename), zipFileName); 
            string zipFileName = string.Empty;
            string zipPath = string.Empty;
            
            try
            {
                //populate the array based on the paths on the form instead of the current agenda and video
                //string[] files = { Meeting.Current.MeetingFilename, Meeting.Current.VideoFilename }
                string[] files = null;
                {
                    if (this._ogaPathTextBox.Text != string.Empty && this._wmvPathTextBox.Text != string.Empty)
                    {
                        files = new string[] {this._ogaPathTextBox.Text, this._wmvPathTextBox.Text};
                        zipFileName = Path.GetFileNameWithoutExtension(this._wmvPathTextBox.Text) + "_TimeStamp_" + DateTime.Now.ToString("HHmmss_fff") + ".zip";

                        zipPath = Path.Combine(Meeting.Current.FilePath, zipFileName); 
                    }

                    else if (this._ogaPathTextBox.Text != string.Empty && this._wmvPathTextBox.Text == string.Empty)
                    {
                        files = new string[] {this._ogaPathTextBox.Text};
                        zipFileName = Path.GetFileNameWithoutExtension(this._ogaPathTextBox.Text) + "_TimeStamp_" + DateTime.Now.ToString("HHmmss_fff") + ".zip";
                        zipPath = Path.Combine(Meeting.Current.FilePath, zipFileName); 
                    }
                    
                    else if (this._ogaPathTextBox.Text == string.Empty && this._wmvPathTextBox.Text != string.Empty)
                    {
                        files = new string[] { this._wmvPathTextBox.Text };
                        zipFileName = Path.GetFileNameWithoutExtension(this._wmvPathTextBox.Text) + "_TimeStamp_" + DateTime.Now.ToString("HHmmss_fff") + ".zip";
                        zipPath = Path.Combine(Meeting.Current.FilePath, zipFileName); 
                    }
                }

                setControlText(_uploadProgressLabel, "Packing files, please wait...");

                bool packed = packFiles(zipPath, files); 

                if(packed)
                {
                    setControlText(_serverPromptLabel, "Initializing connection to FTP server");
                    setControlText(_uploadProgressLabel, "Starting Upload."); 

                    string directory = "AgendaFiles"; // Pull this from the FTP variable? 
                    string town = Meeting.Current.Org.City.Replace(" ", spaceReplacement);
                    string department = Meeting.Current.Org.Board.Replace(" ", spaceReplacement);

                    //omit boards from the hardcoded path or not?
                    string destinationDirectory = string.Format("{0}/{1}/{2}/Boards/{3}/Completed/",
                    //string destinationDirectory = string.Format("{0}/{1}/{2}/{3}/Completed/",
                                                                directory,
                                                                Meeting.Current.Org.State,
                                                                town,
                                                                department);

                    uploaded = _ftpClient.Upload(zipPath, destinationDirectory + zipFileName);
                }
                else
                {
                    errorString = "Could not pack agenda files."; 
                }
            }
            catch (Exception ex)
            {
                // Catch specific exceptions for FTP and try to make a friendly error message.
                Console.WriteLine(ex);
            }

            // Delete the zip file 
            try
            {
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath); 
                }
            }
            catch (Exception )
            {
                
            }

            // Report to the application whether the upload passed. 
            reportResult(uploaded, errorString); 
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  packFiles
        /// <summary>
        /// pack files into a zip archive. We are not worrying about folders right now, just copy them all as peers.
        /// Going to change this routine to use the CodePlex DotNetZip library instead of the IO.Packaging - 
        /// The IO.Packaging does not compress as much as the CodePlex dll
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------

        private bool packFiles(string zipPath, string[] files)
        {
            
            bool packed = false;

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    {
                        zip.SaveProgress += new EventHandler<SaveProgressEventArgs>(this.onPackProgress);
                    }

                    foreach (string file in files)
                    {
                        ZipEntry e = zip.AddFile(file,string.Format("Agenda_{0}", DateTime.Now.ToString("yyyyMMdd")));
                        e.Comment = "Packing file " + Path.GetFileName(file);
                    }

                    zip.Save(zipPath);
                }

                packed = true;

            }
            catch (Exception ex)
            {
                packed = false;
                Console.WriteLine(ex);
            }

            return packed;
        }

#region " Old Pack Files Method "
        //private bool packFiles(string zipPath, string[] files)
        //{
        //    const int streamBufferSize = 0x1000;

        //    bool packed = false;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(zipPath))
        //        {
        //            using (Package zipFile = ZipPackage.Open(zipPath, FileMode.Create, FileAccess.ReadWrite))
        //            {
        //                int fileIndex = 1;
        //                byte[] streamBuffer = new byte[streamBufferSize];
        //                int bytesRead = 0;
        //                int lastProgress = 0; 
        //                int currentProgress = 0; 

        //                foreach(string file in files)
        //                {
        //                    lastProgress = 0;
        //                    currentProgress = 0;

        //                    onPackProgress(currentProgress, fileIndex, files.Length); 

        //                    string fileName = Path.GetFileName(file);

        //                    Uri partURI = new Uri("/" + fileName, UriKind.Relative);

        //                    PackagePart part = zipFile.CreatePart(partURI, System.Net.Mime.MediaTypeNames.Application.Zip, CompressionOption.Normal);

        //                    // Copy the data to the Document Part
        //                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
        //                    {
        //                        Stream targetStream = part.GetStream();

        //                        fileStream.Seek(0, SeekOrigin.End);
        //                        ulong fileSize = (ulong)fileStream.Position; 
        //                        fileStream.Seek(0, SeekOrigin.Begin); 

        //                        ulong counter = 0; 

        //                        while ((bytesRead = fileStream.Read(streamBuffer, 0, streamBufferSize)) > 0)
        //                        {
        //                            targetStream.Write(streamBuffer, 0, bytesRead);

        //                            currentProgress = (int)((((double)counter * (double)streamBufferSize) / (double)fileSize) * 100.0);  

        //                            if(currentProgress > lastProgress)
        //                            {
        //                                lastProgress = currentProgress;
        //                                onPackProgress(currentProgress, fileIndex, files.Length); 
        //                            }

        //                            counter++; 
        //                        }
        //                    }

        //                    fileIndex++; 
        //                }

        //                packed = true; 
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        packed = false;
        //        Console.WriteLine(ex); 
        //    }

        //    return packed; 
        //}
#endregion

        //----------------------------------------------------------------------------------------------------------------------------
        //  onFtpProgress
        /// <summary>
        /// handle update events from ftp client. This will be runnning from a thread, so call the thread safe functions. 
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        void onFtpProgress(int percentDone)
        {
            if (!InvokeRequired)
            {
                try
                {
                    _serverPromptLabel.Text = "Performing file transfer";
                    _uploadProgress.Value = percentDone;
                    _uploadProgressLabel.Text = string.Format("Upload Progress: {0,3:D} %", percentDone);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { onFtpProgress(percentDone); }));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  onPackProgress
        /// <summary>
        /// handle gui updates due to packing the zip 
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        void onPackProgress(object sender, SaveProgressEventArgs e)
        {
            if (!InvokeRequired)
            {
                try
                {
                    _serverPromptLabel.Text = "Compressing selected files";

                    if (e.CurrentEntry != null)
                    {
                        _uploadProgressLabel.Text = e.CurrentEntry.Comment;
                    }

                    //parse the event args and set status label based on the state
                    {
                        switch (e.EventType)
                        {
                            case ZipProgressEventType.Saving_Started:
                                _uploadProgressLabel.Text = "Initializing compression algorithm";
                                Thread.Sleep(900); //give a little time for the labels to display (avoid flickering)
                                break;

                            case ZipProgressEventType.Saving_BeforeWriteEntry:
                                _uploadProgressLabel.Text = string.Format("Preparing to pack file {0}", Path.GetFileName(e.CurrentEntry.FileName));
                                Thread.Sleep(900); //give a little time for the labels to display (avoid flickering)
                                break;

                            case ZipProgressEventType.Saving_AfterWriteEntry:
                                _uploadProgressLabel.Text = string.Format("Finished packing file {0}", Path.GetFileName(e.CurrentEntry.FileName));
                                Thread.Sleep(900); //give a little time for the labels to display (avoid flickering)
                                break;

                            case ZipProgressEventType.Saving_Completed:
                                _uploadProgressLabel.Text = string.Format("Successfully compressed files to single file {0}", Path.GetFileName(e.ArchiveName));
                                Thread.Sleep(900); //give a little time for the labels to display (avoid flickering)
                                break;

                            case ZipProgressEventType.Saving_EntryBytesRead:
                                _uploadProgress.Value = (int)((((double)e.BytesTransferred / (double)e.TotalBytesToTransfer) * 100.0));
                                _uploadProgressLabel.Text = string.Format("{0}. {1,2:D} % Complete", e.CurrentEntry.Comment, (int)((((double)e.BytesTransferred / (double)e.TotalBytesToTransfer) * 100.0)));
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { onPackProgress(sender, e); }));
            }
        }

 #region " Old Pack Progress Code "
        //current progress, fileindex, filelength
        //void onPackProgress(int percentDone, int fileIndex, int totalFiles)
        //{
        //    if (!InvokeRequired)
        //    {
        //        _serverPromptLabel.Text = "Compressing selected files";
        //        _uploadProgress.Value = percentDone;
        //        _uploadProgressLabel.Text = string.Format("Packing file {0} of {1}. {2,3:D} % Complete", fileIndex, totalFiles, percentDone);
        //    }
        //    else
        //    {
        //        Invoke(new MethodInvoker(delegate() { onPackProgress(percentDone, fileIndex, totalFiles); }));
        //    }
        //}

        #endregion

        //----------------------------------------------------------------------------------------------------------------------------
        //  ReportResult
        /// <summary>
        /// Use this to do a thread safe update of the upload result
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void reportResult(bool passed, string errorString)
        {
            if (!InvokeRequired)
            {
                _exitBtn.Enabled = true; 

                if (passed)
                {
                    _uploadProgressLabel.Text = "Upload Complete.";
                    _uploadBtn.Visible = false;
                    DialogResult = DialogResult.OK; 
                }
                else
                {
                    _uploadProgressLabel.Text = string.Format("Upload Failed. {0}", errorString);
                    _uploadBtn.Text = "Retry";
                    _uploadBtn.Enabled = true;
                }
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { reportResult(passed, errorString); }));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  setControlText
        /// <summary>
        /// Use this to do a thread safe update of the control text
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void setControlText(Control control, string text)
        {
            if (!InvokeRequired)
            {
                control.Text = text; 
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { setControlText(control, text); }));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  Oga checkbox
        /// <summary>
        /// sets properties of form controls based on value
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void _uploadOgaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SelectOGAFileForPublishing();
        }

        private void SelectOGAFileForPublishing()
        {
            //set a default path and file name if there is a current meeting opened
            string fullName = string.Empty;

            if (Meeting.Current.Agenda.Count != 0)
            {
                fullName = Meeting.Current.MeetingFilename;
            }
            else
            {
                fullName = string.Empty;
            }

            //set the properties of the controls based on the value of the check box
            if (this._uploadOgaCheckBox.Checked)
            {
                this._ogaPathTextBox.Text = fullName;
            }
            else
            {
                this._ogaPathTextBox.Text = string.Empty;
            }

            this._ogaFileLabel.Enabled = !this._ogaFileLabel.Enabled;
            this._ogaPathTextBox.Enabled = !this._ogaPathTextBox.Enabled;
            this._locationOgaFileBtn.Enabled = !this._locationOgaFileBtn.Enabled;
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  Wmv checkbox
        /// <summary>
        /// sets properties of form controls based on value
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void _uploadWmvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SelectVideoFileForPublishing();
        }

        private void SelectVideoFileForPublishing()
        {
            //set a default path and file name if there is a current meeting opened
            string fullName = string.Empty;

            if (Meeting.Current.Agenda.Count != 0)
            {
                fullName = Meeting.Current.VideoFilename;
            }
            else
            {
                fullName = string.Empty;
            }

            //set the properties of the controls based on the value of the check box
            if (this._uploadWmvCheckBox.Checked)
            {
                this._wmvPathTextBox.Text = fullName;
            }
            else
            {
                this._wmvPathTextBox.Text = string.Empty;
            }

            this._wmvFileLabel.Enabled = !this._wmvFileLabel.Enabled;
            this._wmvPathTextBox.Enabled = !this._wmvPathTextBox.Enabled;
            this._locationWmvFileBtn.Enabled = !this._locationWmvFileBtn.Enabled;
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  Oga open file button
        /// <summary>
        /// allows user to browse for agenda file
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
        private void _locationOgaFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Agenda files (*.oga)|*.oga";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this._ogaPathTextBox.Text = ofd.FileName;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  Wmv open file button
        /// <summary>
        /// allows user to browse for video file
        /// </summary>
        //----------------------------------------------------------------------------------------------------------------------------
      
        private void _locationWmvFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video files (*.wmv)|*.wmv";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this._wmvPathTextBox.Text = ofd.FileName;
            }
        }

        private void _ogaPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(_ogaPathTextBox.Text))
            {
                XDocument xdoc = XDocument.Load(_ogaPathTextBox.Text);
                string targetVideoFile = xdoc.XPathSelectElement("meeting/filename").Value;
                targetVideoFile = Path.Combine(Meeting.Current.FilePath, targetVideoFile) + ".wmv";
                if (File.Exists(targetVideoFile))
                    _wmvPathTextBox.Text = targetVideoFile;

            }
        }

        private void frmAutoPublish_Load(object sender, EventArgs e)
        {
            SelectOGAFileForPublishing();
            SelectVideoFileForPublishing();
        }
    }
}
