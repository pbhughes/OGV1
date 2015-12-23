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
    public partial class LoadingSplash : Form
    {
        //private 

        public LoadingSplash()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void LoadingSplash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        protected override void  OnPaint(PaintEventArgs e)
        {
            //Do nothing here!
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
 	        //base.OnPaintBackground(e);
            Graphics gfx = pevent.Graphics;
            gfx.DrawImage(Properties.Resources.splash_logo, new Rectangle(0, 0, this.Width, this.Height));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value++ > (progressBar1.Maximum - 3))
            {
                progressBar1.Value = progressBar1.Maximum;
                timer1.Stop();
                this.Close();
            }
            /*
            OnPaintBackground(new PaintEventArgs(
                Graphics.FromHwnd(this.Handle),
                Rectangle.Empty
            ));
            */
        }

    }
}
