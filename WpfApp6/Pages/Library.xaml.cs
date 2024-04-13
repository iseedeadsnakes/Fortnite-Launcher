using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Wpf.Ui.Controls;
using WpfApp5.Utilities;

namespace WpfApp5.Pages
{
    public partial class Library : Page
    {
        public Library()
        {
            InitializeComponent();

            if (Properties.Settings.Default.Path != "")
            {
                Build1.Content = "Launch";
                SetPath1Icon.Symbol = SymbolRegular.Play20;
                ModifyBuild1Button.Visibility = Visibility.Visible;
                ModifySymbol.Visibility = Visibility.Visible;
            }
        }

        private void Library_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsProcessRunning("FortniteLauncher")
                || IsProcessRunning("FortniteClient-Win64-Shipping_EAC")
                || IsProcessRunning("Poopy-AC-Launcher")
                || IsProcessRunning("FortniteClient-Win64-Shipping_BE")
                || IsProcessRunning("FortniteClient-Win64-Shipping"))
            {
                Build1.Content = "Close";
                SetPath1Icon.Symbol = SymbolRegular.ErrorCircle16;
            }

            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(.2)
            };

            DoubleAnimation slideDownAnimation = new DoubleAnimation
            {
                From = -20,
                To = 0,
                Duration = TimeSpan.FromSeconds(.3)
            };

            TranslateTransform translateTransform = new TranslateTransform();
            LibraryGrid.RenderTransform = translateTransform;

            LibraryGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideDownAnimation);
        }

        private async void Build1_Click(object sender, RoutedEventArgs e)
        {
            if (Build1.Content.ToString() == "Launch")
            {
                Fortnite.Fortnite.Launch(Properties.Settings.Default.Path, Properties.Settings.Default.Email, Properties.Settings.Default.Password);

                Toast.ShowToast("Launcher", "Launching Fortnite...");

                Build1.Content = "Close";
                SetPath1Icon.Symbol = SymbolRegular.ErrorCircle16;

                DependencyObject parent = VisualTreeHelper.GetParent(this);
                while (!(parent is Window) && parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent is Window parentWindow)
                {
                    parentWindow.WindowState = WindowState.Minimized;
                }
            }
            else if (Build1.Content == "Close".ToString())
            {
                Utils.KillProcess("FortniteClient-Win64-Shipping");
                Utils.KillProcess("FortniteClient-Win64-Shipping_BE");
                Utils.KillProcess("FortniteClient-Win64-Shipping_EAC");
                Utils.KillProcess("FortniteLauncher");
                Build1.Content = "Launch";
                SetPath1Icon.Symbol = SymbolRegular.Play20;
            }
            else
            {
                var openFileDialog = new OpenFolderDialog
                {
                    Title = "Select a Folder"
                };

                FadeInLoadingBuffer();

                bool? result = openFileDialog.ShowDialog();
                bool executableExists = System.IO.File.Exists(System.IO.Path.Combine(openFileDialog.FolderName, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));

                if (executableExists)
                {
                    string buildVersion = await Utilities.VersionChecker.GetBuildVersion(openFileDialog.FolderName);
                    Logs.Log($"Build Version: {buildVersion}");
                    Logs.Log($"Added Build: {openFileDialog.FolderName}");

                    if (buildVersion == "8.51")
                    {
                        Properties.Settings.Default.Path = openFileDialog.FolderName;
                        Properties.Settings.Default.Save();
                        FadeOutLoadingBuffer();
                    }
                    else
                    {
                        FadeOutLoadingBuffer();
                        ShowError("You can only play 8.51!");
                        return;
                    }
                }
                else
                {
                    FadeOutLoadingBuffer();
                    ShowError("The selected folder does not contain the required executable.");
                    return;
                }
                FadeOutLoadingBuffer();

                Build1.Content = "Launch";
                SetPath1Icon.Symbol = SymbolRegular.Play20;
                ModifyBuild1Button.Visibility = Visibility.Visible;
                ModifySymbol.Visibility = Visibility.Visible;
            }
        }

        private async void ModifyBuild1Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFolderDialog
            {
                Title = "Select a Folder"
            };

            FadeInLoadingBuffer();

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string buildVersion = await Utilities.VersionChecker.GetBuildVersion(openFileDialog.FolderName);

                Logs.Log($"Build Versionand: {buildVersion}");
                Logs.Log($"Added Build: {openFileDialog.FolderName}");

                if (buildVersion == "8.51")
                {
                    Properties.Settings.Default.Path = openFileDialog.FolderName;
                    Properties.Settings.Default.Save();
                    FadeOutLoadingBuffer();
                }
                else
                {
                    ShowError("You can only play 8.51!");
                    FadeOutLoadingBuffer();
                }
            }
        }

        private void FadeInLoadingBuffer()
        {
            LoadingGrid.Visibility = Visibility.Visible;

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            Storyboard.SetTarget(fadeInAnimation, LoadingGrid);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);

            storyboard.Begin();
        }

        private void FadeOutLoadingBuffer()
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            Storyboard.SetTarget(fadeInAnimation, LoadingGrid);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            LoadingGrid.Visibility = Visibility.Hidden;

            storyboard.Begin();
        }

        private async void ShowError(string message)
        {
            ErrorGrid.Visibility = Visibility.Visible;
            ErrorButton.Content = message;

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            ErrorGrid.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            await Task.Delay(3000);

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            ErrorGrid.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);

            await Task.Delay(3000);
            ErrorGrid.Visibility = Visibility.Collapsed;
        }

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }
    }
}