using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
        }

        public bool[] Language { get; set; } = new bool[3];

        public bool[] Time { get; set; } = new bool[3];

        public bool[] Complexity { get; set; } = new bool[2];

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
