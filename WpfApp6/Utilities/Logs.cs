using System.IO;

namespace WpfApp5.Utilities
{
    class Logs
    {
        private static string AppDataPath;

        public static void InitializeLogger()
        {
            AppDataPath = GetAppDataFolderPath();
            InitializeLogFile(AppDataPath);
            Log("Logger initialized.");
        }

        public static async Task Log(string message)
        {
            await Task.Run(() =>
            {
                if (AppDataPath == null)
                {
                    AppDataPath = GetAppDataFolderPath();
                    InitializeLogFile(AppDataPath);
                    Log("Logger folder path re-initialized.");
                }

                try
                {
                    using (StreamWriter writer = File.AppendText(AppDataPath))
                    {
                        string logMessage = $"Launcher->{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}";
                        writer.WriteLine(logMessage);
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error while logging: {ex.Message}");
                }
            });
        }

        static void InitializeLogFile(string appDataPath)
        {
            try
            {
                if (!File.Exists(appDataPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(appDataPath));
                    File.Create(appDataPath).Close();
                    Log("Log file created.");
                }
                else
                {
                    string oldLogFilePath = appDataPath.Replace(".txt", $"_{DateTime.Now:yyyyMMddHHmmss}.txt");
                    File.Move(appDataPath, oldLogFilePath);
                    Log($"Old log file renamed to: {Path.GetFileName(oldLogFilePath)}");
                }
            }
            catch (Exception ex)
            {
                Log(":( Launcher Logs File Failed To Initialize!");
                Log(ex.Message);
            }
        }

        public static string GetAppDataFolderPath()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(appDataFolder, "Launcher"); // location for folder
            string logFilePath = Path.Combine(folderPath, "Launcher-Logs.txt"); // name of logs txt

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Log("Logger folder created.");
            }

            return logFilePath;
        }
    }
}
