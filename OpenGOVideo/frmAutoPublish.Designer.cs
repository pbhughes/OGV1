namespace OpenGOVideo
{
    partial class frmAutoPublish
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
            this._uploadProgress = new System.Windows.Forms.ProgressBar();
            this._uploadProgressLabel = new System.Windows.Forms.Label();
            this._serverPromptLabel = new System.Windows.Forms.Label();
            this._uploadBtn = new System.Windows.Forms.Button();
            this._exitBtn = new System.Windows.Forms.Button();
            this._ogaFileLabel = new System.Windows.Forms.Label();
            this._wmvFileLabel = new System.Windows.Forms.Label();
            this._ogaPathTextBox = new System.Windows.Forms.TextBox();
            this._wmvPathTextBox = new System.Windows.Forms.TextBox();
            this._uploadOgaCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._locationOgaFileBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._locationWmvFileBtn = new System.Windows.Forms.Button();
            this._uploadWmvCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _uploadProgress
            // 
            this._uploadProgress.Location = new System.Drawing.Point(12, 52);
            this._uploadProgress.Name = "_uploadProgress";
            this._uploadProgress.Size = new System.Drawing.Size(785, 15);
            this._uploadProgress.TabIndex = 0;
            this._uploadProgress.Visible = false;
            // 
            // _uploadProgressLabel
            // 
            this._uploadProgressLabel.Location = new System.Drawing.Point(12, 79);
            this._uploadProgressLabel.Name = "_uploadProgressLabel";
            this._uploadProgressLabel.Size = new System.Drawing.Size(785, 13);
            this._uploadProgressLabel.TabIndex = 1;
            this._uploadProgressLabel.Text = "Upload Progress";
            this._uploadProgressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._uploadProgressLabel.Visible = false;
            // 
            // _serverPromptLabel
            // 
            this._serverPromptLabel.Location = new System.Drawing.Point(12, 26);
            this._serverPromptLabel.Name = "_serverPromptLabel";
            this._serverPromptLabel.Size = new System.Drawing.Size(785, 13);
            this._serverPromptLabel.TabIndex = 3;
            this._serverPromptLabel.Text = "serverPrompt";
            this._serverPromptLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _uploadBtn
            // 
            this._uploadBtn.Location = new System.Drawing.Point(316, 116);
            this._uploadBtn.Name = "_uploadBtn";
            this._uploadBtn.Size = new System.Drawing.Size(87, 27);
            this._uploadBtn.TabIndex = 4;
            this._uploadBtn.Text = "Publish";
            this._uploadBtn.UseVisualStyleBackColor = true;
            this._uploadBtn.Click += new System.EventHandler(this.onUploadClicked);
            // 
            // _exitBtn
            // 
            this._exitBtn.Location = new System.Drawing.Point(409, 116);
            this._exitBtn.Name = "_exitBtn";
            this._exitBtn.Size = new System.Drawing.Size(87, 27);
            this._exitBtn.TabIndex = 5;
            this._exitBtn.Text = "Exit";
            this._exitBtn.UseVisualStyleBackColor = true;
            this._exitBtn.Click += new System.EventHandler(this.onExitClicked);
            // 
            // _ogaFileLabel
            // 
            this._ogaFileLabel.AutoSize = true;
            this._ogaFileLabel.Enabled = false;
            this._ogaFileLabel.Location = new System.Drawing.Point(32, 48);
            this._ogaFileLabel.Name = "_ogaFileLabel";
            this._ogaFileLabel.Size = new System.Drawing.Size(100, 13);
            this._ogaFileLabel.TabIndex = 6;
            this._ogaFileLabel.Text = "Location of oga file:";
            // 
            // _wmvFileLabel
            // 
            this._wmvFileLabel.AutoSize = true;
            this._wmvFileLabel.Enabled = false;
            this._wmvFileLabel.Location = new System.Drawing.Point(32, 48);
            this._wmvFileLabel.Name = "_wmvFileLabel";
            this._wmvFileLabel.Size = new System.Drawing.Size(104, 13);
            this._wmvFileLabel.TabIndex = 7;
            this._wmvFileLabel.Text = "Location of wmv file:";
            // 
            // _ogaPathTextBox
            // 
            this._ogaPathTextBox.Enabled = false;
            this._ogaPathTextBox.Location = new System.Drawing.Point(145, 48);
            this._ogaPathTextBox.Name = "_ogaPathTextBox";
            this._ogaPathTextBox.Size = new System.Drawing.Size(618, 20);
            this._ogaPathTextBox.TabIndex = 8;
            this._ogaPathTextBox.TextChanged += new System.EventHandler(this._ogaPathTextBox_TextChanged);
            // 
            // _wmvPathTextBox
            // 
            this._wmvPathTextBox.Enabled = false;
            this._wmvPathTextBox.Location = new System.Drawing.Point(145, 48);
            this._wmvPathTextBox.Name = "_wmvPathTextBox";
            this._wmvPathTextBox.Size = new System.Drawing.Size(618, 20);
            this._wmvPathTextBox.TabIndex = 9;
            // 
            // _uploadOgaCheckBox
            // 
            this._uploadOgaCheckBox.AutoSize = true;
            this._uploadOgaCheckBox.Checked = true;
            this._uploadOgaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._uploadOgaCheckBox.Location = new System.Drawing.Point(15, 19);
            this._uploadOgaCheckBox.Name = "_uploadOgaCheckBox";
            this._uploadOgaCheckBox.Size = new System.Drawing.Size(115, 17);
            this._uploadOgaCheckBox.TabIndex = 10;
            this._uploadOgaCheckBox.Text = "Publish agenda file";
            this._uploadOgaCheckBox.UseVisualStyleBackColor = true;
            this._uploadOgaCheckBox.CheckedChanged += new System.EventHandler(this._uploadOgaCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._locationOgaFileBtn);
            this.groupBox1.Controls.Add(this._uploadOgaCheckBox);
            this.groupBox1.Controls.Add(this._ogaPathTextBox);
            this.groupBox1.Controls.Add(this._ogaFileLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(812, 86);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // _locationOgaFileBtn
            // 
            this._locationOgaFileBtn.Enabled = false;
            this._locationOgaFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._locationOgaFileBtn.Image = global::OpenGOVideo.Properties.Resources.openHS;
            this._locationOgaFileBtn.Location = new System.Drawing.Point(769, 48);
            this._locationOgaFileBtn.Name = "_locationOgaFileBtn";
            this._locationOgaFileBtn.Size = new System.Drawing.Size(28, 20);
            this._locationOgaFileBtn.TabIndex = 12;
            this.toolTip1.SetToolTip(this._locationOgaFileBtn, "Browse for *.oga file to upload");
            this._locationOgaFileBtn.UseVisualStyleBackColor = true;
            this._locationOgaFileBtn.Click += new System.EventHandler(this._locationOgaFileBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._locationWmvFileBtn);
            this.groupBox2.Controls.Add(this._uploadWmvCheckBox);
            this.groupBox2.Controls.Add(this._wmvPathTextBox);
            this.groupBox2.Controls.Add(this._wmvFileLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(812, 86);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // _locationWmvFileBtn
            // 
            this._locationWmvFileBtn.Enabled = false;
            this._locationWmvFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._locationWmvFileBtn.Image = global::OpenGOVideo.Properties.Resources.openHS;
            this._locationWmvFileBtn.Location = new System.Drawing.Point(769, 48);
            this._locationWmvFileBtn.Name = "_locationWmvFileBtn";
            this._locationWmvFileBtn.Size = new System.Drawing.Size(28, 20);
            this._locationWmvFileBtn.TabIndex = 13;
            this.toolTip1.SetToolTip(this._locationWmvFileBtn, "Browse for *.wmv file to upload");
            this._locationWmvFileBtn.UseVisualStyleBackColor = true;
            this._locationWmvFileBtn.Click += new System.EventHandler(this._locationWmvFileBtn_Click);
            // 
            // _uploadWmvCheckBox
            // 
            this._uploadWmvCheckBox.AutoSize = true;
            this._uploadWmvCheckBox.Checked = true;
            this._uploadWmvCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._uploadWmvCheckBox.Location = new System.Drawing.Point(15, 19);
            this._uploadWmvCheckBox.Name = "_uploadWmvCheckBox";
            this._uploadWmvCheckBox.Size = new System.Drawing.Size(105, 17);
            this._uploadWmvCheckBox.TabIndex = 11;
            this._uploadWmvCheckBox.Text = "Publish video file";
            this._uploadWmvCheckBox.UseVisualStyleBackColor = true;
            this._uploadWmvCheckBox.CheckedChanged += new System.EventHandler(this._uploadWmvCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._serverPromptLabel);
            this.groupBox3.Controls.Add(this._uploadProgress);
            this.groupBox3.Controls.Add(this._uploadProgressLabel);
            this.groupBox3.Controls.Add(this._exitBtn);
            this.groupBox3.Controls.Add(this._uploadBtn);
            this.groupBox3.Location = new System.Drawing.Point(12, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(812, 162);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // frmAutoPublish
            // 
            this.AcceptButton = this._uploadBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 375);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAutoPublish";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Publish To Ftp";
            this.Load += new System.EventHandler(this.frmAutoPublish_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar _uploadProgress;
        private System.Windows.Forms.Label _uploadProgressLabel;
        private System.Windows.Forms.Label _serverPromptLabel;
        private System.Windows.Forms.Button _uploadBtn;
        private System.Windows.Forms.Button _exitBtn;
        private System.Windows.Forms.Label _ogaFileLabel;
        private System.Windows.Forms.Label _wmvFileLabel;
        private System.Windows.Forms.TextBox _ogaPathTextBox;
        private System.Windows.Forms.TextBox _wmvPathTextBox;
        private System.Windows.Forms.CheckBox _uploadOgaCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _locationOgaFileBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button _locationWmvFileBtn;
        private System.Windows.Forms.CheckBox _uploadWmvCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}