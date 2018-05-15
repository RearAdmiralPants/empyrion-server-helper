namespace EmpyrionManager.Abstractions.Archive {
    using System.Collections.Generic;

    public class CompressedArchive : BackupArchive {
        private PackageType _packageType = PackageType.Unpacked;
        /// <summary>
        /// Gets the package type (compressoion type) of the archive.
        /// </summary>
        public PackageType PackageType { 
            get {
                return this._packageType;
            }
        }

        /// <summary>
        /// Creates a new instance of the Compressed Archive.
        /// </summary>
        /// <param name="backups">The collection of backups to add to the archive.</param>
        public CompressedArchive(ICollection<Backup> backups) {
            this.Backups = backups;
        }

        public CompressedArchive() { }


    }
}