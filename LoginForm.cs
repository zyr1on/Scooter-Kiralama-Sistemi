using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            if (this.DesignMode) return;

            SwitchToLoginMode();
        }

        // giriş paneline geçiş yap
        private void SwitchToLoginMode()
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            this.AcceptButton = btnLogin;
        }

        // kayıt paneline geçiş yap
        private void SwitchToRegisterMode()
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            pnlRegister.Location = pnlLogin.Location;
            this.AcceptButton = btnRegister;
        }

        // giriş işlemini yönet
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmailLogin == null || txtPassLogin == null) return;

            string email = txtEmailLogin.Text.Trim();
            string password = txtPassLogin.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen e-posta ve şifre alanlarını boş bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = DatabaseHelper.UserLogin(email, password);

            if (user != null)
            {
                this.Hide();
                if (user.role == UserRole.Admin)
                    new AdminForm().ShowDialog();
                else
                    new MainForm(user).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("E-posta adresiniz veya şifreniz hatalı.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // yeni kullanıcı kaydını gerçekleştir
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string email = txtEmailRegister.Text.Trim();
            string password = txtPassRegister.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.AddUser(name, surname, email, password))
            {
                MessageBox.Show("Kayıt işleminiz başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToLoginMode();
                txtEmailLogin.Text = email;
                txtPassLogin.Focus();
            }
            else
            {
                MessageBox.Show("Bu e-posta adresi zaten kullanımda veya bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // panel geçiş eventleri
        private void lblGoToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SwitchToRegisterMode();
        private void lblGoToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SwitchToLoginMode();
    }
}