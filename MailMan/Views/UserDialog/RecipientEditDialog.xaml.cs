using System.Linq;
using System.Windows;

using MailMan.Models;

namespace MailMan.Views.UserDialog
{
    /// <summary>
    /// Interaction logic for RecipientEditDialog.xaml
    /// </summary>
    public partial class RecipientEditDialog : Window
    {
        public RecipientEditDialog()
        {
            InitializeComponent();
        }
        public static bool ShowDialog(string title, Recipient recipient)
        {
            var window = new RecipientEditDialog
            {
                Title = title,
                tbName = { Text = recipient.Name },
                tbAddress = { Text = recipient.Address },
                tbDescription = { Text = recipient.Description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (window.ShowDialog() is false) return false;
            recipient.Name = window.tbName.Text;
            recipient.Address = window.tbAddress.Text;
            recipient.Description = window.tbDescription.Text;
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
