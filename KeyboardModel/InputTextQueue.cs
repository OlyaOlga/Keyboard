using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KeyboardModel.Annotations;

namespace KeyboardModel
{
    public class InputTextQueue:
        INotifyPropertyChanged
    {
        public Queue<char> data;

        public Queue<char> Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public InputTextQueue(string data)
        {
            Data = new Queue<char>();
            for (int i = 0; i < data.Length; ++i)
            {
                Data.Enqueue(data[i]);
            }
        }

        public override string ToString()
        {
            string total= string.Empty;
            //List<char> queueList = Data.ToList();
            foreach (var item in Data)
            {
                total += item;
            }
            return total;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
