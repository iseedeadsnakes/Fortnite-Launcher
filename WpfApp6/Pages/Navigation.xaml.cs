using System.Windows;
using System.Windows.Controls;

namespace WpfApp5.Pages
{

    public partial class Navigation : Page
    {
        public Navigation()
        {
            InitializeComponent();

            if (!(NavigationFrame.Content is Home))
            {
                NavigationFrame.Navigate(new Home());
            }
        }

        private void NavigationViewItem_Click(object sender, RoutedEventArgs e) // Shop
        {
            ShopNav.IsActive = true;
            LibraryNav.IsActive = false;
            HomeNav.IsActive = false;
            SettingsNav.IsActive = false;

            if (!(NavigationFrame.Content is Shop))
            {
                NavigationFrame.Navigate(new Shop());
            }

        }

        private void NavigationViewItem_Click_1(object sender, RoutedEventArgs e) // Library
        {
            LibraryNav.IsActive = true;
            HomeNav.IsActive = false;
            SettingsNav.IsActive = false;
            ShopNav.IsActive = false;

            if (!(NavigationFrame.Content is Library))
            {
                NavigationFrame.Navigate(new Library());
            }
        }

        private void NavigationViewItem_Click_2(object sender, RoutedEventArgs e) // Home
        {
            HomeNav.IsActive = true;
            SettingsNav.IsActive = false;
            ShopNav.IsActive = false;
            LibraryNav.IsActive = false;

            if (!(NavigationFrame.Content is Home))
            {
                NavigationFrame.Navigate(new Home());
            }
        }

        private void NavigationViewItem_Click_3(object sender, RoutedEventArgs e) // Settings
        {
            SettingsNav.IsActive = true;
            ShopNav.IsActive = false;
            LibraryNav.IsActive = false;
            HomeNav.IsActive = false;

            if (!(NavigationFrame.Content is Settings))
            {
                NavigationFrame.Navigate(new Settings());
            }
        }
    }

}
