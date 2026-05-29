using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Scooter_Kiralama_Sistemi
{
    public partial class AdminForm : Form
    {
        #region Değişkenler ve Tanımlamalar

        MapHelper mapHelper;

        #endregion

        #region Yapıcı Metot (Constructor)

        public AdminForm()
        {
            InitializeComponent();
            
            // harita
            mapHelper = new MapHelper();
            tabHarita.Controls.Add(mapHelper.gmapControl);
            mapHelper.setupMap();

            // yönetim verileri dbden alınır
            LoadScooterData();
            LoadUserData();
            KiralamalariYukle();
        }

        #endregion

        #region Veri Yükleme ve Arayüz Tazeleme Metotları (Data Loading)

        private void LoadScooterData()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetScooters();
                dgvScooterlar.DataSource = dt;

                if (dgvScooterlar.Columns["id"] != null) dgvScooterlar.Columns["id"].Visible = true;

                dgvScooterlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvScooterlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvScooterlar.ReadOnly = true; 

                // pinleri refreshle
                mapHelper.RefreshMapMarkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Scooter verileri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserData()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetAllUsers();
                dgvKullanicilar.DataSource = dt;

                dgvKullanicilar.Columns["id"].HeaderText = "ID";
                dgvKullanicilar.Columns["name"].HeaderText = "Ad";
                dgvKullanicilar.Columns["surname"].HeaderText = "Soyad";
                dgvKullanicilar.Columns["email"].HeaderText = "E-Posta";
                dgvKullanicilar.Columns["balance"].HeaderText = "Bakiye (TL)";
                dgvKullanicilar.Columns["role"].HeaderText = "Rol (0:Admin, 1:User)";
                dgvKullanicilar.Columns["created_at"].HeaderText = "Kayıt Tarihi";

                // hash gizle
                if (dgvKullanicilar.Columns["password_hash"] != null)
                    dgvKullanicilar.Columns["password_hash"].Visible = false;

                dgvKullanicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvKullanicilar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KiralamalariYukle()
        {
            dgvKiralamalar.DataSource = DatabaseHelper.GetAllRentals();
            dgvKiralamalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKiralamalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

        #region Scooter Yönetimi Buton ve Eventleri (Scooter Management)

        private void btnKonumSec_Click(object sender, EventArgs e)
        {
            var currentPos = mapHelper.gmapControl.Position;

            txtScooterLat.Text = currentPos.Lat.ToString(CultureInfo.InvariantCulture);
            txtScooterLng.Text = currentPos.Lng.ToString(CultureInfo.InvariantCulture);
            
            tabControl1.SelectedTab = tabScooterlar;
            txtScooterName.Focus();
        }

        private void btnScooterEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtScooterName.Text.Trim();
                string batteryStr = txtScooterBattery.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(txtScooterLat.Text) || string.IsNullOrEmpty(txtScooterLng.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları ve haritadan konumu eksiksiz doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double lat = double.Parse(txtScooterLat.Text, CultureInfo.InvariantCulture);
                double lng = double.Parse(txtScooterLng.Text, CultureInfo.InvariantCulture);
                int battery = int.Parse(batteryStr);
                
                string qrCodeContent = "SC-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                
                bool sonuc = DatabaseHelper.AddScooter(name, lat, lng, battery, qrCodeContent);
                if (sonuc)
                {
                    MessageBox.Show($"{name} başarıyla sisteme eklendi!\nQR Kod: {qrCodeContent}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadScooterData();   
                    
                    txtScooterName.Clear();
                    txtScooterLat.Clear();
                    txtScooterLng.Clear();
                    txtScooterBattery.Text = "100";
                }
                else
                {
                    MessageBox.Show("Scooter eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş verileri hatalı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnScooterSil_Click(object sender, EventArgs e)
        {
            if (dgvScooterlar.SelectedRows.Count > 0)
            {
                var row = dgvScooterlar.SelectedRows[0];
                string durum = row.Cells["status"].Value.ToString();

                if (durum != "available")
                {
                    MessageBox.Show($"Bu scooter şu anda '{durum}' durumunda olduğu için silemezsiniz.\nSadece müsait (available) scooterlar silinebilir.", "İşlem Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int scooterId = Convert.ToInt32(row.Cells["id"].Value);
                string scooterName = row.Cells["name"].Value.ToString();

                DialogResult dialogResult = MessageBox.Show($"{scooterName} isimli scooter sistemden tamamen silinecek. Onaylıyor musunuz?", "Scooter Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    if (DatabaseHelper.DeleteScooter(scooterId))
                    {
                        MessageBox.Show("Scooter başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadScooterData(); // Hem tabloyu hem haritayı tazeler
                    }
                    else
                    {
                        MessageBox.Show("Hata: Bu scooterın geçmiş kiralama kayıtları olduğu için veri bütünlüğü nedeniyle silinemiyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz scooterı tablodan seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvScooterlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvScooterlar.Rows[e.RowIndex];

                if (row.Cells["lat"].Value != DBNull.Value && row.Cells["lng"].Value != DBNull.Value)
                {
                    double lat = Convert.ToDouble(row.Cells["lat"].Value);
                    double lng = Convert.ToDouble(row.Cells["lng"].Value);

                    mapHelper.gmapControl.Position = new GMap.NET.PointLatLng(lat, lng);
                    mapHelper.gmapControl.Zoom = 18; 

                    tabControl1.SelectedTab = tabHarita;
                }
            }
        }

        #endregion

        #region Kullanıcı Yönetimi Buton ve Eventleri (User Management)

        private void btnBakiyeEkle_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvKullanicilar.SelectedRows[0].Cells["id"].Value);

                if (double.TryParse(txtBakiyeMiktari.Text, out double miktar) && miktar > 0)
                {
                    if (DatabaseHelper.UpdateUserBalance(userId, miktar))
                    {
                        MessageBox.Show("Bakiye başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserData(); 
                        txtBakiyeMiktari.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli ve pozitif bir sayı giriniz.", "Geçersiz Tutar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce listeden bir kullanıcı seçin.", "Seçim Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {
                var row = dgvKullanicilar.SelectedRows[0];

                int userId = Convert.ToInt32(row.Cells["id"].Value);
                string adSoyad = $"{row.Cells["name"].Value} {row.Cells["surname"].Value}";
                string email = row.Cells["email"].Value.ToString();

                string mesaj = $"{adSoyad} ({email}) isimli kullanıcı sistemden tamamen silinecek.\n\nBu işlem geri alınamaz. Onaylıyor musunuz?";

                DialogResult dialogResult = MessageBox.Show(mesaj, "Kullanıcı Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    if (DatabaseHelper.DeleteUser(userId))
                    {
                        MessageBox.Show("Kullanıcı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserData();
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı silinemez!\nKullanıcının geçmiş kiralama veya ödeme kayıtları mevcut olabilir.", "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı tablodan seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Kiralama Geçmişi Yönetimi (Rental Control)

        private void btnKiralamaSonlandir_Click(object sender, EventArgs e)
        {
            if (dgvKiralamalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen sonlandırmak istediğiniz kiralamayı tablodan seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seciliSatir = dgvKiralamalar.SelectedRows[0];
            int kiralamaId = Convert.ToInt32(seciliSatir.Cells["Kiralama ID"].Value);
            string durum = seciliSatir.Cells["Durum"].Value.ToString();

            if (durum != "active")
            {
                MessageBox.Show("Seçtiğiniz kiralama işlemi zaten sonlandırılmış veya tamamlanmış!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var onay = MessageBox.Show("Bu aktif kiralamayı el ile sonlandırmak istediğinize emin misiniz? Scooter haritada tekrar müsait duruma geçecektir.", "Kiralamayı Sonlandır", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                bool basarili = DatabaseHelper.EndRental(kiralamaId);

                if (basarili)
                {
                    MessageBox.Show("Kiralama admin müdahalesiyle başarıyla sonlandırıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KiralamalariYukle(); 
                    LoadScooterData();
                }
                else
                {
                    MessageBox.Show("İşlem sırasında sistemsel bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}