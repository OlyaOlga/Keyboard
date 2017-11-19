using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using KeyboardModel.Annotations;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    public class Model:
        INotifyPropertyChanged
    {
        
        private InputTextQueue text;
        private Timer timer;
        public Complexity Complexity { get; set; }
        public Time Time { get; set; }
        public Language Language { get; set; }
        public Timer Timer {
            get { return timer; }
            set
            {
                timer = value;
                OnPropertyChanged(nameof(Timer));
            } }
        public InputTextQueue Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private string wayToFile;

        public Model(Complexity complexity, Time time, Language language)
        {
            Complexity = complexity;
            Time = time;
            Language = language;
            wayToFile = complexity.ToString() + '/' + language.ToString()+".txt";
            Text = new InputTextQueue(File.ReadAllText(wayToFile));
            Timer = new Timer(300);
            Timer.Elapsed += OnTimeElapsed;
        }

        private int quantity = 0;

        public void OnTimeElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("time");
            System.Timers.Timer t = sender as System.Timers.Timer;
            Console.WriteLine(e.SignalTime);
            if (t!=null)
            {
                if ((int) Time/300 < quantity)
                {
                    t.Stop();
                }
            }
            ++quantity;
        }

        public void StartTimer()
        {
            Timer.Start();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
