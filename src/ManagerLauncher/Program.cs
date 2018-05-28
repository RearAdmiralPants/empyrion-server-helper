namespace ManagerLauncher
{
    using System;
    using System.IO;
    using System.Reflection;
    class Program
    {
        static void Main(string[] args)
        {
            var currentDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;

            PrintWelcomeMessage();
            var launcher = new Launcher(currentDir);
            launcher.LaunchEmpyrionManager();
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Empyrion Server Launcher");
            Console.WriteLine("=======================================");
            Console.WriteLine("");
        }
    }
}
