namespace SI_ZAKAT
{
    partial class FormDonasi
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
            this.cbIDDonatur = new System.Windows.Forms.ComboBox();
            this.cbKategori = new System.Windows.Forms.ComboBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbIDDonatur
            // 
            this.cbIDDonatur.FormattingEnabled = true;
            this.cbIDDonatur.Location = new System.Drawing.Point(325, 57);
            this.cbIDDonatur.Name = "cbIDDonatur";
            this.cbIDDonatur.Size = new System.Drawing.Size(185, 28);
            this.cbIDDonatur.TabIndex = 0;
            // 
            // cbKategori
            // 
            this.cbKategori.FormattingEnabled = true;
            this.cbKategori.Location = new System.Drawing.Point(325, 114);
            this.cbKategori.Name = "cbKategori";
            this.cbKategori.Size = new System.Drawing.Size(185, 28);
            this.cbKategori.TabIndex = 1;
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(325, 168);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(185, 26);
            this.txtJumlah.TabIndex = 2;
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.Location = new System.Drawing.Point(325, 225);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(200, 26);
            this.dtpTanggal.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(252, 327);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(201, 42);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit Donasi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "ID Donatur";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kategori";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Jumlah (Rp)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tanggal";
            // 
            // FormDonasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.cbKategori);
            this.Controls.Add(this.cbIDDonatur);
            this.Name = "FormDonasi";
            this.Text = "FormDonasi";
            this.Click += new System.EventHandler(this.FormDonasi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIDDonatur;
        private System.Windows.Forms.ComboBox cbKategori;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}