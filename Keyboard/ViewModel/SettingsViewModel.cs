namespace Keyboard.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool[] Language { get; set; } = new bool[3];

        public bool[] Time { get; set; } = new bool[3];

        public bool[] Complexity { get; set; } = new bool[2];

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
