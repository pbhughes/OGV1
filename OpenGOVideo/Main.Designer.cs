namespace OpenGOVideo
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mneFileNewAgenda = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.mnuPublishToFTP = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditBoard = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuEditSettings = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.recordingStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dropcapStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.curPathStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel_Preview = new System.Windows.Forms.Panel();
            this.treeview = new System.Windows.Forms.TreeView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutBase = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutAgenda = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdSaveAgenda = new System.Windows.Forms.Button();
            this.cmdEditAgendaItem = new System.Windows.Forms.Button();
            this.cmdEditSession = new System.Windows.Forms.Button();
            this.cmdAddItem = new System.Windows.Forms.Button();
            this.cmdDeleteAgendaItem = new System.Windows.Forms.Button();
            this.cmdTimeStamp = new System.Windows.Forms.Button();
            this.pnlLink = new System.Windows.Forms.Panel();
            this.cmdMoveUp = new System.Windows.Forms.Button();
            this.cmdMoveNodeDown = new System.Windows.Forms.Button();
            this.lblLink = new System.Windows.Forms.LinkLabel();
            this.cmdVideoStatus = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutDisplaySettings = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkfile = new System.Windows.Forms.CheckBox();
            this.chkstream = new System.Windows.Forms.CheckBox();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.cmdStartRecording = new System.Windows.Forms.Button();
            this.imgListRecordingPics = new System.Windows.Forms.ImageList(this.components);
            this.cmdStopRecording = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdFileStreaming = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.tableLayoutBase.SuspendLayout();
            this.flowLayoutAgenda.SuspendLayout();
            this.pnlLink.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutDisplaySettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem [ ] {
            this.mnuFile,
            this.mnuEdit,
            this.menuItem3});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem [ ] {
            this.mneFileNewAgenda,
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.menuItem12,
            this.mnuPublishToFTP,
            this.menuItem9,
            this.mnuExit});
            this.mnuFile.Text = "&File";
            // 
            // mneFileNewAgenda
            // 
            this.mneFileNewAgenda.Index = 0;
            this.mneFileNewAgenda.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mneFileNewAgenda.Text = "&Get Agenda File";
            this.mneFileNewAgenda.Click += new System.EventHandler(this.menuFileNewAgenda_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.mnuFileOpen.Text = "&Open Agenda File";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Enabled = false;
            this.mnuFileSave.Index = 2;
            this.mnuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.mnuFileSave.Text = "&Save Agenda File";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Enabled = false;
            this.mnuFileSaveAs.Index = 3;
            this.mnuFileSaveAs.Text = "Save Agenda File &As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.menuItem12.Text = "-";
            // 
            // mnuPublishToFTP
            // 
            this.mnuPublishToFTP.Index = 5;
            this.mnuPublishToFTP.Text = "&Publish to Server";
            this.mnuPublishToFTP.Click += new System.EventHandler(this.mnuPublishToFTP_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 6;
            this.menuItem9.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 7;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem [ ] {
            this.mnuEditBoard,
            this.menuItem8,
            this.mnuEditSettings});
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditBoard
            // 
            this.mnuEditBoard.Index = 0;
            this.mnuEditBoard.Text = "&Select Board";
            this.mnuEditBoard.Click += new System.EventHandler(this.mnuEditOrganization_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "-";
            // 
            // mnuEditSettings
            // 
            this.mnuEditSettings.Index = 2;
            this.mnuEditSettings.Text = "&Settings";
            this.mnuEditSettings.Click += new System.EventHandler(this.mnuEditSettings_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem [ ] {
            this.menuItem6});
            this.menuItem3.Text = "&Help";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "&About";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "OpenGov Agenda|*.OGA";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem [ ] {
            this.recordingStatusLabel,
            this.dropcapStatusLabel,
            this.toolStripStatusLabel1,
            this.curPathStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 514);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1210, 22);
            this.statusStrip.TabIndex = 36;
            this.statusStrip.Text = "statusStrip1";
            // 
            // recordingStatusLabel
            // 
            this.recordingStatusLabel.Name = "recordingStatusLabel";
            this.recordingStatusLabel.Size = new System.Drawing.Size(84, 17);
            this.recordingStatusLabel.Text = "Status: Inactive";
            // 
            // dropcapStatusLabel
            // 
            this.dropcapStatusLabel.Name = "dropcapStatusLabel";
            this.dropcapStatusLabel.Size = new System.Drawing.Size(121, 17);
            this.dropcapStatusLabel.Text = "0 Dropped / 0 Captured";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(871, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // curPathStatusLabel
            // 
            this.curPathStatusLabel.Name = "curPathStatusLabel";
            this.curPathStatusLabel.Size = new System.Drawing.Size(119, 17);
            this.curPathStatusLabel.Text = "Current Path: unknown";
            // 
            // Panel_Preview
            // 
            this.Panel_Preview.AutoSize = true;
            this.Panel_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Preview.Location = new System.Drawing.Point(0, 0);
            this.Panel_Preview.Name = "Panel_Preview";
            this.Panel_Preview.Size = new System.Drawing.Size(603, 392);
            this.Panel_Preview.TabIndex = 35;
            this.Panel_Preview.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Preview_Paint);
            // 
            // treeview
            // 
            this.treeview.AllowDrop = true;
            this.treeview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.treeview.HideSelection = false;
            this.treeview.Location = new System.Drawing.Point(0, 0);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(599, 392);
            this.treeview.TabIndex = 36;
            this.treeview.DoubleClick += new System.EventHandler(this.btnAddTimestamp_Click);
            this.treeview.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeview_DragDrop);
            this.treeview.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeview_DragEnter);
            this.treeview.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeview_ItemDrag);
            this.treeview.DragOver += new System.Windows.Forms.DragEventHandler(this.treeview_Dragover);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "OpenGov Agenda|*.OGA";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Window|*.WMV";
            // 
            // tableLayoutBase
            // 
            this.tableLayoutBase.ColumnCount = 1;
            this.tableLayoutBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1212F));
            this.tableLayoutBase.Controls.Add(this.flowLayoutAgenda, 0, 1);
            this.tableLayoutBase.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutBase.Controls.Add(this.flowLayoutDisplaySettings, 0, 2);
            this.tableLayoutBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBase.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutBase.Name = "tableLayoutBase";
            this.tableLayoutBase.RowCount = 3;
            this.tableLayoutBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutBase.Size = new System.Drawing.Size(1210, 514);
            this.tableLayoutBase.TabIndex = 0;
            // 
            // flowLayoutAgenda
            // 
            this.flowLayoutAgenda.Controls.Add(this.cmdSaveAgenda);
            this.flowLayoutAgenda.Controls.Add(this.cmdEditAgendaItem);
            this.flowLayoutAgenda.Controls.Add(this.cmdEditSession);
            this.flowLayoutAgenda.Controls.Add(this.cmdAddItem);
            this.flowLayoutAgenda.Controls.Add(this.cmdDeleteAgendaItem);
            this.flowLayoutAgenda.Controls.Add(this.cmdTimeStamp);
            this.flowLayoutAgenda.Controls.Add(this.pnlLink);
            this.flowLayoutAgenda.Controls.Add(this.cmdVideoStatus);
            this.flowLayoutAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutAgenda.Location = new System.Drawing.Point(3, 401);
            this.flowLayoutAgenda.Name = "flowLayoutAgenda";
            this.flowLayoutAgenda.Size = new System.Drawing.Size(1206, 53);
            this.flowLayoutAgenda.TabIndex = 43;
            // 
            // cmdSaveAgenda
            // 
            this.cmdSaveAgenda.Enabled = false;
            this.cmdSaveAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdSaveAgenda.Location = new System.Drawing.Point(3, 3);
            this.cmdSaveAgenda.Name = "cmdSaveAgenda";
            this.cmdSaveAgenda.Padding = new System.Windows.Forms.Padding(5);
            this.cmdSaveAgenda.Size = new System.Drawing.Size(116, 50);
            this.cmdSaveAgenda.TabIndex = 43;
            this.cmdSaveAgenda.Text = "Save Agenda";
            this.cmdSaveAgenda.UseVisualStyleBackColor = true;
            this.cmdSaveAgenda.Click += new System.EventHandler(this.cmdSaveAgenda_Click);
            // 
            // cmdEditAgendaItem
            // 
            this.cmdEditAgendaItem.Enabled = false;
            this.cmdEditAgendaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdEditAgendaItem.Image = global::OpenGOVideo.Properties.Resources.tools;
            this.cmdEditAgendaItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEditAgendaItem.Location = new System.Drawing.Point(125, 3);
            this.cmdEditAgendaItem.Name = "cmdEditAgendaItem";
            this.cmdEditAgendaItem.Padding = new System.Windows.Forms.Padding(5);
            this.cmdEditAgendaItem.Size = new System.Drawing.Size(116, 50);
            this.cmdEditAgendaItem.TabIndex = 51;
            this.cmdEditAgendaItem.Text = "Edit Agenda Item";
            this.cmdEditAgendaItem.UseVisualStyleBackColor = true;
            this.cmdEditAgendaItem.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // cmdEditSession
            // 
            this.cmdEditSession.Enabled = false;
            this.cmdEditSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdEditSession.Image = global::OpenGOVideo.Properties.Resources.tools;
            this.cmdEditSession.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEditSession.Location = new System.Drawing.Point(247, 3);
            this.cmdEditSession.Name = "cmdEditSession";
            this.cmdEditSession.Padding = new System.Windows.Forms.Padding(5);
            this.cmdEditSession.Size = new System.Drawing.Size(116, 50);
            this.cmdEditSession.TabIndex = 48;
            this.cmdEditSession.Text = "Edit Time Stamps";
            this.cmdEditSession.UseVisualStyleBackColor = true;
            this.cmdEditSession.Click += new System.EventHandler(this.cmdEditSession_Click);
            // 
            // cmdAddItem
            // 
            this.cmdAddItem.Enabled = false;
            this.cmdAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdAddItem.Image = ( ( System.Drawing.Image ) ( resources.GetObject("cmdAddItem.Image") ) );
            this.cmdAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAddItem.Location = new System.Drawing.Point(369, 3);
            this.cmdAddItem.Name = "cmdAddItem";
            this.cmdAddItem.Padding = new System.Windows.Forms.Padding(5);
            this.cmdAddItem.Size = new System.Drawing.Size(116, 50);
            this.cmdAddItem.TabIndex = 52;
            this.cmdAddItem.Text = "Add Agenda Item";
            this.cmdAddItem.UseVisualStyleBackColor = true;
            this.cmdAddItem.Click += new System.EventHandler(this.cmdAddItem_Click);
            // 
            // cmdDeleteAgendaItem
            // 
            this.cmdDeleteAgendaItem.Enabled = false;
            this.cmdDeleteAgendaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdDeleteAgendaItem.Image = ( ( System.Drawing.Image ) ( resources.GetObject("cmdDeleteAgendaItem.Image") ) );
            this.cmdDeleteAgendaItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDeleteAgendaItem.Location = new System.Drawing.Point(491, 3);
            this.cmdDeleteAgendaItem.Name = "cmdDeleteAgendaItem";
            this.cmdDeleteAgendaItem.Padding = new System.Windows.Forms.Padding(5);
            this.cmdDeleteAgendaItem.Size = new System.Drawing.Size(116, 50);
            this.cmdDeleteAgendaItem.TabIndex = 53;
            this.cmdDeleteAgendaItem.Text = "Delte Agenda Item";
            this.cmdDeleteAgendaItem.UseVisualStyleBackColor = true;
            this.cmdDeleteAgendaItem.Click += new System.EventHandler(this.cmdDeleteAgendaItem_Click);
            // 
            // cmdTimeStamp
            // 
            this.cmdTimeStamp.Enabled = false;
            this.cmdTimeStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdTimeStamp.Location = new System.Drawing.Point(613, 3);
            this.cmdTimeStamp.Name = "cmdTimeStamp";
            this.cmdTimeStamp.Padding = new System.Windows.Forms.Padding(5);
            this.cmdTimeStamp.Size = new System.Drawing.Size(116, 50);
            this.cmdTimeStamp.TabIndex = 42;
            this.cmdTimeStamp.Text = "Time Stamp";
            this.cmdTimeStamp.UseVisualStyleBackColor = true;
            this.cmdTimeStamp.Click += new System.EventHandler(this.cmdTimeStamp_Click);
            // 
            // pnlLink
            // 
            this.pnlLink.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlLink.Controls.Add(this.cmdMoveUp);
            this.pnlLink.Controls.Add(this.cmdMoveNodeDown);
            this.pnlLink.Controls.Add(this.lblLink);
            this.pnlLink.Location = new System.Drawing.Point(735, 3);
            this.pnlLink.Name = "pnlLink";
            this.pnlLink.Size = new System.Drawing.Size(215, 50);
            this.pnlLink.TabIndex = 48;
            this.pnlLink.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLink_Paint);
            // 
            // cmdMoveUp
            // 
            this.cmdMoveUp.BackgroundImage = global::OpenGOVideo.Properties.Resources.moveUp;
            this.cmdMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdMoveUp.Location = new System.Drawing.Point(96, 9);
            this.cmdMoveUp.Name = "cmdMoveUp";
            this.cmdMoveUp.Padding = new System.Windows.Forms.Padding(5);
            this.cmdMoveUp.Size = new System.Drawing.Size(55, 33);
            this.cmdMoveUp.TabIndex = 46;
            this.cmdMoveUp.UseVisualStyleBackColor = true;
            this.cmdMoveUp.Click += new System.EventHandler(this.cmdMoveUp_Click);
            // 
            // cmdMoveNodeDown
            // 
            this.cmdMoveNodeDown.BackgroundImage = global::OpenGOVideo.Properties.Resources.moveDown;
            this.cmdMoveNodeDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdMoveNodeDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdMoveNodeDown.Location = new System.Drawing.Point(157, 9);
            this.cmdMoveNodeDown.Name = "cmdMoveNodeDown";
            this.cmdMoveNodeDown.Padding = new System.Windows.Forms.Padding(5);
            this.cmdMoveNodeDown.Size = new System.Drawing.Size(55, 33);
            this.cmdMoveNodeDown.TabIndex = 45;
            this.cmdMoveNodeDown.UseVisualStyleBackColor = true;
            this.cmdMoveNodeDown.Click += new System.EventHandler(this.cmdMoveNodeDown_Click);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(13, 21);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(0, 13);
            this.lblLink.TabIndex = 0;
            this.lblLink.Visible = false;
            this.lblLink.Click += new System.EventHandler(this.lblLink_Click);
            // 
            // cmdVideoStatus
            // 
            this.cmdVideoStatus.BackColor = System.Drawing.Color.FromArgb(( ( int ) ( ( ( byte ) ( 255 ) ) ) ), ( ( int ) ( ( ( byte ) ( 255 ) ) ) ), ( ( int ) ( ( ( byte ) ( 128 ) ) ) ));
            this.cmdVideoStatus.Location = new System.Drawing.Point(956, 3);
            this.cmdVideoStatus.Name = "cmdVideoStatus";
            this.cmdVideoStatus.Size = new System.Drawing.Size(140, 31);
            this.cmdVideoStatus.TabIndex = 1;
            this.cmdVideoStatus.Text = "Video Streaming";
            this.cmdVideoStatus.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Panel_Preview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeview);
            this.splitContainer1.Size = new System.Drawing.Size(1206, 392);
            this.splitContainer1.SplitterDistance = 603;
            this.splitContainer1.TabIndex = 37;
            // 
            // flowLayoutDisplaySettings
            // 
            this.flowLayoutDisplaySettings.Controls.Add(this.groupBox3);
            this.flowLayoutDisplaySettings.Controls.Add(this.txtDuration);
            this.flowLayoutDisplaySettings.Controls.Add(this.cmdStartRecording);
            this.flowLayoutDisplaySettings.Controls.Add(this.cmdStopRecording);
            this.flowLayoutDisplaySettings.Controls.Add(this.panel1);
            this.flowLayoutDisplaySettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutDisplaySettings.Location = new System.Drawing.Point(3, 460);
            this.flowLayoutDisplaySettings.Name = "flowLayoutDisplaySettings";
            this.flowLayoutDisplaySettings.Size = new System.Drawing.Size(1206, 51);
            this.flowLayoutDisplaySettings.TabIndex = 45;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkfile);
            this.groupBox3.Controls.Add(this.chkstream);
            this.groupBox3.Controls.Add(this.chkPreview);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 40);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // chkfile
            // 
            this.chkfile.AutoSize = true;
            this.chkfile.Checked = true;
            this.chkfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkfile.Enabled = false;
            this.chkfile.Location = new System.Drawing.Point(129, 13);
            this.chkfile.Name = "chkfile";
            this.chkfile.Size = new System.Drawing.Size(42, 17);
            this.chkfile.TabIndex = 37;
            this.chkfile.Text = "File";
            this.chkfile.UseVisualStyleBackColor = true;
            // 
            // chkstream
            // 
            this.chkstream.AutoSize = true;
            this.chkstream.Checked = true;
            this.chkstream.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkstream.Location = new System.Drawing.Point(6, 13);
            this.chkstream.Name = "chkstream";
            this.chkstream.Size = new System.Drawing.Size(59, 17);
            this.chkstream.TabIndex = 36;
            this.chkstream.Text = "Stream";
            this.chkstream.UseVisualStyleBackColor = true;
            this.chkstream.CheckedChanged += new System.EventHandler(this.chkstream_CheckedChanged);
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Enabled = false;
            this.chkPreview.Location = new System.Drawing.Point(65, 13);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(64, 17);
            this.chkPreview.TabIndex = 38;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // txtDuration
            // 
            this.txtDuration.BackColor = System.Drawing.Color.Green;
            this.txtDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.txtDuration.ForeColor = System.Drawing.Color.White;
            this.txtDuration.Location = new System.Drawing.Point(192, 4);
            this.txtDuration.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.txtDuration.MaxLength = 8;
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size(226, 29);
            this.txtDuration.TabIndex = 32;
            this.txtDuration.TabStop = false;
            this.txtDuration.Text = "0:00:00";
            this.txtDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdStartRecording
            // 
            this.cmdStartRecording.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdStartRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdStartRecording.ImageIndex = 0;
            this.cmdStartRecording.ImageList = this.imgListRecordingPics;
            this.cmdStartRecording.Location = new System.Drawing.Point(424, 3);
            this.cmdStartRecording.Name = "cmdStartRecording";
            this.cmdStartRecording.Padding = new System.Windows.Forms.Padding(5);
            this.cmdStartRecording.Size = new System.Drawing.Size(180, 40);
            this.cmdStartRecording.TabIndex = 46;
            this.cmdStartRecording.Text = "Record";
            this.cmdStartRecording.UseVisualStyleBackColor = true;
            this.cmdStartRecording.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // imgListRecordingPics
            // 
            this.imgListRecordingPics.ImageStream = ( ( System.Windows.Forms.ImageListStreamer ) ( resources.GetObject("imgListRecordingPics.ImageStream") ) );
            this.imgListRecordingPics.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListRecordingPics.Images.SetKeyName(0, "RecordNormal.png");
            this.imgListRecordingPics.Images.SetKeyName(1, "Stop1Normal.png");
            // 
            // cmdStopRecording
            // 
            this.cmdStopRecording.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
            this.cmdStopRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdStopRecording.ImageIndex = 1;
            this.cmdStopRecording.ImageList = this.imgListRecordingPics;
            this.cmdStopRecording.Location = new System.Drawing.Point(610, 3);
            this.cmdStopRecording.Name = "cmdStopRecording";
            this.cmdStopRecording.Padding = new System.Windows.Forms.Padding(5);
            this.cmdStopRecording.Size = new System.Drawing.Size(180, 40);
            this.cmdStopRecording.TabIndex = 47;
            this.cmdStopRecording.Text = "Stop";
            this.cmdStopRecording.UseVisualStyleBackColor = true;
            this.cmdStopRecording.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdFileStreaming);
            this.panel1.Location = new System.Drawing.Point(796, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 49);
            this.panel1.TabIndex = 48;
            // 
            // cmdFileStreaming
            // 
            this.cmdFileStreaming.BackColor = System.Drawing.Color.FromArgb(( ( int ) ( ( ( byte ) ( 255 ) ) ) ), ( ( int ) ( ( ( byte ) ( 255 ) ) ) ), ( ( int ) ( ( ( byte ) ( 128 ) ) ) ));
            this.cmdFileStreaming.Location = new System.Drawing.Point(160, 3);
            this.cmdFileStreaming.Name = "cmdFileStreaming";
            this.cmdFileStreaming.Size = new System.Drawing.Size(140, 31);
            this.cmdFileStreaming.TabIndex = 2;
            this.cmdFileStreaming.Text = "File Recording";
            this.cmdFileStreaming.UseVisualStyleBackColor = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1210, 536);
            this.Controls.Add(this.tableLayoutBase);
            this.Controls.Add(this.statusStrip);
            this.Enabled = false;
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject("$this.Icon") ) );
            this.Menu = this.mainMenu;
            this.Name = "Main";
            this.Text = "OpenGOVideo (Alpha 1.1)";
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutBase.ResumeLayout(false);
            this.flowLayoutAgenda.ResumeLayout(false);
            this.pnlLink.ResumeLayout(false);
            this.pnlLink.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutDisplaySettings.ResumeLayout(false);
            this.flowLayoutDisplaySettings.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mneFileNewAgenda;
        private System.Windows.Forms.MenuItem mnuFileOpen;
        private System.Windows.Forms.MenuItem mnuFileSave;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem mnuPublishToFTP;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel recordingStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel dropcapStatusLabel;
        private System.Windows.Forms.MenuItem mnuFileSaveAs;
        private System.Windows.Forms.Panel Panel_Preview;
        public System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuEditSettings;
        private System.Windows.Forms.MenuItem mnuEditBoard;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.ToolStripStatusLabel curPathStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBase;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutAgenda;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkfile;
        private System.Windows.Forms.CheckBox chkstream;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cmdSaveAgenda;
        private System.Windows.Forms.Button cmdTimeStamp;
        private System.Windows.Forms.Button cmdStartRecording;
        private System.Windows.Forms.Button cmdStopRecording;
        private System.Windows.Forms.Button cmdEditSession;
        private System.Windows.Forms.Button cmdEditAgendaItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutDisplaySettings;
        private System.Windows.Forms.ImageList imgListRecordingPics;
        private System.Windows.Forms.Panel pnlLink;
        private System.Windows.Forms.LinkLabel lblLink;
        private System.Windows.Forms.Button cmdVideoStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdFileStreaming;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdMoveNodeDown;
        private System.Windows.Forms.Button cmdMoveUp;
        private System.Windows.Forms.Button cmdAddItem;
        private System.Windows.Forms.Button cmdDeleteAgendaItem;
    }
}

