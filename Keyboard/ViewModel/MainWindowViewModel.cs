using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Keyboard.Command;

namespace Keyboard.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(WindowMediator settings)
        {
            Settings = new SettingsViewModel();
            SettingsInvoker = settings;
            SettingsInvoker.OnClose += Settings_OnClose;

            OpenSettingsCommand = new RelayCommand(OpenSettings);
            KeyDownCommand = new RelayCommand(KeyDown);
        }

        /// <summary>
        /// View model for Settings window
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
            string lang = "[\n";
            foreach (var i in Settings.Language)
            {
                lang += i + "\n";
            }
            lang += "]";

            string time = "[\n";
            foreach (var i in Settings.Time)
            {
                time += i + "\n";
            }
            time += "]\n";

            string comp = "[\n";
            foreach (var i in Settings.Complexity)
            {
                comp += i + "\n";
            }
            comp += "]";

            MessageBox.Show($"Language: \n{lang}\n Time: \n{time}\n Complexity:\n {comp}");
        }

        private void KeyDown(object parameter)
        {
            Console.WriteLine($"key down -> [{parameter?.ToString()}]");
        }
    }
}
