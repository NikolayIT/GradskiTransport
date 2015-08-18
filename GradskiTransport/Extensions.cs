using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GradskiTransport
{
    public static class Extensions
    {
        public static int ToInteger(this string s)
        {
            int integerValue = 0;
            int.TryParse(s, out integerValue);
            return integerValue;
        }
        public static double ToDouble(this string s)
        {
            double integerValue = 0;
            double.TryParse(s, out integerValue);
            return integerValue;
        }
    }
}
