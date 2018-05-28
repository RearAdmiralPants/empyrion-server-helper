namespace ManagerLauncher
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using EmpyrionManager.Extensions;
    using EmpyrionManager.Data;

    public class Launcher
    {
        private const string DEFAULT_EXECUTABLE = "EmpyrionManager.exe";

        public string ManagerPath { get; set; }

        public Launcher(string path)
        {
            this.ManagerPath = path.TrailingBackslash();
        }

        public int LaunchEmpyrionManager()
        {
            var exe = this.ManagerPath + DEFAULT_EXECUTABLE;
            if (!File.Exists(exe))
            {
                Console.WriteLine("* ERROR: Could not find empyrion manager at (" + exe + ")");
                return -1;
            }

            var info = new ProcessStartInfo();
            info.FileName = exe;
            info.Verb = "runas";
            info.Arguments = LauncherData.MANAGER_SPAWN_ARGUMENT;
            info.UseShellExecute = true;

            var proc = Process.Start(info);
            Console.WriteLine("- Manager successfully launched at PID: " + proc.Id.ToString());

            return 0;
        }
    }
}
