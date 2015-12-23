using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestAgnedaServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AgendaServices.StorageService x = new TestAgnedaServices.AgendaServices.StorageService();
                AgendaServices.AuthenTokenHeader token = new TestAgnedaServices.AgendaServices.AuthenTokenHeader();
                token.Token = "7734879GPDIE3IEUDKDKLCJDK";
                x.AuthenTokenHeaderValue = token;
                string payload = string.Empty;
                while (System.Text.Encoding.ASCII.GetByteCount(payload) < 2097152)
                {
                    payload += "cCKDKDKDKkdkdkdkdkdkdkdkdkdkdkdkdCKCKCKCKCKCKCKCKCKCKCKCKCKCKCKCKcCKDKDKDKkdkdkdkdkdkdkdkdkdkdkdkdCKCKCKCKCKCKCKCKCKCKCKCKCKCKCKCKcCKDKDKDKkdkdkdkdkdkdkdkdkdkdkdkdCKCKCKCKCKCKCKCKCKCKCKCKCKCKCKCK";
                }


                bool result = false;
                x.Timeout = -1;
                result = x.SaveAgendaFile(payload, "test5.oga");
                if (result == true)
                    MessageBox.Show("Success");
                else
                    MessageBox.Show("Failure");

            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
