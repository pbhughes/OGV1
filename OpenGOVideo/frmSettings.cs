using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Expression.Encoder.Live;
using Profiles = Microsoft.Expression.Encoder.Profiles;
using Encoder = Microsoft.Expression.Encoder;
using System.IO;
using System.Management;
using System.Management.Instrumentation;



namespace OpenGOVideo
{
    public delegate void DelReloadComboBoxes(object sender, EventArrivedEventArgs e);

    public partial class frmSettings : Form
    {

        string queryString =@"SELECT * 
                    FROM __InstanceOperationEvent
                    WITHIN 1
                    WHERE 
                    TargetInstance ISA 'Win32_USBHub'";

        ManagementEventWatcher usbWatcher;
        SettingsCache cache = new SettingsCache();
        bool _LoadComplete = false;

        public frmSettings()
        {
            InitializeComponent();
            usbWatcher = new ManagementEventWatcher(queryString);
            usbWatcher.EventArrived += new EventArrivedEventHandler(usbWatcher_EventArrived);
            usbWatcher.Start();
        }

        void usbWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            

            if (this.InvokeRequired)
            {
                DelReloadComboBoxes del = new DelReloadComboBoxes(usbWatcher_EventArrived);
                object[] state = { sender, e };
                Invoke(del, state);

            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    int iterations = 0;
                    while ((Meeting.Current.Job.VideoDevices.Count == 0) && (iterations < 3))
                    {
                        System.Threading.Thread.Sleep(5000);
                        iterations++;
                    }

                    
                    LoadDeviceOptions();
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

        }


        #region Properties and Fields

        decimal[] frameRates = new decimal[] { 5M, 12M, 15M, 20M, 23.976M, 24M, 25M, 29.97M, 30M };
        public SettingsCache SettingsCache {
            get
            {
                return cache;
            }
            set
            {
                cache = value;
            }
        }

        #endregion

        #region Events Handlers

        private void frmOptions_Load(object sender, EventArgs e)
        {


            LoadDeviceOptions();

            treeSections.ExpandAll();

            for (int i = 0; i < frameRates.Length; i++)
            {
                cboFrameRates.Items.Add(frameRates[i]);
            }
            cboFrameRates.SelectedItem = cboFrameRates.Items[cboFrameRates.Items.Count - 1];

            //// Get saved preferences
            //{
            //    var matchingAudioSource = cboAudioDevices.Items.Cast<LiveDevice>().Where(a => a.Name == Properties.Settings.Default.Audio);
            //    if (matchingAudioSource.Count() > 0)
            //        cboAudioDevices.SelectedItem = matchingAudioSource.First();

            //    var matchingVideoSource = cboVideoDevices.Items.Cast<LiveDevice>().Where(v => v.Name == Properties.Settings.Default.Video);
            //    if (matchingVideoSource.Count() > 0)
            //        cboVideoDevices.SelectedItem = matchingVideoSource.First();
            //}


            // Video Dimensions
            cboVideoSize.Items.AddRange(
                new object[] {
                        new Size(320,240),
                        new Size(480,360),
                        new Size(640,480),
                        new Size(720,540)
                    }
            );

            cboVideoSize.SelectedItem = cboVideoSize.Items[0];

            treeSections.SelectedNode = treeSections.Nodes[0].Nodes[0];

            if (File.Exists(Path.Combine(OpenGovStatics.AppDataPath(), CacheManager.SETTINGFILENAME)))
            {
                cache = CacheManager.ReadSettings<SettingsCache>();
                cboFrameRates.SelectedItem = cache.FrameRate;
                cboVideoSize.SelectedItem = cache.VideoSize;

                if (cboVideoDevices.Items.Count >= 1)
                {
                    foreach (LiveDevice liv in cboVideoDevices.Items)
                        if (liv.Name == cache.PreferedVideoDeviceName)
                            cboVideoDevices.SelectedItem = liv;

                }
                else
                {
                    foreach (LiveDevice x in Meeting.Current.Job.VideoDevices)
                    {
                        if (x.Name == cache.PreferedVideoDeviceName)
                        {

                            foreach (LiveDevice y in cboVideoDevices.Items)
                            {
                                if (y.Name == x.Name)
                                    cboVideoDevices.SelectedItem = y;
                            }
                        }
                    }
                }



                if (cboAudioDevices.Items.Count >= 1)
                    foreach (LiveDevice liv in cboAudioDevices.Items)
                        if (liv.Name == cache.PreferedAudioDeviceName)
                            cboAudioDevices.SelectedItem = liv;
                else
                {
                    foreach (LiveDevice x in Meeting.Current.Job.AudioDevices)
                    {
                        if (x.Name == cache.PreferedAudioDeviceName)
                        {

                            foreach (LiveDevice y in cboAudioDevices.Items)
                            {
                                if (y.Name == x.Name)
                                    cboAudioDevices.SelectedItem = y;
                            }
                        }
                    }
                }

                if (cboAudioDevices.Items.Count > 0 && cboAudioDevices.SelectedItem == null)
                    cboAudioDevices.SelectedItem = cboAudioDevices.Items[0];

                if (cboVideoDevices.Items.Count > 0 && cboVideoDevices.SelectedItem == null)
                    cboVideoDevices.SelectedItem = cboVideoDevices.Items[0];

                if (cboVideoDevices.Items.Count == 0)
                    ssStatus.Text = "No video capture devices attached, please plug in or turn on your camera.";


            }

            _LoadComplete = true;

        }

