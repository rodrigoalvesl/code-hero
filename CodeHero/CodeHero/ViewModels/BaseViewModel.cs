using System.ComponentModel;

namespace CodeHero.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
