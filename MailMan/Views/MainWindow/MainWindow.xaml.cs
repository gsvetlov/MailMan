using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;

using MailMan.Data;
using MailMan.Models;
using MailMan.Services.MailSenderService;
using MailMan.ViewModels;
using MailMan.Views.UserDialog;
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
            if (cbMesagesList.SelectedItem is not Message message) return;


            var serverConfig = new ServerConfiguration
            {
                Host = server.Address,
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

            ctx.MailSender.SendMailResultEvent += DisplayUserNotification;
            ctx.MailSender.SendMail(messageConfig, serverConfig);

            /*try
            {
                mail_sender.SendMailResultEvent += DisplayUserNotification;
                mail_sender.SendMail(messageConfig, serverConfig);
                MessageBox.Show(
                $"Почта успешно отправлена",
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
            }*/
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
                ctx.TextForeground = Brushes.DarkRed;
            }

            uDialog.ShowDialog();
        }

        private void OnAddServerButtonClick(object _sender, RoutedEventArgs E)
        {
            if (!ServerEditDialog.Create(
            out var name,
            out var address,
            out var port,
            out var ssl,
            out var description,
            out var login,
            out var password))
                return;
            var server = new Server
            {
                Id = TestData.Servers.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };

            var ctx = (MainWindowViewModel)DataContext;
            ctx.Servers.Add(server);
            ServersList.SelectedItem = server;
        }

        private void OnEditServerButtonClick(object _sender, RoutedEventArgs e)
        {
            Server server = (Server)ServersList.SelectedItem;
            var ctx = (MainWindowViewModel)DataContext;
            int idx = ctx.Servers.IndexOf(server);
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var descr = server.Description;
            var login = server.Login;
            var pwd = server.Password;

            if (!ServerEditDialog.ShowDialog(
                "Редактирование сервера",
                ref name,
                ref address,
                ref port,
                ref ssl,
                ref descr,
                ref login,
                ref pwd)) return;

            var updServer = ctx.Servers[idx];
            updServer.Name = name;
            updServer.Address = address;
            updServer.Port = port;
            updServer.UseSSL = ssl;
            updServer.Description = descr;
            updServer.Login = login;
            updServer.Password = pwd;
            ServersList.SelectedItem = ctx.Servers[idx];
        }

        private void OnRemoveServerButtonClick(object _sender, RoutedEventArgs e)
        {
            var ctx = (MainWindowViewModel)DataContext;
            ctx.Servers.Remove(ctx.ServerListSelected);
            ServersList.SelectedIndex = 0;
        }

        private void OnAddSenderButtonClick(object _sender, RoutedEventArgs e)
        {
            if (!SenderEditDialog.Create(
                "Добавить отправителя",
                out string name,
                out string address,
                out string descriprion)) return;
            var sender = new Sender
            {
                Id = TestData.Senders.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Description = descriprion
            };
            var ctx = (MainWindowViewModel)DataContext;
            ctx.Senders.Add(sender);
            SendersList.SelectedItem = sender;
        }

        private void OnEditSenderButtonClick(object _sender, RoutedEventArgs e)
        {
            var ctx = (MainWindowViewModel)DataContext;
            var idx = ctx.Senders.IndexOf(ctx.SenderListSelected);
            string name = ctx.SenderListSelected.Name;
            string address = ctx.SenderListSelected.Address;
            string description = ctx.SenderListSelected.Description;
            if (!SenderEditDialog.ShowDialog(
                "Редактировать отправителя",
                ref name,
                ref address,
                ref description)) return;
            ctx.Senders[idx].Name = name;
            ctx.Senders[idx].Address = address;
            ctx.Senders[idx].Description = description;
            SendersList.SelectedItem = ctx.Senders[idx];
        }

        private void OnDeleteSenderButtonClick(object _sender, RoutedEventArgs e)
        {
            var ctx = (MainWindowViewModel)DataContext;
            ctx.Senders.Remove(ctx.SenderListSelected);
            ServersList.SelectedIndex = 0;
        }

    }
}