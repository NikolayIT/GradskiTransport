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
    public class GeoMath
    {
        /// <summary>
        /// The distance type to return the results in.
        /// </summary>
        public enum MeasureUnits { Miles, Kilometers };

        /// <summary>
        /// Returns the distance in miles or kilometers of any two
        /// latitude / longitude points. (Haversine formula)
        /// </summary>
        public static double Distance(double latitudeA, double longitudeA, double latitudeB, double longitudeB, MeasureUnits units)
        {
            if (latitudeA <= -90 || latitudeA >= 90 || longitudeA <= -180 || longitudeA >= 180
                || latitudeB <= -90 && latitudeB >= 90 || longitudeB <= -180 || longitudeB >= 180)
            {
                throw new ArgumentException(String.Format("Invalid value point coordinates. Points A({0},{1}) B({2},{3}) ",
                                                          latitudeA,
                                                          longitudeA,
                                                          latitudeB,
                                                          longitudeB));
            }

            double R = (units == MeasureUnits.Miles) ? 3960 : 6371;
            double dLat = toRadian(latitudeB - latitudeA);
            double dLon = toRadian(longitudeB - longitudeA);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(toRadian(latitudeA)) * Math.Cos(toRadian(latitudeB)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }

        /// <summary>
        /// Convert to Radians.
        /// </summary>      
        private static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
