namespace EmpyrionManager.Abstractions {
    using System;
    using System.Collections.Generic;

    public class Backup {
        public string Name { get; set; }

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        public string SourceSavegamePath { get; set; }

        public string DestinationPath { get; set; }

        public long Size { get; set; } = 0;

        public int FileCount { get; private set; } = 0;

        public int DirCount { get; private set; } = 0;

        public ICollection<BackupComponent> IncludedComponents { get; private set; } = new HashSet<BackupComponent>();

        /// <summary>
        /// Adds a single <see cref="BackupComponent"/> to the Backup.
        /// </summary>
        /// <param name="component">The <see cref="BackupComponent"/> to add.</param>
        /// <remarks>
        /// Better than <see cref="IncludedComponents"/>' .Add() as it increments the Backup's File and Directory counts.
        /// </remarks>
        public void AddComponent(BackupComponent component)
        {
            var hash = this.IncludedComponents as HashSet<BackupComponent>;

            this.IncludedComponents.Add(component);
            if (component.Type == BackupComponentType.Directory) { this.DirCount++; }
            else if (component.Type == BackupComponentType.File) { this.FileCount++; }
        }

        /// <summary>
        /// Adds multiple <see cref="BackupComponent"/>s to this Backup's included components.
        /// </summary>
        /// <remarks>
        /// Could find a more efficient implementation.
        /// </remarks>
        /// <param name="components">The <see cref="BackupComponent"/>s to add.</param>
        public void AddComponents(IEnumerable<BackupComponent> components)
        {
            foreach (var component in components)
            {
                this.AddComponent(component);
            }
        }

        /// <summary>
        /// Refreshes the Directory and File counts for the Backup.
        /// </summary>
        public void RefreshCounts()
        {
            var files = 0;
            var dirs = 0;

            foreach (var comp in this.IncludedComponents)
            {
                if (comp.Type == BackupComponentType.File)
                {
                    files++;
                }
                else if (comp.Type == BackupComponentType.Directory)
                {
                    dirs++;
                }
            }

            this.FileCount = files;
            this.DirCount = dirs;
        }
    }
}