using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp5.Pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            AutoLoginSwitch.IsChecked = Properties.Settings.Default.AutoLogin;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            FadeInLoadingBuffer();

            // add your own integration for logging in

            /*try
            {
                string email = Email.Text;
                string password = Password.Password;

                using (HttpClient client = new HttpClient())
                {
                    var requestBody = new
                    {
                        email,
                        password
                    };

                    string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        if (responseContent.Contains("Welcome"))
                        {
                            FadeOutLoadingBuffer();

                            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                            if (mainWindow.MainFrame != null)
                            {
                                HttpResponseMessage setLoggedInResponse = await client.PostAsync($"{email}", null);
                                if (!setLoggedInResponse.IsSuccessStatusCode)
                                {
                                    ShowError("Failed to set login status in the server.");
                                }
                                else
                                {
                                    mainWindow.MainFrame.Navigate(new Navigation());
                                }
                            }
                            else
                            {
                                ShowError("I don't know what happened");
                                FadeOutLoadingBuffer();
                            }
                        }
                        else if (responseContent.Contains("User is banned"))
                        {
                            ShowError("You are banned.");
                            FadeOutLoadingBuffer();
                        }
                        else
                        {
                            ShowError("Invalid Credentials");
                            FadeOutLoadingBuffer();
                        }
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("User is banned"))
                        {
                            ShowError("You are banned.");
                            FadeOutLoadingBuffer();
                        }
                        else
                        {
                            ShowError("Invalid Credentials");
                            FadeOutLoadingBuffer();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Launcher Offline");
                FadeOutLoadingBuffer();
            }*/

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                mainWindow.MainFrame.Navigate(new Navigation());
            }
            else
            {
                ShowError("I don't know what happened");
                FadeOutLoadingBuffer();
            }

        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.Email = Email.Text;
            Properties.Settings.Default.Save();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.Password = Password.Password;
            Properties.Settings.Default.Save();
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoLogin = true;
            Properties.Settings.Default.Save();
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoLogin = false;
            Properties.Settings.Default.Save();
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

        public void FadeInLoadingBuffer()
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

        public void FadeOutLoadingBuffer()
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
    }
}
