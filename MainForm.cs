using GMap.NET.WindowsForms;
using Scooter_Kiralama_Sistemi.Helpers;
using System.Data;

namespace Scooter_Kiralama_Sistemi
{
    public partial class MainForm : Form
    {
        private MapHelper _mapHelper;
        private Users _currentUser;
        private Rentals? _activeRental = null;

        public MainForm(Users user)
        {
            InitializeComponent();
            _currentUser = user;

            if (this.DesignMode) return;

            InitializeMap();
            InitializeUI();
        }

        // harita kurulumu
        private void InitializeMap()
        {
            _mapHelper = new MapHelper();
            tabHarita.Controls.Add(_mapHelper.gmapControl);
            _mapHelper.gmapControl.Dock = DockStyle.Fill;
            _mapHelper.setupMap();
            _mapHelper.RefreshMapMarkers();
            _mapHelper.gmapControl.OnMarkerClick += HandleMarkerClick;
        }

        // kullanıcı arayüzünü başlat
        private void InitializeUI()
        {
            LoadProfileDetails();
            LoadRentalHistory();
            RefreshActiveRental();
        }

        // aktif kiralamayı getir
        private void RefreshActiveRental()
        {
            _activeRental = DatabaseHelper.GetActiveRental(_currentUser.id);

            if (_activeRental != null)
            {
                UpdateRentalDisplay(_activeRental);
                CheckRentalExpiration();
            }
            else
            {
                ClearRentalUI();
            }
        }

        // kiralama verilerini ekrana bas
        private void UpdateRentalDisplay(Rentals rental)
        {
            lblScooterAdi.Text = rental.scooter_name;
            lblToplamUcret.Text = $"{rental.total_price} TL";
            lblDurumPanel.Visible = true;
            lblAktifDurum.Visible = false;
            btnQRGoster.Visible = true;

            if (DateTime.TryParse(rental.start_date, out DateTime start))
                lblBaslangicTarihi.Text = start.ToString("yyyy-MM-dd HH:mm:ss");

            if (rental.status == "pending")
            {
                lblBitisTarihi.Text = "QR Okutulması Bekleniyor";
                lblKalanGun.Text = "Beklemede";
                return;
            }

            if (DateTime.TryParse(rental.end_date, out DateTime end))
            {
                lblBitisTarihi.Text = end.ToString("yyyy-MM-dd HH:mm:ss");
                lblKalanGun.Text = Math.Ceiling((end - DateTime.Now).TotalDays).ToString();
            }
        }

        // kiralama süresini kontrol et
        private void CheckRentalExpiration()
        {
            if (_activeRental != null && DateTime.TryParse(_activeRental.end_date, out DateTime end) && DateTime.Now >= end)
            {
                DatabaseHelper.EndRental(_activeRental.id);
                _mapHelper.RefreshMapMarkers();
                _activeRental = null;
                MessageBox.Show("Süreniz doldu, kiralama sonlandırıldı.");
                ClearRentalUI();
            }
        }

        // kiralama arayüzünü temizle
        private void ClearRentalUI()
        {
            lblScooterAdi.Text = lblBaslangicTarihi.Text = lblBitisTarihi.Text = lblKalanGun.Text = lblToplamUcret.Text = "-";
            lblDurumPanel.Visible = false;
            lblAktifDurum.Visible = true;
            btnQRGoster.Visible = false;
            pbQRKod.Image = null;
        }

        // marker tıklama işlemi
        private void HandleMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag == null) return;

            int id = Convert.ToInt32(item.Tag);
            DataRow scooter = DatabaseHelper.GetScooterById(id);

            if (scooter != null && scooter["status"].ToString() == "available")
            {
                using (KiralamaForm frm = new KiralamaForm(scooter, _currentUser))
                {
                    if (frm.ShowDialog() == DialogResult.OK) RefreshActiveRental();
                }
            }
            else
            {
                MessageBox.Show("Scooter şu an müsait değil.");
            }
        }

        // profil detaylarını yükle
        public void LoadProfileDetails()
        {
            lblProfileTabUserName.Text = lblMapTabUserName.Text = _currentUser.name;
            lblProfileTabEmail.Text = _currentUser.email;
            lblProfileTabSurname.Text = _currentUser.surname;
            lblProfileTabBalance.Text = _currentUser.balance.ToString();
        }

        // kiralama geçmişini yükle
        public void LoadRentalHistory()
        {
            var dt = DatabaseHelper.GetUserRentalHistory(_currentUser.id);
            dataGridView1.DataSource = dt;
            lblToplamKiralama.Text = $"Toplam: {dt.Rows.Count}";
            lblToplamHarcama.Text = $"Toplam Harcama: {dt.AsEnumerable().Sum(r => r.Field<double>("Ücret (TL)")):F2} TL";
        }

        // sekme değişimini yönet
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (tabControl1.SelectedIndex)
            //{
            //    case 0: _mapHelper.RefreshMapMarkers(); break;  //harita
            //    case 1: RefreshActiveRental(); break;           // aktif kiralama
            //    case 2: LoadProfileDetails(); break;            // profil
            //    case 3: LoadRentalHistory(); break;             // profil gecmisi
            //}
            InitializeUI();
        }

        // event handler'lar
        private void btnQRGoster_Click(object sender, EventArgs e) => pbQRKod.Image = QRHelper.GenerateQR(_activeRental?.qr_code);
        private void lblBtnAddBalance_Click(object sender, EventArgs e) { if (new BakiyeYüklemeFormu(_currentUser).ShowDialog() == DialogResult.OK) LoadProfileDetails(); }
        private void btnRefreshRental_Click(object sender, EventArgs e) => RefreshActiveRental();
        private void btnRefreshProfileHistory_Click(object sender, EventArgs e) => LoadRentalHistory();
    }
}