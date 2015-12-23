namespace OpenGOVideo
{
    partial class Publish
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkAgenda = new System.Windows.Forms.CheckBox();
            this.chkVideo = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDirectory);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTP Settings";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(72, 99);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(155, 20);
            this.txtDirectory.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Directory";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(72, 73);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(155, 20);
            this.txtPass.TabIndex = 5;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(72, 48);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(155, 20);
            this.txtUser.TabIndex = 4;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(72, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(155, 20);
            this.txtServer.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(262, 52);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(74, 35);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(342, 52);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(262, 119);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(151, 22);
            this.progressBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(264, 94);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 19);
            this.lblStatus.TabIndex = 4;
            // 
            // chkAgenda
            // 
            this.chkAgenda.AutoSize = true;
            this.chkAgenda.Location = new System.Drawing.Point(265, 21);
            this.chkAgenda.Name = "chkAgenda";
            this.chkAgenda.Size = new System.Drawing.Size(63, 17);
            this.chkAgenda.TabIndex = 5;
            this.chkAgenda.Text = "Agenda";
            this.chkAgenda.UseVisualStyleBackColor = true;
            // 
            // chkVideo
            // 
            this.chkVideo.AutoSize = true;
            this.chkVideo.Location = new System.Drawing.Point(342, 21);
            this.chkVideo.Name = "chkVideo";
            this.chkVideo.Size = new System.Drawing.Size(53, 17);
            this.chkVideo.TabIndex = 6;
            this.chkVideo.Text = "Video";
            this.chkVideo.UseVisualStyleBackColor = true;
            // 
            // Publish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 155);
            this.Controls.Add(this.chkVideo);
            this.Controls.Add(this.chkAgenda);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.groupBox1);
            this.Name = "Publish";
            this.Text = "Publish Meeting via FTP";
            this.Load += new System.EventHandler(this.Publish_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAgenda;
        private System.Windows.Forms.CheckBox chkVideo;

    }
}