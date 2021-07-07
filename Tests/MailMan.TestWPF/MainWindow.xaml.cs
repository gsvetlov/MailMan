using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailMan.TestWPF
{
    public partial class MainWindow
    {
        private static readonly string _bodyText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean id sodales massa, vitae rhoncus leo. Fusce diam lorem, pulvinar ut ullamcorper non, iaculis sed quam. Mauris sagittis vulputate magna, id eleifend est sagittis in. Ut ultrices leo ut ultrices vestibulum. Nunc scelerisque, orci vitae commodo semper, erat nisi cursus arcu.";
        public MainWindow()
        {
            InitializeComponent();
            BodyEdit.AppendText(_bodyText);
        }

        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            using var message = new MailMessage("svetlov.georg@yandex.ru", "svetlov.georg@yandex.ru");
            message.Subject = SubjectEdit.Text + DateTime.Now.ToString("F");
            message.Body = BodyEdit.Text;

            using var client = new SmtpClient("smtp.yandex.ru", 25)
            {                
                EnableSsl = true,
                Credentials = new NetworkCredential
                {
                    UserName = LoginEdit.Text,
                    SecurePassword = PasswordEdit.SecurePassword,
                }
            };


            try
            {
                client.Send(message);           
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
            }
        }
    }
}