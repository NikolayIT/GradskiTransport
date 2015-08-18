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
    public class Station
    {
        public int Code { get; set; }
        public string Label { get; set; }
        public double Latitude { get; set; }
        public double Longtitute { get; set; }

        public Station(string code, string label, string latitude, string longtitude)
        {
            this.Code = code.ToInteger();
            this.Label = label;
            this.Latitude = latitude.ToDouble();
            this.Longtitute = longtitude.ToDouble();
        }
    }
}
