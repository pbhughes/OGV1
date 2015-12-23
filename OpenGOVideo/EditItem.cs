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
    public partial class EditItem : Form
    {
        public EditItem()
        {
            InitializeComponent();
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            int ai_id = (int)Meeting.Current.parentnode.Tag;
            txtName.Text = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().title;
            txtdescr.Text=(string)Meeting.Current.parentnode.ToolTipText;
        
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int ai_id = (int)Meeting.Current.parentnode.Tag;
            Meeting.Current.Agenda.Where(i => i.id == ai_id).First().title = txtName.Text;
            Meeting.Current.parentnode.ToolTipText = txtdescr.Text;
            Meeting.Current.Agenda.Where(i => i.id == ai_id).First().desc = (string)Meeting.Current.parentnode.ToolTipText;
            if (Meeting.Current.Agenda.Where(i => i.id == ai_id).First().timestamp != null)
            {
                Meeting.Current.parentnode.Text = String.Concat("[", prettyTimeStamp(Meeting.Current.Agenda.Where(i => i.id == ai_id).First().timestamp.position), "]  ", txtName.Text);
            }
            else
            {
                Meeting.Current.parentnode.Text = txtName.Text;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }
        public static string prettyTimeStamp(TimeSpan ts)
        {
            int seconds = ts.Seconds;
            int hours = ts.Hours;
            int minutes = ts.Minutes;
            return String.Concat(
                hours.ToString(),
                ":",
                minutes.ToString().PadLeft(2, '0'),
                ":",
                seconds.ToString().PadLeft(2, '0')
            );
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
