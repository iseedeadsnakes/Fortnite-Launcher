using System.Diagnostics;
using System.Net;

namespace WpfApp5.Utilities
{
    class Utils
    {

        public static void KillProcess(string name)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(name);

                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void DownloadFile(string url, string path)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, path);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
