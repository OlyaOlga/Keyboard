namespace KeyboardModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using KeyboardModel.Annotations;
    using KeyboardModel.Enums;

    public class RemainedTimeTimer : Timer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RemainedTimeTimer(double interval):
            base(interval)
        {
            this.AutoReset = true;
        }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Remain => EndTime - DateTime.Now;

        public new void Start()
        {
            base.Start();
            StartTime = DateTime.Now;
            EndTime = StartTime.AddMilliseconds(base.Interval);
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}