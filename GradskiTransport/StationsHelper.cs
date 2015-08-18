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
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Windows.Resources;

namespace GradskiTransport
{
    public static class StationsHelper
    {
        public static List<Station> Stations;
        public const string StationsFileName = "coordinates.xml";

        static StationsHelper()
        {
            Stations = new List<Station>();
            StreamResourceInfo xmlFile = Application.GetResourceStream(new Uri("coordinates.xml", UriKind.Relative));
            XmlReader reader = XmlReader.Create(xmlFile.Stream);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "station")
                    {
                        reader.MoveToAttribute("code");
                        string code = reader.Value;
                        reader.MoveToAttribute("label");
                        string label = reader.Value;
                        reader.MoveToAttribute("lat");
                        string lat = reader.Value;
                        reader.MoveToAttribute("lon");
                        string lon = reader.Value;
                        Stations.Add(new Station(code, label, lat, lon));
                    }
                }
            }
        }

        public static List<Station> GetNearestStations(double latitude, double longtitude, int count)
        {
            List<Station> SortedStations = Stations.OrderBy(x => GeoMath.Distance(latitude, longtitude, x.Latitude, x.Longtitute, GeoMath.MeasureUnits.Kilometers)).ToList();
            return SortedStations.Take(count).ToList();
        }

        

        //public static 
    }
}
