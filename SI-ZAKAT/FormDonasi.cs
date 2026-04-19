using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDonasi : Form
    {
        // 1. TAMBAHKAN INI (Deklarasi String Koneksi)
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDonasi()
        {
            InitializeComponent();
        }

        // 2. TAMBAHKAN EVENT LOAD (Agar dropdown terisi otomatis saat form dibuka)
        private void FormDonasi_Load(object sender, EventArgs e)
        {
            LoadDonatur();

            // Isi pilihan kategori secara manual jika belum diisi di designer
            if (cbKategori.Items.Count == 0)
            {
                cbKategori.Items.Add("Zakat");
                cbKategori.Items.Add("Infaq");
                cbKategori.Items.Add("Sedekah");
            }
        }

        public void LoadDonatur()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT NIK, nama FROM Tabel_Warga";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbDonatur.Items.Clear(); // Bersihkan dulu agar tidak double
                    while (reader.Read())
                    {
                        // Menampilkan NIK - Nama di dropdown
                        cbDonatur.Items.Add(reader["NIK"].ToString() + " - " + reader["nama"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data donatur: " + ex.Message);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validasi agar tidak error jika belum pilih donatur atau kategori
            if (cbDonatur.SelectedItem == null || cbKategori.SelectedItem == null || string.IsNullOrEmpty(txtJumlah.Text))
            {
                MessageBox.Show("Mohon lengkapi semua data donasi!", "Peringatan");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ambil NIK saja dari ComboBox (ambil string sebelum tanda "-")
                    string nikDonatur = cbDonatur.SelectedItem.ToString().Split('-')[0].Trim();

                    string query = "INSERT INTO Tabel_Donasi (nik_warga, kategori, jumlah, tanggal, status) " +
                                   "VALUES (@nik, @kat, @jml, @tgl, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nik", nikDonatur);
                    cmd.Parameters.AddWithValue("@kat", cbKategori.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@jml", decimal.Parse(txtJumlah.Text));
                    cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value);
                    cmd.Parameters.AddWithValue("@status", "Pending");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donasi Berhasil Disubmit! Menunggu validasi admin.", "Sukses");

                    // Kosongkan form setelah berhasil
                    txtJumlah.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Submit: " + ex.Message);
                }
            }
        }
    }
}