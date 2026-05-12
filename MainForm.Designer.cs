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
            tabControl1 = new TabControl();
            tabHarita = new TabPage();
            tabProfil = new TabPage();
            tabGecmis = new TabPage();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabHarita);
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
            tabHarita.Location = new Point(4, 24);
            tabHarita.Name = "tabHarita";
            tabHarita.Padding = new Padding(3);
            tabHarita.Size = new Size(792, 422);
            tabHarita.TabIndex = 0;
            tabHarita.Text = "Harita";
            tabHarita.UseVisualStyleBackColor = true;
            // 
            // tabProfil
            // 
            tabProfil.Location = new Point(4, 24);
            tabProfil.Name = "tabProfil";
            tabProfil.Padding = new Padding(3);
            tabProfil.Size = new Size(792, 422);
            tabProfil.TabIndex = 1;
            tabProfil.Text = "Profil";
            tabProfil.UseVisualStyleBackColor = true;
            // 
            // tabGecmis
            // 
            tabGecmis.Location = new Point(4, 24);
            tabGecmis.Name = "tabGecmis";
            tabGecmis.Padding = new Padding(3);
            tabGecmis.Size = new Size(792, 422);
            tabGecmis.TabIndex = 2;
            tabGecmis.Text = "Profil Geçmişi";
            tabGecmis.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "MainForm";
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabHarita;
        private TabPage tabProfil;
        private TabPage tabGecmis;
    }
}