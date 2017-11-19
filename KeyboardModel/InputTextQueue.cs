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

        public char Peek()
        {
            return Data.Peek();
        }

        public void Pop()
        {
            Data.Dequeue();
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
            return Data.Aggregate(string.Empty, (current, item) => current + item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
