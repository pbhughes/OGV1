using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OpenGOVideo
{
    public partial class frmGetAgendaFile : Form
    {
        Organization _Org;

        public frmGetAgendaFile(Organization Org)
        {
            _Org = Org;
            InitializeComponent();
            GetAgendaFiles();
        }

        private void GetAgendaFiles()
        {
            StorageService.StorageServiceSoapClient x = new OpenGOVideo.StorageService.StorageServiceSoapClient();
            this.bindingSource1.DataSource = x.GetAvailableAgendaFilesAndDates(_Org.City, _Org.State, _Org.Board);

            
        }

        public string TargetFile { 
            get
            {
                string fileName = this.ultraGrid1.Selected.Rows[0].Cells["FileName"].Text;
                if (fileName == "")
                    throw new Exception("Agenda file name not selected.");
                return fileName;

            }
        }

        private void cboFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmGetAgendaFile_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        
    }
}
