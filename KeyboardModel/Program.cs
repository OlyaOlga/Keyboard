using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    class Program
    {
          static  RemainedTimeTimer t = new RemainedTimeTimer(10000);
          static  Timer u = new Timer(1000);

        public static void hand(object s, EventArgs e)
        {
            Console.WriteLine($"Time remaining {t.Remain}");
        }

        public static void hand1(object s, EventArgs e)
        {
            u.Elapsed -= hand;
        }

        static void Main(string[] args)
        {
            t.Start();
            
            u.Elapsed += hand;
            t.Elapsed += hand1;
                     
            u.Start();
            Console.ReadKey(); 
        }
       
    }
}
