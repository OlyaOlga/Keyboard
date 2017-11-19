namespace Keyboard.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using Keyboard.Command;
    using Keyboard.ViewModel.ViewModelExtension;
    using KeyboardModel.Statistic;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(WindowMediator settings)
        {
            Settings = new SettingsViewModel();
            SettingsInvoker = settings;
            SettingsInvoker.OnClose += Settings_OnClose;

            OpenSettingsCommand = new RelayCommand(OpenSettings);
            KeyDownCommand = new RelayCommand(KeyDown);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets view model for Settings window
        /// </summary>
        public SettingsViewModel Settings { get; set; }

        public WindowMediator SettingsInvoker { get; set; }

        public ICommand OpenSettingsCommand { get; set; }

        public ICommand KeyDownCommand { get; set; }

        /// <summary>
        /// Gets or sets current settings (time, complexity, etc.)
        /// </summary>
        public StatisticsIdentifier CurrentSettings { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenSettings(object parametr)
        {
            SettingsInvoker.ShowDialog(Settings);
        }

        private void Settings_OnClose(object sender, EventArgs e)
        {
            CurrentSettings = Settings.Setting;
        }

        private void KeyDown(object parameter)
        {
            var key = (Key) parameter;
            Console.WriteLine(key.GetCharFromKey());
        }
    }
}
