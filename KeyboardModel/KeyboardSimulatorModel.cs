using KeyboardModel.Statistic;

namespace KeyboardModel
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using KeyboardModel.Annotations;
    using KeyboardModel.Enums;

    public class KeyboardSimulatorModel : INotifyPropertyChanged
    {
        private InputTextQueue text;

        private string fileName = "statistics.txt";

        public KeyboardSimulatorModel(Complexity complexity, Time time, Language language)
        {
            Complexity = complexity;
            Time = time;
            Language = language;
            var path = complexity.ToString() + '/' + language.ToString() + ".txt";
            Text = new InputTextQueue(File.ReadAllText(path));
            Timer = new RemainedTimeTimer(60000 * (int)Time);
            StatisticsIdentifier = new StatisticsIdentifier(complexity, language, time);
            ErrorStatistics = new ErrorStatistics();
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

        public StatisticsIdentifier StatisticsIdentifier { get; set; }

        public ErrorStatistics ErrorStatistics { get; set; }

        public void CheckCurrentSymbol(char symb)
        {
            if (symb == Text.Peek())
            {
                ErrorStatistics.Correct();
                Text.Pop();
            }
            else
            {
                ErrorStatistics.Error();
            }
        }

        public void StartTimer()
        {
            Timer.Start();
        }

        public void Correct()
        {
            ErrorStatistics.Correct();
        }

        public void Error()
        {
            ErrorStatistics.Error();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
