using System.Windows;

namespace MailMan.Views.UserNotificationDialog
{
    public partial class UserNotificationDialog : Window
    {
        public UserNotificationDialog() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
