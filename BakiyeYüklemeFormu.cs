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
    public partial class BakiyeYüklemeFormu : Form
    {
        private Users user;
        public BakiyeYüklemeFormu(Users _user)
        {
            InitializeComponent();
            if(!this.DesignMode)
            {
                user = _user;

                // Form tasarımsal ayarları
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
    
                cmbTutar.Items.Clear();
                // Kullanıcının seçebileceği sabit tutarlar
                cmbTutar.Items.AddRange(new object[] { 50, 100, 200, 300, 500 });
                cmbTutar.SelectedIndex = 0; // Varsayılan olarak 50 seçili gelsin
            }

        }
        

        private bool LuhnKontrol(string kartNo)
        {
            // Boşlukları ve tireleri temizle
            kartNo = kartNo.Replace(" ", "").Replace("-", "");

            // Sadece sayı mı ve uzunluğu mantıklı mı kontrolü (Genelde 16 hanedir)
            if (!long.TryParse(kartNo, out _) || kartNo.Length < 13 || kartNo.Length > 19)
                return false;

            int toplam = 0;
            bool ciftMi = false;

            // String'i tersten okuyoruz
            for (int i = kartNo.Length - 1; i >= 0; i--)
            {
                int rakam = kartNo[i] - '0'; // Char'ı integer'a hızlı çevirme

                if (ciftMi)
                {
                    rakam *= 2;
                    if (rakam > 9)
                    {
                        rakam -= 9; // 18 çıkarsa 1+8=9 yapmak yerine direkt 9 çıkartıyoruz
                    }
                }

                toplam += rakam;
                ciftMi = !ciftMi;
            }

            // Toplam 10'a tam bölünüyorsa kart numarası geçerlidir
            return (toplam % 10 == 0);
        }


        private void btnYukle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKartNo.Text) ||
                    string.IsNullOrWhiteSpace(txtTarih.Text) ||
                    string.IsNullOrWhiteSpace(txtCvv.Text))
            {
                MessageBox.Show("Lütfen tüm kart bilgilerini eksiksiz doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!LuhnKontrol(txtKartNo.Text))
            {
                MessageBox.Show("Geçersiz bir kredi kartı numarası girdiniz! Lütfen kontrol edin.", "Kart Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hatalıysa işlemi anında durdur
            }
            if (cmbTutar.SelectedItem == null) return;
            double eklenecekTutar = Convert.ToDouble(cmbTutar.SelectedItem);
            bool basarili = DatabaseHelper.UpdateUserBalance(user.id, eklenecekTutar);
            if (basarili)
            {
                // 4. Geçerli kullanıcı nesnesinin bakiyesini güncelle
                user.balance += eklenecekTutar;

                MessageBox.Show($"{eklenecekTutar} TL bakiyeniz başarıyla yüklendi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Bakiye yüklenirken sistemsel bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
