namespace EmpyrionManager.Abstractions {
    using System.Collections.Generic;

    public class Backup {
        public string Name { get; set; }

        public string SourceSavegamePath { get; set; }

        public string DestinationPath { get; set; }

        public long Size { get; set; }

        public int FileCount { get; set; }

        public int DirCount { get; set; }

        public List<string> IncludedFiles { get; private set; } = new List<string>();
    }
}