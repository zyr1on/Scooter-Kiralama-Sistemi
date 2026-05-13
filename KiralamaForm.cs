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
    public partial class KiralamaForm : Form
    {
        private DataRow scooter;
        private Users user;
        private const double GunlukFiyat = 50.0;
        public KiralamaForm(DataRow _scooter, Users _user)
        {
            InitializeComponent();
            scooter = _scooter;
            user = _user;

            lblScooterName.Text = "Scooter Adı: " + scooter["name"].ToString();
            lblStatus.Text = "Scooter Durumu: " + scooter["status"].ToString();
            lblBattery.Text = "Batarya: %" + scooter["battery"].ToString();

            // ComboBox'a gün seçeneklerini doldur (Formdaki ComboBox'ın adı cmbDuration olmalı)
            cmbDuration.Items.Clear();
            cmbDuration.Items.AddRange(new object[] { 1, 3, 5, 7, 30 });
            cmbDuration.SelectedIndex = 0; // Varsayılan olarak ilk seçenek (1) gelsin


            //FiyatGuncelle();


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
                MessageBox.Show($"Bakiyeniz yetersiz!\nGereken: {totalTutar} TL\nMevcut Bakiye: {user.balance} TL", "Yetersiz Bakiye", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Parası yetmiyorsa işlemi durdur
            }

            int scooterId = Convert.ToInt32(scooter["id"]);

            // 2. ADIM: Onay Kutusu (Formda fiyat göstermediğimiz için fiyatı burada söylüyoruz)
            var onay = MessageBox.Show(
                $"{selectedDays} gün kiralama bedeli toplam {totalTutar} TL tutmaktadır.\nBu tutar bakiyenizden düşülecektir. Onaylıyor musunuz?",
                "Kiralama Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                // 3. ADIM: Veritabanına kiralama kaydını ekle ve scooter'ı 'rented' yap
                bool kiralamaBasarili = DatabaseHelper.RentScooter(user.id, scooterId, selectedDays, totalTutar);

                if (kiralamaBasarili)
                {
                    // 4. ADIM: Başarılıysa bakiyeden düş (Eksi değer gönderiyoruz)
                    DatabaseHelper.UpdateUserBalance(user.id, -totalTutar);

                    // Ana formun (MainForm) güncel bakiyeyi görebilmesi için nesneyi de güncelliyoruz
                    user.balance -= totalTutar;

                    MessageBox.Show("Kiralama başarıyla tamamlandı! QR Kodunuza 'Aktif Kiralama' sekmesinden ulaşabilirsiniz.",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // İşlem bitti, pencereyi kapat
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kiralama sırasında sistemsel bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FiyatGuncelle()
        {
            if (cmbDuration.SelectedItem != null)
            {
                int gun = Convert.ToInt32(cmbDuration.SelectedItem);
                double toplamTutar = gun * GunlukFiyat;

                // Formdaki fiyat label'ına yazdır
                lblTotalPrice.Text = "Toplam Ücret: " + toplamTutar.ToString("0.00") + " TL";
            }
        }

        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiyatGuncelle();
        }
    }
}
