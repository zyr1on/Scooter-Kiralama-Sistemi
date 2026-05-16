using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Scooter_Kiralama_Sistemi.Helpers
{
    // GMapMarker'dan miras alan özel sınıfımız
    public class BatteryScooterMarker : GMapMarker
    {
        private readonly Bitmap scooterIcon;
        private readonly int batteryLevel; // 0-100 arası

        // Marker'ın boyutu (Halka dahil)
        private const int MarkerSize = 40;
        private const int IconSize = 28; // İçteki scooter logosunun boyutu

        public BatteryScooterMarker(PointLatLng pos, Bitmap icon, int battery)
            : base(pos)
        {
            this.scooterIcon = icon;
            this.batteryLevel = Math.Max(0, Math.Min(100, battery)); // 0-100 arası sabitle

            // Marker'ın tıklanabilir alanını belirliyoruz (merkezlenmiş)
            this.Size = new Size(MarkerSize, MarkerSize);
            this.Offset = new Point(-MarkerSize / 2, -MarkerSize / 2);
        }

        // Çizim işinin yapıldığı yer
        public override void OnRender(Graphics g)
        {
            // Harita üzerindeki enlem/boylam koordinatını, ekranın X/Y koordinatına çevir
            // LocalPosition base class tarafından hesaplanır.

            // 1. Çizim kalitesini artır (Pürüzsüz daireler için)
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Marker'ın merkez koordinatları
            int centerX = LocalPosition.X + MarkerSize / 2;
            int centerY = LocalPosition.Y + MarkerSize / 2;

            // 2. Batarya Halkasını Çiz (Arka plan - Gri)
            int arcPadding = 2; // Dış halka ile ikon arası boşluk
            Rectangle arcRect = new Rectangle(
                LocalPosition.X + arcPadding,
                LocalPosition.Y + arcPadding,
                MarkerSize - (arcPadding * 2),
                MarkerSize - (arcPadding * 2)
            );

            using (Pen bgPen = new Pen(Color.FromArgb(100, Color.Gray), 4)) // Saydam gri halka
            {
                g.DrawEllipse(bgPen, arcRect);
            }

            // 3. Batarya Halkasını Çiz (Doluluk - Yeşil/Kırmızı)
            Color batteryColor = GetBatteryColor(batteryLevel);
            using (Pen fillPen = new Pen(batteryColor, 4))
            {
                fillPen.StartCap = LineCap.Round;
                fillPen.EndCap = LineCap.Round;

                // Doluluk oranına göre açı hesapla (0-360 derece)
                // SQLite'dan gelen yüzdeyi açıya çeviriyoruz.
                float sweepAngle = (batteryLevel / 100f) * 360f;

                // Saatin tepesinden (-90 derece) başla ve doluluk kadar çiz
                g.DrawArc(fillPen, arcRect, -90, sweepAngle);
            }

            // 4. Scooter İkonunu Çiz (Merkeze)
            if (scooterIcon != null)
            {
                Rectangle iconRect = new Rectangle(
                    centerX - IconSize / 2,
                    centerY - IconSize / 2,
                    IconSize,
                    IconSize
                );
                g.DrawImage(scooterIcon, iconRect);
            }
        }

        // Batarya seviyesine göre renk belirleyen yardımcı metot
        private Color GetBatteryColor(int level)
        {
            if (level > 50) return Color.FromArgb(200, Color.LimeGreen); // İyi durum
            if (level > 20) return Color.FromArgb(200, Color.Orange);    // Orta durum
            return Color.FromArgb(200, Color.Red);                       // Kritik durum
        }
    }
}