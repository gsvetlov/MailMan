using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailMan.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected string _title = "Title";
        public string Title { get => _title; set => Set(ref _title, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
