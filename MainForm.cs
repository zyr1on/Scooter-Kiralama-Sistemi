using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;


namespace Scooter_Kiralama_Sistemi
{

    public partial class MainForm : Form
    {
        MapHelper mapHelper = new MapHelper();
        private Users currentUser; // Giriş yapan kullanıcıyı burada tutacağız, Profil kısmı bunun üzerinden dolacak.

        public MainForm(Users _user)
        {
            InitializeComponent();
            tabHarita.Controls.Add(mapHelper.gmapControl);

            mapHelper.gmapControl.Dock = DockStyle.Fill;
            mapHelper.setupMap();
            
            currentUser = _user;
            mapHelper.RefreshMapMarkers();
            

            // 4. Kullanıcıya sadece MÜSAİT scooterları gösterelim
        }
    }
}
