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

        // --- STQA FEATURE: Load Referensi Data Dinamis ke Dropdown Menghindari Blind-Input ---
        private void FormPenyaluran_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Ambil data NIK warga yang perannya Mustahik untuk dropdown ID Mustahik
                    string queryWarga = "SELECT NIK FROM Tabel_Warga WHERE peran = 'Mustahik'";
                    using (SqlCommand cmd = new SqlCommand(queryWarga, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        cbMustahik.Items.Clear(); // Bersihkan data sampah terdahulu
                        while (reader.Read())
                        {
                            cbMustahik.Items.Add(reader["NIK"].ToString());
                        }
                        reader.Close();
                    }

                    // 2. Ambil referensi nama logistik dari gudang sembako untuk dropdown Barang
                    string queryBarang = "SELECT nama_barang FROM Tabel_Stok";
                    using (SqlCommand cmd = new SqlCommand(queryBarang, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        cbBarang.Items.Clear();
                        while (reader.Read())
                        {
                            cbBarang.Items.Add(reader["nama_barang"].ToString());
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sistem STQA Warning: Gagal memuat data dropdown! Hubungi IT Support.\nDetail: " + ex.Message,
                                    "Error Pemuatan Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- STQA SECURITY LAYER: Validasi Tombol Eksekusi Penyaluran Logistik ---
        private void btnSalurkan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Kelayakan Input (Mencegah NullReferenceException)
            if (cbMustahik.SelectedItem == null || cbBarang.SelectedItem == null || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Aturan STQA: Pilihan Mustahik, Barang, dan Jumlah Keluar tidak boleh dikosongkan!",
                                "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi Logika Bisnis (Angka input harus logis dan bernilai positif)
            int jumlahKeluar = Convert.ToInt32(txtJumlah.Text.Trim());
            if (jumlahKeluar <= 0)
            {
                MessageBox.Show("Aturan STQA: Jumlah barang yang disalurkan minimal harus 1 atau lebih!",
                                "Validasi Angka Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus();
                return;
            }

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
                        cmd.Parameters.Add("@NIK_Mustahik", SqlDbType.Char, 16).Value = cbMustahik.SelectedItem.ToString();
                        cmd.Parameters.Add("@Tanggal", SqlDbType.Date).Value = dtpTanggal.Value.Date;
                        cmd.Parameters.Add("@NamaBarang", SqlDbType.VarChar, 100).Value = cbBarang.SelectedItem.ToString();
                        cmd.Parameters.Add("@JumlahKeluar", SqlDbType.Int).Value = jumlahKeluar;

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Transaksi Berhasil!\nLogistik sukses disalurkan dan stok logistik otomatis dipotong.",
                                        "Sukses STQA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
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

        // --- STQA DATA FILTER: Pengunci keyboard agar kolom teks jumlah hanya mau menerima nomor ---
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
    }
}