namespace Keyboard.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using KeyboardModel.Enums;
    using Keyboard.ViewModel.ViewModelExtension;
    using KeyboardModel.Statistic;

    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool[] LanguageRadioBox { get; set; } = new bool[3];

        public bool[] TimeRadioBox { get; set; } = new bool[3];

        public bool[] ComplexityRadioBox { get; set; } = new bool[2];

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
