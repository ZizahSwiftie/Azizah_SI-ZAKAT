using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDataWarga : Form
    {
        // Sesuaikan nama server dengan laptop kamu (AZIZAH\AZIZAH)
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDataWarga()
        {
            InitializeComponent();
        }

        // FUNGSI READ: Menampilkan data ke DataGridView
        public void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Tabel_Warga";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvDataWarga.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message);
                }
            }
        }

        // EVENT SAAT FORM DIBUKA: Munculkan pesan koneksi berhasil
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Koneksi ke Database SI-ZAKAT Berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi Gagal: " + ex.Message);
                }
            }
        }

        // TOMBOL SIMPAN (CREATE)
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // VALIDASI: Cek apakah ComboBox sudah dipilih atau belum
            if (cbPeran.SelectedItem == null)
            {
                MessageBox.Show("Pilih Peran terlebih dahulu (Muzakki/Mustahik)!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Berhenti di sini, jangan lanjut ke database
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO Tabel_Warga (NIK, nama, alamat, peran) VALUES (@nik, @nama, @alamat, @peran)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);

                    // Sekarang aman karena sudah divalidasi di atas
                    cmd.Parameters.AddWithValue("@peran", cbPeran.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan!");
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Simpan: " + ex.Message);
                }
            }
        }


        // TOMBOL UPDATE (UPDATE)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE Tabel_Warga SET nama=@nama, alamat=@alamat, peran=@peran WHERE NIK=@nik";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@peran", cbPeran.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilkanData();
                }
                catch (Exception ex) { MessageBox.Show("Gagal Update: " + ex.Message); }
            }
        }

        // TOMBOL DELETE (DELETE)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Tabel_Warga WHERE NIK=@nik", conn);
                        cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Terhapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TampilkanData();
                    }
                    catch (Exception ex) { MessageBox.Show("Gagal Hapus: " + ex.Message); }
                }
            }
        }

        // EVENT KLIK TABEL: Pindahkan data dari tabel ke kotak input
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

        // TOMBOL TAMPILKAN DATA (REFRESH)
        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        // FITUR PENCARIAN
        private void btnCari_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Tabel_Warga WHERE nama LIKE @cari OR NIK LIKE @cari";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cari", "%" + txtCari.Text + "%");

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    // Pasang hasil ke tabel
                    dgvDataWarga.DataSource = dt;

                    // Tambahkan Notifikasi di sini
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Data warga dengan nama atau NIK tersebut tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Opsional: Refresh tabel ke data awal jika tidak ketemu
                        TampilkanData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

    }
}