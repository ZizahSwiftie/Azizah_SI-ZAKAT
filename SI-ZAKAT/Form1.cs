using System.Data.SqlClient; // Wajib ada untuk koneksi ke SQL Server
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class Form1 : Form
    {
        // Deklarasi connection string
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString); // Membuat objek koneksi [cite: 126, 472]
            try
            {
                conn.Open(); // Membuka koneksi ke SQL Server [cite: 131, 514]
                MessageBox.Show("Koneksi ke Database DB_SIZAKAT Berhasil!", "Status Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Information); // Menampilkan status koneksi 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Gagal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); // Menutup koneksi kembali
                }
            }
        }

        public void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Tabel_Warga";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Menggunakan SqlDataReader (Metode ExecuteReader)
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Menyiapkan DataTable untuk menampung hasil bacaan
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Menampilkan data ke DataGridView
                    dgvWarga.DataSource = dt;

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menampilkan data: " + ex.Message);
                }
            }
        }
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input (Bagian F - Soal UCP)
            // Memastikan field penting tidak kosong sebelum diproses
            if (string.IsNullOrEmpty(txtNIK.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtAlamat.Text))
            {
                MessageBox.Show("Semua data wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Implementasi Koneksi dan Command (Bagian D - Soal UCP)
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query INSERT untuk Tabel_Warga
                    // Menggunakan Parameter (@) untuk mencegah SQL Injection
                    string query = "INSERT INTO Tabel_Warga (NIK, nama, alamat, peran) VALUES (@nik, @nama, @alamat, 'Muzakki')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);

                    // 3. Menjalankan perintah menggunakan ExecuteNonQuery
                    // Method ini mengembalikan jumlah baris yang terpengaruh (integer)
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data Warga Berhasil Disimpan ke Database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Membersihkan TextBox setelah data berhasil masuk
                        txtNIK.Clear();
                        txtNama.Clear();
                        txtAlamat.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }
    }

}
    