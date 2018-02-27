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
    using Graphics;
    public partial class frmEmpyrionMain : Form
    {
        // App settings - reason why constants are not named properly
        private const string _puttyLocation = "C:\\Users\\pklingman\\Desktop\\putty.exe";
        private const string _backupEmpyrion = "BackupEmpyrion.bat";
        private const string _backupDestination = "C:\\steamcmd\\empBackup\\";
        private const int _minimumBackupSize = 5000;

        private frmViewImage viewImageForm = null;

        public frmEmpyrionMain()
        {
            InitializeComponent();
        }

        private void btnLaunchTelnet_Click(object sender, EventArgs e)
        {
            var text = @"
CD OF REGISTERED GAMES
CREATED FRIDAY 8/8/96
README
-=-=-=-=-=-=-=-=-=-=-=-=-

Contents

1.  List of Stuff on the CD
2.  How to install stuff
3.  Miscellaneous info

-=-=-=-=-=-=-=-=-=-=-=-=-

1.  LIST OF STUFF ON THE CD

On this CD (As of 8/8/96) there are the following games/utilities/things:
Hexen, Heretic, Doom, Doom 2, Descent, Rise of the Triad, Quake, Duke
Nukem 3D, WarCraft II, WarCraft II Expansion Pack, Caligari TrueSpace, 
Settlers 2, One Must Fall, Metal & Lace, Worms Plus, Terminal Velocity,
Caesar 2, the PK Utilities, the Inredible Machine, and Champions of Xanth.

All of these games/utilities/things are REGISTERED/CRACKED with the
exception of Descent and Terminal Velocity, which are Shareware.

All of these games/utilities/things were also zipped straight off of
my computer or my friend's computer, and therefore may be in a state
of use when installed (i.e. savegame files exist, configurations exist,
etc.).  If this is the case, feel free to overwrite/change them after
installation.

Scheduled updates to this CD include:  Mortal Kombat 3, Virtua Fighter, 
and possibly some others.  But you'll have to wait for those.  ;)

2.  HOW TO INSTALL STUFF

The general procedure to follow when you want to play a game is to
get into a DOS environment (NOT an MS-DOS prompt icon in Windows 3.1)
and go to your CD-ROM drive, change directories to the game you want
to try, and type the appropriate commandline (i.e. doom for doom or
quake for quake or doom2 for doom II, etc).  In about half the cases,
this should work.  If not, no big deal, just XCOPY the whole subdirectory
to a directory on your hard drive (i.e. XCOPY *.* C:\QUAKE /S, typed from
the D:\QUAKE directory) and change the configuration, and run it from
there.  Right now all games are configured for a SoundBlaster AWE32 if
they came off my computer and a SoundBlaster 100% compatible if they
came off my friend's computer.

3.  MISCELLANEOUS INFO

Although it hurts to do so, I'd like to thank my boss for allowing
me to burn this CD with his sacred HP 2040i SCSI CD-Recordable drive,
which of course I installed and configured, but he spent the dough on.
I'd also like to thank Earl Peters for contributing his BOX of disks
full of software to this cause.  I'd like to thank all my friends for
furthering the bytes of software piracy...  Earl Peters, Todd Snyder,
Nate Myers, Steve Fair, Taylor Smith, Frank Hanner, Chris Billman,
Robert Baumer, and a bunch of others that my brain's too fried to
remember right now.

There's a file called KLINGMAN.CD which contains legaleze info from
yours truly so I can't get too burnt for anything screwing up on your
end or the Feds' end...  ;)

And be sure to give this back to me soon, I only burnt one copy ;)
Should any updates of this CD follow, an updated README file will
accompany them.  Thanks for reading!

Caligari TrueSpace, a 3-D modeling, rendering, and animation studio,
needs a serial number/key to install.  That key can be found in the
file TRUSPACE.SN in the root directory of the CD or in the
TrueSpace directory.

README.TXT 8/8/96
";

            var consoleOut = new ConsoleBitmap();
            consoleOut.ForeColor = System.Drawing.Color.Yellow;
            consoleOut.BackColor = System.Drawing.Color.DarkBlue;
            consoleOut.OutputFont = new System.Drawing.Font("Courier New", 10, System.Drawing.FontStyle.Bold);
            var fullImg = consoleOut.GenerateConsoleOutputImage(text);
            pbConsole.Image = ConsoleBitmap.Resample(fullImg, pbConsole.Size.ToRectangle());
            pbConsole.Tag = fullImg;
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
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;

                startInfo.FileName = this.GetSteamCmdDirectory(txtDedicatedDir.Text).TrailingBackslash() + _backupEmpyrion;
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

                var backupSize = DirectoryExtensions.FullDirectorySize(_backupDestination + txtBackupName.Text.TrailingBackslash());
                if (backupSize > _minimumBackupSize)
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
    }
}
