using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.ViewModels.Base;

namespace MailMan.ViewModels.UserDialog
{
    public class EditServerDialogVM : ViewModel
    {
        private int _Id;
        private string _Name;
        private string _Address;
        private int _Port;
        private bool _UseSSL;
        private string _Login;
        private string _Password;
        private string _Description;

        public int Id { get =>_Id; set=>Set(ref _Id, value); }
        public string Name { get => _Name; set => Set(ref _Name, value); }

        public string Address { get => _Address; set => Set(ref _Address, value); }

        public int Port { get => _Port; set => Set(ref _Port, value); }

        public bool UseSSL { get => _UseSSL; set => Set(ref _UseSSL, value); }

        public string Login { get => _Login; set => Set(ref _Login, value); }

        public string Password { get => _Password; set => Set(ref _Password, value); }

        public string Description { get => _Description; set => Set(ref _Description, value); }

    }
}
