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

            Model m = new Model(Complexity.Hard, Time.OneMinute, Language.Eng);
            m.StartTimer();
            m.CheckCurrentSymbol('H');
            m.CheckCurrentSymbol('D');
            m.CheckCurrentSymbol('D');
            m.CheckCurrentSymbol('A');
            m.CheckCurrentSymbol('A');
            Console.WriteLine(m.ErrorStatistics.Result.Correct);
            Console.WriteLine(m.ErrorStatistics.Result.Error);
            Console.ReadKey();
        }
    }
}