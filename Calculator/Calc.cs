using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Math;

namespace Calculator
{
    public static class Calc
    {
        static char[] sign = new char[7] { '+', '-', '÷', '×', '√', '^', ',' };
        public static double Sum(double a, double b) => a + b;
        public static double Substraction(double a, double b) => a - b;
        public static double Multiplication(double a, double b) => a * b;
        public static double Division(double a, double b) => a / b;
        public static double Exponentiation(double a, double b) => Pow(a, b);
        public static double Sqrt(double b, double a) => Math.Sqrt(b) * a;

        public static char LastChar(string text)
        {
            return text[text.Length - 1];
        }

        //получение результата для TextBox
        public static double? GetResult(string text)
        {
            double? result = 0;
            //result = double.TryParse(text, out double tmp) ? (int?)tmp : null;

            double tmp;
            if (double.TryParse(text, out tmp))
            {
                return tmp;
            }

            double? res = 0; //доп переменная для результата
            if (!CheckLastSign(text))
            {
                res = GetOperationResult('^', ref text);
                if (res != null) result = res;
                res = GetOperationResult('√', ref text);
                if (res != null) result = res;



                while (text.IndexOf('÷') != -1 || text.IndexOf('×') != -1)
                {
                    if(text.IndexOf('÷') != -1 && text.IndexOf('×') == -1)
                    {
                        res = GetOperationResult('÷', ref text);
                        if(res != null) result = res;
                    }
                    else if(text.IndexOf('×') != -1 && text.IndexOf('÷') == -1)
                    {
                        res = GetOperationResult('×', ref text);
                        if (res != null) result = res;
                    }

                    else if (text.IndexOf('÷') < text.IndexOf('×'))
                    {
                        res = GetOperationResult('÷', ref text);
                        if (res != null) result = res;
                    }
                    else if (text.IndexOf('÷') > text.IndexOf('×'))
                    {
                        res = GetOperationResult('×', ref text);
                        if (res != null) result = res;
                    }
                }

                while ((text.IndexOf('+') != -1) || (text.IndexOf('-', 1) != -1))
                {
                    if (text.IndexOf('+') != -1 && text.IndexOf('-', 1) == -1)
                    {
                        res = GetOperationResult('+', ref text);
                        if (res != null) result = res;
                    }
                    else if (text.IndexOf('-', 1) != -1 && text.IndexOf('+') == -1)
                    {
                        res = GetOperationResult('-', ref text);
                        if (res != null) result = res;
                    }

                    else if (text.IndexOf('+') < text.IndexOf('-', 1))
                    {
                        res = GetOperationResult('+', ref text);
                        if (res != null) result = res;
                    }
                    else if (text.IndexOf('+') > text.IndexOf('-', 1))
                    {
                        res = GetOperationResult('-', ref text);
                        if (res != null) result = res;
                    }
                }
            }
            else return null;
            return result;
        }

        private static double? GetOperationResult(char znak, ref string text)
        {
            double? result = null;
            double tmp;
            if (double.TryParse(text, out tmp))
            {
                return tmp;
            }

            //выполняет операции со знаком пока не закончатся
            int znakIndex;

            //ищем положение знака
            if (text[0] == '-' && znak == '-')
            {
                znakIndex = text.IndexOf(znak, 1);
            }
            else
            {
                znakIndex = text.IndexOf(znak);
                if (znakIndex == -1) return null;
            }
            string left = text.Substring(0, znakIndex);
            string right = text.Substring(znakIndex + 1);
            double a;
            double b;

            if (left == "" || CheckLastSign(left)) a = 1;
            else
            {
                a = GetA(left, ref text, znak);
            }
            b = GetB(right, ref text, znak);

            switch (znak)
            {
                case '+':
                    result = Sum(a, b);
                    break;
                case '-':
                    result = Substraction(a, b);
                    break;
                case '×':
                    result = Multiplication(a, b);
                    break;
                case '÷':
                    result = Division(a, b);
                    break;
                case '^':
                    result = Exponentiation(a, b);
                    break;
                case '√':
                    result = Sqrt(b, a);
                    break;
            }
            znakIndex = text.IndexOf(znak);
            text = text.Remove(znakIndex, 1).Insert(znakIndex, result.ToString());
            double res;
            if (double.TryParse(text, out res))
            {
                result = res;
            }
            return result;
        }

