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
            this.dgvDataWarga = new System.Windows.Forms.DataGridView();
            this.btnTampilkanData = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPeran = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataWarga)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(190, 74);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(267, 26);
            this.txtNIK.TabIndex = 0;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(190, 132);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(267, 26);
            this.txtNama.TabIndex = 1;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(190, 191);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(267, 71);
            this.txtAlamat.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "NIK:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nama:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Alamat:";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(69, 380);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(118, 38);
            this.btnSimpan.TabIndex = 6;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // dgvDataWarga
            // 
            this.dgvDataWarga.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataWarga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDataWarga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataWarga.Location = new System.Drawing.Point(631, 178);
            this.dgvDataWarga.Name = "dgvDataWarga";
            this.dgvDataWarga.RowHeadersWidth = 62;
            this.dgvDataWarga.RowTemplate.Height = 28;
            this.dgvDataWarga.Size = new System.Drawing.Size(588, 368);
            this.dgvDataWarga.TabIndex = 7;
            this.dgvDataWarga.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataWarga_CellClick);
            // 
            // btnTampilkanData
            // 
            this.btnTampilkanData.Location = new System.Drawing.Point(654, 563);
            this.btnTampilkanData.Name = "btnTampilkanData";
            this.btnTampilkanData.Size = new System.Drawing.Size(188, 39);
            this.btnTampilkanData.TabIndex = 8;
            this.btnTampilkanData.Text = "Tampilkan Data";
            this.btnTampilkanData.Click += new System.EventHandler(this.btnTampilkanData_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 299);
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
            this.cbPeran.Location = new System.Drawing.Point(190, 291);
            this.cbPeran.Name = "cbPeran";
            this.cbPeran.Size = new System.Drawing.Size(267, 28);
            this.cbPeran.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCari);
            this.groupBox1.Controls.Add(this.txtCari);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(631, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 86);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pencarian";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(473, 33);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(85, 39);
            this.btnCari.TabIndex = 2;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(150, 39);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(301, 26);
            this.txtCari.TabIndex = 1;
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
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(221, 380);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(117, 38);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(368, 380);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(106, 38);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNama);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNIK);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.txtAlamat);
            this.groupBox2.Controls.Add(this.cbPeran);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSimpan);
            this.groupBox2.Location = new System.Drawing.Point(40, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 470);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 655);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTampilkanData);
            this.Controls.Add(this.dgvDataWarga);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataWarga)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DataGridView dgvDataWarga;
        private System.Windows.Forms.Button btnTampilkanData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPeran;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

