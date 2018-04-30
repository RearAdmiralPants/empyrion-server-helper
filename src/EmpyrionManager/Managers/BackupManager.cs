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
            result.AddComponents(this.GetFiles(result.SourceSavegamePath));
            return result;
        }

        /// <summary>
        /// Recursively retrieves a list of all backup components within a particular directory (and all of its subdirectories).
        /// </summary>
        /// <param name="path">The path for which to retrieve all backup components.</param>
        /// <returns>A list of all backup components within the given path and all of its subdirectories.</returns>
        private HashSet<BackupComponent> GetFiles(string path) {
            var result = new HashSet<BackupComponent>();

            var comp = new BackupComponent();
            comp.Type = BackupComponentType.Directory;
            comp.Path = path;
            result.Add(comp);

            foreach (var dir in Directory.GetDirectories(path)) {
                result.UnionWith(this.GetFiles(dir));
            }

            foreach (var file in Directory.GetFiles(path)) {
                result.Add(BackupComponent.FromFile(path));
            }

            return result;
        }

        private void ExecuteBackup(Backup backup) {
            /*
            foreach (var file in backup.IncludedFiles) {
                File.Copy(file, this.GetDestinationFile(file, backup));                
            }
            */
            foreach (var comp in backup.IncludedComponents)
            {
                this.CopyComponent(comp, backup);
            }
        }

        private void CopyComponent(BackupComponent component, Backup backup)
        {
            if (component.Type == BackupComponentType.File)
            {
                this.CopyFile(component.Path, this.GetDestinationFile(component.Path, backup));
            }
            else if (component.Type == BackupComponentType.Directory && !Directory.Exists(component.Path))
            {
                Directory.CreateDirectory(component.Path);
            }
        }

        private void CopyFile(string source, string dest)
        {
            var fileDir = Path.GetDirectoryName(dest);

            if (!Directory.Exists(fileDir)) {
                Directory.CreateDirectory(fileDir);
            }

            File.Copy(source, dest);
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
 