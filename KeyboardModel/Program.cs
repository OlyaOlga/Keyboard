﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("привіт");
            Complexity c = Complexity.Simple;
            Time t = Time.FiveMinutes;
            Language l = Language.Ukr;
            Model m = new Model(c,t,l);
            Console.WriteLine(m.ReadData());
            Console.ReadKey();
        }
    }
}
