using Microsoft.Toolkit.Uwp.Notifications;


namespace WpfApp5.Utilities
{
    class Toast
    {
        public static void ShowToast(string title, string message)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .Show();
        }
    }
}
