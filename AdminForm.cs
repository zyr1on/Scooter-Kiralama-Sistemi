using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Scooter_Kiralama_Sistemi
{
    public partial class AdminForm : Form
    {
        MapHelper mapHelper;
        public AdminForm()
        {
            InitializeComponent();
            mapHelper = new MapHelper();
            tabHarita.Controls.Add(mapHelper.gmapControl);
            mapHelper.setupMap();
            mapHelper.addMarker(40.22624, 28.87281, "Scooter #1", GMarkerGoogleType.red_dot);

            LoadScooterData();
            mapHelper.RefreshMapMarkers();
            LoadUserData();

        }

        private void btnKonumSec_Click(object sender, EventArgs e)
        {
            var currentPos = mapHelper.gmapControl.Position;


            txtScooterLat.Text = currentPos.Lat.ToString().Replace(',', '.');
            txtScooterLng.Text = currentPos.Lng.ToString().Replace(',', '.');
            tabControl1.SelectedTab = tabScooterlar;
            txtScooterName.Focus();
        }

        private void LoadScooterData()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetScooters();
                dgvScooterlar.DataSource = dt;

                if (dgvScooterlar.Columns["id"] != null) dgvScooterlar.Columns["id"].Visible = false;

                dgvScooterlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvScooterlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvScooterlar.ReadOnly = true; // Admin sadece baksın, değiştirmesin (değişiklik butonla yapılır)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        

        private void btnScooterEkle_Click(object sender, EventArgs e)
        {
            string name = txtScooterName.Text.Trim();
            string batteryStr = txtScooterBattery.Text.Trim();
            double lat = double.Parse(txtScooterLat.Text.Replace('.', ','));
            double lng = double.Parse(txtScooterLng.Text.Replace('.', ','));
            int battery = int.Parse(batteryStr);
            string qrCodeContent = "SC-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            bool sonuc = DatabaseHelper.AddScooter(name, lat, lng, battery, qrCodeContent);
            if (sonuc)
            {
                MessageBox.Show($"{name} başarıyla sisteme eklendi!\nQR Kod: {qrCodeContent}",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);


                LoadScooterData();   // Tablo güncellensin
                mapHelper.RefreshMapMarkers(); // Haritada yeni scooter görünsün


                txtScooterName.Clear();
                txtScooterBattery.Text = "100"; //varsayılan olarak batarya dolu
            }
            else
                MessageBox.Show("Scooter eklenirken bir hata oluştu.");

        }

        private void dgvScooterlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvScooterlar.Rows[e.RowIndex];

                // Koordinatların boş olup olmadığını kontrol edelim
                if (row.Cells["lat"].Value != DBNull.Value && row.Cells["lng"].Value != DBNull.Value)
                {
                    double lat = Convert.ToDouble(row.Cells["lat"].Value);
                    double lng = Convert.ToDouble(row.Cells["lng"].Value);


                    mapHelper.gmapControl.Position = new GMap.NET.PointLatLng(lat, lng);
                    mapHelper.gmapControl.Zoom = 18; // Yakınlaştırarak göster


                    tabControl1.SelectedTab = tabHarita;
                }
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


                if (dgvKullanicilar.Columns["password_hash"] != null)
                    dgvKullanicilar.Columns["password_hash"].Visible = false;

                dgvKullanicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata: " + ex.Message);
            }
        }


        // Bakiye ekle butonu
        private void btnBakiyeEkle_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {

                int userId = Convert.ToInt32(dgvKullanicilar.SelectedRows[0].Cells["id"].Value);


                if (double.TryParse(txtBakiyeMiktari.Text, out double miktar))
                {
                    if (DatabaseHelper.UpdateUserBalance(userId, miktar))
                    {
                        MessageBox.Show("Bakiye başarıyla güncellendi.");
                        LoadUserData(); // Tabloyu tazele
                        txtBakiyeMiktari.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir sayı giriniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce listeden bir kullanıcı seçin.");
            }
        }


        // Scooter Sil Butonu
        private void btnScooterSil_Click(object sender, EventArgs e)
        {
            if (dgvScooterlar.SelectedRows.Count > 0)
            {
                var row = dgvScooterlar.SelectedRows[0];


                string durum = row.Cells["status"].Value.ToString();
                if (durum != "available")
                {
                    MessageBox.Show($"Bu scooter şu anda '{durum}' durumunda olduğu için silemezsiniz.\nSadece müsait (available) scooterlar silinebilir.",
                                    "İşlem Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }


                int scooterId = Convert.ToInt32(row.Cells["id"].Value);
                string scooterName = row.Cells["name"].Value.ToString();

                DialogResult dialogResult = MessageBox.Show($"{scooterName} : scooteri sistemden tamamen silinecek. Onaylıyor musunuz?",
                                                            "Scooter Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    if (DatabaseHelper.DeleteScooter(scooterId))
                    {
                        MessageBox.Show("Scooter başarıyla silindi.");
                        LoadScooterData();   // Tabloyu tazele
                        mapHelper.RefreshMapMarkers(); // Haritayı güncelle
                    }
                    else
                    {

                        MessageBox.Show("Hata: Bu scooterın geçmiş kiralama kayıtları olduğu için silinemiyor.");
                    }
                }
            }
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {
                var row = dgvKullanicilar.SelectedRows[0];

                // Silinecek kişinin bilgilerini ekranda göstermek için alalım
                int userId = Convert.ToInt32(row.Cells["id"].Value);
                string adSoyad = $"{row.Cells["name"].Value} {row.Cells["surname"].Value}";
                string email = row.Cells["email"].Value.ToString();

                // 2. Güvenlik Uyarısı (Scooter silmedeki gibi)
                string mesaj = $"{adSoyad} ({email}) isimli kullanıcı sistemden tamamen silinecek.\n\n" +
                               "Bu işlem geri alınamaz. Onaylıyor musunuz?";

                DialogResult dialogResult = MessageBox.Show(mesaj, "Kullanıcı Silme Onayı",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // 3. Veritabanından silmeyi dene
                    if (DatabaseHelper.DeleteUser(userId))
                    {
                        MessageBox.Show("Kullanıcı başarıyla silindi.", "Bilgi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 4. Tabloyu yenile
                        LoadUserData();
                    }
                    else
                    {
                        // Silinememe durumunda bilgilendirme
                        MessageBox.Show("Bu kullanıcı silinemez!\nKullanıcının geçmiş kiralama veya ödeme kayıtları mevcut olabilir.",
                                        "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı tablodan seçin.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
