using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KeyboardModel.Statistic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using KeyboardModel.Annotations;

namespace Keyboard.ViewModel
{
    public class StatisticsViewModel:
        INotifyPropertyChanged
    {
        private GlobalStatistics globalStatistics;
        private StatisticsIdentifier currentStatisticsIdentifier;
        private ErrorStatistics currentErrorStatistics;

        public StatisticsViewModel(GlobalStatistics parGlobalStatistics,
            StatisticsIdentifier parCurrentStatisticsIdentifier, ErrorStatistics parCurrentErrorStatistics)
        {
            GlobalStatistics = parGlobalStatistics;
            CurrentStatisticsIdentifier = parCurrentStatisticsIdentifier;
            CurrentErrorStatistics = parCurrentErrorStatistics;
        }

        public GlobalStatistics GlobalStatistics
        {
            get { return globalStatistics; }
            set
            {
                globalStatistics = value;
                OnPropertyChanged(nameof(GlobalStatistics));
            }
        }

        public StatisticsIdentifier CurrentStatisticsIdentifier
        {
            get { return currentStatisticsIdentifier; }
            set
            {
                currentStatisticsIdentifier = value;
                OnPropertyChanged(nameof(CurrentStatisticsIdentifier));
            }
        }

        public ErrorStatistics CurrentErrorStatistics
        {
            get { return currentErrorStatistics; }
            set
            {
                currentErrorStatistics = value;
                OnPropertyChanged(nameof(CurrentErrorStatistics));
            }
        }

        public List<KeyValuePair< string, List< KeyValuePair<string, int>>>> GlobalStatisticsKeyValuePairs
        {
            get
            {
                List<KeyValuePair<string, List<KeyValuePair<string, int>>>> result = new List<KeyValuePair<string, List<KeyValuePair<string, int>>>>();
                foreach (var item in GlobalStatistics.LocalStatistics)
                {
                    var currentCorrectValue = new KeyValuePair<string, int>("Correct", (int) item.Value.Result.Correct);
                    var currentInCorrectValue = new KeyValuePair<string, int>("Incorrect", (int)item.Value.Result.Error);
                    var currentList = new List<KeyValuePair<string, int>> {currentCorrectValue, currentInCorrectValue};
                    result.Add(new KeyValuePair<string, List<KeyValuePair<string, int>>> (item.Key.ToString(), currentList));
                }
                return result;
            }
        }

        public List<KeyValuePair<string, int>> CurrentStatisticsKeyValuePairs
        {
            get
            {
                return new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("Correct", (int) CurrentErrorStatistics.Result.Correct),
                    new KeyValuePair<string, int>("Incorrect", (int) CurrentErrorStatistics.Result.Error)
                };
            }
        }

        public List<KeyValuePair<StatisticsIdentifier, ErrorStatistics>> StatisticsList
        {
            get
            {
                return GlobalStatistics.LocalStatistics.ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}