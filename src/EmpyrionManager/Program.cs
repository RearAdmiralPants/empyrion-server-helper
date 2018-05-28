using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpyrionManager
{
    static class Program
    {
        private const string MANAGER_SPAWN_ARGUMENT = "StartedFromManager";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool cancelled;
            CheckForManagerSpawn(args, out cancelled);

            if (!cancelled)
            {
                Application.Run(new frmEmpyrionMain());
            }
        }

        private static void CheckForManagerSpawn(string[] args, out bool cancelled)
        {
            ////HACK: Could probably do this a better way without arguments or a ghetto cancellation token; named pipes might be overkill, find a happy medium
            cancelled = false;
            foreach (var arg in args)
            {
                if (arg.Trim().ToLowerInvariant() == MANAGER_SPAWN_ARGUMENT.ToLowerInvariant())
                {
                    return;
                }
            }

            if (MessageBox.Show("This application was designed to be started with the ManagerLauncher.exe utility that should be found in the same directory. It's recommended you start this application in that fashion. Would you like to continue with non-standard launch?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                cancelled = true;
            }
        }
    }
}
