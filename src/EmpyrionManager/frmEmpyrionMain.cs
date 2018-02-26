/*
 Empyrion Server Helper
    Copyright (C) 2018  Paul Klingman, III

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
namespace EmpyrionManager
{
    using System;
    using System.Windows.Forms;
    using System.Diagnostics;
    using System.IO;
    using Extensions;
    using Helpers;
    public partial class frmEmpyrionMain : Form
    {
        // App settings - reason why constants are not named properly
        private const string _puttyLocation = "C:\\Users\\pklingman\\Desktop\\putty.exe";
        private const string _backupEmpyrion = "BackupEmpyrion.bat";
        private const string _backupDestination = "C:\\steamcmd\\empBackup\\";

        public frmEmpyrionMain()
        {
            InitializeComponent();
        }

        private void btnLaunchTelnet_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseDedicatedDir_Click(object sender, EventArgs e)
        {
            if (fbdDialog.ShowDialog() == DialogResult.OK)
            {
                txtDedicatedDir.Text = fbdDialog.SelectedPath.TrailingBackslash();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;

                startInfo.FileName = this.GetSteamCmdDirectory(txtDedicatedDir.Text).TrailingBackslash() + _backupEmpyrion;
                startInfo.Arguments = txtBackupName.Text;

                startInfo.UseShellExecute = false;

                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                using (var proc = Process.Start(startInfo))
                {
                    using (StreamReader reader = proc.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        txtShellOutput.AppendText(result);
                    }
                }

                // Verify that backup exists now
                if (!this.BackupExists(txtBackupName.Text))
                {
                    throw new ApplicationException("Attemped to create backup; does not exist.");
                }
                //var backupInfo = new DirectoryInfo(_backupDestination + txtBackupName.Text.TrailingBackslash());
                //MessageBox.Show(_backupDestination + txtBackupName.Text.TrailingBackslash());
                var backupSize = DirectoryExtensions.FullDirectorySize(_backupDestination + txtBackupName.Text.TrailingBackslash());
                if (backupSize > 5000)
                {
                    var backupSizeMb = backupSize / 1000000d;
                    lblBackupStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblBackupStatus.Text = "Backup successful (" + backupSizeMb.ToString("f2") + " MB)!";
                    lblBackupStatus.Visible = true;
                    var fader = new FadeHelper(lblBackupStatus);
                    fader.ScheduleFade(7500);
                    //FadeHelper.ScheduleFade(lblBackupStatus, 5000);
                }
            }
            catch (Exception ex)
            {
                txtShellOutput.AppendText(ex.Message + ": Failed to run the backup.");
            }
        }

        private string GetSteamCmdDirectory(string empyrionDedicatedDir)
        {
            var destination = empyrionDedicatedDir + "..\\";
            return Path.GetFullPath(destination);
        }

        private void txtBackupName_TextChanged(object sender, EventArgs e)
        {
            string backupNameError;
            if (!IsValidBackupName(txtBackupName.Text, out backupNameError))
            {
                lblBackupStatus.Text = backupNameError;
                lblBackupStatus.Visible = true;
                btnBackup.Enabled = false;
            }
            else
            {
                lblBackupStatus.Visible = false;
                btnBackup.Enabled = true;
            }
        }

        private bool BackupExists(string backupName)
        {
            foreach (var dir in Directory.GetDirectories(_backupDestination))
            {
                if (txtBackupName.Text.ToLowerInvariant() == dir.Replace(_backupDestination, "").ToLowerInvariant())
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidBackupName(string backupName, out string errorMessage)
        {
            errorMessage = null;
            var lowerName = backupName.ToLowerInvariant();

            if (lowerName.Length < 2)
            {
                errorMessage = "Backup name is too short.";
                return false;
            }

            if (lowerName == "normsaves")
            {
                errorMessage = "Backup name is reserved.";
                return false;
            }

            if (lowerName.ContainsInvalidPathCharacter())
            {
                errorMessage = "Backup contains invalid character(s).";
                return false;
            }

            if (this.BackupExists(lowerName))
            {
                errorMessage = "Backup already exists with this name.";
                return false;
            }

            return true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            lblBackupStatus.ForeColor = System.Drawing.Color.Green;
            lblBackupStatus.Text = "This is only a test; do not adjust your set.";
            lblBackupStatus.Visible = true;
            lblBackupStatus.BackColor = this.BackColor;
            Application.DoEvents();
            //lblBackupStatus.ForeColor = System.Drawing.Color.FromArgb(127, lblBackupStatus.ForeColor.R, lblBackupStatus.ForeColor.G, lblBackupStatus.ForeColor.B);
            //this.FadeOutBackupLabel(2000);
            var fader = new FadeHelper(lblBackupStatus);
            fader.ScheduleFade(2000);
            
            Application.DoEvents();
        }

        /*
        private void FadeOutBackupLabel(int milsUntilFade)
        {
            var scheduleFade = new Timer();
            scheduleFade.Interval = milsUntilFade;
            scheduleFade.Tick += ScheduleFade_Tick;
            scheduleFade.Start();
        }

        private void ScheduleFade_Tick(object sender, EventArgs e)
        {
            var scheduler = (Timer)sender;
            var fadeStepper = new Timer();
            fadeStepper.Interval = 75;
            fadeStepper.Tick += FadeStepper_Tick;
            fadeStepper.Start();
            scheduler.Stop();
        }

        private void FadeStepper_Tick(object sender, EventArgs e)
        {
            var newAlpha = lblToTransparent.BackColor.A - 20;
            if (newAlpha < 0) { newAlpha = 0; }
            lblToTransparent.BackColor = System.Drawing.Color.FromArgb(newAlpha, lblToTransparent.BackColor.R, lblToTransparent.BackColor.G, lblToTransparent.BackColor.B);
            if (newAlpha == 0)
            {
                var fader = (Timer)sender;
                fader.Stop();
            }
        }
        */
    }
}
