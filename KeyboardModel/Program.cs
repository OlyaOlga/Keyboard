using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    class Program
    {
        static void Main(string[] args)
        {
           /* Complexity c = Complexity.Simple;
            Time t = Time.FiveMinutes;
            Language l = Language.Ukr;
            Model m = new Model(c,t,l);
            Console.WriteLine(m.ReadData());*/
            string data = "123456";
            InputTextQueue q = new InputTextQueue(data);
            Console.WriteLine( q.ToString());
            Console.ReadKey(); 
        }
    }
}
