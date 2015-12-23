namespace OpenGOVideo
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Devices");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Encoding");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Recording", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.treeSections = new System.Windows.Forms.TreeView();
            this.flowPanelBase = new System.Windows.Forms.FlowLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.flowPanelDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.lblVideo = new System.Windows.Forms.Label();
            this.cboVideoDevices = new System.Windows.Forms.ComboBox();
            this.lblAudio = new System.Windows.Forms.Label();
            this.cboAudioDevices = new System.Windows.Forms.ComboBox();
            this.flowPanelEncoding = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBitRate = new System.Windows.Forms.Label();
            this.numBitRate = new System.Windows.Forms.NumericUpDown();
            this.lblFrameRate = new System.Windows.Forms.Label();
            this.cboFrameRates = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.cboVideoSize = new System.Windows.Forms.ComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.ssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowPanelBase.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.flowPanelDevices.SuspendLayout();
            this.flowPanelEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBitRate)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeSections
            // 
            this.treeSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeSections.FullRowSelect = true;
            this.treeSections.HotTracking = true;
            this.treeSections.Location = new System.Drawing.Point(5, 5);
            this.treeSections.Margin = new System.Windows.Forms.Padding(5);
            this.treeSections.Name = "treeSections";
            treeNode1.Name = "nodeRecordingDevices";
            treeNode1.Tag = "Devices";
            treeNode1.Text = "Devices";
            treeNode2.Name = "nodeRecordingEncoding";
            treeNode2.Tag = "Encoding";
            treeNode2.Text = "Encoding";
            treeNode3.Name = "nodeRecording";
            treeNode3.Text = "Recording";
            this.treeSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeSections.Size = new System.Drawing.Size(157, 335);
            this.treeSections.TabIndex = 0;
            this.treeSections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSections_AfterSelect);
            this.treeSections.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeSections_NodeMouseClick);
            // 
            // flowPanelBase
            // 
            this.flowPanelBase.Controls.Add(this.treeSections);
            this.flowPanelBase.Controls.Add(this.panelLeft);
            this.flowPanelBase.Controls.Add(this.statusStrip);
            this.flowPanelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelBase.Location = new System.Drawing.Point(0, 0);
            this.flowPanelBase.Name = "flowPanelBase";
            this.flowPanelBase.Size = new System.Drawing.Size(721, 369);
            this.flowPanelBase.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.cmdCancel);
            this.panelLeft.Controls.Add(this.cmdOk);
            this.panelLeft.Controls.Add(this.flowPanelDevices);
            this.panelLeft.Controls.Add(this.flowPanelEncoding);
            this.panelLeft.Location = new System.Drawing.Point(172, 5);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(5);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(539, 335);
            this.panelLeft.TabIndex = 1;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(461, 309);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(370, 309);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 6;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // flowPanelDevices
            // 
            this.flowPanelDevices.Controls.Add(this.lblVideo);
            this.flowPanelDevices.Controls.Add(this.cboVideoDevices);
            this.flowPanelDevices.Controls.Add(this.lblAudio);
            this.flowPanelDevices.Controls.Add(this.cboAudioDevices);
            this.flowPanelDevices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelDevices.Location = new System.Drawing.Point(3, 7);
            this.flowPanelDevices.Name = "flowPanelDevices";
            this.flowPanelDevices.Padding = new System.Windows.Forms.Padding(10);
            this.flowPanelDevices.Size = new System.Drawing.Size(328, 109);
            this.flowPanelDevices.TabIndex = 0;
            this.flowPanelDevices.Visible = false;
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideo.Location = new System.Drawing.Point(13, 10);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(163, 20);
            this.lblVideo.TabIndex = 0;
            this.lblVideo.Text = "Video Capture Device";
            // 
            // cboVideoDevices
            // 
            this.cboVideoDevices.DisplayMember = "Name";
            this.cboVideoDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVideoDevices.FormattingEnabled = true;
            this.cboVideoDevices.Location = new System.Drawing.Point(13, 33);
            this.cboVideoDevices.Name = "cboVideoDevices";
            this.cboVideoDevices.Size = new System.Drawing.Size(280, 28);
            this.cboVideoDevices.TabIndex = 1;
            this.cboVideoDevices.ValueMember = "Name";
            this.cboVideoDevices.SelectedIndexChanged += new System.EventHandler(this.cboVideoDevices_SelectedIndexChanged);
            // 
            // lblAudio
            // 
            this.lblAudio.AutoSize = true;
            this.lblAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudio.Location = new System.Drawing.Point(13, 64);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(163, 20);
            this.lblAudio.TabIndex = 2;
            this.lblAudio.Text = "Audio Capture Device";
            // 
            // cboAudioDevices
            // 
            this.cboAudioDevices.DisplayMember = "Name";
            this.cboAudioDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAudioDevices.FormattingEnabled = true;
            this.cboAudioDevices.Location = new System.Drawing.Point(299, 13);
            this.cboAudioDevices.Name = "cboAudioDevices";
            this.cboAudioDevices.Size = new System.Drawing.Size(280, 28);
            this.cboAudioDevices.TabIndex = 3;
            this.cboAudioDevices.ValueMember = "Name";
            this.cboAudioDevices.SelectedIndexChanged += new System.EventHandler(this.cboAudioDevices_SelectedIndexChanged);
            // 
            // flowPanelEncoding
            // 
            this.flowPanelEncoding.Controls.Add(this.lblBitRate);
            this.flowPanelEncoding.Controls.Add(this.numBitRate);
            this.flowPanelEncoding.Controls.Add(this.lblFrameRate);
            this.flowPanelEncoding.Controls.Add(this.cboFrameRates);
            this.flowPanelEncoding.Controls.Add(this.lblSize);
            this.flowPanelEncoding.Controls.Add(this.cboVideoSize);
            this.flowPanelEncoding.Location = new System.Drawing.Point(3, 122);
            this.flowPanelEncoding.Name = "flowPanelEncoding";
            this.flowPanelEncoding.Padding = new System.Windows.Forms.Padding(10);
            this.flowPanelEncoding.Size = new System.Drawing.Size(517, 174);
            this.flowPanelEncoding.TabIndex = 4;
            this.flowPanelEncoding.Visible = false;
            // 
            // lblBitRate
            // 
            this.lblBitRate.AutoSize = true;
            this.lblBitRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBitRate.Location = new System.Drawing.Point(17, 17);
            this.lblBitRate.Margin = new System.Windows.Forms.Padding(7, 7, 23, 10);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(67, 20);
            this.lblBitRate.TabIndex = 0;
            this.lblBitRate.Text = "Bit Rate";
            // 
            // numBitRate
            // 
            this.numBitRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBitRate.Location = new System.Drawing.Point(110, 13);
            this.numBitRate.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numBitRate.Minimum = new decimal(new int[] {
            126,
            0,
            0,
            0});
            this.numBitRate.Name = "numBitRate";
            this.numBitRate.Size = new System.Drawing.Size(120, 26);
            this.numBitRate.TabIndex = 1;
            this.numBitRate.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numBitRate.ValueChanged += new System.EventHandler(this.numBitRate_ValueChanged);
            // 
            // lblFrameRate
            // 
            this.lblFrameRate.AutoSize = true;
            this.lblFrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrameRate.Location = new System.Drawing.Point(240, 17);
            this.lblFrameRate.Margin = new System.Windows.Forms.Padding(7);
            this.lblFrameRate.Name = "lblFrameRate";
            this.lblFrameRate.Size = new System.Drawing.Size(94, 20);
            this.lblFrameRate.TabIndex = 2;
            this.lblFrameRate.Text = "Frame Rate";
            // 
            // cboFrameRates
            // 
            this.cboFrameRates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFrameRates.FormattingEnabled = true;
            this.cboFrameRates.Location = new System.Drawing.Point(344, 13);
            this.cboFrameRates.Name = "cboFrameRates";
            this.cboFrameRates.Size = new System.Drawing.Size(119, 28);
            this.cboFrameRates.TabIndex = 3;
            this.cboFrameRates.SelectedIndexChanged += new System.EventHandler(this.cboFrameRates_SelectedIndexChanged);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(17, 54);
            this.lblSize.Margin = new System.Windows.Forms.Padding(7, 7, 6, 10);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(85, 20);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Video Size";
            // 
            // cboVideoSize
            // 
            this.cboVideoSize.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cboVideoSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVideoSize.FormattingEnabled = true;
            this.cboVideoSize.Location = new System.Drawing.Point(111, 50);
            this.cboVideoSize.Name = "cboVideoSize";
            this.cboVideoSize.Size = new System.Drawing.Size(119, 28);
            this.cboVideoSize.TabIndex = 5;
            this.cboVideoSize.SelectedIndexChanged += new System.EventHandler(this.cboVideoSize_SelectedIndexChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 345);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(48, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // ssStatus
            // 
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 369);
            this.Controls.Add(this.flowPanelBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.flowPanelBase.ResumeLayout(false);
            this.flowPanelBase.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.flowPanelDevices.ResumeLayout(false);
            this.flowPanelDevices.PerformLayout();
            this.flowPanelEncoding.ResumeLayout(false);
            this.flowPanelEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBitRate)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeSections;
        private System.Windows.Forms.FlowLayoutPanel flowPanelBase;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.FlowLayoutPanel flowPanelDevices;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.ComboBox cboVideoDevices;
        private System.Windows.Forms.Label lblAudio;
        private System.Windows.Forms.ComboBox cboAudioDevices;
        private System.Windows.Forms.FlowLayoutPanel flowPanelEncoding;
        private System.Windows.Forms.Label lblBitRate;
        private System.Windows.Forms.NumericUpDown numBitRate;
        private System.Windows.Forms.Label lblFrameRate;
        private System.Windows.Forms.ComboBox cboFrameRates;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ComboBox cboVideoSize;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ssStatus;
    }
}