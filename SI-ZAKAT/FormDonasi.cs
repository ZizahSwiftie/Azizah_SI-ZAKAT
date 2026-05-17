using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDonasi : Form
    {
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDonasi()
        {
            InitializeComponent();
        }

        // --- STQA LAYER 1: Mengisi ID Donatur Otomatis dari Tabel Warga saat Form Terbuka ---
        private void FormDonasi_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Mengambil NIK warga untuk dijadikan pilihan di ComboBox ID Donatur
                    string query = "SELECT NIK, nama FROM Tabel_Warga";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Menambahkan NIK ke pilihan ComboBox
                            cbIDDonatur.Items.Add(reader["NIK"].ToString());
                        }
                    }

                    // Isi pilihan Kategori Zakat secara default
                    cbKategori.Items.Add("Zakat Fitrah");
                    cbKategori.Items.Add("Zakat Maal");
                    cbKategori.Items.Add("Infaq/Sedekah");
                    cbKategori.Items.Add("Bantuan Sembako");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data donatur: " + ex.Message, "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- STQA LAYER 2: Tombol Submit dengan Validasi Kelayakan Data ---
        private void btnSubmitDonasi_Click(object sender, EventArgs e)
        {
            // 1. Validasi ComboBox ID Donatur tidak boleh kosong
            if (cbIDDonatur.SelectedItem == null || cbIDDonatur.SelectedIndex == -1)
            {
                MessageBox.Show("Aturan STQA: Silakan pilih ID Donatur terlebih dahulu!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi ComboBox Kategori tidak boleh kosong
            if (cbKategori.SelectedItem == null || cbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Aturan STQA: Silakan pilih Kategori Donasi terlebih dahulu!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validasi Kolom Jumlah Uang tidak boleh kosong
            if (string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Aturan STQA: Kolom Jumlah Uang wajib diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus();
                return;
            }

            // 4. Validasi Batas Nominal Uang (Minimal Donasi Rp 10.000 untuk menghindari data sampah)
            int nominalDonasi = Convert.ToInt32(txtJumlah.Text.Trim());
            if (nominalDonasi < 10000)
            {
                MessageBox.Show("Aturan STQA: Minimal nominal donasi yang dapat diproses adalah Rp 10.000!", "Validasi Nominal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus();
                return;
            }

            // --- JIKA SEMUA VALIDASI AMAN, JALANKAN STORED PROCEDURE ---
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertDonasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Binding parameter dengan aman menghindari SQL Injection
                        cmd.Parameters.Add("@NIK_Warga", SqlDbType.Char, 16).Value = cbIDDonatur.SelectedItem.ToString();
                        cmd.Parameters.Add("@Kategori", SqlDbType.VarChar, 50).Value = cbKategori.SelectedItem.ToString();
                        cmd.Parameters.Add("@Jumlah", SqlDbType.Int).Value = nominalDonasi;
                        cmd.Parameters.Add("@Tanggal", SqlDbType.Date).Value = dtpTanggal.Value.Date; // Membaca tanggal dari DateTimePicker

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Donasi Berhasil Disubmit!\nStatus saat ini: PENDING (Menunggu Validasi Admin)",
                                        "Sukses STQA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memproses transaksi donasi: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- STQA LAYER 3: Pengunci Keyboard agar Kolom Jumlah Hanya Bisa Diisi Angka ---
        private void txtJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tolak huruf/simbol
            }
        }

        private void ClearForm()
        {
            cbIDDonatur.SelectedIndex = -1;
            cbKategori.SelectedIndex = -1;
            txtJumlah.Clear();
            dtpTanggal.Value = DateTime.Now;
        }
    }
}