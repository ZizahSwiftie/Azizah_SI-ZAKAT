using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions; // WAJIB: Untuk validasi format angka NIK
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDataWarga : Form
    {
        // String koneksi resmi pangkalan data SI-ZAKAT milikmu
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDataWarga()
        {
            InitializeComponent();
        }

        // =========================================================================
        // 1. FUNGSI READ (MENAMPILKAN DATA) - Menggunakan SqlDataReader (Connected Mode)
        // =========================================================================
        BindingSource bsWarga = new BindingSource();

        public void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // POIN 2 UCP 2: Membaca data dari objek VIEW (vw_DaftarWarga), bukan tabel langsung
                    string query = "SELECT * FROM vw_DaftarWarga";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // POIN 4 UCP 2: Manfaatkan Binding dalam DataGridView
                            bsWarga.DataSource = dt;
                            dgvDataWarga.DataSource = bsWarga;

                            // POIN 5 UCP 2: Hubungkan Binding Navigator dengan BindingSource
                            bindingNavigator1.BindingSource = bsWarga;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data dengan sistem binding: " + ex.Message, "Error QA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 2. TOMBOL SIMPAN (CREATE) - DENGAN STANDAR VALIDASI QA YANG KETAT
        // =========================================================================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // --- VALIDASI QA LAYER 1: Cek Kolom Kosong ---
            if (string.IsNullOrWhiteSpace(txtNIK.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Aturan QA: Kolom NIK, Nama, dan Alamat wajib diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses, jangan tembak ke SQL Server
            }

            // --- VALIDASI QA LAYER 2: Proteksi NIK Harus Hanya Angka & Tepat 16 Digit ---
            // @"^\d{16}$" artinya: teks harus berisi digit angka saja dari awal sampai akhir sepanjang 16 karakter
            if (!Regex.IsMatch(txtNIK.Text.Trim(), @"^\d{16}$"))
            {
                MessageBox.Show("Input NIK tidak valid! Masukan harus berupa angka saja dan tepat berjumlah 16 digit.",
                                "Validasi Format Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIK.Focus();
                return;
            }

            // --- VALIDASI QA LAYER 3: Cek Pilihan Peran Warga ---
            if (cbPeran.SelectedItem == null || cbPeran.SelectedIndex == -1)
            {
                MessageBox.Show("Aturan QA: Silakan pilih peran warga terlebih dahulu (Muzakki/Mustahik)!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- JIKA VALIDASI DATA LOLOS, EKSEKUSI ADO.NET PARAMETERIZED QUERY ---
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // UBAH: Panggil nama Stored Procedure, bukan query INSERT manual
                    using (SqlCommand cmd = new SqlCommand("sp_InsertWarga", conn))
                    {
                        // WAJIB: Beritahu SqlCommand bahwa kita menggunakan Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameter tetap sama seperti UCP 1
                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                        cmd.Parameters.Add("@Nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim();
                        cmd.Parameters.Add("@Alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim();
                        cmd.Parameters.Add("@Peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();

                        // Sesuai Modul 4, ExecuteNonQuery mengembalikan jumlah baris yang terpengaruh (Integer)
                        int rowsAffected = cmd.ExecuteNonQuery(); 

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data warga berhasil divalidasi dan disimpan!", "Sukses QA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            TampilkanData(); // Refresh DataGridView
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Menangkap error kode 2627 (Pelanggaran Duplicate Primary Key di SQL Server)
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Gagal menyimpan data! NIK tersebut sudah terdaftar di database desa.", "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // --- VALIDASI QA: Proteksi Kolom Alamat ---
                // 1. Cek apakah alamat hanya diisi spasi doang
                if (string.IsNullOrWhiteSpace(txtAlamat.Text))
                {
                    MessageBox.Show("Kolom Alamat wajib diisi dengan jelas!",
                                    "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlamat.Focus();
                    return;
                }

                // 2. Cek batas panjang karakter agar tidak melebihi kapasitas database (VarChar 200)
                if (txtAlamat.Text.Length > 200)
                {
                    MessageBox.Show("Aturan STQA: Alamat terlalu panjang! Maksimal pengisian adalah 200 karakter.",
                                    "Validasi Batas Karakter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlamat.Focus();
                    return;
                }
            }
        }

        // =========================================================================
        // 3. TOMBOL UPDATE (UPDATE) - DENGAN KEAMANAN PARAMETERIZED QUERY
        // =========================================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIK.Text)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // UBAH: Panggil Stored Procedure Update
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateWarga", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                        cmd.Parameters.Add("@Nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim();
                        cmd.Parameters.Add("@Alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim();
                        cmd.Parameters.Add("@Peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data warga berhasil diperbarui via Stored Procedure!", "Sukses UCP 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        TampilkanData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 4. TOMBOL DELETE (DELETE)
        // =========================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIK.Text)) return;

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data warga dengan NIK ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // UBAH: Panggil Stored Procedure Delete
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteWarga", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data warga berhasil dihapus via Stored Procedure!", "Sukses UCP 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            TampilkanData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =========================================================================
        // 5. FITUR SMART SEARCH - AMAN DARI SIMULASI SQL INJECTION
        // =========================================================================
        private void btnCari_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // POIN 1 UCP 2: Memanggil Stored Procedure untuk fitur Search
                    using (SqlCommand cmd = new SqlCommand("sp_SearchWarga", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Masukkan parameter keyword pencarian
                        cmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 100).Value = txtCari.Text.Trim();

                        DataTable dt = new DataTable();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }

                        // Ikat hasil pencarian ke BindingSource agar DataGridView ter-refresh otomatis
                        bsWarga.DataSource = dt;
                        dgvDataWarga.DataSource = bsWarga;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Data warga tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TampilkanData(); // Kembalikan ke data awal jika tidak ketemu
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan pencarian: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDataWarga_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDataWarga.Rows[e.RowIndex];
                txtNIK.Text = row.Cells["NIK"].Value.ToString();
                txtNama.Text = row.Cells["nama"].Value.ToString();
                txtAlamat.Text = row.Cells["alamat"].Value.ToString();
                cbPeran.SelectedItem = row.Cells["peran"].Value.ToString();
            }
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        // Helper untuk mereset form isian
        private void ClearForm()
        {
            txtNIK.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
            cbPeran.SelectedIndex = -1;
            txtNIK.Focus();
        }

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cek jika karakter yang ditekan bukan angka, dan bukan tombol Backspace (untuk menghapus)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tolak mentah-mentah karakter tersebut agar tidak muncul di TextBox
            }
        }
    }
}