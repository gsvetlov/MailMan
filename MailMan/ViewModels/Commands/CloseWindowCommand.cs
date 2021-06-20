using System.Linq;
using System.Windows;

using MailMan.ViewModels.Base;

namespace MailMan.ViewModels.Commands
{
    public class CloseWindowCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            var window = parameter as Window;
            window ??= Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
            window ??= Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}
