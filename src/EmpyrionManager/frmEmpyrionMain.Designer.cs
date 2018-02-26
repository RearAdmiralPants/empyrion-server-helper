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
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLaunchTelnet = new System.Windows.Forms.Button();
            this.fbdDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtShellOutput = new System.Windows.Forms.TextBox();
            this.txtBackupName = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblBackupStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDedicatedDir
            // 
            this.txtDedicatedDir.Location = new System.Drawing.Point(12, 12);
            this.txtDedicatedDir.Name = "txtDedicatedDir";
            this.txtDedicatedDir.Size = new System.Drawing.Size(277, 20);
            this.txtDedicatedDir.TabIndex = 0;
            this.txtDedicatedDir.Text = "C:\\steamcmd\\empyriondedicatedserver\\";
            // 
            // btnBrowseDedicatedDir
            // 
            this.btnBrowseDedicatedDir.Location = new System.Drawing.Point(295, 10);
            this.btnBrowseDedicatedDir.Name = "btnBrowseDedicatedDir";
            this.btnBrowseDedicatedDir.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDedicatedDir.TabIndex = 1;
            this.btnBrowseDedicatedDir.Text = "Browse...";
            this.btnBrowseDedicatedDir.UseVisualStyleBackColor = true;
            this.btnBrowseDedicatedDir.Click += new System.EventHandler(this.btnBrowseDedicatedDir_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(31, 95);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(121, 97);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(13, 514);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLaunchTelnet
            // 
            this.btnLaunchTelnet.Location = new System.Drawing.Point(160, 514);
            this.btnLaunchTelnet.Name = "btnLaunchTelnet";
            this.btnLaunchTelnet.Size = new System.Drawing.Size(75, 23);
            this.btnLaunchTelnet.TabIndex = 4;
            this.btnLaunchTelnet.Text = "Telnet";
            this.btnLaunchTelnet.UseVisualStyleBackColor = true;
            this.btnLaunchTelnet.Click += new System.EventHandler(this.btnLaunchTelnet_Click);
            // 
            // txtShellOutput
            // 
            this.txtShellOutput.Location = new System.Drawing.Point(295, 95);
            this.txtShellOutput.Multiline = true;
            this.txtShellOutput.Name = "txtShellOutput";
            this.txtShellOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShellOutput.Size = new System.Drawing.Size(602, 359);
            this.txtShellOutput.TabIndex = 5;
            // 
            // txtBackupName
            // 
            this.txtBackupName.Location = new System.Drawing.Point(295, 460);
            this.txtBackupName.Name = "txtBackupName";
            this.txtBackupName.Size = new System.Drawing.Size(277, 20);
            this.txtBackupName.TabIndex = 6;
            this.txtBackupName.TextChanged += new System.EventHandler(this.txtBackupName_TextChanged);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(822, 460);
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
            this.lblBackupStatus.Location = new System.Drawing.Point(295, 480);
            this.lblBackupStatus.Name = "lblBackupStatus";
            this.lblBackupStatus.Size = new System.Drawing.Size(182, 13);
            this.lblBackupStatus.TabIndex = 8;
            this.lblBackupStatus.Text = "Backup Already Exists with this name";
            // 
            // frmEmpyrionMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 549);
            this.Controls.Add(this.lblBackupStatus);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.txtBackupName);
            this.Controls.Add(this.txtShellOutput);
            this.Controls.Add(this.btnLaunchTelnet);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnBrowseDedicatedDir);
            this.Controls.Add(this.txtDedicatedDir);
            this.Name = "frmEmpyrionMain";
            this.Text = "Empyrion Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDedicatedDir;
        private System.Windows.Forms.Button btnBrowseDedicatedDir;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnLaunchTelnet;
        private System.Windows.Forms.FolderBrowserDialog fbdDialog;
        private System.Windows.Forms.TextBox txtShellOutput;
        private System.Windows.Forms.TextBox txtBackupName;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblBackupStatus;
        //private Inheritors.TransparentLabel lblBackupStatus;
    }
}

