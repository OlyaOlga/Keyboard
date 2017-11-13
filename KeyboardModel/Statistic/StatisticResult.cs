namespace KeyboardModel.Statistic
{
    using System;

    /// <summary>
    /// Represent statistic result in percents
    /// </summary>
    public struct StatisticResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticResult"/> struct
        /// </summary>
        /// <param name="correct">Perncent of correct asswers</param>
        /// <param name="error">Percnet of error answers</param>
        public StatisticResult(double correct, double error)
        {
            if (correct < 0 || correct > 1)
            {
                throw new ArgumentException("Percent must equls to [0;1] range", nameof(correct));
            }

            if (error < 0 || error > 1)
            {
                throw new ArgumentException("Percent must equls to [0;1] range", nameof(error));
            }

            if (Math.Abs(correct + error - 1) > float.Epsilon)
            {
                throw new ArgumentException("Sum of percents must equls to 1", nameof(correct) + ", " + nameof(error));
            }

            Correct = correct;
            Error = error;
        }

        /// <summary>
        /// Gets percent of correct answers
        /// </summary>
        public double Correct { get; }

        /// <summary>
        /// Gets percent of error answers
        /// </summary>
        public double Error { get; }
    }
}