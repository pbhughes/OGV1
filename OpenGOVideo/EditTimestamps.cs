using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using th = System.Threading;
using System.Text.RegularExpressions;


namespace OpenGOVideo
{
    public partial class EditTimestamps : Form
    {

        public string filename { get; set; }
        public string curdir { get; set; }
        private string targetURL;

        private DateTime start = new DateTime();

       

        public EditTimestamps()
        {
            InitializeComponent();
            wmpViewer.settings.autoStart = false;
            filename = Meeting.Current.VideoFilename;
            curdir = System.IO.Directory.GetCurrentDirectory();
        }

        
        private void EditTimestamps_Load(object sender, EventArgs e)
        {
            /*foreach (TreeNode originalNode in ((treeView)(CaptureTest.ActiveForm.Controls["treeView"])).Nodes)
            {
                TreeNode newNode = new TreeNode(originalNode.Text);
                newNode.Tag = originalNode.Tag;
                this.treeView1.Nodes.Add(newNode);
                IterateTreeNodes(originalNode, newNode);
            }*/
            this.treeView1.Nodes.AddRange(Meeting.Current.LoadTreeFromAgenda(Meeting.Current.Agenda.Where(a => a.parent_id==0).ToArray<AgendaItem>()));
            reloadVideo();
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reloadVideo();
        }

        private void reloadVideo()
        {
            string path = Path.Combine(curdir, filename);
            if (System.IO.File.Exists(path))
            {

                wmpViewer.URL = path;

                             
            }
            else
            {
                DialogResult dr = MessageBox.Show("Associated file " + filename + " not found. Would you like to manually locate it?", "File not found", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                OpenFileDialog ofd = new OpenFileDialog()
                {
                    DefaultExt = ".oga",
                    InitialDirectory = Meeting.Current.FilePath
                };
                //TODO: Check for cancel
                DialogResult file = ofd.ShowDialog();
                if (file == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

                wmpViewer.URL = ofd.FileName;

            }

        }

        private void cmdStamp_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                try
                {
                    wmpViewer.Ctlcontrols.pause();

                    int ai_id = (int)treeView1.SelectedNode.Tag;
                    
                    Timestamp ts = new Timestamp
                    {
                        frame = (long)wmpViewer.Ctlcontrols.currentPosition * (long)29.997,
                        position = System.TimeSpan.FromSeconds(wmpViewer.Ctlcontrols.currentPosition - (double)Meeting.Current.Org.offset)
                    };
                    
                    Meeting.Current.Agenda.Where(a => a.id == ai_id).First().timestamp = ts;
                    string title = Meeting.Current.Agenda.Where(i => i.id == ai_id).First().title;

                    treeView1.SelectedNode.Text = "[" + ts.position.ToPrettyTimeStamp() + "]  " + title;
                    treeView1.SelectedNode.BackColor = Color.Blue;
                    treeView1.SelectedNode.ForeColor = Color.White;
                    
                    Meeting.Current.change = true;
                    Meeting.Current.editwindow = true;
                    Meeting.Current.editwindowlocation = this.Location;

                    wmpViewer.Ctlcontrols.play();

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void IterateTreeNodes(TreeNode originalNode, TreeNode rootNode)
        {
            foreach (TreeNode childNode in originalNode.Nodes)
            {

                TreeNode newNode = new TreeNode(childNode.Text);
                newNode.Tag = childNode.Tag;
                this.treeView1.SelectedNode = rootNode;
                this.treeView1.SelectedNode.Nodes.Add(newNode);
                IterateTreeNodes(childNode, newNode);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null)
                {
                    int ai_id = Convert.ToInt32(treeView1.SelectedNode.Name);
                    wmpViewer.Ctlcontrols.currentPosition = Meeting.Current.Agenda.Where(a => a.id == ai_id).First().timestamp.position.TotalSeconds;
                }
            }
            catch(Exception ex)
            { 

            }
        }
        // The ItemDrag event is called when the item drag begins. Here is
        // where you can perform any tracking, or validate if the drag
        // operation should occur, and so on.
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sourceNode = (TreeNode)e.Item;
            DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);
        }

        // Define the event that occurs while the dragging happens
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
        //Coppy all children and add them to a new node

