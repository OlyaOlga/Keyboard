using System.Collections.Generic;

namespace KeyboardModel.Statistic
{
    public class GlobalStatistics
    {
        public Dictionary<StatisticsIdentifier, ErrorStatistics> LocalStatistics { get; set; }

        public GlobalStatistics()
        {
            LocalStatistics = new Dictionary<StatisticsIdentifier, ErrorStatistics>();
        }
    }
}
