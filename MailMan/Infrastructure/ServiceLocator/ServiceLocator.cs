
using MailMan.ViewModels;
using MailMan.ViewModels.UserDialog;

using Microsoft.Extensions.DependencyInjection;

namespace MailMan
{
    public class ServiceLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public NotifyUserDialogViewModel NotifyUserDialogViewModel => App.Services.GetRequiredService<NotifyUserDialogViewModel>();
        public EditServerDialogVM EditServerDialogVM => App.Services.GetRequiredService<EditServerDialogVM>();
    }
}