        public static double GetA(string strLeft, ref string text, char znak)
        {
            string aStr = "";
            bool flag = false;
            for (int i = strLeft.Length - 1; i >= 0; i--)
            {
                if (strLeft[i] == '-' && i == 0)
                {
                    aStr += '-';
                    text = text.Remove(i, 1);
                    break;
                }
                else if (strLeft[i] == ',')
                {
                    aStr += ',';
                    text = text.Remove(i, 1);
                }
                else
                {
                    flag = CheckLastSign(strLeft[i].ToString());
                    if (flag == false)
                    {
                        aStr += strLeft[i];
                        text = text.Remove(i, 1);

                    }
                    else break;

                }

            }
                string reverseAStr = new string(aStr.ToCharArray().Reverse().ToArray());
                return Convert.ToDouble(reverseAStr);
        }

        public static double GetB(string strLeft, ref string text, char znak)
        {
            string bStr = "";
            bool flag = false;
            for (int i = 0; i < strLeft.Length; i++)
            {
                if (strLeft[i] == '-' && i == 0)
                {
                    bStr += '-';
                    int posZnak = text.IndexOf(znak);
                    text = text.Remove(posZnak, 1);
                    break;
                }
                else if (strLeft[i] == ',')
                {
                    bStr += ',';
                    text = text.Remove(i, 1);
                }
                else
                {
                    flag = CheckLastSign(strLeft[i].ToString());

                    if (flag == false)
                    {
                        bStr += strLeft[i];
                        text = text.Remove(text.IndexOf(znak) + 1, 1);

                    }
                    else break;

                }

            }
            return Convert.ToDouble(bStr);
        }

        public static string Clear(string text, ref int dotCount)
        {
            if (text.Length > 0)
            {
                if (LastChar(text) == ',')
                {
                    dotCount = 0;
                }
                else
                {
                    if ("+-÷×".Contains(LastChar(text)))
                    {
                        if (text.Contains(","))
                        {
                            dotCount++;
                        }
                    }
                }
                text = Regex.Replace(text, ".$", "");

            }
            return text;

        }

        public static bool CheckFirst(string text)
        {

            bool flag = false;
            for (int i = 0; i < sign.Length; i++)
            {
                if (text.Length == 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            return flag;

        }

        public static string Set(string text, char sign, ref int dotCount)
        {
            if (text.Length > 0)
            {
                if (CheckLastSign(text))
                {
                    if (sign == '√')
                    {
                        if (LastChar(text) != ',')
                            text += sign;
                    }
                    else if (sign != ',')
                    {
                        if (LastChar(text) != ',') text = RemoveSign(text, sign);
                    }
                }
                else
                {
                    if (sign == ',' && dotCount == 0)
                    {
                        dotCount++;
                        text += sign;
                    }
                    else
                    {
                        dotCount = 0;
                        text += sign;
                    }
                }
            }
            else
            {
                if (sign == '-' || sign == '√')
                    text += sign;
            }
            return text;
        }

        public static bool CheckLastSign(string text)
        {
            bool flag = false;
            char lastChar = LastChar(text);

            if (sign.Contains(lastChar)) flag = true;
            else flag = false;
            return flag;
        }

        public static string RemoveSign(string text, char sign)
        {
            if (LastChar(text) != '√')
            {
                text = text.Remove(text.Length - 1, 1) + sign;
            }
            else //есть корень
            {
                if ((text.Length < 1) && text[text.Length - 2] != '-')
                {
                    text = text.Remove(LastChar(text), 1) + sign;
                }
            }
            return text;
        }

    }
}
