namespace Keyboard.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using KeyboardModel.Enums;
    using Keyboard.ViewModel.ViewModelExtension;
    using KeyboardModel.Statistic;

    public class SettingsViewModel : INotifyPropertyChanged
    {
        private bool[] languageRadioBox;

        private bool[] timeRadioBox;

        private bool[] complexityRadioBox;

        public SettingsViewModel()
        {
            LanguageRadioBox = new bool[3];
            TimeRadioBox = new bool[3];
            ComplexityRadioBox = new bool[2];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool[] LanguageRadioBox
        {
            get
            {
                return languageRadioBox;
            }
            set
            {
                languageRadioBox = value;
                OnPropertyChanged(nameof(LanguageRadioBox));
            }
        }

        public bool[] TimeRadioBox
        {
            get
            {
                return timeRadioBox;
            }
            set
            {
                timeRadioBox = value;
                OnPropertyChanged(nameof(TimeRadioBox));
            }
        }

        public bool[] ComplexityRadioBox
        {
            get
            {
                return complexityRadioBox;
            }
            set
            {
                complexityRadioBox = value;
                OnPropertyChanged(nameof(ComplexityRadioBox));
            }
        }

        public Language Language
        {
            get
            {
                Language value;
                EnumBuilder<Language>.MakeEnum(LanguageRadioBox, out value);
                return value;
            }
        }

        public Complexity Complexity
        {
            get
            {
                Complexity value;
                EnumBuilder<Complexity>.MakeEnum(ComplexityRadioBox, out value);
                return value;
            }
        }

        public Time Time
        {
            get
            {
                Time value;
                EnumBuilder<Time>.MakeEnum(TimeRadioBox, out value);
                return value;
            }
        }

        public StatisticsIdentifier Setting => new StatisticsIdentifier(Complexity, Language, Time);
    
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
