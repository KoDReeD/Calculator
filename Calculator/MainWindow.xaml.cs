using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        static int dotCount = 0;


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "1";
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "2";
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "3";
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "4";
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "5";
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "6";
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "7";
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "8";
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text += "9";
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBlock_Text.Text;
            if (text[text.Length - 1] != 0) TextBlock_Text.Text += "0";
        }

        private void Button_Sum_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '+', ref dotCount);
        }

        private void Button_Razn_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '-', ref dotCount);
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Clear(TextBlock_Text.Text, ref dotCount);
        }

        private void Button_Stepen_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '^', ref dotCount);
        }

        private void Button_Tochka_Click(object sender, RoutedEventArgs e)
        {
            if (dotCount == 0)
                TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, ',', ref dotCount);
        }

        private void Button_Sqrt_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '√', ref dotCount);
        }

        private void Button_Result_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBlock_Text.Text;
            string result = Calc.GetResult(text).ToString();
            if (result == "") result = text;
                TextBlock_Text.Text = result;
        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '÷', ref dotCount);
        }

        private void Button_Ymn_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Text.Text = Calc.Set(TextBlock_Text.Text, '×', ref dotCount);
        }
    }
}
