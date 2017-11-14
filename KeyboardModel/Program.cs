using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyboardModel.Enums;
using KeyboardModel.Statistic;

namespace KeyboardModel
{
    class Program
    {
        static void Main(string[] args)
        {
            ErrorStatistics s = new ErrorStatistics();
            ErrorStatistics u = new ErrorStatistics();
            u.Error();
            u.Error();
            u.Error();


            s.Correct();
            s.Correct();
            s.Error();

            var v = s + u;
            StatisticsIdentifier i = new StatisticsIdentifier(Complexity.Hard, Language.Eng, Time.TenMinutes);
            StatisticsIdentifier j = new StatisticsIdentifier(Complexity.Hard, Language.Ukr, Time.TenMinutes);

            GlobalStatistics g = new GlobalStatistics();

            g.LocalStatistics[i] = s;
            s.Error();
            g.LocalStatistics[j] = s;

            Console.ReadKey(); 
        }
    }
}
