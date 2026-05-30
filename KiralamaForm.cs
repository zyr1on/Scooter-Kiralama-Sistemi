using Scooter_Kiralama_Sistemi.Helpers;
using System.Globalization;
using System.Data;

namespace Scooter_Kiralama_Sistemi
{
    public partial class KiralamaForm : Form
    {
        private DataRow _scooter;
        private Users _user;
        private const double DailyPrice = 50.0;

        public KiralamaForm(DataRow scooter, Users user)
        {
            InitializeComponent();
            if (this.DesignMode) return;

            _scooter = scooter;
            _user = user;

            InitializeDisplay();
        }

        // ekran verilerini başlat
        private void InitializeDisplay()
        {
            lblScooterName.Text = $"Scooter: {_scooter["name"]}";
            lblStatus.Text = $"Durum: {_scooter["status"]}";
            lblBattery.Text = $"Batarya: %{_scooter["battery"]}";

            cmbDuration.Items.AddRange(new object[] { 1, 3, 5, 7, 30 });
            cmbDuration.SelectedIndex = 0;
            UpdatePrice();
        }

        // fiyatı hesapla ve göster
        private void UpdatePrice()
        {
            if (cmbDuration.SelectedItem == null) return;

            int days = Convert.ToInt32(cmbDuration.SelectedItem);
            double total = days * DailyPrice;

            lblTotalPrice.Text = $"Toplam Ücret: {total.ToString("C2", CultureInfo.CurrentCulture)}";
            lblTotalPrice.ForeColor = (_user.balance < total) ? Color.Red : Color.Green;
        }

        // kiralama işlemini gerçekleştir
        private void btnRent_Click(object sender, EventArgs e)
        {
            if (cmbDuration.SelectedItem == null) return;

            int days = Convert.ToInt32(cmbDuration.SelectedItem);
            double total = days * DailyPrice;

            if (_user.balance < total)
            {
                MessageBox.Show($"Bakiye yetersiz! Gereken: {total:C2}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"{days} gün kiralama onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ProcessRental(days, total);
            }
        }

        // veritabanı işlemlerini yönet
        private void ProcessRental(int days, double total)
        {
            int scooterId = Convert.ToInt32(_scooter["id"]);

            if (DatabaseHelper.RentScooter(_user.id, scooterId, days, total))
            {
                DatabaseHelper.UpdateUserBalance(_user.id, -total);
                _user.balance -= total;

                MessageBox.Show("Kiralama başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sistem hatası oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // süre değişimini tetikle
        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e) => UpdatePrice();
    }
}