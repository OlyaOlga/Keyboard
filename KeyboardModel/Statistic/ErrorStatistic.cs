namespace KeyboardModel.Statistic
{
    public class ErrorStatistics
    {
        public ErrorStatistics()
        {
        }

        /// <summary>
        /// Gets all answers count
        /// </summary>
        public int TotalItems { get; private set; }

        /// <summary>
        /// Gets error answers count
        /// </summary>
        public int ErrorItems { get; private set; }

        /// <summary>
        /// Gets pretty view statistic result
        /// </summary>
        public StatisticResult Result
        {
            get
            {
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
    }
}
