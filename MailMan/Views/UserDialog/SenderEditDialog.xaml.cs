using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using MailMan.Models;

namespace MailMan.Views.UserDialog
{
    public partial class SenderEditDialog : Window
    {
        private SenderEditDialog()
        {
            InitializeComponent();
        }

        public static bool ShowDialog(string title, Sender sender)
        {
            var window = new SenderEditDialog
            {
                Title = title,
                tbName = { Text = sender.Name },
                tbAddress = { Text = sender.Address },
                tbDescription = { Text = sender.Description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (window.ShowDialog() is false) return false;
            sender.Name = window.tbName.Text;
            sender.Address = window.tbAddress.Text;
            sender.Description = window.tbDescription.Text;
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
