using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormPenyaluran : Form
    {
        // Hubungkan string koneksi dengan server database lokalmu
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormPenyaluran()
        {
            InitializeComponent();
        }

        private void FormPenyaluran_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Ambil NIK + Nama warga berperan Mustahik, tampil "NIK - Nama"
                    cbMustahik.DropDownStyle = ComboBoxStyle.DropDownList; // Hanya pilih, tidak bisa ketik
                    string queryWarga = "SELECT NIK, nama FROM Tabel_Warga WHERE peran = 'Mustahik' ORDER BY nama";
                    using (SqlCommand cmd = new SqlCommand(queryWarga, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbMustahik.Items.Clear(); // Bersihkan data sampah terdahulu
                        while (reader.Read())
                        {
                            string nik = reader["NIK"].ToString().Trim();
                            string nama = reader["nama"].ToString().Trim();
                            cbMustahik.Items.Add($"{nik} - {nama}");
                        }
                    }

                    if (cbMustahik.Items.Count == 0)
                        MessageBox.Show("Belum ada data Mustahik terdaftar.\nSilakan daftarkan warga terlebih dahulu.",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 2. Ambil referensi nama logistik dari gudang sembako untuk dropdown Barang
                    cbBarang.DropDownStyle = ComboBoxStyle.DropDownList;
                    string queryBarang = "SELECT nama_barang FROM Tabel_Stok";
                    using (SqlCommand cmd = new SqlCommand(queryBarang, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbBarang.Items.Clear();
                        while (reader.Read())
                        {
                            cbBarang.Items.Add(reader["nama_barang"].ToString());
                        }
                    }

                    HitungStokTerbaru();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sistem Warning: Gagal memuat data dropdown! Hubungi IT Support.\nDetail: " + ex.Message,
                                    "Error Pemuatan Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalurkan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Kelayakan Input (Mencegah NullReferenceException)
            if (cbMustahik.SelectedItem == null || cbBarang.SelectedItem == null || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Aturan: Pilihan Mustahik, Barang, dan Jumlah Keluar tidak boleh dikosongkan!",
                                "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi Logika Bisnis (Angka input harus logis dan bernilai positif)
            if (!int.TryParse(txtJumlah.Text.Trim(), out int jumlahKeluar))
            {
                MessageBox.Show("Aturan: Jumlah keluar harus berupa angka!",
                                "Validasi Angka Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus();
                return;
            }

            if (jumlahKeluar <= 0)
            {
                MessageBox.Show("Aturan: Jumlah barang yang disalurkan minimal harus 1 atau lebih!",
                                "Validasi Angka Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus();
                return;
            }

            // Ambil NIK saja dari pilihan "NIK - Nama"
            string pilihanMustahik = cbMustahik.SelectedItem.ToString();
            string nikMustahik = pilihanMustahik.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();

            // --- JIKA PROSES VALIDASI SELESAI & LOLOS, EKSEKUSI STORED PROCEDURE ---
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SalurkanBantuan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Mengamankan parameter data bertipe aman (Bebas dari celah SQL Injection)
                        cmd.Parameters.Add("@NIK_Mustahik", SqlDbType.Char, 16).Value = nikMustahik;
                        cmd.Parameters.Add("@Tanggal", SqlDbType.Date).Value = dtpTanggal.Value.Date;
                        cmd.Parameters.Add("@NamaBarang", SqlDbType.VarChar, 100).Value = cbBarang.SelectedItem.ToString();
                        cmd.Parameters.Add("@JumlahKeluar", SqlDbType.Int).Value = jumlahKeluar;

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Transaksi Berhasil!\nLogistik sukses disalurkan dan stok logistik otomatis dipotong.",
                                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        HitungStokTerbaru(); // Refresh label stok setelah berkurang
                    }
                }
                catch (SqlException ex)
                {
                    // Menangkap kustom proteksi RAISERROR (Stok Habis / Barang Rusak) dari database SQL Server
                    MessageBox.Show(ex.Message, "Validasi Stok Gudang", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error kegagalan sistem: " + ex.Message, "System Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HitungStokTerbaru()
        {
            // Ambil barang yang dipilih dari ComboBox (contoh: "Beras 5 Kg")
            string namaBarangDipilih = cbBarang.Text;

            if (string.IsNullOrWhiteSpace(namaBarangDipilih))
            {
                lblStokBeras.Text = "Stok saat ini : -";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT jumlah_stok FROM Tabel_Stok WHERE nama_barang = @NamaBarang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NamaBarang", namaBarangDipilih);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int stoksaatini = Convert.ToInt32(result);
                            lblStokBeras.Text = $"Stok {namaBarangDipilih} saat ini : {stoksaatini}";
                        }
                        else
                        {
                            lblStokBeras.Text = $"Stok {namaBarangDipilih} saat ini : 0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat info stok: " + ex.Message);
            }
        }

        private void cbBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            HitungStokTerbaru();
        }

        private void txtJumlahKeluar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tolak input jika user mengetik huruf abjad atau simbol
            }
        }

        private void ClearForm()
        {
            cbMustahik.SelectedIndex = -1;
            cbBarang.SelectedIndex = -1;
            txtJumlah.Clear();
            dtpTanggal.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Penanda tutup resmi untuk memicu pemanggilan dashboard kembali
            this.Close();
        }
    }
}