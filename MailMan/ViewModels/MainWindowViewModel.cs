using System;
using System.Collections.ObjectModel;
using System.Linq;

using MailMan.Data;
using MailMan.Models;
using MailMan.Services.MailSenderService;
using MailMan.Services.Repositories;
using MailMan.ViewModels.Base;

namespace MailMan.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private IServerRepository serverRepository;
        private ISenderRepository senderRepository;
        private IRecipientRepository recipientRepository;
        private IMessageRepository messageRepository;
        private IMailingListRepository mailingListRepository;
        private IMailSenderService mailSenderService;

        private ObservableCollection<Server> _Servers;
        private ObservableCollection<Sender> _Senders;
        private ObservableCollection<Recipient> _Recipients;
        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Server> Servers { get => _Servers; set => Set(ref _Servers, value); }
        public ObservableCollection<Sender> Senders { get => _Senders; set => Set(ref _Senders, value); }
        public ObservableCollection<Recipient> Recipients { get => _Recipients; set => Set(ref _Recipients, value); }
        public ObservableCollection<Message> Messages { get => _Messages; set => Set(ref _Messages, value); }

       
        public MainWindowViewModel(IServerRepository serverRepository,
                                   ISenderRepository senderRepository,
                                   IRecipientRepository recipientRepository,
                                   IMessageRepository messageRepository,
                                   IMailingListRepository mailingListRepository,
                                   IMailSenderService mailSenderService)
        {
            this.serverRepository = serverRepository ?? throw new ArgumentNullException(nameof(serverRepository));
            this.senderRepository = senderRepository ?? throw new ArgumentNullException(nameof(senderRepository));
            this.recipientRepository = recipientRepository ?? throw new ArgumentNullException(nameof(recipientRepository));
            this.messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this.mailingListRepository = mailingListRepository ?? throw new ArgumentNullException(nameof(mailingListRepository));
            this.mailSenderService = mailSenderService ?? throw new ArgumentNullException(nameof(mailSenderService));
            _title = "MailMan: mass-email app";
            _Servers = new(serverRepository.GetAll());
            _Senders = new(senderRepository.GetAll());
            _Recipients = new(recipientRepository.GetAll());
            _Messages = new(messageRepository.GetAll());
        }

        public Server ServerListSelected { get; set; }
        public Sender SenderListSelected { get; set; }
    }
}
