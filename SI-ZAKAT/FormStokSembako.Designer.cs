namespace SI_ZAKAT
{
    partial class FormStokSembako
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
            this.txtIDBarang = new System.Windows.Forms.TextBox();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.txtStok = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.dgvStok = new System.Windows.Forms.DataGridView();
            this.btnTampilkan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStok)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Barang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nama Barang";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stok";
            // 
            // txtIDBarang
            // 
            this.txtIDBarang.Location = new System.Drawing.Point(328, 44);
            this.txtIDBarang.Name = "txtIDBarang";
            this.txtIDBarang.Size = new System.Drawing.Size(250, 26);
            this.txtIDBarang.TabIndex = 3;
            // 
            // txtNamaBarang
            // 
            this.txtNamaBarang.Location = new System.Drawing.Point(328, 94);
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.Size = new System.Drawing.Size(250, 26);
            this.txtNamaBarang.TabIndex = 4;
            // 
            // txtStok
            // 
            this.txtStok.Location = new System.Drawing.Point(328, 155);
            this.txtStok.Name = "txtStok";
            this.txtStok.Size = new System.Drawing.Size(250, 26);
            this.txtStok.TabIndex = 5;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(88, 226);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(119, 34);
            this.btnSimpan.TabIndex = 6;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(228, 226);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(109, 34);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(363, 226);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(125, 34);
            this.btnHapus.TabIndex = 8;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // dgvStok
            // 
            this.dgvStok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStok.Location = new System.Drawing.Point(79, 276);
            this.dgvStok.Name = "dgvStok";
            this.dgvStok.RowHeadersWidth = 62;
            this.dgvStok.RowTemplate.Height = 28;
            this.dgvStok.Size = new System.Drawing.Size(641, 150);
            this.dgvStok.TabIndex = 9;
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.Location = new System.Drawing.Point(546, 226);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(174, 34);
            this.btnTampilkan.TabIndex = 10;
            this.btnTampilkan.Text = "Tampilkan";
            this.btnTampilkan.UseVisualStyleBackColor = true;
            // 
            // FormStokSembako
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTampilkan);
            this.Controls.Add(this.dgvStok);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.txtStok);
            this.Controls.Add(this.txtNamaBarang);
            this.Controls.Add(this.txtIDBarang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormStokSembako";
            this.Text = "FormStokSembako";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIDBarang;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.TextBox txtStok;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.DataGridView dgvStok;
        private System.Windows.Forms.Button btnTampilkan;
    }
}