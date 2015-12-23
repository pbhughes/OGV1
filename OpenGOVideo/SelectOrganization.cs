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

namespace OpenGOVideo
{
    public partial class SelectOrganization : Form
    {
        public SelectOrganization()
        {
            InitializeComponent();
        }

        private void SelectOrganization_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFromXML(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Organizations.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void LoadFromXML(string filename)
        {
            try
            {
                if(!File.Exists(filename))
                {
                    DialogResult loadOther = MessageBox.Show(this, "Organization settings file not found.  Would you like to manually locate it?", "File not found", MessageBoxButtons.YesNo);
                    if(loadOther == DialogResult.No)
                        throw new ApplicationException();
                
                    OpenFileDialog ofd = new OpenFileDialog()
                    {
                        InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath),
                        Filter = ".xml",
                        Multiselect = false
                    };
                    DialogResult choseOtherFile = ofd.ShowDialog();
                    
                    if(choseOtherFile == DialogResult.Cancel)
                        throw new ApplicationException();
                    
                    if(!File.Exists(ofd.FileName))
                        throw new ApplicationException();
                }
            }
            catch (Exception ex)
            {
                if(!String.IsNullOrEmpty(ex.Message))
                    MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            var organizations = new System.Collections.ObjectModel.Collection<Organization>();

            try
            {
                XDocument xmlDoc = XDocument.Load(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid or corrupt settings file.  Will continue with the default settings.");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            try
            {
                XDocument xmlDoc = XDocument.Load(filename);
                foreach (var org in xmlDoc.Descendants("org"))
                {
                    try
                    {
                        organizations.Add(new Organization(org));
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex) { }

            switch(organizations.Count())
            {
                case 0:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;

                case 1:
                    Meeting.Current.Org = organizations.First();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                default:
                    cmbOrganization.Items.Clear();
                    cmbOrganization.Items.AddRange(organizations.ToArray());
                    cmbOrganization.SelectedIndex = 0;
                    break;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Meeting.Current.Org = (Organization)cmbOrganization.SelectedItem;
        }

        private void cmbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrganization.SelectedItem != null)
                btnOK.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
