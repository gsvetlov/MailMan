using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace MailMan
{
    public class ServiceLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public NotifyUserDialogViewModel NotifyUserDialogViewModel => App.Services.GetRequiredService<NotifyUserDialogViewModel>();
    }
}
