using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int invokes=0;

        public static List<char> DataList = new List<char>
        { 'T', 'h', 'i', 's', ' ', 'i', 's', ' ', 't', 'e', 's', 't', ' ', 's', 't',
            'r', 'i', 'n', 'g'
        };

        public string CurrentString()
        {
            string result = string.Empty;
            foreach (var item in DataQueue.ToList())
            {
                result += item;
            }
            return result;
        }

        public static Queue<char> DataQueue = new Queue<char>(DataList); 
        public MainWindow()
        {
            InitializeComponent();
            dataBlock.Text = CurrentString();
        }
        
        private void user_prints_sth(object sender, TextCompositionEventArgs e)
        {
            ++invokes;
            InputText.Text += e.Text;
            button.Content = invokes.ToString();
            if (DataQueue.Count!=0 &&
                e.Text[0] == DataQueue.Peek())
            {
                DataQueue.Dequeue();
            }
            dataBlock.Text = CurrentString();
        }


    }
}
