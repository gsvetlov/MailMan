using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailMan.Views.UserDialog
{
    /// <summary>
    /// Interaction logic for SenderEditDialog.xaml
    /// </summary>
    public partial class SenderEditDialog : Window
    {
        private SenderEditDialog()
        {
            InitializeComponent();
        }

        public static bool ShowDialog(string title, ref string name, ref string address, ref string description)
        {
            var window = new SenderEditDialog
            {
                Title = title,
                tbName = { Text = name },
                tbAddress = { Text = address },
                tbDescription = { Text = description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (window.ShowDialog() is not true) return false;
            name = window.tbName.Text;
            address = window.tbAddress.Text;
            description = window.tbDescription.Text;
            return true;
        }

        public static bool Create(string title, out string name, out string address, out string description)
        {
            name = null;
            address = null;
            description = null;
            return ShowDialog(title, ref name, ref address, ref description);
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

        private void PreviewAddressInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBlock tb || String.IsNullOrWhiteSpace(tb.Text)) return;
            e.Handled = !IsValidEmail(tb.Text);

        }

        // стянуто отсюда: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format?redirectedfrom=MSDN
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
