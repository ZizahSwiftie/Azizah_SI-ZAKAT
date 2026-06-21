using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormValidasi : Form
    {
        // Variabel global koneksi database
        private string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        // Memanfaatkan Data Binding sesuai standar modul praktikum (Connected & Disconnected Mode)
        private BindingSource bsValidasi = new BindingSource();
        private DataGridView dataGridView1;
        private Button btnApprove;
        private Button btnBatalkan;
        private Button btnBack;
        private DataTable dtValidasi = new DataTable();

        public FormValidasi()
        {
            InitializeComponent();
        }

        // --- EVENT LOAD: Berjalan otomatis saat form dibuka ---
        private void FormValidasi_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            TampilkanDonasiPending();

            // Mendaftarkan event handler tombol secara programmatika jika belum didaftarkan di Designer
            btnApprove.Click += new EventHandler(btnApprove_Click);
            btnBatalkan.Click += new EventHandler(btnBatalkan_Click);
            btnBack.Click += new EventHandler(btnBack_Click);
        }

        // --- KOORDINASI TAMPILAN: Konfigurasi Grid agar Rapi dan Sesuai Standar QA ---
        private void ConfigureDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- FUNGSI READ: Menampilkan Data Donasi yang Berstatus 'Pending' ---
        private void TampilkanDonasiPending()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Donasi, NIK_Warga AS [NIK Donatur], kategori AS [Kategori], jumlah AS [Jumlah (Rp)], tanggal AS [Tanggal], status AS [Status] FROM Tabel_Donasi WHERE status = 'Pending'";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtValidasi = new DataTable();
                        da.Fill(dtValidasi);

                        bsValidasi.DataSource = dtValidasi;
                        dataGridView1.DataSource = bsValidasi;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data antrean donasi: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- SECURITY LAYER & STORED PROCEDURE: Validasi / Approve Donasi ---
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Aturan: Silakan pilih baris donasi yang ingin divalidasi terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mengambil ID_Donasi dari baris yang dipilih
            int idDonasi = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Donasi"].Value);
            string namaKategori = dataGridView1.SelectedRows[0].Cells["Kategori"].Value.ToString();

            DialogResult result = MessageBox.Show($"Apakah Anda yakin ingin menyetujui (Approve) donasi ini?",
                "Konfirmasi Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // POIN 1 UCP 2: Memanggil Stored Procedure sp_ApproveDonasi (Anti-SQL Injection)
                        using (SqlCommand cmd = new SqlCommand("sp_ApproveDonasi", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Parameterized binding secara ketat untuk proteksi manipulasi parameter id
                            cmd.Parameters.Add("@ID_Donasi", SqlDbType.Int).Value = idDonasi;

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Sukses! Donasi berhasil divalidasi dan resmi masuk ke kas desa.", "Transaksi Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                TampilkanDonasiPending(); // Refresh Grid otomatis
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memproses validasi donasi: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- TOMBOL BATALKAN: Mereset pilihan atau menolak transaksi ---
        private void btnBatalkan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            MessageBox.Show("Transaksi dibatalkan. Status donasi tetap berada dalam antrean 'Pending'.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.ClearSelection();
        }

        // --- TOMBOL KEMBALI: Menutup Form Validasi ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Menutup form validasi anak, sehingga control kembali otomatis ke dashboard utama
            this.Close();
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnBatalkan = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(126, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(680, 216);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.ForestGreen;
            this.btnApprove.Location = new System.Drawing.Point(126, 392);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(219, 34);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Validasi (Approve)";
            this.btnApprove.UseVisualStyleBackColor = false;
            // 
            // btnBatalkan
            // 
            this.btnBatalkan.BackColor = System.Drawing.Color.Crimson;
            this.btnBatalkan.Location = new System.Drawing.Point(368, 392);
            this.btnBatalkan.Name = "btnBatalkan";
            this.btnBatalkan.Size = new System.Drawing.Size(142, 34);
            this.btnBatalkan.TabIndex = 2;
            this.btnBatalkan.Text = "Batalkan";
            this.btnBatalkan.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Info;
            this.btnBack.Location = new System.Drawing.Point(25, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(112, 39);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // FormValidasi
            // 
            this.ClientSize = new System.Drawing.Size(960, 477);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnBatalkan);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormValidasi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.Name = "FormValidasi";
            this.Load += new System.EventHandler(this.FormValidasi_Load);   // <-- TAMBAHKAN INI
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

    }
}