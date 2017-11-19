using Keyboard.Model;

namespace Keyboard.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using Keyboard.Command;

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
            string lang = Settings.Language.Aggregate("[\n", (current, i) => current + (i + "\n"));
            lang += "]";

            string time = Settings.Time.Aggregate("[\n", (current, i) => current + (i + "\n"));
            time += "]\n";

            string comp = Settings.Complexity.Aggregate("[\n", (current, i) => current + (i + "\n"));
            comp += "]";

            MessageBox.Show($"Language: \n{lang}\n Time: \n{time}\n Complexity:\n {comp}");
        }

        private void KeyDown(object parameter)
        {
            var key = (Key) parameter;
            Console.WriteLine($"key down -> [{parameter?.ToString()}]");
            Console.WriteLine(key.GetCharFromKey());
        }
    }
}
