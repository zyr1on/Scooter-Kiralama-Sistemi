namespace Scooter_Kiralama_Sistemi
{
    partial class BakiyeYüklemeFormu
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
            txtKartNo = new MaskedTextBox();
            label1 = new Label();
            txtTarih = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            txtCvv = new Label();
            cmbTutar = new ComboBox();
            label3 = new Label();
            btnYukle = new Button();
            SuspendLayout();
            // 
            // txtKartNo
            // 
            txtKartNo.Location = new Point(264, 108);
            txtKartNo.Name = "txtKartNo";
            txtKartNo.Size = new Size(288, 23);
            txtKartNo.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(101, 110);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 1;
            label1.Text = "Kart Numarası";
            // 
            // txtTarih
            // 
            txtTarih.Location = new Point(264, 149);
            txtTarih.Name = "txtTarih";
            txtTarih.Size = new Size(100, 23);
            txtTarih.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.Location = new Point(101, 151);
            label2.Name = "label2";
            label2.Size = new Size(147, 21);
            label2.TabIndex = 3;
            label2.Text = "Son Kullanma Tarihi";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(264, 200);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            // 
            // txtCvv
            // 
            txtCvv.AutoSize = true;
            txtCvv.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtCvv.Location = new Point(101, 202);
            txtCvv.Name = "txtCvv";
            txtCvv.Size = new Size(40, 21);
            txtCvv.TabIndex = 5;
            txtCvv.Text = "CVV";
            // 
            // cmbTutar
            // 
            cmbTutar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTutar.FormattingEnabled = true;
            cmbTutar.Location = new Point(264, 263);
            cmbTutar.Name = "cmbTutar";
            cmbTutar.Size = new Size(121, 23);
            cmbTutar.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label3.Location = new Point(101, 265);
            label3.Name = "label3";
            label3.Size = new Size(46, 21);
            label3.TabIndex = 7;
            label3.Text = "Tutar";
            // 
            // btnYukle
            // 
            btnYukle.Anchor = AnchorStyles.None;
            btnYukle.BackColor = Color.FromArgb(96, 165, 250);
            btnYukle.FlatAppearance.BorderSize = 0;
            btnYukle.FlatStyle = FlatStyle.Flat;
            btnYukle.Location = new Point(183, 333);
            btnYukle.Name = "btnYukle";
            btnYukle.Size = new Size(369, 33);
            btnYukle.TabIndex = 11;
            btnYukle.Text = "Bakiye Yükle";
            btnYukle.UseVisualStyleBackColor = false;
            btnYukle.Click += btnYukle_Click;
            // 
            // BakiyeYüklemeFormu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnYukle);
            Controls.Add(label3);
            Controls.Add(cmbTutar);
            Controls.Add(txtCvv);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(txtTarih);
            Controls.Add(label1);
            Controls.Add(txtKartNo);
            Name = "BakiyeYüklemeFormu";
            Text = "BakiyeYüklemeFormu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox txtKartNo;
        private Label label1;
        private TextBox txtTarih;
        private Label label2;
        private TextBox textBox1;
        private Label txtCvv;
        private ComboBox cmbTutar;
        private Label label3;
        private Button btnYukle;
    }
}