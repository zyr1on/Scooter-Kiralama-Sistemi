namespace Scooter_Kiralama_Sistemi
{
    partial class AdminForm
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
            tabControl1 = new TabControl();
            tabHarita = new TabPage();
            btnKonumSec = new Button();
            tabScooterlar = new TabPage();
            panel1 = new Panel();
            btnQRGoster = new Button();
            label4 = new Label();
            btnScooterSil = new Button();
            txtScooterBattery = new TextBox();
            btnScooterEkle = new Button();
            label3 = new Label();
            txtScooterLng = new TextBox();
            label2 = new Label();
            txtScooterLat = new TextBox();
            label1 = new Label();
            txtScooterName = new TextBox();
            dgvScooterlar = new DataGridView();
            tabKullanicilar = new TabPage();
            panel2 = new Panel();
            btnKiralamaGecmisi = new Button();
            txtBakiyeMiktari = new TextBox();
            btnKullaniciSil = new Button();
            label5 = new Label();
            btnBakiyeEkle = new Button();
            dgvKullanicilar = new DataGridView();
            tabKiralamalar = new TabPage();
            panel3 = new Panel();
            btnKiralamaSonlandir = new Button();
            dgvKiralamalar = new DataGridView();
            tabIstatistikler = new TabPage();
            pnlKiralanmis = new Panel();
            pnlMusait = new Panel();
            pnlToplamKazanc = new Panel();
            pnlAktifKiralama = new Panel();
            pnlToplamScooter = new Panel();
            pnlToplamKullanici = new Panel();
            tabControl1.SuspendLayout();
            tabHarita.SuspendLayout();
            tabScooterlar.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScooterlar).BeginInit();
            tabKullanicilar.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).BeginInit();
            tabKiralamalar.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKiralamalar).BeginInit();
            tabIstatistikler.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabHarita);
            tabControl1.Controls.Add(tabScooterlar);
            tabControl1.Controls.Add(tabKullanicilar);
            tabControl1.Controls.Add(tabKiralamalar);
            tabControl1.Controls.Add(tabIstatistikler);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(963, 524);
            tabControl1.TabIndex = 0;
            // 
            // tabHarita
            // 
            tabHarita.Controls.Add(btnKonumSec);
            tabHarita.Location = new Point(4, 24);
            tabHarita.Name = "tabHarita";
            tabHarita.Padding = new Padding(3);
            tabHarita.Size = new Size(955, 496);
            tabHarita.TabIndex = 0;
            tabHarita.Text = "Harita";
            tabHarita.UseVisualStyleBackColor = true;
            // 
            // btnKonumSec
            // 
            btnKonumSec.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnKonumSec.Location = new Point(833, 447);
            btnKonumSec.Name = "btnKonumSec";
            btnKonumSec.Size = new Size(114, 43);
            btnKonumSec.TabIndex = 0;
            btnKonumSec.Text = "📍 Bu Konuma Scooter Ekle";
            btnKonumSec.UseVisualStyleBackColor = true;
            btnKonumSec.Click += btnKonumSec_Click;
            // 
            // tabScooterlar
            // 
            tabScooterlar.Controls.Add(panel1);
            tabScooterlar.Controls.Add(dgvScooterlar);
            tabScooterlar.Location = new Point(4, 24);
            tabScooterlar.Name = "tabScooterlar";
            tabScooterlar.Padding = new Padding(3);
            tabScooterlar.Size = new Size(955, 496);
            tabScooterlar.TabIndex = 1;
            tabScooterlar.Text = " Scooterlar";
            tabScooterlar.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(btnQRGoster);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(btnScooterSil);
            panel1.Controls.Add(txtScooterBattery);
            panel1.Controls.Add(btnScooterEkle);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtScooterLng);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtScooterLat);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtScooterName);
            panel1.Location = new Point(660, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(277, 308);
            panel1.TabIndex = 1;
            // 
            // btnQRGoster
            // 
            btnQRGoster.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnQRGoster.Location = new Point(134, 279);
            btnQRGoster.Name = "btnQRGoster";
            btnQRGoster.Size = new Size(86, 26);
            btnQRGoster.TabIndex = 4;
            btnQRGoster.Text = "QR Göster";
            btnQRGoster.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 116);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 9;
            label4.Text = "Batarya";
            // 
            // btnScooterSil
            // 
            btnScooterSil.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnScooterSil.Location = new Point(42, 279);
            btnScooterSil.Name = "btnScooterSil";
            btnScooterSil.Size = new Size(86, 26);
            btnScooterSil.TabIndex = 3;
            btnScooterSil.Text = "Scooter Sil";
            btnScooterSil.UseVisualStyleBackColor = true;
            btnScooterSil.Click += btnScooterSil_Click;
            // 
            // txtScooterBattery
            // 
            txtScooterBattery.Location = new Point(90, 113);
            txtScooterBattery.Name = "txtScooterBattery";
            txtScooterBattery.Size = new Size(100, 23);
            txtScooterBattery.TabIndex = 8;
            // 
            // btnScooterEkle
            // 
            btnScooterEkle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnScooterEkle.Location = new Point(90, 142);
            btnScooterEkle.Name = "btnScooterEkle";
            btnScooterEkle.Size = new Size(86, 26);
            btnScooterEkle.TabIndex = 2;
            btnScooterEkle.Text = "Scooter Ekle";
            btnScooterEkle.UseVisualStyleBackColor = true;
            btnScooterEkle.Click += btnScooterEkle_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 87);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 7;
            label3.Text = "Boylam";
            // 
            // txtScooterLng
            // 
            txtScooterLng.Location = new Point(90, 84);
            txtScooterLng.Name = "txtScooterLng";
            txtScooterLng.Size = new Size(100, 23);
            txtScooterLng.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 58);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 5;
            label2.Text = "Enlem";
            // 
            // txtScooterLat
            // 
            txtScooterLat.Location = new Point(90, 55);
            txtScooterLat.Name = "txtScooterLat";
            txtScooterLat.Size = new Size(100, 23);
            txtScooterLat.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 29);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 3;
            label1.Text = "Scooter Adı";
            // 
            // txtScooterName
            // 
            txtScooterName.Location = new Point(90, 26);
            txtScooterName.Name = "txtScooterName";
            txtScooterName.Size = new Size(100, 23);
            txtScooterName.TabIndex = 2;
            // 
            // dgvScooterlar
            // 
            dgvScooterlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScooterlar.Dock = DockStyle.Left;
            dgvScooterlar.Location = new Point(3, 3);
            dgvScooterlar.Name = "dgvScooterlar";
            dgvScooterlar.Size = new Size(640, 490);
            dgvScooterlar.TabIndex = 0;
            dgvScooterlar.CellDoubleClick += dgvScooterlar_CellDoubleClick;
            // 
            // tabKullanicilar
            // 
            tabKullanicilar.Controls.Add(panel2);
            tabKullanicilar.Controls.Add(dgvKullanicilar);
            tabKullanicilar.Location = new Point(4, 24);
            tabKullanicilar.Name = "tabKullanicilar";
            tabKullanicilar.Padding = new Padding(3);
            tabKullanicilar.Size = new Size(955, 496);
            tabKullanicilar.TabIndex = 2;
            tabKullanicilar.Text = "Kullanıcılar";
            tabKullanicilar.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.Controls.Add(btnKiralamaGecmisi);
            panel2.Controls.Add(txtBakiyeMiktari);
            panel2.Controls.Add(btnKullaniciSil);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(btnBakiyeEkle);
            panel2.Location = new Point(723, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(224, 155);
            panel2.TabIndex = 1;
            // 
            // btnKiralamaGecmisi
            // 
            btnKiralamaGecmisi.Location = new Point(110, 99);
            btnKiralamaGecmisi.Name = "btnKiralamaGecmisi";
            btnKiralamaGecmisi.Size = new Size(109, 23);
            btnKiralamaGecmisi.TabIndex = 4;
            btnKiralamaGecmisi.Text = "Kiralama Geçmişi";
            btnKiralamaGecmisi.UseVisualStyleBackColor = true;
            // 
            // txtBakiyeMiktari
            // 
            txtBakiyeMiktari.Location = new Point(110, 14);
            txtBakiyeMiktari.Name = "txtBakiyeMiktari";
            txtBakiyeMiktari.Size = new Size(100, 23);
            txtBakiyeMiktari.TabIndex = 2;
            // 
            // btnKullaniciSil
            // 
            btnKullaniciSil.Location = new Point(12, 99);
            btnKullaniciSil.Name = "btnKullaniciSil";
            btnKullaniciSil.Size = new Size(92, 23);
            btnKullaniciSil.TabIndex = 3;
            btnKullaniciSil.Text = "Kullanıcı Sil";
            btnKullaniciSil.UseVisualStyleBackColor = true;
            btnKullaniciSil.Click += btnKullaniciSil_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 17);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 3;
            label5.Text = "Bakiye Miktarı";
            // 
            // btnBakiyeEkle
            // 
            btnBakiyeEkle.Location = new Point(110, 43);
            btnBakiyeEkle.Name = "btnBakiyeEkle";
            btnBakiyeEkle.Size = new Size(100, 23);
            btnBakiyeEkle.TabIndex = 2;
            btnBakiyeEkle.Text = "Bakiye Ekle";
            btnBakiyeEkle.UseVisualStyleBackColor = true;
            btnBakiyeEkle.Click += btnBakiyeEkle_Click;
            // 
            // dgvKullanicilar
            // 
            dgvKullanicilar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKullanicilar.Dock = DockStyle.Left;
            dgvKullanicilar.Location = new Point(3, 3);
            dgvKullanicilar.Name = "dgvKullanicilar";
            dgvKullanicilar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKullanicilar.Size = new Size(714, 490);
            dgvKullanicilar.TabIndex = 0;
            // 
            // tabKiralamalar
            // 
            tabKiralamalar.Controls.Add(panel3);
            tabKiralamalar.Controls.Add(dgvKiralamalar);
            tabKiralamalar.Location = new Point(4, 24);
            tabKiralamalar.Name = "tabKiralamalar";
            tabKiralamalar.Size = new Size(955, 496);
            tabKiralamalar.TabIndex = 3;
            tabKiralamalar.Text = "Kiralamalar";
            tabKiralamalar.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panel3.Controls.Add(btnKiralamaSonlandir);
            panel3.Location = new Point(8, 362);
            panel3.Name = "panel3";
            panel3.Size = new Size(198, 57);
            panel3.TabIndex = 1;
            // 
            // btnKiralamaSonlandir
            // 
            btnKiralamaSonlandir.Location = new Point(3, 15);
            btnKiralamaSonlandir.Name = "btnKiralamaSonlandir";
            btnKiralamaSonlandir.Size = new Size(195, 30);
            btnKiralamaSonlandir.TabIndex = 0;
            btnKiralamaSonlandir.Text = "Kiralamaları Sonlandır";
            btnKiralamaSonlandir.UseVisualStyleBackColor = true;
            btnKiralamaSonlandir.Click += btnKiralamaSonlandir_Click;
            // 
            // dgvKiralamalar
            // 
            dgvKiralamalar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKiralamalar.Dock = DockStyle.Fill;
            dgvKiralamalar.Location = new Point(0, 0);
            dgvKiralamalar.Name = "dgvKiralamalar";
            dgvKiralamalar.Size = new Size(955, 496);
            dgvKiralamalar.TabIndex = 0;
            // 
            // tabIstatistikler
            // 
            tabIstatistikler.Controls.Add(pnlKiralanmis);
            tabIstatistikler.Controls.Add(pnlMusait);
            tabIstatistikler.Controls.Add(pnlToplamKazanc);
            tabIstatistikler.Controls.Add(pnlAktifKiralama);
            tabIstatistikler.Controls.Add(pnlToplamScooter);
            tabIstatistikler.Controls.Add(pnlToplamKullanici);
            tabIstatistikler.Location = new Point(4, 24);
            tabIstatistikler.Name = "tabIstatistikler";
            tabIstatistikler.Size = new Size(955, 496);
            tabIstatistikler.TabIndex = 4;
            tabIstatistikler.Text = "İstatistikler";
            tabIstatistikler.UseVisualStyleBackColor = true;
            // 
            // pnlKiralanmis
            // 
            pnlKiralanmis.Location = new Point(791, 12);
            pnlKiralanmis.Name = "pnlKiralanmis";
            pnlKiralanmis.Size = new Size(156, 137);
            pnlKiralanmis.TabIndex = 4;
            // 
            // pnlMusait
            // 
            pnlMusait.Location = new Point(467, 12);
            pnlMusait.Name = "pnlMusait";
            pnlMusait.Size = new Size(156, 137);
            pnlMusait.TabIndex = 3;
            // 
            // pnlToplamKazanc
            // 
            pnlToplamKazanc.Location = new Point(629, 12);
            pnlToplamKazanc.Name = "pnlToplamKazanc";
            pnlToplamKazanc.Size = new Size(156, 137);
            pnlToplamKazanc.TabIndex = 1;
            // 
            // pnlAktifKiralama
            // 
            pnlAktifKiralama.Location = new Point(305, 12);
            pnlAktifKiralama.Name = "pnlAktifKiralama";
            pnlAktifKiralama.Size = new Size(156, 137);
            pnlAktifKiralama.TabIndex = 2;
            // 
            // pnlToplamScooter
            // 
            pnlToplamScooter.Location = new Point(143, 12);
            pnlToplamScooter.Name = "pnlToplamScooter";
            pnlToplamScooter.Size = new Size(156, 137);
            pnlToplamScooter.TabIndex = 1;
            // 
            // pnlToplamKullanici
            // 
            pnlToplamKullanici.Location = new Point(3, 12);
            pnlToplamKullanici.Name = "pnlToplamKullanici";
            pnlToplamKullanici.Size = new Size(134, 137);
            pnlToplamKullanici.TabIndex = 0;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(963, 524);
            Controls.Add(tabControl1);
            Name = "AdminForm";
            Text = "AdminForm";
            tabControl1.ResumeLayout(false);
            tabHarita.ResumeLayout(false);
            tabScooterlar.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScooterlar).EndInit();
            tabKullanicilar.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).EndInit();
            tabKiralamalar.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKiralamalar).EndInit();
            tabIstatistikler.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabHarita;
        private DataGridView dgvScooterlar;
        private TabPage tabKullanicilar;
        private TabPage tabKiralamalar;
        private TabPage tabIstatistikler;
        private Button btnQRGoster;
        private Button btnScooterSil;
        private Button btnScooterEkle;
        private Panel panel1;
        private Label label4;
        private TextBox txtScooterBattery;
        private Label label3;
        private TextBox txtScooterLng;
        private Label label2;
        private TextBox txtScooterLat;
        private Label label1;
        private TextBox txtScooterName;
        private Panel panel2;
        private DataGridView dgvKullanicilar;
        private Button btnKiralamaGecmisi;
        private TextBox txtBakiyeMiktari;
        private Button btnKullaniciSil;
        private Label label5;
        private Button btnBakiyeEkle;
        private Panel panel3;
        private DataGridView dgvKiralamalar;
        private Button btnKiralamaSonlandir;
        private Panel pnlKiralanmis;
        private Button btnKonumSec;
        private TabPage tabScooterlar;
        private Panel pnlMusait;
        private Panel pnlToplamKazanc;
        private Panel pnlAktifKiralama;
        private Panel pnlToplamScooter;
        private Panel pnlToplamKullanici;
    }
}