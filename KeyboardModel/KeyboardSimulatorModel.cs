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

        public KeyboardSimulatorModel(StatisticsIdentifier identifier)
        {
            Complexity = identifier.ComplexityID;
            Time = identifier.TimeID;
            Language = identifier.LanguageID;
            var path = Complexity.ToString() + '/' + Language.ToString() + ".txt";
            Text = new InputTextQueue(File.ReadAllText(path));
            Timer = new RemainedTimeTimer(60000 * (int)Time);
            StatisticsIdentifier = new StatisticsIdentifier(Complexity, Language, Time);
            ErrorStatistics = new ErrorStatistics();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler TextOver;

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

        public bool CheckCurrentSymbol(char symb)
        {
            bool res = false;
             if (Timer.IsTimerWorking == true)
            {
                try
                {
                    if (symb == Text.Peek())
                    {
                        ErrorStatistics.Correct();
                        Text.Pop();
                        OnPropertyChanged(nameof(Text));
                        res = true;
                    }
                    else
                    {
                        ErrorStatistics.Error();
                        res = false;
                    }
                }
                catch (Exception exception)
                {
                    OnTextOver(null);

                }
            }
            return res;
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

        protected virtual void OnTextOver(EventArgs e)
        {
            if (TextOver != null)
                TextOver(this, e);
        }
    }
}
