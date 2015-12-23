using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Expression.Encoder.Live;

namespace OpenGOVideo
{
    public partial class AdvancedDeviceConfig : Form
    {
        public AdvancedDeviceConfig()
        {
            InitializeComponent();
        }

        private void AdvancedDeviceConfig_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Meeting.Current.Job.DeviceSources[0].GetSupportedConfigurationDialogs();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            System.Runtime.InteropServices.HandleRef h = new System.Runtime.InteropServices.HandleRef(this, this.Handle);
            Meeting.Current.Job.DeviceSources[0].ShowConfigurationDialog((ConfigurationDialog)comboBox1.SelectedValue, h);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
