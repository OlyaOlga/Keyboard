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
            string data = "123456";
            Model m = new Model(Complexity.Hard, Time.TenMinutes, Language.Eng);
            m.StartTimer();
            Console.WriteLine(m.Text);
            m.Text.Pop();
            Console.WriteLine(m.Text);
            

            Console.ReadKey(); 
        }
       
    }
}
