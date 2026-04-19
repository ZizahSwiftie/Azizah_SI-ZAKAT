namespace SI_ZAKAT
{
    partial class Form1
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
            this.txtNIK = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dgvWarga = new System.Windows.Forms.DataGridView();
            this.btnTampil = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPeran = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarga)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(208, 178);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(267, 26);
            this.txtNIK.TabIndex = 0;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(208, 233);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(267, 26);
            this.txtNama.TabIndex = 1;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(208, 286);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(267, 71);
            this.txtAlamat.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "NIK:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nama:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Alamat:";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(208, 466);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(118, 38);
            this.btnSimpan.TabIndex = 6;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // dgvWarga
            // 
            this.dgvWarga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarga.Location = new System.Drawing.Point(654, 178);
            this.dgvWarga.Name = "dgvWarga";
            this.dgvWarga.RowHeadersWidth = 62;
            this.dgvWarga.RowTemplate.Height = 28;
            this.dgvWarga.Size = new System.Drawing.Size(690, 368);
            this.dgvWarga.TabIndex = 7;
            // 
            // btnTampil
            // 
            this.btnTampil.Location = new System.Drawing.Point(654, 563);
            this.btnTampil.Name = "btnTampil";
            this.btnTampil.Size = new System.Drawing.Size(188, 39);
            this.btnTampil.TabIndex = 8;
            this.btnTampil.Text = "Tampilkan Data";
            this.btnTampil.UseVisualStyleBackColor = true;
            this.btnTampil.Click += new System.EventHandler(this.btnTampil_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 390);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Peran:";
            // 
            // cbPeran
            // 
            this.cbPeran.FormattingEnabled = true;
            this.cbPeran.Items.AddRange(new object[] {
            "Muzakki",
            "Mustahik"});
            this.cbPeran.Location = new System.Drawing.Point(208, 382);
            this.cbPeran.Name = "cbPeran";
            this.cbPeran.Size = new System.Drawing.Size(267, 28);
            this.cbPeran.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCari);
            this.groupBox1.Controls.Add(this.txtCari);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(654, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 86);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pencarian";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cari Nama/NIK";
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(150, 39);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(419, 26);
            this.txtCari.TabIndex = 1;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(587, 33);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(85, 39);
            this.btnCari.TabIndex = 2;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 655);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbPeran);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTampil);
            this.Controls.Add(this.dgvWarga);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIK);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarga)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DataGridView dgvWarga;
        private System.Windows.Forms.Button btnTampil;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPeran;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCari;
    }
}

