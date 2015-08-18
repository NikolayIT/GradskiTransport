using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Text;

namespace GradskiTransport
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher = null;
        bool webBrowserSumc = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            webBrowser1.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(webBrowser1_Navigated);
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(MainPage_BackKeyPress);
        }

        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (webBrowserSumc && watcher != null)
            {
                e.Cancel = true;
                findNearestStations(watcher.Position.Location);
            }
            else
            {
                e.Cancel = false;
            }

        }

        void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().Contains("m.sumc.bg"))
            {
                webBrowserSumc = true;
            }
            else
            {
                webBrowserSumc = false;
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                watcher.MovementThreshold = 20;
                watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
                watcher.Start();
            }
            else
            {
                findNearestStations(watcher.Position.Location);
            }
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see whether the user has disabled the Location Service.
                    //if (watcher.Permission == GeoPositionPermission.Denied)
                    //{
                    //    // The user has disabled the Location Service on their device.
                    //    statusTextBlock.Text = "you have this application access to location.";
                    //}
                    //else
                    //{
                    //    statusTextBlock.Text = "location is not functioning on this device";
                    //}
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    // Disable the Start Location button.
                    //startLocationButton.IsEnabled = false;
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    // Alert the user and enable the Stop Location button.
                    //statusTextBlock.Text = "location data is not available.";
                    //stopLocationButton.IsEnabled = true;
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    // Show the current position and enable the Stop Location button.
                    //statusTextBlock.Text = "location data is available.";
                    //stopLocationButton.IsEnabled = true;
                    break;
            }
        }
        public static string ConvertExtendedASCII(string HTML)
        {
            string retVal = "";
            char[] s = HTML.ToCharArray();

            foreach (char c in s)
            {
                if (Convert.ToInt32(c) > 127)
                    retVal += "&#" + Convert.ToInt32(c) + ";";
                else
                    retVal += c;
            }

            return retVal;
        }  
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!webBrowserSumc)
            {
                findNearestStations(e.Position.Location);
            }
        }
        void findNearestStations(GeoCoordinate coord)
        {
            List<Station> nearestStations = StationsHelper.GetNearestStations(coord.Latitude, coord.Longitude, 13);
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" /></head><body>");
            foreach (var item in nearestStations)
            {
                sb.AppendFormat("<font size=5><a href=\"http://m.sumc.bg/vt?q={1:0000}&o=1&go=1\">{0} ({1:0000})</a></font>", item.Label, item.Code);
                sb.Append("<br />");
            }
            sb.Append("</body></html>");
            webBrowser1.NavigateToString(ConvertExtendedASCII(sb.ToString()));
            webBrowserSumc = false;
        }
    }
}