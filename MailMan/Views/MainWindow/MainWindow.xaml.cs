using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;

using MailMan.Models;
using MailMan.Services.MailSenderService;
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

        private void OnSendNowButtonClick(object eventSender, RoutedEventArgs e)
        {
            var ctx = (MainWindowViewModel)DataContext;

            // Извлекаем исходные параметры по возможности
            if (ctx.SenderListSelected is not Sender sender) return;
            if (RecipientsList.SelectedItem is not Recipient recipient) return;
            if (ctx.ServerListSelected is not Server server) return;
            if (MessagesList.SelectedItem is not Message message) return;

            var mail_sender = ctx.MailSender;
            mail_sender.SendMailResultEvent += DisplayUserNotification;

            var serverConfig = new ServerConfiguration
            {
                Host = server.Name,
                Port = server.Port,
                UseSSL = server.UseSSL,
                Credentials = new NetworkCredential(server.Login, server.Password),
            };

            var messageConfig = new MessageConfiguration
            {
                From = sender.Address,
                To = new List<string>() { recipient.Address },
                Subject = message.Title,
                Body = message.Text
            };

            try
            {
                // Запускаем таймер
                var timer = Stopwatch.StartNew();
                // И запускаем процесс отправки почты
                mail_sender.SendMail(messageConfig, serverConfig);
                timer.Stop();
                MessageBox.Show(
                $"Почта успешно отправлена за {timer.Elapsed.TotalSeconds:0.##}c",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            catch (SmtpException ex)
            {
                MessageBox.Show(
                $"{ex.Message}",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            finally
            {
                mail_sender.SendMailResultEvent -= DisplayUserNotification;
            }
        }

        private void DisplayUserNotification(object sender, SendMailResult result)
        {
            UserNotificationDialog uDialog = new();
            uDialog.Owner = this;
            var ctx = (UserNotificationDialogViewModel)uDialog.DataContext;
            ctx.Title = "Отправка почты";
            if (result.IsSuccessfull)
            {
                
                ctx.NotificationText = "Письмо было успешно отправлено";
                ctx.TextForeground = Brushes.DarkGreen;
            }
            else
            {
                ctx.NotificationText = $"Письмо не было отправлено по причине:{Environment.NewLine}{result.Message}";
                ctx.TextForeground = Brushes.DarkRed ;
            }          
            
            uDialog.ShowDialog();
        }
    }
}
