using System.ComponentModel;
using System.Runtime.CompilerServices;
using KeyboardModel.Annotations;

namespace KeyboardModel.Statistic
{
    using System;

    [Serializable]
    public class ErrorStatistics:INotifyPropertyChanged
    {
        private int errorItems;
        private int totalItems;
        private int correct;
        /// <summary>
        /// Gets all answers count
        /// </summary>
        public int TotalItems
        {
            get
            {
                return totalItems;
            }
            private set
            {
                totalItems = value;
                OnPropertyChanged(nameof(TotalItems));
            }
        }

        /// <summary>
        /// Gets error answers count
        /// </summary>
        public int ErrorItems
        {
            get
            {
                return errorItems;
            }
            private set
            {
                errorItems = value;
                OnPropertyChanged(nameof(ErrorItems));
            }
        }

        /// <summary>
        /// Gets pretty view statistic result
        /// </summary>
        public StatisticResult Result
        {
            get
            {
                if (TotalItems == 0)
                {
                    return new StatisticResult(0, 0);
                }
                double mistakesPercnet = ErrorItems / (double)TotalItems;
                return new StatisticResult(1 - mistakesPercnet, mistakesPercnet);
            }
        }

        /// <summary>
        /// Adds correct answer to statistic
        /// </summary>
        public void Correct() => ++TotalItems;

        /// <summary>
        /// Add Error answer to statistic
        /// </summary>
        public void Error()
        {
            ++TotalItems;
            ++ErrorItems;
        }

        /// <summary>
        /// Adds two statistics
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>Returns statistics with sum of total answers and error items</returns>
        public static ErrorStatistics operator +(ErrorStatistics left, ErrorStatistics right)
        {
            ErrorStatistics sum = new ErrorStatistics
            {
                TotalItems = left.TotalItems + right.TotalItems,
                ErrorItems = left.ErrorItems + right.ErrorItems
            };
            return sum;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
