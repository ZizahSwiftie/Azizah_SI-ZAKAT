namespace SI_ZAKAT
{
    partial class FormDashboard
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
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnPenyaluran = new System.Windows.Forms.Button();
            this.btnStokSembako = new System.Windows.Forms.Button();
            this.btnValidasiDonasi = new System.Windows.Forms.Button();
            this.btnDonasi = new System.Windows.Forms.Button();
            this.btnDataWarga = new System.Windows.Forms.Button();
            this.panelKonten = new System.Windows.Forms.Panel();
            this.panelPieChart = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTotalSalur = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotalStokBarang = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalWarga = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalDana = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelKonten.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.pictureBox1);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnPenyaluran);
            this.panelSidebar.Controls.Add(this.btnStokSembako);
            this.panelSidebar.Controls.Add(this.btnValidasiDonasi);
            this.panelSidebar.Controls.Add(this.btnDonasi);
            this.panelSidebar.Controls.Add(this.btnDataWarga);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(163, 454);
            this.panelSidebar.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SI_ZAKAT.Properties.Resources.logo_si_zakat;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogout.Location = new System.Drawing.Point(0, 390);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(163, 64);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnPenyaluran
            // 
            this.btnPenyaluran.Location = new System.Drawing.Point(0, 329);
            this.btnPenyaluran.Name = "btnPenyaluran";
            this.btnPenyaluran.Size = new System.Drawing.Size(163, 64);
            this.btnPenyaluran.TabIndex = 4;
            this.btnPenyaluran.Text = "Penyaluran";
            this.btnPenyaluran.UseVisualStyleBackColor = true;
            this.btnPenyaluran.Click += new System.EventHandler(this.btnPenyaluran_Click);
            // 
            // btnStokSembako
            // 
            this.btnStokSembako.Location = new System.Drawing.Point(0, 268);
            this.btnStokSembako.Name = "btnStokSembako";
            this.btnStokSembako.Size = new System.Drawing.Size(163, 64);
            this.btnStokSembako.TabIndex = 3;
            this.btnStokSembako.Text = "Stok Sembako";
            this.btnStokSembako.UseVisualStyleBackColor = true;
            this.btnStokSembako.Click += new System.EventHandler(this.btnStokSembako_Click);
            // 
            // btnValidasiDonasi
            // 
            this.btnValidasiDonasi.Location = new System.Drawing.Point(0, 206);
            this.btnValidasiDonasi.Name = "btnValidasiDonasi";
            this.btnValidasiDonasi.Size = new System.Drawing.Size(163, 64);
            this.btnValidasiDonasi.TabIndex = 2;
            this.btnValidasiDonasi.Text = "Validasi Donasi";
            this.btnValidasiDonasi.UseVisualStyleBackColor = true;
            this.btnValidasiDonasi.Click += new System.EventHandler(this.btnValidasiDonasi_Click);
            // 
            // btnDonasi
            // 
            this.btnDonasi.Location = new System.Drawing.Point(0, 144);
            this.btnDonasi.Name = "btnDonasi";
            this.btnDonasi.Size = new System.Drawing.Size(163, 64);
            this.btnDonasi.TabIndex = 1;
            this.btnDonasi.Text = "Donasi";
            this.btnDonasi.UseVisualStyleBackColor = true;
            this.btnDonasi.Click += new System.EventHandler(this.btnDonasi_Click);
            // 
            // btnDataWarga
            // 
            this.btnDataWarga.Location = new System.Drawing.Point(0, 82);
            this.btnDataWarga.Name = "btnDataWarga";
            this.btnDataWarga.Size = new System.Drawing.Size(163, 64);
            this.btnDataWarga.TabIndex = 0;
            this.btnDataWarga.Text = "Data Warga";
            this.btnDataWarga.UseVisualStyleBackColor = true;
            this.btnDataWarga.Click += new System.EventHandler(this.btnDataWarga_Click);
            // 
            // panelKonten
            // 
            this.panelKonten.Controls.Add(this.panelPieChart);
            this.panelKonten.Controls.Add(this.groupBox4);
            this.panelKonten.Controls.Add(this.groupBox3);
            this.panelKonten.Controls.Add(this.groupBox2);
            this.panelKonten.Controls.Add(this.groupBox1);
            this.panelKonten.Controls.Add(this.btnDashboard);
            this.panelKonten.Controls.Add(this.panelHeader);
            this.panelKonten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKonten.Location = new System.Drawing.Point(163, 0);
            this.panelKonten.Name = "panelKonten";
            this.panelKonten.Size = new System.Drawing.Size(641, 454);
            this.panelKonten.TabIndex = 1;
            // 
            // panelPieChart
            // 
            this.panelPieChart.Location = new System.Drawing.Point(235, 271);
            this.panelPieChart.Name = "panelPieChart";
            this.panelPieChart.Size = new System.Drawing.Size(368, 145);
            this.panelPieChart.TabIndex = 6;
            this.panelPieChart.TabStop = false;
            this.panelPieChart.Text = "Donasi Berdasarkan Kategori";
            this.panelPieChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPieChart_Paint);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTotalSalur);
            this.groupBox4.Location = new System.Drawing.Point(34, 271);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(168, 145);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Total Penyaluran";
            // 
            // lblTotalSalur
            // 
            this.lblTotalSalur.AutoSize = true;
            this.lblTotalSalur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalur.Location = new System.Drawing.Point(64, 58);
            this.lblTotalSalur.Name = "lblTotalSalur";
            this.lblTotalSalur.Size = new System.Drawing.Size(24, 25);
            this.lblTotalSalur.TabIndex = 0;
            this.lblTotalSalur.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotalStokBarang);
            this.groupBox3.Location = new System.Drawing.Point(436, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 145);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Total Stok Barang (item)";
            // 
            // lblTotalStokBarang
            // 
            this.lblTotalStokBarang.AutoSize = true;
            this.lblTotalStokBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStokBarang.Location = new System.Drawing.Point(11, 54);
            this.lblTotalStokBarang.Name = "lblTotalStokBarang";
            this.lblTotalStokBarang.Size = new System.Drawing.Size(70, 25);
            this.lblTotalStokBarang.TabIndex = 0;
            this.lblTotalStokBarang.Text = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotalWarga);
            this.groupBox2.Location = new System.Drawing.Point(233, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 145);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Total Warga";
            // 
            // lblTotalWarga
            // 
            this.lblTotalWarga.AutoSize = true;
            this.lblTotalWarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalWarga.Location = new System.Drawing.Point(38, 54);
            this.lblTotalWarga.Name = "lblTotalWarga";
            this.lblTotalWarga.Size = new System.Drawing.Size(70, 25);
            this.lblTotalWarga.TabIndex = 0;
            this.lblTotalWarga.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalDana);
            this.groupBox1.Location = new System.Drawing.Point(35, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total Dana (terkumpul)";
            // 
            // lblTotalDana
            // 
            this.lblTotalDana.AutoSize = true;
            this.lblTotalDana.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDana.Location = new System.Drawing.Point(17, 54);
            this.lblTotalDana.Name = "lblTotalDana";
            this.lblTotalDana.Size = new System.Drawing.Size(70, 25);
            this.lblTotalDana.TabIndex = 0;
            this.lblTotalDana.Text = "label2";
            // 
            // btnDashboard
            // 
            this.btnDashboard.AutoSize = true;
            this.btnDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Location = new System.Drawing.Point(23, 24);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(228, 37);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "DASHBOARD";
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(641, 83);
            this.panelHeader.TabIndex = 1;
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 454);
            this.Controls.Add(this.panelKonten);
            this.Controls.Add(this.panelSidebar);
            this.Name = "FormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDashboard";
            this.panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelKonten.ResumeLayout(false);
            this.panelKonten.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnPenyaluran;
        private System.Windows.Forms.Button btnStokSembako;
        private System.Windows.Forms.Button btnValidasiDonasi;
        private System.Windows.Forms.Button btnDonasi;
        private System.Windows.Forms.Button btnDataWarga;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelKonten;
        private System.Windows.Forms.Label btnDashboard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox panelPieChart;
        private System.Windows.Forms.Label lblTotalSalur;
        private System.Windows.Forms.Label lblTotalStokBarang;
        private System.Windows.Forms.Label lblTotalWarga;
        private System.Windows.Forms.Label lblTotalDana;
    }
}