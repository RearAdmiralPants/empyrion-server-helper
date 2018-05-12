namespace EmpyrionManager.Abstractions.Archive
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents an archive of one or more backups.
    /// </summary>
    public interface IBackupArchive
    {
        ArchiveState State { get; }

        ICollection<Backup> Backups { get; }

        Stream ArchiveStream { get; }
    }
}
