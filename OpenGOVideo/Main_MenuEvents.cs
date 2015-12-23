using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace OpenGOVideo
{
    public partial class Main
    {

        private ManualResetEvent WAITFORNEWFILEHANDLE;
        string _agendaText = string.Empty;



        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            if (Meeting.Current.Agenda.Count == 0)
            {
                MessageBox.Show("Can't save an empty agenda!");
                return;
            }
            TreeNode[] output = new TreeNode[treeview.Nodes.Count];
            int x = 0;

            foreach (TreeNode child in treeview.Nodes)
            {
                output[x] = child;
                x++;
            }

            AgendaFromTree();
            Meeting.Current.AgendaTree = output;

            if (Meeting.Current.SaveToXML())
                MessageBox.Show("Meeting file saved!");
            else
                MessageBox.Show("An error prevented the meeting file from being saved.");

        }
            

       

        private void ObtainNewAgendaFile()
        {
            //treeview.Nodes.Clear();
            //Meeting.Current.Agenda = new BindingList<AgendaItem>();

            WAITFORNEWFILEHANDLE = new ManualResetEvent(false);
                this.Cursor = Cursors.AppStarting;
                if (Meeting.Current.Agenda.Count > 0)
                {
                    DialogResult ask =
                            MessageBox.Show("You have an agenda file loaded.  Continuing will overwrite it.  Continue?", "OpenGOVideo",
                                                MessageBoxButtons.YesNo);

                    if (ask == DialogResult.Yes)
                    {
                        treeview.Nodes.Clear();
                        Meeting.Current.Agenda.Clear();
                        _agendaText = string.Empty;
                    }
                    else
                        return;
                }
                
                //open the agenda files form and let the user choose the file they want
                frmGetAgendaFile frm = new frmGetAgendaFile(Meeting.Current.Org);
                string targetFile = string.Empty;
                if (frm.ShowDialog(this) == DialogResult.Cancel)
                    return;
                targetFile = frm.TargetFile;

                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadGetNewAgendaFile),targetFile);
                WAITFORNEWFILEHANDLE.WaitOne(30000);

                if (_agendaText != string.Empty)
                {
                    Meeting.Current.LoadFromXML(_agendaText,targetFile);
                    this.treeview.Nodes.AddRange(Meeting.Current.AgendaTree);
                    treeview.ExpandAll();
                    _lastHash = ComputeHash(Meeting.Current.GetAgendaXML());

                }
                else
                    throw new Exception("Unable to download new agenda file. "
                                        + "Please check your network settings and try again or load one from the file system.");


           
        }

        private void ThreadGetNewAgendaFile(Object State)
        {
            try
            {
                string targetFile = (string)State;
                StorageService.StorageServiceSoapClient x = new OpenGOVideo.StorageService.StorageServiceSoapClient();
                _agendaText = x.GetAgendaFileFromWebServer(Meeting.Current.Org.City, Meeting.Current.Org.State, Meeting.Current.Org.Board,targetFile);
            }
            catch (Exception ex)
            {

                //Log this
                string message = ex.Message;
            }
            finally
            {
                WAITFORNEWFILEHANDLE.Set();

            }
           
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
            about.Activate();
        }

        // TODO: Refactor and add error handling for failed saves
        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                VerboseSave();            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
            

       

     

       
    }
}
