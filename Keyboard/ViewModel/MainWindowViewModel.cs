using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KeyboardModel;
using KeyboardModel.Enums;

namespace Keyboard.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using Keyboard.Command;
    using Keyboard.ViewModel.ViewModelExtension;
    using KeyboardModel.Statistic;
    using System.Timers;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string waitPackMan = "../Images/pac_man_wait.jpg";
        private string eatingPackMan = "../Images/pac_man_eats.jpg";
        private string statisticsFileName = "../../statistics.txt";
        private SettingsViewModel settings;
        private StatisticsIdentifier currentSettings;
        private KeyboardSimulatorModel keyboardSimulatorModel;
        private Timer timer;
        private TimeSpan timeElapsed;
        private string imgWay;
        private GlobalStatistics globalStatistics;
        private StatisticsViewModel statisticsViewModel;
        public MainWindowViewModel(WindowMediator settings)
        {
            Settings = new SettingsViewModel();
            SettingsInvoker = settings;
            SettingsInvoker.OnClose += Settings_OnClose;

            
            CurrentSettings = new StatisticsIdentifier(Complexity.Simple, Language.Ukr, Time.OneMinute);
            keyboardSimulatorModel = new KeyboardSimulatorModel(CurrentSettings);
            timer = new Timer(1000);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
            timeElapsed = new TimeSpan(0);
            keyboardSimulatorModel.Timer.Elapsed += StopTimer;
            keyboardSimulatorModel.TextOver += TextIsOver;
               
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            KeyDownCommand = new RelayCommand(KeyDown);
            StartCountTimeCommand = new RelayCommand(StartTimer, (o =>
            {
                return (o != null && !(o as KeyboardSimulatorModel).Timer.IsTimerWorking);
            }));
            StopCountTimeCommand = new RelayCommand(StopTimer, (o =>
            {
                return (o != null && (o as KeyboardSimulatorModel).Timer.IsTimerWorking);
            }));
            ImgWay = waitPackMan;
            GlobalStatistics = new GlobalStatistics(statisticsFileName);
            StatisticsViewModel = new StatisticsViewModel(globalStatistics, keyboardSimulatorModel.StatisticsIdentifier, keyboardSimulatorModel.ErrorStatistics);
            OpenStatisticsCommand = new RelayCommand(OpenStatistics);
        }

        public StatisticsViewModel StatisticsViewModel
        {
            get { return statisticsViewModel; }
            set
            {
                statisticsViewModel = value;
                OnPropertyChanged(nameof(StatisticsViewModel));
            }
        }

        public TimeSpan TimeElapsed
        {
            get
            {
                return timeElapsed;
            }
            private set
            {
                timeElapsed = value;
                OnPropertyChanged(nameof(TimeElapsed));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets view model for Settings window
        /// </summary>
        public SettingsViewModel Settings {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        public string ImgWay
        {
            get
            {
                return imgWay;
            }
            set
            {
                imgWay = value;
                OnPropertyChanged(nameof(ImgWay));
            }
        } 

        public WindowMediator SettingsInvoker { get; set; }

        public ICommand OpenSettingsCommand { get; set; }

        public ICommand KeyDownCommand { get; set; }

        public ICommand StartCountTimeCommand { get; set; }

        public ICommand StopCountTimeCommand { get; set; }

        public ICommand OpenStatisticsCommand { get; set; }

        /// <summary>
        /// Gets or sets current settings (time, complexity, etc.)
        /// </summary>
        public StatisticsIdentifier CurrentSettings
        {
            get
            {
                return currentSettings;
            }
            set
            {
                currentSettings = value;
                OnPropertyChanged(nameof(CurrentSettings));
            }
        }

        public KeyboardSimulatorModel KeyboardSimulatorModel
        {
            get
            {
                return keyboardSimulatorModel;
            }
            set
            {
                keyboardSimulatorModel = value;
                OnPropertyChanged(nameof(KeyboardSimulatorModel));
            }
        }

        public GlobalStatistics GlobalStatistics
        {
            get
            {
                return globalStatistics;
            }
            set
            {
                globalStatistics = value;
                OnPropertyChanged(nameof(GlobalStatistics));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenSettings(object parametr)
        {
            SettingsInvoker.ShowDialog(Settings);
        }

        private void Settings_OnClose(object sender, EventArgs e)
        {
            if (Settings.ShouldSaveChanges)
            {
                CurrentSettings = Settings.Setting;
                KeyboardSimulatorModel.TextOver -= TextIsOver;
                KeyboardSimulatorModel.Timer.Elapsed -= StopTimer;
                KeyboardSimulatorModel = new KeyboardSimulatorModel(CurrentSettings);
                StatisticsViewModel.CurrentStatisticsIdentifier = KeyboardSimulatorModel.StatisticsIdentifier;
                StatisticsViewModel.CurrentErrorStatistics = KeyboardSimulatorModel.ErrorStatistics;
                KeyboardSimulatorModel.TextOver += TextIsOver;
                KeyboardSimulatorModel.Timer.Elapsed += StopTimer;
                Settings.ShouldSaveChanges = false;
            }
        }

        private void KeyDown(object parameter)
        {
            var key = (Key) parameter;
            if (key == Key.LeftShift|| key == Key.RightShift || key == Key.CapsLock || key == Key.LeftAlt||key==Key.RightAlt)
            {
                Console.WriteLine("System key was pressed");
                return;
            }
            if (KeyboardSimulatorModel.CheckCurrentSymbol(key.GetCharFromKey()))
            {
                Thread thread = new Thread(() =>
                {
                    ImgWay = eatingPackMan;
                    Timer t = new Timer(90);
                    t.AutoReset = false;
                    t.Elapsed += ChangeImg;
                    t.Start();
                });
                thread.Start();
                thread.Join();
            }
            Console.WriteLine(key.GetCharFromKey());
        }

        private void ChangeImg(object sender, ElapsedEventArgs e)
        {
            ImgWay = waitPackMan;
        }

        private void StartTimer(object parameter)
        {
            KeyboardSimulatorModel.StartTimer();
            StatisticsViewModel.CurrentStatisticsIdentifier = KeyboardSimulatorModel.StatisticsIdentifier;
            StatisticsViewModel.CurrentErrorStatistics = KeyboardSimulatorModel.ErrorStatistics;
        }

        private void StopTimer(object parameter)
        {
            StopTimer();
        }

        private void OpenStatistics(object parametr)
        {
            var win = new Statistics(StatisticsViewModel);
            OnPropertyChanged(nameof(StatisticsViewModel.CurrentErrorStatistics));
            OnPropertyChanged(nameof(GlobalStatistics));
            StatisticsViewModel.GlobalStatistics = GlobalStatistics;
            win.ShowDialog();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimeElapsed = keyboardSimulatorModel.Timer.Remain;
        }

        private void TextIsOver(object sender, EventArgs e)
        {
            StopTimer(sender, null);
        }

        private void StopTimer(object sender, ElapsedEventArgs e)
        {
            var d = Application.Current.Dispatcher;//used the following code because this metod is called in another thread and without this piece of code it raises "The calling thread must be STA, because many UI components require this" exception
            if (d.CheckAccess())
            {
                StopTimer();
            }
            else
            {
                d.BeginInvoke((Action)StopTimer);
            }

        }

        private void StopTimer()
        {
            KeyboardSimulatorModel.TextOver -= TextIsOver;
            KeyboardSimulatorModel.Timer.Elapsed -= StopTimer;
            AddLocalToGlobalStatistics();
            KeyboardSimulatorModel = new KeyboardSimulatorModel(CurrentSettings);
            KeyboardSimulatorModel.Timer.Elapsed += StopTimer;
            KeyboardSimulatorModel.TextOver += TextIsOver;
            TimerOnElapsed(null, null);
            OpenStatistics(null);
        }

        private void AddLocalToGlobalStatistics()
        {
            if (GlobalStatistics.LocalStatistics.ContainsKey(keyboardSimulatorModel.StatisticsIdentifier))
            {
                GlobalStatistics.LocalStatistics[keyboardSimulatorModel.StatisticsIdentifier] +=
                    keyboardSimulatorModel.ErrorStatistics;
            }
            else
            {
                GlobalStatistics.LocalStatistics.Add(keyboardSimulatorModel.StatisticsIdentifier, keyboardSimulatorModel.ErrorStatistics);
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
           GlobalStatistics.Save(statisticsFileName);
        }
    }
}
