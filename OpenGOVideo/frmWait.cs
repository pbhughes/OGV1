using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenGOVideo
{
    public partial class frmWait : Form
    {
        public delegate void delUpdateMessage(string Message);
        private Timer _t = new Timer();

        public frmWait()
        {
            InitializeComponent();
            Main.StatusChanged += new Main.delStatuschanged(Main_StatusChanged);
            _t.Interval = 33000;
            _t.Tick += new EventHandler(_t_Tick);
            _t.Start();
        }

        void _t_Tick(object sender, EventArgs e)
        {
            _t.Stop();
            MessageBox.Show("Failed to complete file recording and video streaming setup in an "
                + "acceptable amount of time.  Please check your network connectivity.", "Setup Error.", MessageBoxButtons.OK);
            this.DialogResult = DialogResult.Cancel;
        }

        void Main_StatusChanged(string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    delUpdateMessage x = new delUpdateMessage(Main_StatusChanged);
                    this.BeginInvoke(x, message);

                }
                else
                {
                    txtMessage.Text = message;
                    if (message.Contains("Video Streaming Ready"))
                    {
                        this.DialogResult = DialogResult.OK;
                       
                    }
                    if (message.ToLower().Contains("error."))
                    {
                        this.DialogResult = DialogResult.Cancel;
                        
                    }
                    _t.Stop();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
