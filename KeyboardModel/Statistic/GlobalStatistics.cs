using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KeyboardModel.Statistic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
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

        /// <summary>
        /// Inializes a new instance of the <see cref="GlobalStatistics"/> class from file
        /// </summary>
        /// <param name="fileName">File name to read from</param>
        public GlobalStatistics(string fileName)
        {
            LocalStatistics = Load(fileName).LocalStatistics;
        }

        public void Save(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }

            using (var stream = File.Open(fileName, FileMode.Truncate, FileAccess.Write))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream,this);
            }
        }

        public GlobalStatistics Load(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bin = new BinaryFormatter();
                return bin.Deserialize(stream) as GlobalStatistics;
            }
        }
    }
}
