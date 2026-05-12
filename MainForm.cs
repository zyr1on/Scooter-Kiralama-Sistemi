using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;


namespace Scooter_Kiralama_Sistemi
{

    public partial class MainForm : Form
    {
        MapHelper mapHelper = new MapHelper();
        public MainForm()
        {
            InitializeComponent();
            this.Controls.Add(mapHelper.gmapControl);
            mapHelper.setupMap();
            mapHelper.addMarker(40.22624, 28.87281, "Scooter #1", GMarkerGoogleType.red_dot);
            //mapHelper.addMarker(40.22624, 28.87281, "Scooter #1"); // scooter #1 diye marker ekleyebiliriz test için
        }
    }
}
