using DiscordRPC;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfApp5.Utilities;

namespace WpfApp5.Pages
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

          /*  try
            {
                using (WebClient wc = new WebClient())
                {
                    string username = wc.DownloadString($"{Properties.Settings.Default.Email}"); // you gotta add this yourself
                    HelloTextBlock.Text = "Hello, " + username + "!";
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string errorResponse = reader.ReadToEnd();
                        MessageBox.Show("Error Response: " + errorResponse);
                    }
                }
                else
                {
                    MessageBox.Show("WebException occurred: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }*/

        }

        private void HomeLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RPC.client != null && RPC.client.CurrentUser != null)
                {
                    DIscordUsername.Text = RPC.client.CurrentUser.Username;
                    var bitmapImage = new BitmapImage(new Uri(RPC.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG)));
                    SkinBorder.Background = new ImageBrush(bitmapImage);
                    AvatarBorder.Background = new ImageBrush(bitmapImage);
                    DiscordIdText.Text = RPC.client.CurrentUser.ID.ToString();
                }

                else
                {
                    var defaultAvatarImage = new BitmapImage(new Uri("pack://application:,,,/Assets/DefaultAvatar.png"));

                    SkinBorder.Background = new ImageBrush(defaultAvatarImage);
                    AvatarBorder.Background = new ImageBrush(defaultAvatarImage);

                    DIscordUsername.Text = "uasset";
                    DiscordIdText.Text = "1130381151937245237";

                }
            }
            catch (Exception ex)
            {
                Logs.Log("Failed to load RPC: " + ex.Message);


                var defaultAvatarImage = new BitmapImage(new Uri("pack://application:,,,/Assets/DefaultAvatar.png"));

                AvatarBorder.Background = new ImageBrush(defaultAvatarImage);
                SkinBorder.Background = new ImageBrush(defaultAvatarImage);

                DIscordUsername.Text = "uasset";
                DiscordIdText.Text = "1130381151937245237";

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
            HomeGrid.RenderTransform = translateTransform;

            HomeGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideDownAnimation);
        }

        private void NewsButton2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewsButton1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewsButton3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}