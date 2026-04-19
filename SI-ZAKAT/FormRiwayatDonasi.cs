using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormRiwayatDonasi : Form
    {
        // String koneksi (sesuaikan dengan servermu)
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormRiwayatDonasi()
        {
            InitializeComponent();
        }

        private void FormRiwayatDonasi_Load(object sender, EventArgs e)
        {
            LoadDonatur();
            TampilkanSemuaRiwayat();
        }

        // 1. Fungsi mengambil daftar nama donatur untuk ComboBox
        private void LoadDonatur()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT NIK, nama FROM Tabel_Warga WHERE peran = 'Muzakki'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbDonatur.Items.Clear();
                    cbDonatur.Items.Add("--- Tampilkan Semua ---");
                    while (reader.Read())
                    {
                        cbDonatur.Items.Add(reader["NIK"].ToString() + " - " + reader["nama"].ToString());
                    }
                    cbDonatur.SelectedIndex = 0;
                }
                catch (Exception ex) { MessageBox.Show("Gagal Load Donatur: " + ex.Message); }
            }
        }

        // 2. Fungsi menampilkan semua riwayat donasi
        private void TampilkanSemuaRiwayat()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Menggunakan JOIN agar nama warga muncul, bukan cuma NIK
                    string query = @"SELECT d.id_donasi, w.nama, d.kategori, d.jumlah, d.tanggal, d.status 
                                     FROM Tabel_Donasi d 
                                     JOIN Tabel_Warga w ON d.nik_warga = w.NIK";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRiwayat.DataSource = dt;

                    // Opsional: Merapikan tampilan tabel
                    dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Gagal Load Riwayat: " + ex.Message); }
            }
        }

        // 3. Event saat pilihan di ComboBox berubah (Filter Otomatis)
        private void cbDonatur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDonatur.SelectedIndex == 0) // Jika pilih "Tampilkan Semua"
            {
                TampilkanSemuaRiwayat();
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ambil NIK dari string "NIK - Nama"
                    string nik = cbDonatur.SelectedItem.ToString().Split('-')[0].Trim();

                    string query = @"SELECT d.id_donasi, w.nama, d.kategori, d.jumlah, d.tanggal, d.status 
                                     FROM Tabel_Donasi d 
                                     JOIN Tabel_Warga w ON d.nik_warga = w.NIK 
                                     WHERE d.nik_warga = @nik";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nik", nik);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRiwayat.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Gagal Filter: " + ex.Message); }
            }
        }

        // 4. Tombol Tutup
        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}