using Scooter_Kiralama_Sistemi.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Scooter_Kiralama_Sistemi
{
    public partial class KiralamaForm : Form
    {
        #region Değişkenler ve Sabitler

        private DataRow scooter;
        private Users user;
        private const double GunlukFiyat = 50.0;

        #endregion

        #region Yapıcı Metot (Constructor)

        public KiralamaForm(DataRow _scooter, Users _user)
        {
            InitializeComponent();
            scooter = _scooter;
            user = _user;

            lblScooterName.Text = $"Scooter Adı: {scooter["name"]}";
            lblStatus.Text = $"Scooter Durumu: {scooter["status"]}";
            lblBattery.Text = $"Batarya: %{scooter["battery"]}";

            cmbDuration.Items.Clear();
            cmbDuration.Items.AddRange(new object[] { 1, 3, 5, 7, 30 });
            cmbDuration.SelectedIndex = 0; // Varsayılan olarak ilk seçeneği (1 Gün) tetikler

            FiyatGuncelle();
        }

        #endregion

        #region Yardımcı Hesaplama Metotları (Business Logic)

        private void FiyatGuncelle()
        {
            if (cmbDuration.SelectedItem != null)
            {
                int gun = Convert.ToInt32(cmbDuration.SelectedItem);
                double toplamTutar = gun * GunlukFiyat;

                lblTotalPrice.Text = $"Toplam Ücret: {toplamTutar.ToString("C2", CultureInfo.CurrentCulture)}";

                if (user.balance < toplamTutar)
                {
                    lblTotalPrice.ForeColor = Color.Red; // Parası yetmiyorsa fiyat kırmızı yansır
                }
                else
                {
                    lblTotalPrice.ForeColor = Color.Green; // Sorun yoksa yeşil kalır
                }
            }
        }

        #endregion

        #region Form Elementleri ve Buton Eventleri (UI Events)

        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiyatGuncelle();
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            if (cmbDuration.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kiralama süresi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedDays = Convert.ToInt32(cmbDuration.SelectedItem);
            double totalTutar = selectedDays * GunlukFiyat;

            // 1. ADIM: Bakiye Kontrolü
            if (user.balance < totalTutar)
            {
                MessageBox.Show($"Bakiyeniz bu işlem için yetersiz!\nGereken: {totalTutar.ToString("C2")}\nMevcut Bakiyeniz: {user.balance.ToString("C2")}", 
                                "Yetersiz Bakiye", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            int scooterId = Convert.ToInt32(scooter["id"]);

            string onayMesaji = $"{selectedDays} gün kiralama bedeli toplam {totalTutar.ToString("C2")} tutmaktadır.\n" +
                                "Bu tutar mevcut bakiyenizden tahsil edilecektir. Onaylıyor musunuz?";

            var onay = MessageBox.Show(onayMesaji, "Kiralama Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                bool kiralamaBasarili = DatabaseHelper.RentScooter(user.id, scooterId, selectedDays, totalTutar);

                if (kiralamaBasarili)
                {
                    DatabaseHelper.UpdateUserBalance(user.id, -totalTutar);

                    user.balance -= totalTutar;

                    MessageBox.Show("Kiralama başarıyla tamamlandı! QR Kodunuza 'Aktif Kiralama' sekmesinden ulaşabilirsiniz.",
                                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kiralama işlemleri sırasında sistemsel bir veritabanı hatası oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}