using System;
using System.Collections.Generic;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void key_pressed(object sender, KeyEventArgs e)
        //{
        //    if (e.IsDown)
        //    {
        //        secondNumber2.Text += e.Key;
        //    }
        //    else
        //    {
        //        secondNumberTextBox.Text += e.Key;
        //    }
        //}

        private void user_prints_sth(object sender, TextCompositionEventArgs e)
        {
            InputText.Text += e.Text;
        }
    }
}
