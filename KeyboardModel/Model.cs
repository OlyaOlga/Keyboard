using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KeyboardModel.Annotations;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    public class Model:
        INotifyPropertyChanged
    {
        private Queue<char> symbols;
        public Complexity Complexity { get; set; }
        public Time Time { get; set; }
        public Language Language { get; set; }
        private string wayToFile;

        public Queue<char> Symbols
        {
            get
            {
                return symbols;
            }
            set
            {
                symbols = value;
                OnPropertyChanged(nameof(Symbols));
            }
        }

        public Model(Complexity complexity, Time time, Language language)
        {
            Complexity = complexity;
            Time = time;
            Language = language;
            wayToFile = complexity.ToString() + '/' + language.ToString()+".txt";
            Console.WriteLine(wayToFile);
        }

        public string ReadData()
        {
            return File.ReadAllText(wayToFile);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
