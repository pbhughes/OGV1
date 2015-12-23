using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OpenGOVideo
{
    public partial class frmAlert : Form
    {       

        public frmAlert(string Message)
        {
            InitializeComponent();
            txtMessage.Text = Message;
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void frmAlert_Load(object sender, EventArgs e)
        {

            txtMessage.SelectedText = "";

        }
    }
}
