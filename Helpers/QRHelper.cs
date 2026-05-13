using QRCoder;
namespace Scooter_Kiralama_Sistemi.Helpers
{
    public static class QRHelper
    {
        public static Bitmap GenerateQR(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                // Login formundaki o güzel açık maviyi (0, 150, 255 gibi) 
                // QR rengi yaparak temaya uydurabilirsin
                return qrCode.GetGraphic(20, Color.Black, Color.White, true);
            }
        }
    }
}
