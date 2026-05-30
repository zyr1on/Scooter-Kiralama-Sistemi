namespace Scooter_Kiralama_Sistemi
{
    partial class KiralamaForm
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
            lblScooterName = new Label();
            lblStatus = new Label();
            lblBattery = new Label();
            cmbDuration = new ComboBox();
            btnRent = new Button();
            lblTotalPrice = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblScooterName
            // 
            lblScooterName.AutoSize = true;
            lblScooterName.Location = new Point(227, 85);
            lblScooterName.Name = "lblScooterName";
            lblScooterName.Size = new Size(71, 15);
            lblScooterName.TabIndex = 0;
            lblScooterName.Text = "Scooter Adı:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(227, 123);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(97, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Scooter Durumu:";
            // 
            // lblBattery
            // 
            lblBattery.AutoSize = true;
            lblBattery.Location = new Point(227, 158);
            lblBattery.Name = "lblBattery";
            lblBattery.Size = new Size(49, 15);
            lblBattery.TabIndex = 2;
            lblBattery.Text = "Batarya:";
            // 
            // cmbDuration
            // 
            cmbDuration.AutoCompleteCustomSource.AddRange(new string[] { "1", "3", "5", "7", "30" });
            cmbDuration.FormattingEnabled = true;
            cmbDuration.Items.AddRange(new object[] { "1", "3", "5", "7", "30" });
            cmbDuration.Location = new Point(227, 218);
            cmbDuration.Name = "cmbDuration";
            cmbDuration.Size = new Size(136, 23);
            cmbDuration.TabIndex = 3;
            cmbDuration.SelectedIndexChanged += cmbDuration_SelectedIndexChanged;
            // 
            // btnRent
            // 
            btnRent.Location = new Point(227, 282);
            btnRent.Name = "btnRent";
            btnRent.Size = new Size(123, 35);
            btnRent.TabIndex = 4;
            btnRent.Text = "Kirala";
            btnRent.UseVisualStyleBackColor = true;
            btnRent.Click += btnRent_Click;
            // 
            // lblTotalPrice
            // 
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Location = new Point(227, 255);
            lblTotalPrice.Name = "lblTotalPrice";
            lblTotalPrice.Size = new Size(77, 15);
            lblTotalPrice.TabIndex = 5;
            lblTotalPrice.Text = "Toplam Fiyat:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(227, 188);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 6;
            label1.Text = "Gün:";
            // 
            // KiralamaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(lblTotalPrice);
            Controls.Add(btnRent);
            Controls.Add(cmbDuration);
            Controls.Add(lblBattery);
            Controls.Add(lblStatus);
            Controls.Add(lblScooterName);
            Name = "KiralamaForm";
            Text = "KiralamaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblScooterName;
        private Label lblStatus;
        private Label lblBattery;
        private ComboBox cmbDuration;
        private Button btnRent;
        private Label lblTotalPrice;
        private Label label1;
    }
}