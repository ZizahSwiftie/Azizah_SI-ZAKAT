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
        public void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // [cite: 67]

                    // Mengambil data dari database menggunakan objek SqlCommand [cite: 12, 68]
                    string query = "SELECT NIK, nama, alamat, peran FROM Tabel_Warga"; // [cite: 69]

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Sesuai Modul 6, menggunakan ExecuteReader karena mengambil banyak data
                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader); // Membaca stream data baris demi baris ke DataTable
                            dgvDataWarga.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data warga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilkanData(); // Otomatis muat data saat aplikasi pertama kali dibuka
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
                MessageBox.Show("Aturan QA: Input NIK tidak valid! Masukan harus berupa angka saja dan tepat berjumlah 16 digit.",
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
                    conn.Open(); // [cite: 266]

                    // Menggunakan Parameterized Query untuk mencegah ancaman SQL Injection sesuai Modul 9 [cite: 185, 515, 629]
                    string sql = "INSERT INTO Tabel_Warga (NIK, nama, alamat, peran) VALUES (@nik, @nama, @alamat, @peran)"; // [cite: 636]

                    using (SqlCommand cmd = new SqlCommand(sql, conn)) // [cite: 639]
                    {
                        // Mendefinisikan tipe data parameter secara eksplisit demi integritas database
                        cmd.Parameters.Add("@nik", SqlDbType.Char, 16).Value = txtNIK.Text.Trim(); // [cite: 641]
                        cmd.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim(); // [cite: 642]
                        cmd.Parameters.Add("@alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim(); // [cite: 645]
                        cmd.Parameters.Add("@peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();

                        // Sesuai Modul 4, ExecuteNonQuery mengembalikan jumlah baris yang terpengaruh (Integer) [cite: 105, 206]
                        int rowsAffected = cmd.ExecuteNonQuery(); // [cite: 648]

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
                    MessageBox.Show("Aturan STQA: Kolom Alamat wajib diisi dengan jelas dan tidak boleh hanya berisi spasi!",
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
                    string sql = "UPDATE Tabel_Warga SET nama=@nama, alamat=@alamat, peran=@peran WHERE NIK=@nik";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@nik", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                        cmd.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim();
                        cmd.Parameters.Add("@alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim();
                        cmd.Parameters.Add("@peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();

                        cmd.ExecuteNonQuery(); // Menjalankan query pembaruan data manipulasi [cite: 106, 209]

                        MessageBox.Show("Data warga berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        string sql = "DELETE FROM Tabel_Warga WHERE NIK=@nik";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@nik", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                            cmd.ExecuteNonQuery(); // Menjalankan query penghapusan [cite: 106, 223]

                            MessageBox.Show("Data warga berhasil dihapus dari sistem!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    // AMAN: Menggunakan klausa parameter Like terstruktur, menutup celah bypass 'OR 1=1' 
                    string query = "SELECT NIK, nama, alamat, peran FROM Tabel_Warga WHERE nama LIKE @cari OR NIK LIKE @cari";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@cari", SqlDbType.VarChar, 100).Value = "%" + txtCari.Text.Trim() + "%";

                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader()); // Membaca data pencarian via DataReader [cite: 12]
                        dgvDataWarga.DataSource = dt;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Data warga dengan nama atau NIK tersebut tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TampilkanData(); // Tampilkan ulang data awal jika pencarian nihil
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