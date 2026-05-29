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
            aktifKiralama = DatabaseHelper.getActiveRental(currentUser.id);

            if (aktifKiralama != null)
            {
                // 1. Scooter Adını Ekrana Bas (Tasarımda label4 yazan ilk yer)
                lblScooterAdi.Text = aktifKiralama.scooter_name;

                // 2. Toplam Ücreti Ekrana Bas
                lblToplamUcret.Text = $"{aktifKiralama.total_price} TL";

                // *** DURUM KONTROLÜ (Pending / QR Bekleme Senaryosu) ***
                if (aktifKiralama.status == "pending")
                {
                    // Eğer daha QR okutulmadıysa başlangıç bellidir ama süre henüz başlamamıştır
                    if (DateTime.TryParse(aktifKiralama.start_date, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime baslangicUtc))
                    {
                        lblBaslangicTarihi.Text = baslangicUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        lblBaslangicTarihi.Text = "Belirlenmedi";
                    }

                    // QR okutulmadığı için bitiş ve kalan gün yerine uyarı yazıyoruz
                    lblBitisTarihi.Text = "QR Okutulması Bekleniyor";
                    lblKalanGun.Text = "Beklemede";

                    // Süre dolma kontrolüne girmemesi için metottan çıkıyoruz
                    return;
                }

                // *** SCOOTER AKTİF/RESERVED İSE (Normal Süre Hesaplama Akışı) ***
                if (DateTime.TryParse(aktifKiralama.end_date, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime bitisTarihiUtc))
                {
                    // Eğer şu anki zaman, bitiş tarihini geçmişse otomatik sonlandır
                    if (DateTime.Now >= bitisTarihiUtc)
                    {
                        DatabaseHelper.EndRental(aktifKiralama.id, DateTime.Now);
                        mapHelper.RefreshMapMarkers();
                        aktifKiralama = null;

                        MessageBox.Show("Kiralama süreniz dolduğu için işleminiz otomatik olarak sonlandırılmıştır. Scooter haritada tekrar müsait duruma geçmiştir.", "Süre Doldu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ekranı temizle (Her şeyi boşalt veya label4'leri temizle)
                        TemizleKiralamaEkranı();
                        return;
                    }
                }

                // Tarihleri normal şekilde labellara dolduruyoruz
                if (DateTime.TryParse(aktifKiralama.start_date, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime normalBaslangicUtc))
                {
                    lblBaslangicTarihi.Text = normalBaslangicUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (DateTime.TryParse(aktifKiralama.end_date, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime normalBitisUtc))
                {
                    lblBitisTarihi.Text = normalBitisUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

                    // Kalan günü tam hesapla
                    TimeSpan kalanSure = normalBitisUtc.ToLocalTime() - DateTime.Now;
                    lblKalanGun.Text = Math.Ceiling(kalanSure.TotalDays).ToString();
                }
            }
            else
            {
                // Aktif kiralama yoksa ekranı temiz tutalım
                TemizleKiralamaEkranı();
            }
        }

        // Aktif kiralama bittiğinde veya olmadığında labelların sıfırlanması için yardımcı metot
        private void TemizleKiralamaEkranı()
        {
            lblScooterAdi.Text = "-";
            lblBaslangicTarihi.Text = "-";
            lblBitisTarihi.Text = "-";
            lblKalanGun.Text = "-";
            lblToplamUcret.Text = "-";
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
