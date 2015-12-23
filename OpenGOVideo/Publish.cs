using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.FTP;

namespace OpenGOVideo
{
    public partial class Publish : Form
    {
        public Publish()
        {
            InitializeComponent();
        }
        
        private void btnUpload_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            lblStatus.Text = "Uploading... please wait.";
            btnClose.Enabled = false;

            FTPclient ftp = new FTPclient(txtServer.Text, txtUser.Text, txtPass.Text)
            {
                CurrentDirectory = "/uploads/"
            };

            ftp.ReportProgress += this.UpdateProgress;
            ftp.Upload(Meeting.Current.VideoFilename, Meeting.Current.VideoFilename);

        }

        private void UpdateProgress(int percent)
        {
            if ((progressBar.Value = percent) >= 99)
            {
                lblStatus.Text = "Upload complete!";
                btnClose.Enabled = true;
            }
            else
            {
                System.Threading.Thread.Sleep(0);
                lblStatus.Text = String.Format("Uploading... ({0} % done)", percent.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Main.ActiveForm.Focus();
        }

        private void Publish_Load(object sender, EventArgs e)
        {
            if (Meeting.Current.Org.IsConfigured)
            {
                txtServer.Text = Meeting.Current.Org.FTPServer.Host;
                txtUser.Text = Meeting.Current.Org.FTPServer.Username;
                txtPass.Text = Meeting.Current.Org.FTPServer.Password;
                txtDirectory.Text = Meeting.Current.Org.FTPServer.Dir;
            }
        }
    }
}
