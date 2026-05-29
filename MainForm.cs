using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Scooter_Kiralama_Sistemi.Helpers;
using System.Data;

namespace Scooter_Kiralama_Sistemi
{
    public partial class MainForm : Form
    {
        #region Değişkenler ve Tanımlamalar

        MapHelper mapHelper = new MapHelper();
        private Users currentUser;
        private Rentals? aktifKiralama = null;

        #endregion

        #region Yapıcı Metot (Constructor)

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
        }

        #endregion

        #region Kiralama Durumu ve Senkronizasyon (Rental Logic)

        private void AktifKiralamayiYukle()
        {
            aktifKiralama = DatabaseHelper.getActiveRental(currentUser.id);

            if (aktifKiralama != null)
            {
                lblScooterAdi.Text = aktifKiralama.scooter_name;
                lblToplamUcret.Text = $"{aktifKiralama.total_price} TL";

                lblDurumPanel.Visible = true;
                lblAktifDurum.Visible = false;
                btnQRGoster.Visible = true;
                

                if (aktifKiralama.status == "pending")
                {
                    if (DateTime.TryParse(aktifKiralama.start_date, out DateTime pendingBaslangic))
                    {
                        lblBaslangicTarihi.Text = pendingBaslangic.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        lblBaslangicTarihi.Text = "Belirlenmedi";
                    }

                    lblBitisTarihi.Text = "QR Okutulması Bekleniyor";
                    lblKalanGun.Text = "Beklemede";
                    return;
                }

                if (DateTime.TryParse(aktifKiralama.end_date, out DateTime bitisTarihi))
                {
                    if (DateTime.Now >= bitisTarihi)
                    {
                        // Kiralama süresi bittiği için veritabanında işlemi bitiriyoruz
                        DatabaseHelper.EndRental(aktifKiralama.id);

                        mapHelper.RefreshMapMarkers();
                        aktifKiralama = null;

                        MessageBox.Show("Kiralama süreniz dolduğu için işleminiz otomatik olarak sonlandırılmıştır. Scooter haritada tekrar müsait duruma geçmiştir.", "Süre Doldu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        TemizleKiralamaEkranı();
                        return;
                    }
                }

                if (DateTime.TryParse(aktifKiralama.start_date, out DateTime baslangicTarihi))
                {
                    lblBaslangicTarihi.Text = baslangicTarihi.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (DateTime.TryParse(aktifKiralama.end_date, out DateTime normalBitisTarihi))
                {
                    lblBitisTarihi.Text = normalBitisTarihi.ToString("yyyy-MM-dd HH:mm:ss");

                    TimeSpan kalanSure = normalBitisTarihi - DateTime.Now;
                    lblKalanGun.Text = Math.Ceiling(kalanSure.TotalDays).ToString();
                }
            }
            else
            {
                TemizleKiralamaEkranı();
            }
        }

        private void TemizleKiralamaEkranı()
        {
            lblScooterAdi.Text = "-";
            lblBaslangicTarihi.Text = "-";
            lblBitisTarihi.Text = "-";
            lblKalanGun.Text = "-";
            lblToplamUcret.Text = "-";
            lblDurumPanel.Visible = false;
            lblAktifDurum.Visible = true;
            btnQRGoster.Visible = false;
            pbQRKod.Image = null; // Eski QR kod ekranda asılı kalmasın
        }

        #endregion

        #region Buton ve Harita Eventleri (UI Events)

        private void btnQRGoster_Click(object sender, EventArgs e)
        {
            if (aktifKiralama == null)
            {
                MessageBox.Show("Aktif bir kiralamanız bulunmadığı için QR kod oluşturulamıyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var qrBitmap = QRHelper.GenerateQR(aktifKiralama.qr_code);
            pbQRKod.Image = qrBitmap;
            pbQRKod.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Harita_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag != null)
            {
                int scooterId = Convert.ToInt32(item.Tag);

                DataRow secilenScooter = DatabaseHelper.GetScooterById(scooterId);

                if (secilenScooter != null)
                {
                    // Eğer scooter müsait değilse (kiralanmışsa veya rezerve edilmişse) kullanıcıyı engelle
                    if (secilenScooter["status"].ToString() != "available")
                    {
                        MessageBox.Show("Bu scooter şu anda kullanımda veya başka bir işlem bekliyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (KiralamaForm frm = new KiralamaForm(secilenScooter, currentUser))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            AktifKiralamayiYukle();         // Profildeki aktif kiralama sayfasını yenile
                            mapHelper.RefreshMapMarkers(); // Haritadaki pinleri (kırmızı/yeşil) yenile
                            lblProfileTabBalance.Text = currentUser.balance.ToString(); // Bakiyeyi güncelle
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

        #endregion

        private void btnRefreshRental_Click(object sender, EventArgs e)
        {
            AktifKiralamayiYukle();
        }
    }
}