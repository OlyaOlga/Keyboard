using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardModel.Enums;

namespace KeyboardModel
{
    class Model
    {
        public Complexity Complexity { get; set; }
        public Time Time { get; set; }
        public Language Language { get; set; }
        private string wayToFile;

        public Model(Complexity complexity, Time time, Language language)
        {
            Complexity = complexity;
            Time = time;
            Language = language;
            wayToFile = complexity.ToString() + '/' + language.ToString()+".txt";
            Console.WriteLine(wayToFile);
        }

        public string ReadData()
        {
            return File.ReadAllText(wayToFile);
        }
    }
}
