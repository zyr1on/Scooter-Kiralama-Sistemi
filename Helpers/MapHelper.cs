using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Data;

namespace Scooter_Kiralama_Sistemi.Helpers
{
    public class MapHelper
    {
        private GMapControl gmap;
        private GMapOverlay overlay;

        public MapHelper() {

            this.gmap = new GMapControl();
            gmap.Dock = DockStyle.Fill;
            this.overlay = new GMapOverlay("scooters");
            this.gmap.Overlays.Add(overlay);
            // Scooter fotosu da koyulabilir marker olarak
            // Bitmap scooterIcon = new Bitmap("scooter.png");
            // transparent olsa daha iyi olur.

        }

        public void setupMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmap.MapProvider = GoogleMapProvider.Instance;
            gmap.Position = new PointLatLng(40.22624, 28.87281);
            gmap.MinZoom = 14;
            gmap.MaxZoom = 20;
            gmap.Zoom = 17;
            gmap.DragButton = MouseButtons.Left;
        }

        public void addMarker(double lat, double lng, string tooltip)
        {
            var marker = new GMarkerGoogle(
                new PointLatLng(lat, lng),
                GMarkerGoogleType.red_dot
            );
            marker.ToolTipText = tooltip;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            overlay.Markers.Add(marker);
        }
        public void addMarker(double lat, double lng, string tooltip,Bitmap bitmap)
        {
            var marker = new GMarkerGoogle(
                new PointLatLng(lat, lng),
                bitmap
            );
            marker.ToolTipText = tooltip;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            overlay.Markers.Add(marker);
        }
        public void addMarker(double lat, double lng, string tooltip,GMarkerGoogleType markertype)
        {
            var marker = new GMarkerGoogle(
                new PointLatLng(lat, lng),
                markertype
            );
            marker.ToolTipText = tooltip;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            overlay.Markers.Add(marker);
        }
        
        //public void addMarkerScooterIcon(double lat, double lng, string tooltip)
        //{
        //    var marker = new GMarkerGoogle(
        //        new PointLatLng(lat, lng),
        //        scooterBitMap
        //    );
        //    marker.ToolTipText = tooltip;
        //    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
        //    overlay.Markers.Add(marker);
        //}

        

        public void clearMarkers()
        {
            overlay.Markers.Clear();
        }
        //public GMapControl returnGmap()
        //{
        //    return gmap;
        
        //}


        public GMapControl gmapControl
        {
            get { return gmap; }
            set;
        }

        public void RefreshMapMarkers()
        {
            clearMarkers(); // Önce eskileri temizle
            DataTable dt = DatabaseHelper.GetScooters();

            foreach (DataRow row in dt.Rows)
            {
                double lat = Convert.ToDouble(row["lat"]);
                double lng = Convert.ToDouble(row["lng"]);
                string name = row["name"].ToString();
                string status = row["status"].ToString();
                string battery = row["battery"].ToString();


                // Duruma göre renk seçimi
                GMarkerGoogleType markerType = GMarkerGoogleType.green_dot; // Müsait
                if (status == "rented") markerType = GMarkerGoogleType.red_dot; // Kiralanmış
                if (status == "maintenance") markerType = GMarkerGoogleType.yellow_dot; // Bakımda

                addMarker(lat, lng, $"{name} (Durum: {status}) \nBatarya: {battery}", markerType);
            }
        }



    }
}
