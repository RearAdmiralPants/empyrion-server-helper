namespace EmpyrionManager
{
    partial class frmEmpyrionMain
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
            this.txtDedicatedDir = new System.Windows.Forms.TextBox();
            this.btnBrowseDedicatedDir = new System.Windows.Forms.Button();
            this.lstBackups = new System.Windows.Forms.ListView();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLaunchTelnet = new System.Windows.Forms.Button();
            this.fbdDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtShellOutput = new System.Windows.Forms.TextBox();
            this.txtBackupName = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblBackupStatus = new System.Windows.Forms.Label();
            this.pbConsole = new EmpyrionManager.Graphics.FadingPictureBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnOpacityTest = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnClearTransient = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDedicatedDir
            // 
            this.txtDedicatedDir.Location = new System.Drawing.Point(12, 12);
            this.txtDedicatedDir.Name = "txtDedicatedDir";
            this.txtDedicatedDir.Size = new System.Drawing.Size(323, 20);
            this.txtDedicatedDir.TabIndex = 0;
            this.txtDedicatedDir.Text = "C:\\steamcmd\\empyriondedicatedserver\\";
            // 
            // btnBrowseDedicatedDir
            // 
            this.btnBrowseDedicatedDir.Location = new System.Drawing.Point(341, 12);
            this.btnBrowseDedicatedDir.Name = "btnBrowseDedicatedDir";
            this.btnBrowseDedicatedDir.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDedicatedDir.TabIndex = 1;
            this.btnBrowseDedicatedDir.Text = "Browse...";
            this.btnBrowseDedicatedDir.UseVisualStyleBackColor = true;
            this.btnBrowseDedicatedDir.Click += new System.EventHandler(this.btnBrowseDedicatedDir_Click);
            // 
            // lstBackups
            // 
            this.lstBackups.Location = new System.Drawing.Point(12, 283);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Size = new System.Drawing.Size(404, 323);
            this.lstBackups.TabIndex = 2;
            this.lstBackups.UseCompatibleStateImageBehavior = false;
            this.lstBackups.View = System.Windows.Forms.View.List;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(950, 612);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLaunchTelnet
            // 
            this.btnLaunchTelnet.Enabled = false;
            this.btnLaunchTelnet.Location = new System.Drawing.Point(950, 385);
            this.btnLaunchTelnet.Name = "btnLaunchTelnet";
            this.btnLaunchTelnet.Size = new System.Drawing.Size(75, 23);
            this.btnLaunchTelnet.TabIndex = 4;
            this.btnLaunchTelnet.Text = "Telnet";
            this.btnLaunchTelnet.UseVisualStyleBackColor = true;
            this.btnLaunchTelnet.Click += new System.EventHandler(this.btnLaunchTelnet_Click);
            // 
            // txtShellOutput
            // 
            this.txtShellOutput.Location = new System.Drawing.Point(423, 10);
            this.txtShellOutput.Multiline = true;
            this.txtShellOutput.Name = "txtShellOutput";
            this.txtShellOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShellOutput.Size = new System.Drawing.Size(602, 215);
            this.txtShellOutput.TabIndex = 5;
            // 
            // txtBackupName
            // 
            this.txtBackupName.Location = new System.Drawing.Point(423, 231);
            this.txtBackupName.Name = "txtBackupName";
            this.txtBackupName.Size = new System.Drawing.Size(521, 20);
            this.txtBackupName.TabIndex = 6;
            this.txtBackupName.TextChanged += new System.EventHandler(this.txtBackupName_TextChanged);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(950, 231);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 7;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblBackupStatus
            // 
            this.lblBackupStatus.AutoSize = true;
            this.lblBackupStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupStatus.ForeColor = System.Drawing.Color.Red;
            this.lblBackupStatus.Location = new System.Drawing.Point(420, 254);
            this.lblBackupStatus.Name = "lblBackupStatus";
            this.lblBackupStatus.Size = new System.Drawing.Size(182, 13);
            this.lblBackupStatus.TabIndex = 8;
            this.lblBackupStatus.Text = "Backup Already Exists with this name";
            this.lblBackupStatus.Visible = false;
            // 
            // pbConsole
            // 
            this.pbConsole.FadeDelaySeconds = 5D;
            this.pbConsole.FadeSeconds = -1D;
            this.pbConsole.FadeStepSeconds = 2D;
            this.pbConsole.Location = new System.Drawing.Point(12, 41);
            this.pbConsole.Name = "pbConsole";
            this.pbConsole.Size = new System.Drawing.Size(175, 184);
            this.pbConsole.TabIndex = 9;
            this.pbConsole.TabStop = false;
            this.pbConsole.Click += new System.EventHandler(this.pbConsole_Click);
            // 
            // btnStartServer
            // 
            this.btnStartServer.Enabled = false;
            this.btnStartServer.Location = new System.Drawing.Point(950, 414);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 10;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnOpacityTest
            // 
            this.btnOpacityTest.Location = new System.Drawing.Point(193, 202);
            this.btnOpacityTest.Name = "btnOpacityTest";
            this.btnOpacityTest.Size = new System.Drawing.Size(75, 23);
            this.btnOpacityTest.TabIndex = 11;
            this.btnOpacityTest.Text = "Opc Test";
            this.btnOpacityTest.UseVisualStyleBackColor = true;
            this.btnOpacityTest.Click += new System.EventHandler(this.btnOpacityTest_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 612);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(341, 612);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 13;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnClearTransient
            // 
            this.btnClearTransient.Location = new System.Drawing.Point(260, 612);
            this.btnClearTransient.Name = "btnClearTransient";
            this.btnClearTransient.Size = new System.Drawing.Size(75, 23);
            this.btnClearTransient.TabIndex = 14;
            this.btnClearTransient.Text = "Clear Mids";
            this.btnClearTransient.UseVisualStyleBackColor = true;
            this.btnClearTransient.Click += new System.EventHandler(this.btnClearTransient_Click);
            // 
            // frmEmpyrionMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 647);
            this.Controls.Add(this.btnClearTransient);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpacityTest);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.pbConsole);
            this.Controls.Add(this.lblBackupStatus);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.txtBackupName);
            this.Controls.Add(this.txtShellOutput);
            this.Controls.Add(this.btnLaunchTelnet);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lstBackups);
            this.Controls.Add(this.btnBrowseDedicatedDir);
            this.Controls.Add(this.txtDedicatedDir);
            this.Name = "frmEmpyrionMain";
            this.Text = "Empyrion Manager";
            this.Load += new System.EventHandler(this.frmEmpyrionMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDedicatedDir;
        private System.Windows.Forms.Button btnBrowseDedicatedDir;
        private System.Windows.Forms.ListView lstBackups;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnLaunchTelnet;
        private System.Windows.Forms.FolderBrowserDialog fbdDialog;
        private System.Windows.Forms.TextBox txtShellOutput;
        private System.Windows.Forms.TextBox txtBackupName;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblBackupStatus;
        private Graphics.FadingPictureBox pbConsole;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnOpacityTest;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnClearTransient;
        //private Inheritors.TransparentLabel lblBackupStatus;
    }
}

