using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp5.Pages
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            SettingsNavFram.Content = new Launcher(); // default page for settings navigation
        }


        private void SettingsLoaded(object sender, RoutedEventArgs e) 
        {

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
            SettingsGrid.RenderTransform = translateTransform;

            SettingsGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            translateTransform.BeginAnimation(TranslateTransform.XProperty, slideDownAnimation);

        }

        private void HomeNav_Click(object sender, RoutedEventArgs e) // Launcher settings
        {
            LauncherSettingsNav.IsActive = true;
            AccountSettingsNav.IsActive = false;
            GameSettingsNav.IsActive = false;

            if (!(SettingsNavFram.Content is Launcher))
            {
                SettingsNavFram.Navigate(new Launcher());
            }
        }

        private void LibraryNav_Click(object sender, RoutedEventArgs e) // account settings 
        {
            LauncherSettingsNav.IsActive = false;
            AccountSettingsNav.IsActive = true;
            GameSettingsNav.IsActive = false;

            if (!(SettingsNavFram.Content is Account))
            {
                SettingsNavFram.Navigate(new Account());
            }
        }

        private void GameSettingsNav_Click(object sender, RoutedEventArgs e) // game settings
        {
            LauncherSettingsNav.IsActive = false;
            AccountSettingsNav.IsActive = false;
            GameSettingsNav.IsActive = true;

            if (!(SettingsNavFram.Content is Game))
            {
                SettingsNavFram.Navigate(new Game());
            }
        }
    }
}
