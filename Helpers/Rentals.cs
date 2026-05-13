namespace Scooter_Kiralama_Sistemi.Helpers
{
    public class Rentals
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int scooter_id { get; set; }
        public string start_date { get; set; }
        public string? end_date { get; set; } // Nullable, kiralama bitmemiş olabilir
        public int days { get; set; }
        public double total_price { get; set; }
        public string status { get; set; } // 'active' veya 'finished'
        public string created_at { get; set; }

        // JOIN ile gelecek ekstra alanlar (Arayüzde göstermek için)
        public string? scooter_name { get; set; }
        public string? qr_code { get; set; }
    }
}
