using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.Data;
using MailMan.Models;
using MailMan.Services.MailSenderService;
using MailMan.ViewModels.Base;

namespace MailMan.ViewModels
{
    class MainWindowViewModel : ViewModel
    {

        private ObservableCollection<Server> _Servers; 
        private ObservableCollection<Sender> _Senders;
        private ObservableCollection<Recipient> _Recipients;
        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Server> Servers { get => _Servers; set => Set(ref _Servers, value); }
        public ObservableCollection<Sender> Senders { get => _Senders; set => Set(ref _Senders, value); }
        public ObservableCollection<Recipient> Recipients { get => _Recipients; set => Set(ref _Recipients, value); }
        public ObservableCollection<Message> Messages { get => _Messages; set => Set(ref _Messages, value); }
        public IMailSenderService MailSender { get; }

        public MainWindowViewModel()
        {
            _title = "MailMan: mass-email app";
            _Servers = new (TestData.Servers);
            _Senders = new(TestData.Senders);
            _Recipients = new(TestData.Recipients);
            _Messages = new(TestData.Messages);
            MailSender = SimpleSmtpService.GetService();
        }

        public Server ServerListSelected { get; set; }
        public Sender SenderListSelected { get; set; }
    }
}
