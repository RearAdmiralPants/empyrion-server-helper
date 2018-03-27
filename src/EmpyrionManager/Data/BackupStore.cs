namespace EmpyrionManager.Data
{
    using System;
    using System.Collections.Generic;
    using DTO;

    public class BackupStore
    {
        public IList<BackupInstance> Instances { get; private set; } = new List<BackupInstance>();

        public void ParseWithinPath(string path)
        {
            throw new NotImplementedException();
        }

        public void ParseWithinArchive(string path)
        {
            throw new NotImplementedException();
        }
    }
}
