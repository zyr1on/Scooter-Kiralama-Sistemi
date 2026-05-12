namespace Scooter_Kiralama_Sistemi
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            label1 = new Label();
            txtName = new TextBox();
            btnRegister = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pnlRegister = new Panel();
            label5 = new Label();
            txtPassRegister = new TextBox();
            label4 = new Label();
            txtEmailRegister = new TextBox();
            label3 = new Label();
            txtSurname = new TextBox();
            pnlLogin = new Panel();
            lblGoToRegister = new LinkLabel();
            label6 = new Label();
            txtPassLogin = new TextBox();
            label7 = new Label();
            btnLogin = new Button();
            txtEmailLogin = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pnlRegister.SuspendLayout();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(17, 14);
            label1.Name = "label1";
            label1.Size = new Size(22, 15);
            label1.TabIndex = 0;
            label1.Text = "Ad";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(27, 38, 59);
            txtName.BorderStyle = BorderStyle.None;
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(17, 32);
            txtName.Name = "txtName";
            txtName.Size = new Size(280, 16);
            txtName.TabIndex = 1;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(96, 165, 250);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Location = new Point(7, 204);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(324, 33);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Kayıt Ol";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(588, 533);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(4, 25, 53);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(581, 23);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(351, 446);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(673, 55);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(172, 76);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pnlRegister
            // 
            pnlRegister.Controls.Add(label5);
            pnlRegister.Controls.Add(txtPassRegister);
            pnlRegister.Controls.Add(label4);
            pnlRegister.Controls.Add(btnRegister);
            pnlRegister.Controls.Add(txtEmailRegister);
            pnlRegister.Controls.Add(label3);
            pnlRegister.Controls.Add(txtSurname);
            pnlRegister.Controls.Add(label1);
            pnlRegister.Controls.Add(txtName);
            pnlRegister.Location = new Point(236, 167);
            pnlRegister.Name = "pnlRegister";
            pnlRegister.Size = new Size(351, 302);
            pnlRegister.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.ForeColor = Color.Transparent;
            label5.Location = new Point(20, 137);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 6;
            label5.Text = "Şifre";
            // 
            // txtPassRegister
            // 
            txtPassRegister.BackColor = Color.FromArgb(27, 38, 59);
            txtPassRegister.BorderStyle = BorderStyle.None;
            txtPassRegister.ForeColor = Color.White;
            txtPassRegister.Location = new Point(20, 155);
            txtPassRegister.Name = "txtPassRegister";
            txtPassRegister.Size = new Size(280, 16);
            txtPassRegister.TabIndex = 7;
            txtPassRegister.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(17, 100);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 4;
            label4.Text = "Email";
            // 
            // txtEmailRegister
            // 
            txtEmailRegister.BackColor = Color.FromArgb(27, 38, 59);
            txtEmailRegister.BorderStyle = BorderStyle.None;
            txtEmailRegister.ForeColor = Color.White;
            txtEmailRegister.Location = new Point(17, 118);
            txtEmailRegister.Name = "txtEmailRegister";
            txtEmailRegister.Size = new Size(280, 16);
            txtEmailRegister.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(17, 63);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 2;
            label3.Text = "Soyad";
            // 
            // txtSurname
            // 
            txtSurname.BackColor = Color.FromArgb(27, 38, 59);
            txtSurname.BorderStyle = BorderStyle.None;
            txtSurname.ForeColor = Color.White;
            txtSurname.Location = new Point(17, 81);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(280, 16);
            txtSurname.TabIndex = 3;
            // 
            // pnlLogin
            // 
            pnlLogin.Controls.Add(lblGoToRegister);
            pnlLogin.Controls.Add(label6);
            pnlLogin.Controls.Add(txtPassLogin);
            pnlLogin.Controls.Add(label7);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(txtEmailLogin);
            pnlLogin.Location = new Point(581, 167);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(351, 302);
            pnlLogin.TabIndex = 10;
            // 
            // lblGoToRegister
            // 
            lblGoToRegister.AutoSize = true;
            lblGoToRegister.Location = new Point(106, 177);
            lblGoToRegister.Name = "lblGoToRegister";
            lblGoToRegister.Size = new Size(142, 15);
            lblGoToRegister.TabIndex = 11;
            lblGoToRegister.TabStop = true;
            lblGoToRegister.Text = "Hesabın Yok Mu? Kayıt Ol";
            lblGoToRegister.LinkClicked += lblGoToRegister_LinkClicked;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.ForeColor = Color.Transparent;
            label6.Location = new Point(29, 63);
            label6.Name = "label6";
            label6.Size = new Size(30, 15);
            label6.TabIndex = 6;
            label6.Text = "Şifre";
            // 
            // txtPassLogin
            // 
            txtPassLogin.BackColor = Color.FromArgb(27, 38, 59);
            txtPassLogin.BorderStyle = BorderStyle.None;
            txtPassLogin.ForeColor = Color.White;
            txtPassLogin.Location = new Point(29, 81);
            txtPassLogin.Name = "txtPassLogin";
            txtPassLogin.Size = new Size(280, 16);
            txtPassLogin.TabIndex = 7;
            txtPassLogin.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.ForeColor = Color.Transparent;
            label7.Location = new Point(29, 14);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 4;
            label7.Text = "Email";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(96, 165, 250);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(12, 109);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(324, 33);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtEmailLogin
            // 
            txtEmailLogin.BackColor = Color.FromArgb(27, 38, 59);
            txtEmailLogin.BorderStyle = BorderStyle.None;
            txtEmailLogin.ForeColor = Color.White;
            txtEmailLogin.Location = new Point(29, 32);
            txtEmailLogin.Name = "txtEmailLogin";
            txtEmailLogin.Size = new Size(280, 16);
            txtEmailLogin.TabIndex = 5;
            // 
            // LoginForm
            // 
            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(5, 26, 54);
            ClientSize = new Size(944, 530);
            Controls.Add(pnlRegister);
            Controls.Add(pnlLogin);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "LoginForm";
            Text = "Giris Ekranı";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pnlRegister.ResumeLayout(false);
            pnlRegister.PerformLayout();
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private TextBox txtPassword;
        private Label label2;
        private Button btnRegister;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel pnlRegister;
        private Label label3;
        private TextBox txtSurname;
        private Label label5;
        private TextBox txtPassRegister;
        private Label label4;
        private TextBox txtEmailRegister;
        private Button button1;
        private Panel pnlLogin;
        private Label label6;
        private TextBox textBox1;
        private TextBox txtPassLogin;
        private Label label7;
        private Button btnLogin;
        private TextBox txtEmailLogin;
        private LinkLabel lblGoToRegister;
    }
}