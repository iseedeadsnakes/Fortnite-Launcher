using System.Diagnostics;
using System.Net;

namespace WpfApp5.Utilities
{
    class Utils
    {

        public static void KillProcess(string processName)
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(processName);
                for (int i = 0; i < processesByName.Length; i++)
                {
                    processesByName[i].Kill();
                }
            }
            catch
            {
            }
        }


        internal static void DownloadFile(string URL, string path)
        {
            try
            {
                new WebClient().DownloadFile(URL, path);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
