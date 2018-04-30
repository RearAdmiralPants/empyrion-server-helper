namespace EmpyrionManager.Abstractions
{
    using System;

    /// <summary>
    /// Represents a single component of a backup.
    /// 
    /// Currently, can be a directory or a file, though future versions of the game may include other components.
    /// </summary>
    /// <remarks>
    /// This class is necessary because restoring backups does not work properly unless empty directories are restored
    /// alongside those with files in them (e.g. Blueprints).
    /// </remarks>
    public class BackupComponent : IEquatable<BackupComponent>, IComparable<BackupComponent>
    {
        /// <summary>
        /// The absolute path to the backup component.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The type of the backup component.
        /// </summary>
        public BackupComponentType Type { get; set; }

        /// <summary>
        /// Creates a new BackupComponent from a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>A BackupComponent representing the given file.</returns>
        public static BackupComponent FromFile(string path)
        {
            var result = new BackupComponent();
            result.Path = path;
            result.Type = BackupComponentType.File;

            return result;
        }

        public int CompareTo(BackupComponent other)
        {
            var thisType = (int)this.Type;
            var otherType = (int)other.Type;

            if (thisType == otherType)
            {
                return this.Path.CompareTo(other.Path);
            }
            else
            {
                return thisType - otherType;
            }
        }

        public bool Equals(BackupComponent other)
        {
            return (this.Path.Equals(other.Path) && this.Type.Equals(other.Type));
        }

        public override bool Equals(object other)
        {
            if (other is BackupComponent)
            {
                return this.Equals(other as BackupComponent);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Path.GetHashCode() * 17 + (int)this.Type.GetHashCode();
        }
    }
}
