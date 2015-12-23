using System.Windows.Forms;
using System.Runtime.InteropServices;
using System;
namespace OpenGOVideo
{
    partial class EditTimestamps
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        // using System.Runtime.InteropServices;

        [DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam,

            int lParam);

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTimestamps));
            this.cmdReloadVideoFile = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.wmpViewer = new AxWMPLib.AxWindowsMediaPlayer();
            this.btncollaps = new System.Windows.Forms.Button();
            this.btnexpand = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCtrl = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdStamp = new System.Windows.Forms.Button();
            this.cmdDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wmpViewer)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlCtrl.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReloadVideoFile
            // 
            this.cmdReloadVideoFile.Location = new System.Drawing.Point(104, 3);
            this.cmdReloadVideoFile.Name = "cmdReloadVideoFile";
            this.cmdReloadVideoFile.Size = new System.Drawing.Size(95, 39);
            this.cmdReloadVideoFile.TabIndex = 2;
            this.cmdReloadVideoFile.Text = "Reload Video File";
            this.cmdReloadVideoFile.UseVisualStyleBackColor = true;
            this.cmdReloadVideoFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(319, 3);
            this.treeView1.Name = "treeView1";
            this.tableLayoutPanel1.SetRowSpan(this.treeView1, 2);
            this.treeView1.Size = new System.Drawing.Size(470, 560);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_Dragover);
            // 
            // wmpViewer
            // 
            this.wmpViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmpViewer.Enabled = true;
            this.wmpViewer.Location = new System.Drawing.Point(3, 3);
            this.wmpViewer.Name = "wmpViewer";
            this.wmpViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpViewer.OcxState")));
            this.wmpViewer.Size = new System.Drawing.Size(310, 384);
            this.wmpViewer.TabIndex = 1;
            this.wmpViewer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmpViewer_PlayStateChange);
            // 
            // btncollaps
            // 
            this.btncollaps.Location = new System.Drawing.Point(205, 3);
            this.btncollaps.Name = "btncollaps";
            this.btncollaps.Size = new System.Drawing.Size(23, 39);
            this.btncollaps.TabIndex = 31;
            this.btncollaps.Text = "-";
            this.toolTip2.SetToolTip(this.btncollaps, "collapse Tree");
            this.btncollaps.UseVisualStyleBackColor = true;
            this.btncollaps.Click += new System.EventHandler(this.btncollaps_Click);
            // 
            // btnexpand
            // 
            this.btnexpand.Location = new System.Drawing.Point(234, 3);
            this.btnexpand.Name = "btnexpand";
            this.btnexpand.Size = new System.Drawing.Size(23, 39);
            this.btnexpand.TabIndex = 30;
            this.btnexpand.Text = "+";
            this.toolTip1.SetToolTip(this.btnexpand, "Expand Tree");
            this.btnexpand.UseVisualStyleBackColor = true;
            this.btnexpand.Click += new System.EventHandler(this.btnexpand_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.wmpViewer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCtrl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // pnlCtrl
            // 
            this.pnlCtrl.Controls.Add(this.flowLayoutPanel1);
            this.pnlCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtrl.Location = new System.Drawing.Point(3, 393);
            this.pnlCtrl.Name = "pnlCtrl";
            this.pnlCtrl.Size = new System.Drawing.Size(310, 170);
            this.pnlCtrl.TabIndex = 33;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdStamp);
            this.flowLayoutPanel1.Controls.Add(this.cmdReloadVideoFile);
            this.flowLayoutPanel1.Controls.Add(this.btncollaps);
            this.flowLayoutPanel1.Controls.Add(this.btnexpand);
            this.flowLayoutPanel1.Controls.Add(this.cmdDone);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(310, 170);
            this.flowLayoutPanel1.TabIndex = 32;
            // 
            // cmdStamp
            // 
            this.cmdStamp.Location = new System.Drawing.Point(3, 3);
            this.cmdStamp.Name = "cmdStamp";
            this.cmdStamp.Size = new System.Drawing.Size(95, 39);
            this.cmdStamp.TabIndex = 32;
            this.cmdStamp.Text = "Time Stamp";
            this.cmdStamp.UseVisualStyleBackColor = true;
            this.cmdStamp.Click += new System.EventHandler(this.cmdStamp_Click);
            // 
            // cmdDone
            // 
            this.cmdDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdDone.Location = new System.Drawing.Point(3, 48);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Size = new System.Drawing.Size(95, 39);
            this.cmdDone.TabIndex = 33;
            this.cmdDone.Text = "Finished";
            this.cmdDone.UseVisualStyleBackColor = true;
            // 
            // EditTimestamps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditTimestamps";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Edit Time Stamps";
            this.Load += new System.EventHandler(this.EditTimestamps_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTimestamps_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.wmpViewer)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlCtrl.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer wmpViewer;
        private System.Windows.Forms.Button cmdReloadVideoFile;
        private System.Windows.Forms.TreeView treeView1;
        private TreeNode sourceNode;
        private Button btncollaps;
        private Button btnexpand;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlCtrl;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button cmdStamp;
        private Button cmdDone; 
    }
}