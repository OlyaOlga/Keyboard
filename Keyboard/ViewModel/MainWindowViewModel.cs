using KeyboardModel;
using KeyboardModel.Enums;

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
        private SettingsViewModel settings;
        private StatisticsIdentifier currentSettings;
        private KeyboardSimulatorModel keyboardSimulatorModel;
        public MainWindowViewModel(WindowMediator settings)
        {
            Settings = new SettingsViewModel();
            SettingsInvoker = settings;
            SettingsInvoker.OnClose += Settings_OnClose;

            OpenSettingsCommand = new RelayCommand(OpenSettings);
            KeyDownCommand = new RelayCommand(KeyDown);
            StartCountTimeCommand = new RelayCommand(StartTimer);
            CurrentSettings = new StatisticsIdentifier(Complexity.Simple, Language.Eng, Time.OneMinute);
            keyboardSimulatorModel = new KeyboardSimulatorModel(CurrentSettings);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets view model for Settings window
        /// </summary>
        public SettingsViewModel Settings {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        public WindowMediator SettingsInvoker { get; set; }

        public ICommand OpenSettingsCommand { get; set; }

        public ICommand KeyDownCommand { get; set; }

        public ICommand StartCountTimeCommand { get; set; }

        /// <summary>
        /// Gets or sets current settings (time, complexity, etc.)
        /// </summary>
        public StatisticsIdentifier CurrentSettings
        {
            get
            {
                return currentSettings;
            }
            set
            {
                currentSettings = value;
                OnPropertyChanged(nameof(CurrentSettings));
            }
        }

        public KeyboardSimulatorModel KeyboardSimulatorModel
        {
            get
            {
                return keyboardSimulatorModel;
            }
            set
            {
                keyboardSimulatorModel = value;
                OnPropertyChanged(nameof(KeyboardSimulatorModel));
            }
        }
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

        private void StartTimer(object parameter)
        {
            keyboardSimulatorModel.StartTimer();
            Console.WriteLine("Timer started");
        }
    }
}
