namespace EmpyrionManager.Helpers
{
    using System.IO;
    using Extensions;

    public static class IOHelpers
    {
        public static void CopyDir(string path, string dest)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                // var totalLog = txtShellOutput.Text; (?)

                if (!Directory.Exists(dest.TrailingBackslash() + dir.LastSplitElement('\\')))
                {
                    Directory.CreateDirectory(dest.TrailingBackslash() + dir.LastSplitElement('\\'));
                }
                CopyDir(dir, dest.TrailingBackslash() + dir.LastSplitElement('\\'));
            }

            foreach (var file in Directory.GetFiles(path))
            {
                if (!Directory.Exists(Path.GetDirectoryName(dest.TrailingBackslash())))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dest.TrailingBackslash()));
                }
                var destFile = dest.TrailingBackslash() + Path.GetFileName(file);
                File.Copy(file, destFile);
            }
        }
    }
}
