using System.Linq;
using System.Windows;

using MailMan.Models;

namespace MailMan.Views.UserDialog
{
    /// <summary>
    /// Interaction logic for MessageEditDialog.xaml
    /// </summary>
    public partial class MessageEditDialog : Window
    {
        public MessageEditDialog()
        {
            InitializeComponent();
        }

        public static bool ShowDialog(string title, Message msg)
        {
            var window = new MessageEditDialog
            {
                Title = title,
                tbTitle = { Text = msg.Title },
                tbMessage = { Text = msg.Text },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (window.ShowDialog() is false) return false;
            msg.Title = window.tbTitle.Text;
            msg.Text = window.tbMessage.Text;
            return true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();

        }
    }
}
