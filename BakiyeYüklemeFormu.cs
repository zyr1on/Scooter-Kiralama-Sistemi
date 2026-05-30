using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi
{
    public partial class BakiyeYüklemeFormu : Form
    {
        private Users _user;

        public BakiyeYüklemeFormu(Users user)
        {
            InitializeComponent();
            if (this.DesignMode) return;

            _user = user;
            InitializeUI();
        }

        // form arayüzünü hazırla
        private void InitializeUI()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            cmbTutar.Items.AddRange(new object[] { 50, 100, 200, 300, 500 });
            cmbTutar.SelectedIndex = 0;
        }

        // luhn algoritması ile kart doğrula
        private bool IsCardValid(string cardNumber)
        {
            string cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (!long.TryParse(cleanNumber, out _) || cleanNumber.Length < 13 || cleanNumber.Length > 19)
                return false;

            int total = 0;
            bool isDouble = false;

            for (int i = cleanNumber.Length - 1; i >= 0; i--)
            {
                int digit = cleanNumber[i] - '0';

                if (isDouble)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                total += digit;
                isDouble = !isDouble;
            }

            return (total % 10 == 0);
        }

        // bakiye yükleme işlemini yönet
        private void btnYukle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKartNo.Text) ||
                string.IsNullOrWhiteSpace(txtTarih.Text) ||
                string.IsNullOrWhiteSpace(txtCvv.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsCardValid(txtKartNo.Text))
            {
                MessageBox.Show("Geçersiz kredi kartı numarası.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessBalanceUpdate();
        }

        // veritabanı bakiye güncellemesini gerçekleştir
        private void ProcessBalanceUpdate()
        {
            double amount = Convert.ToDouble(cmbTutar.SelectedItem);

            if (DatabaseHelper.UpdateUserBalance(_user.id, amount))
            {
                _user.balance += amount;
                MessageBox.Show($"{amount} TL başarıyla yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sistemsel bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}