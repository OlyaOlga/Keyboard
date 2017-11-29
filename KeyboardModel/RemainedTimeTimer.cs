namespace KeyboardModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using KeyboardModel.Annotations;

    public class RemainedTimeTimer : Timer, INotifyPropertyChanged
    {
        private bool hasTimerStarted;
        public RemainedTimeTimer(double interval) :
            base(interval)
        {
            this.AutoReset = true;
            hasTimerStarted = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Remain
        {
            get
            {
                if (hasTimerStarted == true)
                {
                    return EndTime - DateTime.Now;
                }
                return TimeSpan.Zero;
            }
        }

        public new void Start()
        {
            base.Start();
            StartTime = DateTime.Now;
            EndTime = StartTime.AddMilliseconds(Interval);
            hasTimerStarted = true;
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}