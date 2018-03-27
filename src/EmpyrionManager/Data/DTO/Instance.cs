namespace EmpyrionManager.Data.DTO
{
    using System;
    using System.IO;

    public abstract class Instance
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime Modified { get; set; } = DateTime.UtcNow;

        public void FromFile(string path)
        {
            var attrs = new FileInfo(path);
            this.Created = attrs.CreationTimeUtc;
            this.Modified = attrs.LastWriteTimeUtc;            
        }
    }
}
