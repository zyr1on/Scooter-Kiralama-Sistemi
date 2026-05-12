using Scooter_Kiralama_Sistemi.Helpers;


namespace Scooter_Kiralama_Sistemi
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            this.AcceptButton = btnLogin;
        }


        // LoginFormdaki  "Login" Butonuna tıklayınca?
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmailLogin == null || txtPassLogin == null)
            {
                MessageBox.Show("Tasarım hatası: Giriş kutuları bulunamadı!", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string email = txtEmailLogin.Text.Trim();
            string password = txtPassLogin.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen e-posta ve şifre alanlarını boş bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi burada kes
            }
            var loggedInUser = DatabaseHelper.UserLogin(email, password);
            if (loggedInUser != null)
            {
                //string mesaj = $"Giriş Başarılı!\nHoşgeldin {loggedInUser.name} {loggedInUser.surname}\nSistemdeki Rolünüz: {loggedInUser.role}";
                //MessageBox.Show(mesaj, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (loggedInUser.role == UserRole.Admin)
                {
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.ShowDialog();
                    this.Close();

                }
                else
                {
                    this.Hide();
                    MainForm mainForm = new MainForm(loggedInUser);
                    mainForm.ShowDialog();
                    this.Close();
                }

            }
            else
            {
                // Kullanıcı bulunamadı veya şifre yanlış!
                MessageBox.Show("E-posta adresiniz veya şifreniz hatalı. Lütfen tekrar deneyin.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblGoToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            pnlRegister.Location = pnlLogin.Location;
            this.AcceptButton = btnRegister;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string ad = txtName.Text.Trim();
            string soyad = txtSurname.Text.Trim();
            string email = txtEmailRegister.Text.Trim();
            string pass = txtPassRegister.Text;

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool basariliMi = DatabaseHelper.AddUser(ad, soyad, email, pass);
            if (basariliMi)
            {
                MessageBox.Show("Kayıt işleminiz başarıyla tamamlandı! Şimdi giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Panelleri eski haline getir (Otomatik Login'e dönüş)
                pnlRegister.Visible = false;
                pnlLogin.Visible = true;
                this.AcceptButton = btnLogin;

                // Formu temizleyelim ki yeni giriş için temiz kalsın
                txtEmailLogin.Text = email; // Kolaylık olsun diye e-postayı dolduralım
                txtPassLogin.Focus();
            }
            else
            {
                MessageBox.Show("Bu e-posta adresi zaten kullanımda veya bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        // DataBase için Register yapısı eklenmeli
        // Registerde admin register olamaz, kullanıcı register olabilir.
    }
}
