using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp5.Pages
{
    public partial class Shop : Page
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void ShopPageLoaded(object sender, RoutedEventArgs e)
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
            ShopGrid.RenderTransform = translateTransform;

            ShopGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideDownAnimation);
        }

    }
}
