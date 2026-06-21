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

        // =========================================================================
        // LOAD — isi ComboBox & setting DateTimePicker
        // =========================================================================
        private void FormDonasi_Load(object sender, EventArgs e)
        {
            // --- SETTING DATETIMEPICKER ---
            // Format tampilan: dd/MM/yyyy (misal: 21/06/2026)
            dtpTanggal.Format = DateTimePickerFormat.Custom;
            dtpTanggal.CustomFormat = "dd/MM/yyyy";

            // Batasi tanggal: minimal 1 Januari tahun ini, maksimal hari ini
            // Cegah user pilih tanggal masa depan atau tahun tidak masuk akal
            dtpTanggal.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
            dtpTanggal.MaxDate = DateTime.Now;
            dtpTanggal.Value = DateTime.Now; // Default = hari ini

            // --- ISI COMBOBOX ID DONATUR dari Tabel_Warga ---
            // Tampil format "NIK - Nama" agar user tahu memilih siapa
            LoadDonatur();

            // --- ISI COMBOBOX KATEGORI (statis, tidak bisa diketik manual) ---
            cbKategori.DropDownStyle = ComboBoxStyle.DropDownList; // Hanya pilih, tidak bisa ketik
            cbKategori.Items.Clear();
            cbKategori.Items.Add("Zakat Fitrah");
            cbKategori.Items.Add("Zakat Maal");
            cbKategori.Items.Add("Infaq");
            cbKategori.Items.Add("Sedekah");
            cbKategori.Items.Add("Fidyah");
            cbKategori.Items.Add("Bantuan Sembako");
        }

        // =========================================================================
        // LOAD DONATUR — tampil "NIK - Nama" di ComboBox
        // =========================================================================
        private void LoadDonatur()
        {
            cbIDDonatur.DropDownStyle = ComboBoxStyle.DropDownList; // Hanya pilih, tidak bisa ketik
            cbIDDonatur.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ambil NIK + nama, hanya warga dengan peran Muzakki (pemberi zakat)
                    // Jika semua warga boleh donasi, hapus WHERE peran='Muzakki'
                    string query = "SELECT NIK, nama FROM Tabel_Warga WHERE peran = 'Muzakki' ORDER BY nama";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nik = reader["NIK"].ToString().Trim();
                            string nama = reader["nama"].ToString().Trim();
                            // Format tampilan: "NIK - Nama" supaya user tahu pilih siapa
                            cbIDDonatur.Items.Add($"{nik} - {nama}");
                        }
                    }

                    if (cbIDDonatur.Items.Count == 0)
                        MessageBox.Show("Belum ada data Muzakki terdaftar.\nSilakan daftarkan warga terlebih dahulu.",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data donatur:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // KEYPRESS JUMLAH — hanya angka, cegah crash Convert.ToInt32
        // =========================================================================
        private void txtJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // =========================================================================
        // VALIDASI TERPUSAT
        // =========================================================================
        private bool ValidasiInput(out long jumlah)
        {
            jumlah = 0;

            // Cek ID Donatur dipilih
            if (cbIDDonatur.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih ID Donatur terlebih dahulu.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbIDDonatur.Focus(); return false;
            }

            // Cek Kategori dipilih
            if (cbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih Kategori Donasi terlebih dahulu.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbKategori.Focus(); return false;
            }

            // Cek Jumlah tidak kosong
            if (string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Jumlah donasi wajib diisi.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus(); return false;
            }

            // Cek Jumlah adalah angka valid (cegah crash)
            if (!long.TryParse(txtJumlah.Text.Trim(), out jumlah))
            {
                MessageBox.Show("Jumlah donasi harus berupa angka.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus(); return false;
            }

            // Cek minimal donasi Rp 10.000
            if (jumlah < 10000)
            {
                MessageBox.Show("Minimal nominal donasi adalah Rp 10.000.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlah.Focus(); return false;
            }

            // Cek tanggal tidak melebihi hari ini (double-check, sudah dibatasi MaxDate)
            if (dtpTanggal.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal donasi tidak boleh melebihi hari ini.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTanggal.Focus(); return false;
            }

            return true;
        }

        // =========================================================================
        // TOMBOL SUBMIT DONASI
        // =========================================================================
        private void btnSubmitDonasi_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput(out long jumlah)) return;

            // Ambil NIK saja dari pilihan "NIK - Nama"
            // Format: "1234567890123456 - Budi" → split di " - " → ambil bagian pertama
            string pilihan = cbIDDonatur.SelectedItem.ToString();
            string nik = pilihan.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertDonasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NIK_Warga", SqlDbType.Char, 16).Value = nik;
                        cmd.Parameters.Add("@Kategori", SqlDbType.VarChar, 50).Value = cbKategori.SelectedItem.ToString();
                        cmd.Parameters.Add("@Jumlah", SqlDbType.BigInt).Value = jumlah;
                        cmd.Parameters.Add("@Tanggal", SqlDbType.Date).Value = dtpTanggal.Value.Date;
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Donasi berhasil disubmit!\n\n" +
                        $"Donatur  : {pilihan}\n" +
                        $"Kategori : {cbKategori.SelectedItem}\n" +
                        $"Jumlah   : Rp {jumlah:N0}\n" +
                        $"Tanggal  : {dtpTanggal.Value:dd/MM/yyyy}\n\n" +
                        "Status: PENDING — menunggu validasi Admin.",
                        "Submit Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memproses donasi:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // LIHAT RIWAYAT DONASI
        // =========================================================================
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Validasi: donatur harus dipilih dulu sebelum lihat riwayat
            if (cbIDDonatur.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih ID Donatur terlebih dahulu untuk melihat riwayatnya.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbIDDonatur.Focus();
                return;
            }

            // Ambil NIK dari pilihan "NIK - Nama"
            string pilihan = cbIDDonatur.SelectedItem.ToString();
            string nik = pilihan.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();

            // Buka FormRiwayat, kirim NIK via property (bukan constructor)
            FormRiwayat formRiwayat = new FormRiwayat();
            formRiwayat.NikDonatur = nik;
            formRiwayat.ShowDialog();
        }

        // =========================================================================
        // BACK & CLEAR
        // =========================================================================
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ClearForm()
        {
            cbIDDonatur.SelectedIndex = -1;
            cbKategori.SelectedIndex = -1;
            txtJumlah.Clear();
            dtpTanggal.Value = DateTime.Now;
            cbIDDonatur.Focus();
        }
    }
}