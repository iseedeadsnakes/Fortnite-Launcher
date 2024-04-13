using System.Diagnostics;
using WpfApp5.Utilities;
using System.IO;
using System.IO.Compression;
using WpfApp5.Properties;

namespace WpfApp5.Fortnite
{
    class Fortnite
    {

        public static void Launch(string path, string email, string password)
        {
            try
            {
                string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Launcher";
                string Redirect = Settings.Default.Path + "\\Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64";
                string PAC = Settings.Default.Path;


                // make sure to put all download urls for the files below
                if (!File.Exists(Appdata + "\\FortniteClient-Win64-Shipping_BE.exe"))
                {
                    Utils.DownloadFile("download url", Appdata + "\\FortniteClient-Win64-Shipping_BE.exe");
                }
                if (!File.Exists(Appdata + "\\FortniteLauncher.exe"))
                {
                    Utils.DownloadFile("download url", Appdata + "\\FortniteLauncher.exe");
                }
                if (!File.Exists(Redirect + "\\GFSDK_Aftermath_Lib.x64.dll"))
                {
                    Utils.DownloadFile("download url", Redirect + "\\GFSDK_Aftermath_Lib.x64.dll");
                }
                else
                {
                    Utils.DownloadFile("download url", Redirect + "\\GFSDK_Aftermath_Lib.x64.dll");

                }
                if (!File.Exists(Path.Combine(PAC, "PAC.zip")))
                {
                    // you dont need this but if you wanna use EAC this download the zip and extracts it to build path
/*                    Utils.DownloadFile("", Path.Combine(PAC, "PAC.zip"));
                    var fs = new FileStream(Path.Combine(PAC, "PAC.zip"), FileMode.Open);
                    new ZipArchive(fs).ExtractToDirectory(PAC, true);*/
                }

                string FortniteLauncher = "FortniteLauncher.exe";
                string FortniteClient = "FortniteClient-Win64-Shipping_BE.exe";

                Process[] runningProcesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FortniteLauncher));
                if (runningProcesses.Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Appdata, FortniteLauncher),
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }

                runningProcesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FortniteClient));
                if (runningProcesses.Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Appdata, FortniteClient),
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    //if you are using EAC change the path to run the exe for EAC
                    FileName = Settings.Default.Path + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe",
                    Arguments = $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck -AUTH_TYPE=EPIC -AUTH_LOGIN={Settings.Default.Email} -AUTH_PASSWORD={Settings.Default.Password} -calderas=",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                Process proc = new Process
                {
                    StartInfo = startInfo
                };

                proc.Start();
                proc.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
