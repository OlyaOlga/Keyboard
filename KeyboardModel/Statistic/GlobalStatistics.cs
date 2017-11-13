using System.Collections.Generic;

namespace KeyboardModel.Statistic
{
    public class GlobalStatistics
    {
        /// <summary>
        /// Gets stored local statistics
        /// </summary>
        public Dictionary<StatisticsIdentifier, ErrorStatistics> LocalStatistics { get; }

        public GlobalStatistics()
        {
            LocalStatistics = new Dictionary<StatisticsIdentifier, ErrorStatistics>();
        }
    }
}
