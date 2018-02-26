namespace EmpyrionManager.Extensions
{
    using System.IO;
    public static class DirectoryExtensions
    {
        public static long FullDirectorySize(string path)
        {
            var info = new DirectoryInfo(path.TrailingBackslash());
            return info.FullDirectorySize();
        }

        public static long FullDirectorySize(this DirectoryInfo dir)
        {
            long totalSize = 0;

            foreach (var subDir in dir.GetDirectories())
            {
                totalSize += subDir.FullDirectorySize();
            }

            foreach (var file in dir.GetFiles())
            {
                totalSize += file.Length;
            }

            return totalSize;
        }
    }
}
