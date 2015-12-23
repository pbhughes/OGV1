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
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtdescr.Text != "" || txtdescr.Text != " ")
            {
                AgendaItem tmpAI = new AgendaItem
                {
                    id = ++Meeting.Current.lastGivenID,
                    parent_id = int.Parse(Meeting.Current.parentnode.Tag.ToString()),
                    title = txtName.Text,
                    desc = txtdescr.Text,
                    timestamp = null
                };
                Meeting.Current.newnode = new TreeNode
                {
                    Tag = Meeting.Current.lastGivenID,
                    Text = txtName.Text,
                    ToolTipText = txtdescr.Text,
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Name = Meeting.Current.lastGivenID.ToString()
                };
                Meeting.Current.Agenda.Add(tmpAI);
                this.Close();
            }

        }
    }
}
