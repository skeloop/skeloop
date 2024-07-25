using System.Runtime.CompilerServices;

namespace FMC
{
    internal static class Program
    {

        public static MainWindow? mainForm;

        public static string standardFactorioPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Factorio");
        public static string standardFactorioInstallationPath = "E:\\SteamLibrary\\steamapps\\common\\Factorio\\data\\base";

        public static string standardProgramPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FMC");

        public static string iconPath = "E:\\SteamLibrary\\steamapps\\common\\Factorio\\data\\base\\graphics\\icons";

        public static string modName = string.Empty;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitProgramFolders();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            mainForm = new MainWindow();
            mainForm.Start();
            Application.Run(mainForm);
            
        }

        static void InitProgramFolders()
        {
            if (!Directory.Exists(standardProgramPath))
            {
                Directory.CreateDirectory(standardProgramPath);
            }
            string[] subfolders = ["output"];
            foreach (string folder in subfolders)
            {
                var subPath = Path.Combine(standardProgramPath, folder);
                if (!Directory.Exists(subPath))
                {
                    Directory.CreateDirectory(subPath);
                }
            }
        } 
        

    }
}