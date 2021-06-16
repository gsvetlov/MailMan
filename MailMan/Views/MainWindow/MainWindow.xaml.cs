using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MailMan.ViewModels;
using MailMan.Views.UserNotificationDialog;

namespace MailMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void ButtonRunClick(object sender, RoutedEventArgs e)
        {
            UserNotificationDialog uDialog = new();
            uDialog.Owner = this;
            var ctx = (UserNotificationDialogViewModel)uDialog.DataContext;
            ctx.Title = "Running";
            ctx.NotificationText = "Run button hit!";
            ctx.TextForeground = Brushes.DarkGreen;
            uDialog.ShowDialog();
        }

        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            UserNotificationDialog uDialog = new();
            uDialog.Owner = this;
            var ctx = (UserNotificationDialogViewModel)uDialog.DataContext;
            ctx.Title = "Stopped";
            ctx.NotificationText = "Process stopped!";
            ctx.TextForeground = Brushes.Red;
            uDialog.ShowDialog();
        }

        private void OnAboutProgramClick(object sender, RoutedEventArgs e)
        {
            UserNotificationDialog uDialog = new();
            uDialog.Owner = this;
            var ctx = (UserNotificationDialogViewModel)uDialog.DataContext;
            ctx.Title = "О программе";
            ctx.NotificationText = "Рассыльщик почты";
            ctx.TextForeground = Brushes.Black;
            uDialog.ShowDialog();
        }
    }
}
