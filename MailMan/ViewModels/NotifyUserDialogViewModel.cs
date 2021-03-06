
using System.Windows.Input;
using System.Windows.Media;

using MailMan.ViewModels.Base;
using MailMan.ViewModels.Commands;

namespace MailMan.ViewModels
{
    public class NotifyUserDialogViewModel : ViewModel
    {
        private string _text = "This is message text";
        private string _button = "Ok";
        private double _height = 150;
        private double _width = 250;
        private Brush _foreground = Brushes.Black;
        public string NotificationText { get => _text; set => Set(ref _text, value); }
        public string ButtonContent { get => _button; set => Set(ref _button, value); }
        public double Height { get => _height; set => Set(ref _height, value); }
        public double Width { get => _width; set => Set(ref _width, value); }
        public Brush TextForeground { get => _foreground; set => Set(ref _foreground, value); }

        private ICommand _CloseWindowCommand;
        public ICommand CloseWindowCommand => _CloseWindowCommand ??= new CloseWindowCommand();
        
    }
}
