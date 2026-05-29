namespace Scooter_Kiralama_Sistemi
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            tabHarita = new TabPage();
            panel1 = new Panel();
            lblMapTabUserName = new Label();
            pictureBox1 = new PictureBox();
            tabPage1 = new TabPage();
            lblAktifDurum = new Label();
            btnQRGoster = new Button();
            pbQRKod = new PictureBox();
            lblDurumPanel = new Panel();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            lblToplamUcret = new Label();
            lblKalanGun = new Label();
            lblBitisTarihi = new Label();
            lblBaslangicTarihi = new Label();
            lblScooterAdi = new Label();
            tabProfil = new TabPage();
            lblBtnAddBalance = new Button();
            lblProfileTabBalance = new Label();
            lbl4 = new Label();
            lblProfileTabEmail = new Label();
            lblProfileTabSurname = new Label();
            lblProfileTabUserName = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            tabGecmis = new TabPage();
            tabControl1.SuspendLayout();
            tabHarita.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRKod).BeginInit();
            lblDurumPanel.SuspendLayout();
            tabProfil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabHarita);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabProfil);
            tabControl1.Controls.Add(tabGecmis);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabHarita
            // 
            tabHarita.BackColor = Color.Transparent;
            tabHarita.Controls.Add(panel1);
            tabHarita.Location = new Point(4, 24);
            tabHarita.Name = "tabHarita";
            tabHarita.Padding = new Padding(3);
            tabHarita.Size = new Size(792, 422);
            tabHarita.TabIndex = 0;
            tabHarita.Text = "Harita";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblMapTabUserName);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(578, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(206, 92);
            panel1.TabIndex = 0;
            // 
            // lblMapTabUserName
            // 
            lblMapTabUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMapTabUserName.AutoSize = true;
            lblMapTabUserName.Font = new Font("Comic Sans MS", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblMapTabUserName.Location = new Point(82, 33);
            lblMapTabUserName.Name = "lblMapTabUserName";
            lblMapTabUserName.Size = new Size(116, 26);
            lblMapTabUserName.TabIndex = 1;
            lblMapTabUserName.Text = "Kullanıcı Adı";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(10, 26, 58);
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(14, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(62, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lblAktifDurum);
            tabPage1.Controls.Add(btnQRGoster);
            tabPage1.Controls.Add(pbQRKod);
            tabPage1.Controls.Add(lblDurumPanel);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(792, 422);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Aktif Kiralama";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblAktifDurum
            // 
            lblAktifDurum.AutoSize = true;
            lblAktifDurum.Location = new Point(267, 328);
            lblAktifDurum.Name = "lblAktifDurum";
            lblAktifDurum.Size = new Size(196, 15);
            lblAktifDurum.TabIndex = 5;
            lblAktifDurum.Text = "Aktif kiralamanız bulunmamaktadır.";
            // 
            // btnQRGoster
            // 
            btnQRGoster.Location = new Point(235, 293);
            btnQRGoster.Name = "btnQRGoster";
            btnQRGoster.Size = new Size(277, 23);
            btnQRGoster.TabIndex = 2;
            btnQRGoster.Text = "QR Göster";
            btnQRGoster.UseVisualStyleBackColor = true;
            btnQRGoster.Click += btnQRGoster_Click;
            // 
            // pbQRKod
            // 
            pbQRKod.Location = new Point(236, 217);
            pbQRKod.Name = "pbQRKod";
            pbQRKod.Size = new Size(276, 66);
            pbQRKod.TabIndex = 1;
            pbQRKod.TabStop = false;
            // 
            // lblDurumPanel
            // 
            lblDurumPanel.Controls.Add(label8);
            lblDurumPanel.Controls.Add(label7);
            lblDurumPanel.Controls.Add(label6);
            lblDurumPanel.Controls.Add(label5);
            lblDurumPanel.Controls.Add(label4);
            lblDurumPanel.Controls.Add(lblToplamUcret);
            lblDurumPanel.Controls.Add(lblKalanGun);
            lblDurumPanel.Controls.Add(lblBitisTarihi);
            lblDurumPanel.Controls.Add(lblBaslangicTarihi);
            lblDurumPanel.Controls.Add(lblScooterAdi);
            lblDurumPanel.Location = new Point(236, 15);
            lblDurumPanel.Name = "lblDurumPanel";
            lblDurumPanel.Size = new Size(385, 196);
            lblDurumPanel.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 134);
            label8.Name = "label8";
            label8.Size = new Size(80, 15);
            label8.TabIndex = 9;
            label8.Text = "Toplam Ücret:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 110);
            label7.Name = "label7";
            label7.Size = new Size(64, 15);
            label7.TabIndex = 8;
            label7.Text = "Kalan Gün:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 82);
            label6.Name = "label6";
            label6.Size = new Size(32, 15);
            label6.TabIndex = 7;
            label6.Text = "Bitiş:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 57);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 6;
            label5.Text = "Başlangıç:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 32);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 5;
            label4.Text = "Scooter Adı:";
            // 
            // lblToplamUcret
            // 
            lblToplamUcret.AutoSize = true;
            lblToplamUcret.Location = new Point(143, 134);
            lblToplamUcret.Name = "lblToplamUcret";
            lblToplamUcret.Size = new Size(38, 15);
            lblToplamUcret.TabIndex = 4;
            lblToplamUcret.Text = "label4";
            // 
            // lblKalanGun
            // 
            lblKalanGun.AutoSize = true;
            lblKalanGun.Location = new Point(143, 110);
            lblKalanGun.Name = "lblKalanGun";
            lblKalanGun.Size = new Size(38, 15);
            lblKalanGun.TabIndex = 3;
            lblKalanGun.Text = "label4";
            // 
            // lblBitisTarihi
            // 
            lblBitisTarihi.AutoSize = true;
            lblBitisTarihi.Location = new Point(143, 82);
            lblBitisTarihi.Name = "lblBitisTarihi";
            lblBitisTarihi.Size = new Size(38, 15);
            lblBitisTarihi.TabIndex = 2;
            lblBitisTarihi.Text = "label4";
            // 
            // lblBaslangicTarihi
            // 
            lblBaslangicTarihi.AutoSize = true;
            lblBaslangicTarihi.Location = new Point(143, 57);
            lblBaslangicTarihi.Name = "lblBaslangicTarihi";
            lblBaslangicTarihi.Size = new Size(38, 15);
            lblBaslangicTarihi.TabIndex = 1;
            lblBaslangicTarihi.Text = "label4";
            // 
            // lblScooterAdi
            // 
            lblScooterAdi.AutoSize = true;
            lblScooterAdi.Location = new Point(143, 32);
            lblScooterAdi.Name = "lblScooterAdi";
            lblScooterAdi.Size = new Size(38, 15);
            lblScooterAdi.TabIndex = 0;
            lblScooterAdi.Text = "label4";
            // 
            // tabProfil
            // 
            tabProfil.BackColor = Color.FromArgb(10, 26, 58);
            tabProfil.Controls.Add(lblBtnAddBalance);
            tabProfil.Controls.Add(lblProfileTabBalance);
            tabProfil.Controls.Add(lbl4);
            tabProfil.Controls.Add(lblProfileTabEmail);
            tabProfil.Controls.Add(lblProfileTabSurname);
            tabProfil.Controls.Add(lblProfileTabUserName);
            tabProfil.Controls.Add(label3);
            tabProfil.Controls.Add(label2);
            tabProfil.Controls.Add(label1);
            tabProfil.Controls.Add(pictureBox2);
            tabProfil.ForeColor = Color.Black;
            tabProfil.Location = new Point(4, 24);
            tabProfil.Name = "tabProfil";
            tabProfil.Padding = new Padding(3);
            tabProfil.Size = new Size(792, 422);
            tabProfil.TabIndex = 1;
            tabProfil.Text = "Profil";
            // 
            // lblBtnAddBalance
            // 
            lblBtnAddBalance.BackColor = Color.FromArgb(96, 165, 250);
            lblBtnAddBalance.FlatAppearance.BorderSize = 0;
            lblBtnAddBalance.FlatStyle = FlatStyle.Flat;
            lblBtnAddBalance.Location = new Point(211, 339);
            lblBtnAddBalance.Name = "lblBtnAddBalance";
            lblBtnAddBalance.Size = new Size(369, 33);
            lblBtnAddBalance.TabIndex = 10;
            lblBtnAddBalance.Text = "Bakiye Yükle";
            lblBtnAddBalance.UseVisualStyleBackColor = false;
            lblBtnAddBalance.Click += lblBtnAddBalance_Click;
            // 
            // lblProfileTabBalance
            // 
            lblProfileTabBalance.AutoSize = true;
            lblProfileTabBalance.Font = new Font("Comic Sans MS", 14.25F);
            lblProfileTabBalance.ForeColor = Color.White;
            lblProfileTabBalance.Location = new Point(402, 301);
            lblProfileTabBalance.Name = "lblProfileTabBalance";
            lblProfileTabBalance.Size = new Size(56, 26);
            lblProfileTabBalance.TabIndex = 9;
            lblProfileTabBalance.Text = "İsim:";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Font = new Font("Comic Sans MS", 14.25F);
            lbl4.ForeColor = Color.White;
            lbl4.Location = new Point(322, 301);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(74, 26);
            lbl4.TabIndex = 8;
            lbl4.Text = "Bakiye:";
            // 
            // lblProfileTabEmail
            // 
            lblProfileTabEmail.AutoSize = true;
            lblProfileTabEmail.Font = new Font("Comic Sans MS", 14.25F);
            lblProfileTabEmail.ForeColor = Color.White;
            lblProfileTabEmail.Location = new Point(402, 262);
            lblProfileTabEmail.Name = "lblProfileTabEmail";
            lblProfileTabEmail.Size = new Size(56, 26);
            lblProfileTabEmail.TabIndex = 7;
            lblProfileTabEmail.Text = "İsim:";
            // 
            // lblProfileTabSurname
            // 
            lblProfileTabSurname.AutoSize = true;
            lblProfileTabSurname.Font = new Font("Comic Sans MS", 14.25F);
            lblProfileTabSurname.ForeColor = Color.White;
            lblProfileTabSurname.Location = new Point(402, 224);
            lblProfileTabSurname.Name = "lblProfileTabSurname";
            lblProfileTabSurname.Size = new Size(56, 26);
            lblProfileTabSurname.TabIndex = 6;
            lblProfileTabSurname.Text = "İsim:";
            // 
            // lblProfileTabUserName
            // 
            lblProfileTabUserName.AutoSize = true;
            lblProfileTabUserName.Font = new Font("Comic Sans MS", 14.25F);
            lblProfileTabUserName.ForeColor = Color.White;
            lblProfileTabUserName.Location = new Point(402, 186);
            lblProfileTabUserName.Name = "lblProfileTabUserName";
            lblProfileTabUserName.Size = new Size(56, 26);
            lblProfileTabUserName.TabIndex = 5;
            lblProfileTabUserName.Text = "İsim:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Comic Sans MS", 14.25F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(322, 262);
            label3.Name = "label3";
            label3.Size = new Size(64, 26);
            label3.TabIndex = 4;
            label3.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Comic Sans MS", 14.25F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(322, 224);
            label2.Name = "label2";
            label2.Size = new Size(83, 26);
            label2.TabIndex = 3;
            label2.Text = "Soyisim:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 14.25F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(322, 186);
            label1.Name = "label1";
            label1.Size = new Size(56, 26);
            label1.TabIndex = 2;
            label1.Text = "İsim:";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(10, 26, 58);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(322, 18);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(152, 151);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // tabGecmis
            // 
            tabGecmis.BackColor = Color.FromArgb(10, 26, 58);
            tabGecmis.Location = new Point(4, 24);
            tabGecmis.Name = "tabGecmis";
            tabGecmis.Padding = new Padding(3);
            tabGecmis.Size = new Size(792, 422);
            tabGecmis.TabIndex = 2;
            tabGecmis.Text = "Profil Geçmişi";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 26, 58);
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "MainForm";
            tabControl1.ResumeLayout(false);
            tabHarita.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbQRKod).EndInit();
            lblDurumPanel.ResumeLayout(false);
            lblDurumPanel.PerformLayout();
            tabProfil.ResumeLayout(false);
            tabProfil.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabHarita;
        private TabPage tabProfil;
        private TabPage tabGecmis;
        private Panel panel1;
        private Label lblMapTabUserName;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private Label lblProfileTabEmail;
        private Label lblProfileTabSurname;
        private Label lblProfileTabUserName;
        private Label label3;
        private Label label2;
        private Label lblProfileTabBalance;
        private Label lbl4;
        private Button lblBtnAddBalance;
        private TabPage tabPage1;
        private Panel lblDurumPanel;
        private Label lblToplamUcret;
        private Label lblKalanGun;
        private Label lblBitisTarihi;
        private Label lblBaslangicTarihi;
        private Label lblScooterAdi;
        private Label lblAktifDurum;
        private Button btnQRGoster;
        private PictureBox pbQRKod;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
    }
}