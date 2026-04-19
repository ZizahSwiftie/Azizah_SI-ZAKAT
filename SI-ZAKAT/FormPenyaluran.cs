using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormPenyaluran : Form
    {
        // String koneksi (pastikan sama dengan form lainnya)
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormPenyaluran()
        {
            InitializeComponent();
        }

        private void FormPenyaluran_Load(object sender, EventArgs e)
        {
            LoadMustahik();
            // Isi kategori barang jika masih kosong
            if (cbBarang.Items.Count == 0)
            {
                cbBarang.Items.Add("Beras (kg)");
                cbBarang.Items.Add("Minyak Goreng (L)");
                cbBarang.Items.Add("Uang Tunai (Rp)");
                cbBarang.Items.Add("Paket Sembako");
            }
        }

        // 1. Fungsi memuat data warga yang berperan sebagai Mustahik
        private void LoadMustahik()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT NIK, nama FROM Tabel_Warga WHERE peran = 'Mustahik'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbMustahik.Items.Clear();
                    while (reader.Read())
                    {
                        cbMustahik.Items.Add(reader["NIK"].ToString() + " - " + reader["nama"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data mustahik: " + ex.Message);
                }
            }
        }

        // 2. Tombol Salurkan Bantuan (INSERT data ke Tabel_Penyaluran)
        private void btnSalurkan_Click(object sender, EventArgs e)
        {
            // Validasi Input
            if (cbMustahik.SelectedItem == null || cbBarang.SelectedItem == null || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Mohon lengkapi semua data penyaluran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Ambil NIK dari pilihan "NIK - Nama"
                    string nikMustahik = cbMustahik.SelectedItem.ToString().Split('-')[0].Trim();

                    string query = @"INSERT INTO Tabel_Penyaluran (nik_mustahik, tanggal, barang, jumlah) 
                                     VALUES (@nik, @tgl, @brg, @jml)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nik", nikMustahik);
                    cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value);
                    cmd.Parameters.AddWithValue("@brg", cbBarang.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@jml", txtJumlah.Text); // Simpan sebagai string atau sesuaikan tipe data di SQL

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Bantuan berhasil disalurkan kepada Mustahik!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset input setelah sukses
                    txtJumlah.Clear();
                    cbMustahik.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memproses penyaluran: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}