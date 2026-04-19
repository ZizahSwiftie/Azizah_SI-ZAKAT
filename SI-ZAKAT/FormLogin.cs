using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 1. WAJIB tambahkan namespace ini
using System.Data.SqlClient;

namespace SI_ZAKAT
{
    public partial class FormLogin : Form
    {
        // 2. Atur String Koneksi (Sesuaikan "Data Source" dengan nama server SQL kamu)
        // Jika pakai SQL Express biasanya: @"Data Source=.\SQLEXPRESS;Initial Catalog=DB_SIZAKAT;Integrated Security=True"
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
            // Set password agar tersembunyi saat pertama buka
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 3. Validasi Input Kosong
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // 4. Query untuk mengecek data warga dengan peran Admin (Sesuai SRS)
                    string query = "SELECT COUNT(*) FROM Tabel_Warga WHERE nama=@user AND password=@pass AND peran='Admin'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 5. Menggunakan Parameter agar AMAN
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                        // Mengambil hasil hitungan (ExecuteScalar)
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show("Login Berhasil! Selamat Datang Admin.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 6. Pindah ke Form Dashboard
                            FormDashboard dash = new FormDashboard();
                            dash.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username/Password salah atau Anda bukan Admin!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan Koneksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fitur Show/Hide Password
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }
    }
}