using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Controls;
using WpfApp5.Pages;
using WpfApp5.Utilities;

namespace WpfApp5
{
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            RPC.StartRPC();

            if(Properties.Settings.Default.AutoLogin && Properties.Settings.Default.Loggedin == true) // probably should make this more safe because someone could just change the vars in property settings
            {
                MainFrame.Content = new Navigation();
            }
            else
            {
                MainFrame.Content = new Login();
            }

            Logs.Log("Launcher Started");
        }


        private void LauncherClosed(object sender, EventArgs e)
        {
            RPC.StopRPC();
            Utils.KillProcess("FortniteClient-Win64-Shipping");
            Utils.KillProcess("FortniteClient-Win64-Shipping_BE");
            Utils.KillProcess("FortniteClient-Win64-Shipping_EAC");
            Utils.KillProcess("FortniteLauncher");
        }
    }
}