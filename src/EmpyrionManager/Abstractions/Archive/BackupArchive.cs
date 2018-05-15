namespace EmpyrionManager.Abstractions.Archive
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents an archive of one or more backups.
    /// </summary>
    public abstract class BackupArchive : IBackupArchive
    {
        /// <summary>
        /// Gets or sets the name of the archive.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the state of the archive.
        /// </summary>
        public ArchiveState State { get; set; }
        /// <summary>
        /// Gets the collection of backups within the archive.
        /// </summary>
        public ICollection<Backup> Backups { get; protected set; } = new List<Backup>();
        /// <summary>
        /// Gets the stream containing the bytes of the archive.
        /// </summary>
        public Stream ArchiveStream { get; protected set; }
    }
}
