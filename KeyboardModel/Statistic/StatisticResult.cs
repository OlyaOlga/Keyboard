using System.ComponentModel;
using System.Runtime.CompilerServices;
using KeyboardModel.Annotations;

namespace KeyboardModel.Statistic
{
    using System;

    /// <summary>
    /// Represent statistic result in percents
    /// </summary>
    public struct StatisticResult:
        INotifyPropertyChanged
    {
        private double _correct;
        private double _error;
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticResult"/> struct
        /// </summary>
        /// <param name="correct">Perncent of correct asswers</param>
        /// <param name="error">Percnet of error answers</param>
        public StatisticResult(double correct, double error):this()
        {
            if (correct < 0 || correct > 1)
            {
                throw new ArgumentException("Percent must equls to [0;1] range", nameof(correct));
            }

            if (error < 0 || error > 1)
            {
                throw new ArgumentException("Percent must equls to [0;1] range", nameof(error));
            }

            if (Math.Abs(correct + error - 1) > float.Epsilon && correct+error!=0)
            {
                throw new ArgumentException("Sum of percents must equls to 1", nameof(correct) + ", " + nameof(error));
            }

            Correct = correct;
            Error = error;
        }

        /// <summary>
        /// Gets percent of correct answers
        /// </summary>
        public double Correct {
            get
            {
                return _correct*100;
            }
            set
            {
                _correct = value;
                OnPropertyChanged(nameof(Correct));
            }
        }

        /// <summary>
        /// Gets percent of error answers
        /// </summary>
        public double Error
        {
            get
            {
                return _error*100;
            }
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public override string ToString()
        {
            return $"Correct: {Correct} Errors: {Error}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}