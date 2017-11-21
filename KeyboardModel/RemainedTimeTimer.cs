namespace KeyboardModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using KeyboardModel.Annotations;

    public class RemainedTimeTimer : Timer, INotifyPropertyChanged
    {
        public RemainedTimeTimer(double interval) :
            base(interval)
        {
            this.AutoReset = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Remain => EndTime - DateTime.Now;

        public new void Start()
        {
            base.Start();
            StartTime = DateTime.Now;
            EndTime = StartTime.AddMilliseconds(Interval);
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}