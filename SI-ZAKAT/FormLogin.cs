using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // 1. Validasi Input Kosong
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gunakan blok using agar koneksi otomatis tertutup jika terjadi error
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // KODE YANG SENGAJA DIBUAT RENTAN (VULNERABLE) UNTUK KEPERLUAN DEMO UCP 2
                    string query = "SELECT * FROM Tabel_Warga WHERE nama = '" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                        // Menggunakan ExecuteReader karena kita butuh membaca nilai kolom 'peran'
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Jika data ditemukan
                            {
                                string peran = reader["peran"].ToString();

                                MessageBox.Show("Login Berhasil! Selamat Datang " + peran, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // 3. PINDAH KE FORM SESUAI PERAN
                                this.Hide();

                                if (peran == "Admin")
                                {
                                    // Pastikan nama form kamu adalah FormDataWarga atau ganti sesuai nama form Adminmu
                                    FormDataWarga formAdmin = new FormDataWarga();
                                    formAdmin.Show();
                                }
                                else
                                {
                                    // Jika dia Muzakki/User biasa, arahkan ke dashboard user
                                    // Pastikan kamu punya form bernama FormDashboard atau sesuaikan namanya
                                    FormDashboard dashboard = new FormDashboard();
                                    dashboard.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan Koneksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle visibility password
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }
    }
}





