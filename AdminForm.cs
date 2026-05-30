using Scooter_Kiralama_Sistemi.Helpers;
using System.Globalization;
using System.Data;

namespace Scooter_Kiralama_Sistemi
{
    public partial class AdminForm : Form
    {
        private MapHelper _mapHelper;

        public AdminForm()
        {
            InitializeComponent();
            if (this.DesignMode) return;

            InitializeMap();
            RefreshAllData();
        }

        // harita kurulumu
        private void InitializeMap()
        {
            _mapHelper = new MapHelper();
            tabHarita.Controls.Add(_mapHelper.gmapControl);
            _mapHelper.setupMap();
        }

        // tüm verileri tazele
        private void RefreshAllData()
        {
            LoadScooterData();
            LoadUserData();
            LoadRentalsData();
        }

        // scooter listesini yükle
        private void LoadScooterData()
        {
            try
            {
                dgvScooterlar.DataSource = DatabaseHelper.GetScooters();
                dgvScooterlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                _mapHelper.RefreshMapMarkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // kullanıcı listesini yükle
        private void LoadUserData()
        {
            try
            {
                dgvKullanicilar.DataSource = DatabaseHelper.GetAllUsers();
                if (dgvKullanicilar.Columns["password_hash"] != null)
                    dgvKullanicilar.Columns["password_hash"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // kiralama geçmişini yükle
        private void LoadRentalsData()
        {
            dgvKiralamalar.DataSource = DatabaseHelper.GetAllRentals();
        }

        // harita konumunu al
        private void btnKonumSec_Click(object sender, EventArgs e)
        {
            var pos = _mapHelper.gmapControl.Position;
            txtScooterLat.Text = pos.Lat.ToString(CultureInfo.InvariantCulture);
            txtScooterLng.Text = pos.Lng.ToString(CultureInfo.InvariantCulture);
            tabControl1.SelectedTab = tabScooterlar;
        }

        // yeni scooter ekle
        private void btnScooterEkle_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(txtScooterLat.Text, CultureInfo.InvariantCulture);
                double lng = double.Parse(txtScooterLng.Text, CultureInfo.InvariantCulture);
                int battery = int.Parse(txtScooterBattery.Text);
                string qr = "SC-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();

                if (DatabaseHelper.AddScooter(txtScooterName.Text, lat, lng, battery, qr))
                {
                    MessageBox.Show("Eklendi.");
                    RefreshAllData();
                }
            }
            catch { MessageBox.Show("Veri hatası."); }
        }

        // scooter sil
        private void btnScooterSil_Click(object sender, EventArgs e)
        {
            if (dgvScooterlar.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvScooterlar.SelectedRows[0].Cells["id"].Value);

            if (DatabaseHelper.DeleteScooter(id)) RefreshAllData();
            else MessageBox.Show("Silinemedi.");
        }

        // kullanıcıya bakiye ekle
        private void btnBakiyeEkle_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvKullanicilar.SelectedRows[0].Cells["id"].Value);

            if (double.TryParse(txtBakiyeMiktari.Text, out double amount))
            {
                if (DatabaseHelper.UpdateUserBalance(id, amount)) RefreshAllData();
            }
        }

        // kullanıcı sil
        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvKullanicilar.SelectedRows[0].Cells["id"].Value);

            if (DatabaseHelper.DeleteUser(id)) RefreshAllData();
        }

        // kiralama sonlandır
        private void btnKiralamaSonlandir_Click(object sender, EventArgs e)
        {
            if (dgvKiralamalar.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvKiralamalar.SelectedRows[0].Cells["Kiralama ID"].Value);

            if (DatabaseHelper.EndRental(id)) RefreshAllData();
        }

        // haritada scooter konumuna git
        private void dgvScooterlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvScooterlar.Rows[e.RowIndex];
            _mapHelper.gmapControl.Position = new GMap.NET.PointLatLng(
                Convert.ToDouble(row.Cells["lat"].Value),
                Convert.ToDouble(row.Cells["lng"].Value));
            _mapHelper.gmapControl.Zoom = 18;
            tabControl1.SelectedTab = tabHarita;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAllData();
        }
    }
}