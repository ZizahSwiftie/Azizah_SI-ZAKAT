namespace SI_ZAKAT
{
    partial class FormPenyaluran
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSalur = new System.Windows.Forms.Button();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.cbBarang = new System.Windows.Forms.ComboBox();
            this.cbMustahik = new System.Windows.Forms.ComboBox();
            this.lblStokBeras = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Mustahik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Barang";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Jumlah Keluar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 4;
            // 
            // btnSalur
            // 
            this.btnSalur.Location = new System.Drawing.Point(282, 290);
            this.btnSalur.Name = "btnSalur";
            this.btnSalur.Size = new System.Drawing.Size(236, 41);
            this.btnSalur.TabIndex = 5;
            this.btnSalur.Text = "Salurkan Bantuan";
            this.btnSalur.UseVisualStyleBackColor = true;
            this.btnSalur.Click += new System.EventHandler(this.btnSalurkan_Click);
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(317, 227);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(264, 26);
            this.txtJumlah.TabIndex = 7;
            this.txtJumlah.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJumlahKeluar_KeyPress);
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.Location = new System.Drawing.Point(317, 123);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(264, 26);
            this.dtpTanggal.TabIndex = 8;
            // 
            // cbBarang
            // 
            this.cbBarang.FormattingEnabled = true;
            this.cbBarang.Location = new System.Drawing.Point(317, 178);
            this.cbBarang.Name = "cbBarang";
            this.cbBarang.Size = new System.Drawing.Size(264, 28);
            this.cbBarang.TabIndex = 9;
            this.cbBarang.SelectedIndexChanged += new System.EventHandler(this.cbBarang_SelectedIndexChanged);
            // 
            // cbMustahik
            // 
            this.cbMustahik.FormattingEnabled = true;
            this.cbMustahik.Location = new System.Drawing.Point(317, 68);
            this.cbMustahik.Name = "cbMustahik";
            this.cbMustahik.Size = new System.Drawing.Size(264, 28);
            this.cbMustahik.TabIndex = 10;
            // 
            // lblStokBeras
            // 
            this.lblStokBeras.AutoSize = true;
            this.lblStokBeras.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStokBeras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStokBeras.Location = new System.Drawing.Point(277, 365);
            this.lblStokBeras.Name = "lblStokBeras";
            this.lblStokBeras.Size = new System.Drawing.Size(213, 25);
            this.lblStokBeras.TabIndex = 11;
            this.lblStokBeras.Text = "Hitung Stok Terbaru:";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Info;
            this.btnBack.Location = new System.Drawing.Point(34, 28);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(112, 39);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormPenyaluran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblStokBeras);
            this.Controls.Add(this.cbMustahik);
            this.Controls.Add(this.cbBarang);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.btnSalur);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPenyaluran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPenyaluran";
            this.Load += new System.EventHandler(this.FormPenyaluran_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSalur;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.ComboBox cbBarang;
        private System.Windows.Forms.ComboBox cbMustahik;
        private System.Windows.Forms.Label lblStokBeras;
        private System.Windows.Forms.Button btnBack;
    }
}