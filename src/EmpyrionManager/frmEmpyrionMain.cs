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
    using System.Threading.Tasks;

    using Extensions;
    using Helpers;
    using Graphics;
    using Managers;

    using PrimS.Telnet;
    using AutoMapper;

    public partial class frmEmpyrionMain : Form
    {
        // App settings - reason why constants are not named properly
        //private const string _puttyLocation = "C:\\Users\\pklingman\\Desktop\\putty.exe";
        //private const string _backupEmpyrion = "BackupEmpyrion.bat";
        //private const string _backupDestination = "C:\\steamcmd\\empBackup\\";
        //private const int _minimumBackupSize = 5000;
        private const string DEDICATED_SERVER_PROCESSNAME = "empyriondedicated";

        private frmViewImage viewImageForm = null;

        private BackupManager backupManager = null;

        private IMapper mapper;

        public frmEmpyrionMain()
        {
            InitializeComponent();

            var minimumBackupSize = Convert.ToInt32(AppSettingRetriever.GetAppSetting("MinimumBackupSize"));
            var backupDestination = AppSettingRetriever.GetAppSetting("BackupDestination");

            this.backupManager = new BackupManager();
            this.backupManager.BackupRootDirectory = backupDestination;
            this.backupManager.MinimumBackupSize = minimumBackupSize;

            this.mapper = Mappers.Mappings.GetMappings().CreateMapper();
        }

        private void AppendShellText(string text)
        {
            Invoke(new MethodInvoker(() => txtShellOutput.AppendText(text)));
        }

        private void btnLaunchTelnet_Click(object sender, EventArgs e)
        {
            Task.Run(() => LoginToTelnet());
        }

        private async Task LoginToTelnet()
        {
            using (var client = new Client("192.168.2.25", 30023, new System.Threading.CancellationToken()))
            {
                var foundColon = await client.TerminatedReadAsync(": ", new TimeSpan(0, 0, 10));


                //MessageBox.Show("Found colon: " + foundColon.ToString());
                this.AppendShellText(foundColon);

                if (foundColon.Length > 5)
                {
                    await client.WriteLine("pklingman\r\n");
                    foundColon = await client.TerminatedReadAsync(": ", new TimeSpan(0, 0, 10));
                    this.AppendShellText(foundColon);
                    if (foundColon.Contains("password"))
                    {
                        await client.WriteLine("(password)\r\n");
                        foundColon = await client.TerminatedReadAsync(">", new TimeSpan(0, 0, 10));
                        this.AppendShellText(foundColon);
                        if (foundColon.Length > 3)
                        {
                            await client.WriteLine("dir\r\n");
                            foundColon = await client.TerminatedReadAsync(">", new TimeSpan(0, 0, 10));
                            this.AppendShellText(foundColon);

                        }
                    }
                }
            }
        }

        private string PointString(System.Drawing.Point point)
        {
            return "(" + point.X + ", " + point.Y + ")";
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
            var minimumBackupSize = Convert.ToInt32(AppSettingRetriever.GetAppSetting("MinimumBackupSize"));
            var backupScript = AppSettingRetriever.GetAppSetting("BackupEmpyrionScript");
            var backupDestination = AppSettingRetriever.GetAppSetting("BackupDestination");

            try
            {
                

                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;

                startInfo.FileName = this.GetSteamCmdDirectory(txtDedicatedDir.Text).TrailingBackslash() + backupScript;
                startInfo.Arguments = txtBackupName.Text;

                startInfo.UseShellExecute = false;

                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                string result;
                using (var proc = Process.Start(startInfo))
                {
                    using (StreamReader reader = proc.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                }

                // Verify that backup exists now
                if (!this.BackupExists(txtBackupName.Text))
                {
                    throw new ApplicationException("Attemped to create backup, but it does not exist; failure implied.");
                }

                var backupSize = DirectoryExtensions.FullDirectorySize(backupDestination + txtBackupName.Text.TrailingBackslash());
                if (backupSize > minimumBackupSize)
                {
                    var backupSizeMb = backupSize / 1000000d;
                    lblBackupStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    var successText = "Backup successful (" + backupSizeMb.ToString("f2") + " MB)!";
                    lblBackupStatus.Text = successText;
                    lblBackupStatus.Visible = true;
                    var fader = new FadeHelper(lblBackupStatus);
                    fader.ScheduleFade(7500);

                    if (result.Length > 2)
                    {
                        result += "\r\n" + successText;
                        txtShellOutput.AppendText(result);

                        var consoleOut = new ConsoleBitmap();
                        consoleOut.ForeColor = System.Drawing.Color.Yellow;
                        consoleOut.BackColor = System.Drawing.Color.DarkBlue;
                        consoleOut.OutputFont = new System.Drawing.Font("Courier New", 10, System.Drawing.FontStyle.Bold);
                        var fullImg = consoleOut.GenerateConsoleOutputImage(result);
                        pbConsole.Image = ConsoleBitmap.Resample(fullImg, pbConsole.Size.ToRectangle());
                        pbConsole.Tag = fullImg;
                    }

                }
                else
                {
                    throw new ApplicationException("Attempted to create backup, but it falls under minimum configured size. Failure implied.");
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
                this.RestoreDefaultBackupLabel();
                btnBackup.Enabled = true;
            }
        }

        private bool BackupExists(string backupName)
        {
            var backupDestination = AppSettingRetriever.GetAppSetting("BackupDestination");

            foreach (var dir in Directory.GetDirectories(backupDestination))
            {
                if (txtBackupName.Text.ToLowerInvariant() == dir.Replace(backupDestination, "").ToLowerInvariant())
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

            if (lowerName.ContainsInvalidPathCharacter() || lowerName.ContainsInvalidFileCharacter())
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
            var fader = new FadeHelper(lblBackupStatus);
            fader.ScheduleFade(2000);
        }

        private void pbConsole_Click(object sender, EventArgs e)
        {
            this.viewImageForm = new frmViewImage();
            this.viewImageForm.DisplayImage = (System.Drawing.Image)pbConsole.Tag;
            this.viewImageForm.ShowDialog();
        }

        private void RestoreDefaultBackupLabel()
        {
            lblBackupStatus.ForeColor = System.Drawing.Color.Black;
            lblBackupStatus.Text = "Name of Backup";
            lblBackupStatus.Visible = true;
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (this.IsServerRunning())
            {
                MessageBox.Show("Can't start server; an instance is already running.");
                return;
            }

            var serverProc = this.StartServer();

            if (serverProc != null)
            {
                this.AppendShellText("Server is running at PID " + serverProc.Id.ToString() + ".");
            }
        }

        private Process StartServer()
        {
            return null; // Currently doesn't work
            var dedicatedDir = txtDedicatedDir.Text.TrailingBackslash();

            var startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;

            startInfo.FileName = dedicatedDir + "EmpyrionDedicated_FromMgr.cmd";

            startInfo.UseShellExecute = false;

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            string result;
            using (var proc = Process.Start(startInfo))
            {
                using (StreamReader reader = proc.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
            }

            this.AppendShellText("\r\nAttempted to start dedicated server:");
            this.AppendShellText("\r\n" + result + "\r\n");

            Process serverProc = null;
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.ToLowerInvariant().Contains(DEDICATED_SERVER_PROCESSNAME))
                {
                    serverProc = process;
                    break;
                }
            }

            return serverProc;
        }

        private bool IsServerRunning()
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.ToLowerInvariant().Contains(DEDICATED_SERVER_PROCESSNAME))
                {
                    return true;
                }
            }

            return false;
        }

        private void btnOpacityTest_Click(object sender, EventArgs e)
        {
            pbConsole.Test();
        }

        private void frmEmpyrionMain_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ////TODO: Use new BackupManager for this
            var backupDestination = AppSettingRetriever.GetAppSetting("BackupDestination");

            foreach (var backup in Directory.GetDirectories(backupDestination))
            {
                var listItem = new ListViewItem(backup.LastSplitElement('\\'));
                listItem.Tag = backup;
                lstBackups.Items.Add(listItem);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            var backupDestination = AppSettingRetriever.GetAppSetting("BackupDestination");
            var savesDir = AppSettingRetriever.GetAppSetting("SavesDirectory");
            
            if (IsServerRunning())
            {
                MessageBox.Show("Cannot restore a save game while the server is running. Exit the server and try again.");
            }

            if (!this.EmergencyBackup())
            {
                MessageBox.Show("Transient backup failed. Restore not proceeding.");
            }

            MessageBox.Show("Proceed?");
            foreach (var saveDir in Directory.GetDirectories(savesDir))
            {
                Directory.Delete(saveDir, true);
            }

            var restoreItem = lstBackups.SelectedItems[0];
            var restoreDir = (string)restoreItem.Tag;
            this.CopyDir(restoreDir, savesDir);

            this.AppendShellText("Restore complete.");
        }

        private bool EmergencyBackup()
        {
            try
            {
                var emergDest = AppSettingRetriever.GetAppSetting("EmergSavesDirectory");
                var toBackup = AppSettingRetriever.GetAppSetting("EmergSourceDirectory");
                var destDir = emergDest.TrailingBackslash() + "EmergBackup" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Millisecond.ToString() + "\\";
                Directory.CreateDirectory(destDir);

                this.CopyDir(toBackup, destDir);
                var sizeCalc = new DirectoryInfo(destDir);
                this.AppendShellText("\r\nTransient backup completed: " + sizeCalc.FullDirectorySize().ToString() + " bytes.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void CopyDir(string path, string dest)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                var totalLog = txtShellOutput.Text;

                if (!Directory.Exists(dest.TrailingBackslash() + dir.LastSplitElement('\\')))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dest.TrailingBackslash() + dir.LastSplitElement('\\')));
                }
                this.CopyDir(dir, dest.TrailingBackslash() + dir.LastSplitElement('\\'));
            }

            foreach (var file in Directory.GetFiles(path))
            {
                if (!Directory.Exists(Path.GetDirectoryName(dest.TrailingBackslash()))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(dest.TrailingBackslash()));
                }
                var destFile = dest.TrailingBackslash() + Path.GetFileName(file);
                File.Copy(file, destFile);
            }
        }

        private void btnClearTransient_Click(object sender, EventArgs e)
        {
            var emergDest = AppSettingRetriever.GetAppSetting("EmergSavesDirectory");

            if (MessageBox.Show("Delete all transient directories used during restore operations? Make sure all restores are successful and the game is in a playable state.", "Delete Transient Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            Directory.Delete(emergDest, true);
        }
    }
}
