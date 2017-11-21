namespace KeyboardModel
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using KeyboardModel.Annotations;
    using KeyboardModel.Enums;

    public class Model : INotifyPropertyChanged
    {
        private InputTextQueue text;
        private string wayToFile;
        private int quantity = 0;

        public Model(Complexity complexity, Time time, Language language)
        {
            Complexity = complexity;
            Time = time;
            Language = language;
            wayToFile = complexity.ToString() + '/' + language.ToString() + ".txt";
            Text = new InputTextQueue(File.ReadAllText(wayToFile));
            Timer = new RemainedTimeTimer(60000*(int)Time);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Complexity Complexity { get; set; }

        public Time Time { get; set; }

        public Language Language { get; set; }

        public InputTextQueue Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public RemainedTimeTimer Timer { get; }

        public void StartTimer()
        {
            Timer.Start();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