        private TreeNode children(TreeNode parent)
        {
            TreeNode node = new TreeNode(parent.Text, parent.ImageIndex, parent.SelectedImageIndex)
            {
                ForeColor = parent.ForeColor,
                BackColor = parent.BackColor,
                Tag = parent.Tag
            };

            foreach (TreeNode child in parent.Nodes)
                node.Nodes.Add(children(child));

            return node;
        }
        // Determine what node in the tree we are dropping on to (target),
        // copy the drag source (sourceNode), make the new node and delete
        // the old one.
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
               DialogResult dlgResult = MessageBox.Show("Do you want to move selected Item", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (dlgResult == DialogResult.Yes)
               {
                   Meeting.Current.change = true;
                   Point pos = treeView1.PointToClient(new Point(e.X, e.Y));
                   TreeNode targetNode = treeView1.GetNodeAt(pos);
                   TreeNode nodeCopy;

                   if (targetNode != null)
                   {
                       nodeCopy = new TreeNode(sourceNode.Text, sourceNode.ImageIndex, sourceNode.SelectedImageIndex)
                       {
                           Tag = sourceNode.Tag,
                           ForeColor = sourceNode.ForeColor,
                           BackColor = sourceNode.BackColor
                       };
                    
                       foreach (TreeNode child in sourceNode.Nodes)
                       {
                           // nodeCopy.Nodes.Add(new TreeNode(child.Text, child.ImageIndex, child.SelectedImageIndex));
                           nodeCopy.Nodes.Add(children(child));
                       }


                       if (sourceNode.Index > targetNode.Index)
                       {
                           if (targetNode.Parent == null)
                           {
                               treeView1.Nodes.Insert(targetNode.Index, nodeCopy);
                               treeView1.Nodes.Remove(sourceNode);
                           }
                           else
                           {
                               targetNode.Parent.Nodes.Insert(targetNode.Index, nodeCopy);
                               sourceNode.Remove();
                           }
                       }
                       else
                       {
                           if (targetNode.Parent == null)
                           {
                               treeView1.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                               treeView1.Nodes.Remove(sourceNode);
                           }
                           else
                           {
                               targetNode.Parent.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                               sourceNode.Remove();
                           }
                       }

                       treeView1.Invalidate();
                       AgendaFromTree();
                   }
               }
        }

        private void btnexpand_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private void btncollaps_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes != null)
                treeView1.CollapseAll();
        }

        // TODO: This seems to be a duplicate of something on the Main form
        private void treeView1_Dragover(object sender, DragEventArgs e)
        {
            Point pos = treeView1.PointToClient(new Point(e.X, e.Y));
           // treeView1.SelectedNode = treeView1.GetNodeAt(pos);
            // Set a constant to define the autoscroll region

            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = treeView1.PointToClient(Cursor.Position);

            // See if we need to scroll up or down
            if ((pt.Y + scrollRegion) > treeView1.Height)
            {
                // Call the API to scroll down
                SendMessage(treeView1.Handle, (int)277, (int)1, 0);
            }
            else if (pt.Y < (treeView1.Top + scrollRegion))
            {
                // Call thje API to scroll up
                SendMessage(treeView1.Handle, (int)277, (int)0, 0);
            }
        }
        //used to convert the current tree strut to agenda list to save
        public void AgendaFromTree()
        {
            Meeting.Current.Agendatemp = new BindingList<AgendaItem>();
            
            foreach (TreeNode child in treeView1.Nodes)
                Meeting.Current.AgendaChild(child);

            //treeView1.Nodes.Clear();
            Meeting.Current.Agenda = Meeting.Current.Agendatemp;
            Meeting.Current.Agendatemp = new BindingList<AgendaItem>();
          //  this.treeView1.Nodes.AddRange(Meeting.Current.LoadTreeFromAgenda(Meeting.Current.Agenda.Where(a => a.parent_id == 0).ToArray<AgendaItem>()));
          //  if (treeView1.Nodes.Count > 0)
       //     {
       //         treeView1.ExpandAll();
       //         treeView1.SelectedNode = treeView1.Nodes[0];
          //  }
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (wmpViewer.URL == "")
                    wmpViewer.URL = targetURL;

                wmpViewer.Ctlcontrols.play();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cmdPause_Click(object sender, EventArgs e)
        {
            try
            {
                wmpViewer.Ctlcontrols.pause();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdFastForward_Click(object sender, EventArgs e)
        {
            try
            {
                wmpViewer.Ctlcontrols.fastForward();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cmdRewind_Click(object sender, EventArgs e)
        {
            try
            {
                wmpViewer.Ctlcontrols.fastReverse();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                               
            }
            catch (Exception ex)
            {

                ;
            }
        }

        private void wmpViewer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                if (wmpViewer.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    ;
                if (wmpViewer.playState == WMPLib.WMPPlayState.wmppsPaused)
                    ;
                if (wmpViewer.playState == WMPLib.WMPPlayState.wmppsStopped)
                    ;
            }
            catch (Exception ex)
            {

                ;
            }
        }

        private void EditTimestamps_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (wmpViewer.playState != WMPLib.WMPPlayState.wmppsStopped)
                    wmpViewer.Ctlcontrols.stop();
            }
            catch (Exception ex)
            {
                
                ;
            }
        }
        /// <summary>
        /// Attempt to change the text on the time stamp button to re-time stamp
        /// if there had been a time stamp employed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string pattern = @"\[*\]";   //A bracket any number of characters and a bracket
                Regex x = new Regex(pattern);
                string text = e.Node.ToString();
                if (x.Matches(text).Count > 0)
                    cmdStamp.Text = "Re-Stamp";
                else
                    cmdStamp.Text = "Time Stamp";

            }
            catch (Exception ex)
            {

                ;
            }
           
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {

        }

        

    }
}