        private void LoadDeviceOptions()
        {
            cboVideoDevices.Items.Clear();
            foreach (LiveDevice x in Meeting.Current.Job.VideoDevices)
            {
                cboVideoDevices.Items.Add(x);
            }
            cboVideoDevices.Refresh();

            cboAudioDevices.Items.Clear();
            foreach (LiveDevice x in Meeting.Current.Job.AudioDevices)
            {
                cboAudioDevices.Items.Add(x);
            }
            cboAudioDevices.Refresh();

            if (cboVideoDevices.Items.Count == 0)
                ssStatus.Text = "No video capture devices attached, please plug in or turn on your camera.";
            else
                ssStatus.Text = "";

        }

        private void cboVideoDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;
        }

        private void cboAudioDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;
        }

        private void treeSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                 SetCurrentOptionsPanel(e.Node);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void treeSections_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {

                SetCurrentOptionsPanel(e.Node);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void SetCurrentOptionsPanel(TreeNode e)
        {
           
                if (e.Tag == null)
                    return;

                switch (e.Tag.ToString())
                {
                    case "Encoding":
                        flowPanelEncoding.Visible = true;
                        flowPanelEncoding.Dock = DockStyle.Fill;
                        flowPanelDevices.Visible = false;
                        break;
                    case "Devices":
                        flowPanelDevices.Visible = true;
                        flowPanelDevices.Dock = DockStyle.Fill;
                        flowPanelEncoding.Visible = false;
                        break;
                    case "Settings":

                        break;
                    default:
                        throw new Exception("Unkown settings entry.");
                        break;
                }

            
           
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Meeting.Current.Status == RecordingStatus.Recording)
                {
                    MessageBox.Show("Setting changes will not be applied while recording is in progress.", "OpenGOVideo");
                    this.Close();
                    return;
                }

                if (cboVideoDevices.Items.Count == 0)
                {
                    MessageBox.Show("No video capture devices found.  You must have a video capture device installed.","OpenGOVideo", MessageBoxButtons.OK);
                    return;
                }

                if (cboAudioDevices.Items.Count > 0 && cboAudioDevices.SelectedItem == null)
                    cboAudioDevices.SelectedItem = cboAudioDevices.Items[0];

                if (cboVideoDevices.Items.Count > 0 && cboVideoDevices.SelectedItem == null)
                    cboVideoDevices.SelectedItem = cboVideoDevices.Items[0];


                cache.PreferedAudioDeviceName = ((Microsoft.Expression.Encoder.Live.LiveDevice)cboAudioDevices.SelectedItem).Name;
                cache.PreferedVideoDeviceName = ((Microsoft.Expression.Encoder.Live.LiveDevice)cboVideoDevices.SelectedItem).Name;

                cache.BitRate = (int)numBitRate.Value;
                if (cboFrameRates.SelectedItem == null)
                {
                    MessageBox.Show("Please choose a frame rate on the Encoder settings page.", "OpenGOVideo", MessageBoxButtons.OK);
                    return;
                }

                cache.FrameRate = decimal.Parse(cboFrameRates.SelectedItem.ToString());
                if (cboVideoSize.SelectedItem == null)
                {
                    MessageBox.Show("Please choose a video size from the Encoder settings page.", "OpenGOVideo", MessageBoxButtons.OK);
                    return;
                }


                cache.VideoSize = (Size)cboVideoSize.SelectedItem;
                cache.LastAccess = DateTime.Now;

                CacheManager.CacheSettings<SettingsCache>(cache);
                DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       

        private void ApplySettingsToCurrentJob()
        {
            
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void checkBoxFile_CheckedChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;
        }

        private void checkBoxStream_CheckedChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;

        }

        private void numBitRate_ValueChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;

        }

        private void cboFrameRates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_LoadComplete)
                cache.IsDirty = true;

        }

        private void cboVideoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_LoadComplete)
                cache.IsDirty = true;

        }
        #endregion

        #region Functions and Methods
        private void AssembleCacheFile()
        {

        }

        private void DistributeCacheFile()
        {

        }
        #endregion

        private void flowPanelDevices_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Enumerations


       
        #endregion
    }

       
}
