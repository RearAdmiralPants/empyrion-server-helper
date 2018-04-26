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
namespace EmpyrionManager.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Abstractions;
    using Extensions;
    using Helpers;

    public class BackupManager
    {
        public string EmpyrionSaveDirectory { get; set; }

        public string BackupRootDirectory { get; set; }

        public int MinimumBackupSize { get; set; }

        public BackupManager() { }

        private string PathToBackup(string backupName) {
            return BackupRootDirectory.TrailingBackslash() + backupName.TrailingBackslash();            
        }

        private Backup StageBackupSaves(string name) {
            var result = new Backup();

            result.Name = name;
            result.DestinationPath = this.PathToBackup(name);
            result.SourceSavegamePath = this.EmpyrionSaveDirectory.TrailingBackslash();
            result.IncludedFiles.AddRange(this.GetFiles(result.SourceSavegamePath));
            return result;
        }

        private List<string> GetFiles(string path) {
            var result = new List<string>();

            foreach (var dir in Directory.GetDirectories(path)) {
                result.AddRange(this.GetFiles(dir));
            }

            foreach (var file in Directory.GetFiles(path)) {
                result.Add(file);
            }

            return result;
        }

        private void ExecuteBackup(Backup backup) {
            foreach (var file in backup.IncludedFiles) {
                File.Copy(file, this.GetDestinationFile(file, backup));                
            }
        }

        private string GetDestinationFile(string file, Backup backup) {
            var justFile = this.GetPathRelativeToSaveDir(file, backup);
            return backup.DestinationPath.TrailingBackslash() + justFile;
        }

        private string GetPathRelativeToSaveDir(string file, Backup backup) {
            var result = file.Substring(backup.SourceSavegamePath.Length);

            ////TODO: Write extension method (?)
            if (result.StartsWith("\\")) {
                result = result.Substring(1);
            }
            return result;
        }
    }
}
 