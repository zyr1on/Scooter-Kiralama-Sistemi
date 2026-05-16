using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;
using System.Data;


namespace Scooter_Kiralama_Sistemi
{

    public partial class MainForm : Form
    {
        MapHelper mapHelper = new MapHelper();
        private Users currentUser; // Giriş yapan kullanıcıyı burada tutacağız, Profil kısmı bunun üzerinden dolacak.
        private Rentals? aktifKiralama = null;

        public MainForm(Users _user)
        {
            InitializeComponent();
            tabHarita.Controls.Add(mapHelper.gmapControl);

            mapHelper.gmapControl.Dock = DockStyle.Fill;
            mapHelper.setupMap();

            currentUser = _user;
            mapHelper.RefreshMapMarkers();

            lblProfileTabUserName.Text = lblMapTabUserName.Text = currentUser.name;
            lblProfileTabEmail.Text = currentUser.email;
            lblProfileTabSurname.Text = currentUser.surname;
            lblProfileTabBalance.Text = currentUser.balance.ToString();

            mapHelper.gmapControl.OnMarkerClick += Harita_OnMarkerClick;

            AktifKiralamayiYukle();
            // 4. Kullanıcıya sadece MÜSAİT scooterları gösterelim
        }


        private void AktifKiralamayiYukle()
        {
            // Veritabanından kullanıcının aktif kiralamasını çekiyoruz
            aktifKiralama = DatabaseHelper.getActiveRental(currentUser.id);

            // *** YENİ: SÜRE KONTROLÜ VE OTOMATİK SONLANDIRMA ***
            if (aktifKiralama != null)
            {
                if (DateTime.TryParse(aktifKiralama.end_date, out DateTime bitisTarihi))
                {
                    // Eğer şu anki zaman, bitiş tarihini geçmişse
                    if (DateTime.Now >= bitisTarihi)
                    {
                        // Admin paneli için yazdığımız metodu kullanarak kiralamayı bitir ve scooter'ı boşa çıkar
                        DatabaseHelper.EndRental(aktifKiralama.id);

                        // Haritadaki pinleri yenile (Scooter tekrar yeşil olacak)
                        mapHelper.RefreshMapMarkers();

                        // Artık aktif kiralama kalmadı, değişkeni null yapıyoruz
                        aktifKiralama = null;

                        // Kullanıcıya bilgi ver (İsteğe bağlı)
                        MessageBox.Show("Kiralama süreniz dolduğu için işleminiz otomatik olarak sonlandırılmıştır. Scooter haritada tekrar müsait duruma geçmiştir.", "Süre Doldu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            // Kiralama yoksa 'hasRental' false, varsa true olacak
            bool hasRental = (aktifKiralama != null);

            // 1. Durum Mesajı: Kiralama YOKSA görünür olmalı, VARSA gizlenmeli
            lblAktifDurum.Visible = !hasRental;
            lblAktifDurum.Text = "Aktif kiralamanız bulunmamaktadır.";

            // 2. Kiralama Detayları: Kiralama VARSA görünür olmalı, YOKSA gizlenmeli
            // NOT: Tasarımdaki Label isimlerine göre burayı kendine uydurabilirsin
            lblDurumPanel.Visible = hasRental;
            pbQRKod.Visible = hasRental;
            btnQRGoster.Visible = hasRental;

            // Eğer kiralama varsa verileri yerleştiriyoruz
            if (hasRental)
            {
                lblScooterAdi.Text = $"Scooter: {aktifKiralama.scooter_name}";
                lblBaslangicTarihi.Text = $"Başlangıç: {aktifKiralama.start_date}";
                lblBitisTarihi.Text = $"Bitiş: {aktifKiralama.end_date}";

                if (DateTime.TryParse(aktifKiralama.end_date, out DateTime bitis))
                {
                    TimeSpan fark = bitis - DateTime.Now;
                    int kalanGun = fark.Days > 0 ? fark.Days : 0;
                    lblKalanGun.Text = $"Kalan Gün: {kalanGun}";
                }

                lblToplamUcret.Text = $"Toplam Ücret: {aktifKiralama.total_price} TL";
                pbQRKod.Image = null; // Eski QR kodu temizle
            }
        }

        private void btnQRGoster_Click(object sender, EventArgs e)
        {
            if (aktifKiralama == null) return;

            var qrBitmap = QRHelper.GenerateQR(aktifKiralama.qr_code);
            pbQRKod.Image = qrBitmap;
            pbQRKod.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Harita_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag != null)
            {
                int scooterId = Convert.ToInt32(item.Tag);

                // 1. Veritabanından tıklanan scooter'ın tüm bilgilerini al
                DataRow secilenScooter = DatabaseHelper.GetScooterById(scooterId);

                if (secilenScooter != null)
                {
                    // 2. Eğer scooter müsait değilse (kiralanmışsa vs.) kullanıcıyı engelle
                    if (secilenScooter["status"].ToString() != "available")
                    {
                        MessageBox.Show("Bu scooter şu anda kullanımda veya bakımda.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Kiralama Formunu (Pop-up) Aç
                    using (KiralamaForm frm = new KiralamaForm(secilenScooter, currentUser))
                    {
                        // Kullanıcı formu 'OK' (Kirala) diyerek kapattıysa arayüzü güncelle
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            AktifKiralamayiYukle(); // Profildeki aktif kiralama sayfasını yenile
                            mapHelper.RefreshMapMarkers(); // Haritadaki pinleri (kırmızı/yeşil) yenile
                            lblProfileTabBalance.Text = currentUser.balance.ToString();
                        }
                    }
                }
            }
        }

        private void lblBtnAddBalance_Click(object sender, EventArgs e)
        {
            using (BakiyeYüklemeFormu frm = new BakiyeYüklemeFormu(currentUser))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lblProfileTabBalance.Text = currentUser.balance.ToString();
                }

            }
        }
    }
}
