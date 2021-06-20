using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using MailMan.Models;
using MailMan.Models.Base;
using MailMan.Services.MailSenderService;
using MailMan.Services.Repositories;
using MailMan.Services.Repositories.Base;
using MailMan.ViewModels.Base;
using MailMan.Views.UserDialog;

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
        private ObservableCollection<MailingList> _MailingLists;
        public ObservableCollection<Server> Servers { get => _Servers; set => Set(ref _Servers, value); }
        public ObservableCollection<Sender> Senders { get => _Senders; set => Set(ref _Senders, value); }
        public ObservableCollection<Recipient> Recipients { get => _Recipients; set => Set(ref _Recipients, value); }
        public ObservableCollection<Message> Messages { get => _Messages; set => Set(ref _Messages, value); }
        public ObservableCollection<MailingList> MailingLists { get => _MailingLists; set => Set(ref _MailingLists, value); }

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
            _MailingLists = new(mailingListRepository.GetAll());
        }

        private Server _ServerListSelected;
        public Server ServerListSelected { get => _ServerListSelected; set => Set(ref _ServerListSelected, value); }
        
        private Sender _SenderListSelected;
        public Sender SenderListSelected { get => _SenderListSelected; set => Set(ref _SenderListSelected, value); }
        
        private Recipient _RecipientListSelected;
        public Recipient RecipientListSelected { get => _RecipientListSelected; set => Set(ref _RecipientListSelected, value); }
        
        private Message _MessageListSelected;
        public Message MessageListSelected { get => _MessageListSelected; set => Set(ref _MessageListSelected, value); }
        
        private MailingList _MailingListSelected;
        public MailingList MailingListSelected { get => _MailingListSelected; set => Set(ref _MailingListSelected, value); }

        #region Команды тулбокса
        #region AddSenderCommand
        private ICommand _AddSenderCommand;
        public ICommand AddSenderCommand => _AddSenderCommand ??= new LambdaCommand(OnAddSenderCommandExecuted);
        private void OnAddSenderCommandExecuted(object obj)
        {
            var select = SenderListSelected;
            AddCommand(Senders, senderRepository, SenderEditDialog.ShowDialog, "Добавить отправителя", ref select);
            SenderListSelected = select;
        }
        #endregion
        #region EditSenderCommand
        private ICommand _EditSenderCommand;
        public ICommand EditSenderCommand => _EditSenderCommand ??= new LambdaCommand(OnEditSenderCommandExecuted, CanEditSenderCommandExecute);
        private bool CanEditSenderCommandExecute(object arg) => arg is Sender;
        private void OnEditSenderCommandExecuted(object obj)
        {
            var select = SenderListSelected;
            EditCommand(obj, Senders, senderRepository, SenderEditDialog.ShowDialog, "Редактировать отправителя", ref select);
            SenderListSelected = select;
        }
        #endregion
        #region RemoveSenderCommand
        private ICommand _RemoveSenderCommand;
        public ICommand RemoveSenderCommand => _RemoveSenderCommand ??= new LambdaCommand(OnRemoveSenderCommandExecuted, CanRemoveSenderCommandExecute);
        private bool CanRemoveSenderCommandExecute(object arg) => arg is Sender;
        private void OnRemoveSenderCommandExecuted(object obj)
        {
            var select = SenderListSelected;
            RemoveCommand(obj, Senders, senderRepository, ref select);
            SenderListSelected = select;
        }
        #endregion

        #region AddServerCommand
        private ICommand _AddServerCommand;
        public ICommand AddServerCommand => _AddServerCommand ??= new LambdaCommand(OnAddServerCommandExecuted);
        private void OnAddServerCommandExecuted(object obj)
        {
            var select = ServerListSelected;
            AddCommand(Servers, serverRepository, ServerEditDialog.ShowDialog, "Добавить сервер", ref select);
            ServerListSelected = select;
        }
        #endregion
        #region EditServerCommand
        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);
        private bool CanEditServerCommandExecute(object arg) => arg is Server;
        private void OnEditServerCommandExecuted(object obj)
        {
            var select = ServerListSelected;
            EditCommand(obj, Servers, serverRepository, ServerEditDialog.ShowDialog, "Редактировать сервер", ref select);
            ServerListSelected = select;
        }
        #endregion
        #region RemoveServerCommand
        private ICommand _RemoveServerCommand;
        public ICommand RemoveServerCommand => _RemoveServerCommand ??= new LambdaCommand(OnRemoveServerCommandExecuted, CanRemoveServerCommandExecute);
        private bool CanRemoveServerCommandExecute(object arg) => arg is Server;
        private void OnRemoveServerCommandExecuted(object obj)
        {
            var select = ServerListSelected;
            RemoveCommand(obj, Servers, serverRepository, ref select);
            ServerListSelected = select;
        }
        #endregion

        #region AddRecipientCommand
        private ICommand _AddRecipientCommand;
        public ICommand AddRecipientCommand => _AddRecipientCommand ??= new LambdaCommand(OnAddRecipientCommandExecuted);
        private void OnAddRecipientCommandExecuted(object obj)
        {
            var select = RecipientListSelected;
            AddCommand(Recipients, recipientRepository, RecipientEditDialog.ShowDialog, "Добавить получателя", ref select);
            RecipientListSelected = select;
        }
        #endregion
        #region EditRecipientCommand
        private ICommand _EditRecipientCommand;
        public ICommand EditRecipientCommand => _EditRecipientCommand ??= new LambdaCommand(OnEditRecipientCommandExecuted, CanEditRecipientCommandExecute);
        private bool CanEditRecipientCommandExecute(object arg) => arg is Recipient;
        private void OnEditRecipientCommandExecuted(object obj)
        {
            var select = RecipientListSelected;
            EditCommand(obj, Recipients, recipientRepository, RecipientEditDialog.ShowDialog, "Редактировать получателя", ref select);
            RecipientListSelected = select;
        }
        #endregion
        #region RemoveRecipientCommand
        private ICommand _RemoveRecipientCommand;
        public ICommand RemoveRecipientCommand => _RemoveRecipientCommand ??= new LambdaCommand(OnRemoveRecipientCommandExecuted, CanRemoveRecipientCommandExecute);
        private bool CanRemoveRecipientCommandExecute(object arg) => arg is Recipient;
        private void OnRemoveRecipientCommandExecuted(object obj)
        {
            var select = RecipientListSelected;
            RemoveCommand(obj, Recipients, recipientRepository, ref select);
            RecipientListSelected = select;
        }
        #endregion

        #region AddMessageCommand
        private ICommand _AddMessageCommand;
        public ICommand AddMessageCommand => _AddMessageCommand ??= new LambdaCommand(OnAddMessageCommandExecuted);
        private void OnAddMessageCommandExecuted(object obj)
        {
            var select = MessageListSelected;
            AddCommand(Messages, messageRepository, MessageEditDialog.ShowDialog, "Добавить письмо", ref select);
            MessageListSelected = select;
        }
        #endregion
        #region EditMessageCommand
        private ICommand _EditMessageCommand;
        public ICommand EditMessageCommand => _EditMessageCommand ??= new LambdaCommand(OnEditMessageCommandExecuted, CanEditMessageCommandExecute);
        private bool CanEditMessageCommandExecute(object arg) => arg is Message;
        private void OnEditMessageCommandExecuted(object obj)
        {
            var select = MessageListSelected;
            EditCommand(obj, Messages, messageRepository, MessageEditDialog.ShowDialog, "Редактировать письмо", ref select);
            MessageListSelected = select;
        }
        #endregion
        #region RemoveMessageCommand
        private ICommand _RemoveMessageCommand;
        public ICommand RemoveMessageCommand => _RemoveMessageCommand ??= new LambdaCommand(OnRemoveMessageCommandExecuted, CanRemoveMessageCommandExecute);
        private bool CanRemoveMessageCommandExecute(object arg) => arg is Message;
        private void OnRemoveMessageCommandExecuted(object obj)
        {
            var select = MessageListSelected;
            RemoveCommand(obj, Messages, messageRepository, ref select);
            MessageListSelected = select;
        }
        #endregion

        #endregion


        private void AddCommand<T>(Collection<T> collection, IRepository<T> repo, Func<string, T, bool> dialog, string title, ref T select) where T : BaseModel, new()
        {
            T entity = new();
            if (dialog(title, entity) is false) return;
            entity = repo.Create(entity);
            collection.Add(entity);
            select = entity;
        }

        private void EditCommand<T>(object obj,
                                    Collection<T> collection,
                                    IRepository<T> repo,
                                    Func<string, T, bool> dialog,
                                    string title,
                                    ref T selected) where T : BaseModel
        {
            if (obj is not T entity) return;
            int listIdx = collection.IndexOf(entity);
            if (listIdx == -1 || dialog(title, entity) is false || repo.Update(entity, s => s.Id == entity.Id) is false) return;
            collection.RemoveAt(listIdx);
            collection.Insert(listIdx, entity);
            selected = entity;
        }

        private void RemoveCommand<T>(object obj, Collection<T> collection, IRepository<T> repo, ref T selected) where T : BaseModel
        {
            if (obj is not T entity) return;
            int listIdx = collection.IndexOf(entity);
            if (listIdx-- < 0 || !repo.Remove(entity)) return;
            if (collection.Remove(entity) && collection.Count > 0)
                selected = collection[FitInRange(collection.Count, listIdx)];
        }
        private static int FitInRange(int size, int index)
        {
            if (index < 0) index = 0;
            else if (index >= size) index = size - 1;
            return index;
        }
    }
}
