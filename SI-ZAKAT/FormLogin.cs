using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormLogin : Form
    {
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();

            // Password disembunyikan saat form dibuka
            txtPassword.UseSystemPasswordChar = true;
        }

        // Show / Hide Password
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }

        // Tombol Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(
                    "Username wajib diisi!",
                    "Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Password wajib diisi!",
                    "Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show(
                    "Password minimal 8 karakter!",
                    "Validasi Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Menggunakan Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("sp_Login", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameterized Query
                        // Aman dari SQL Injection
                        cmd.Parameters.Add(
                            "@Username",
                            SqlDbType.VarChar,
                            100).Value = txtUsername.Text.Trim();

                        cmd.Parameters.Add(
                            "@Password",
                            SqlDbType.VarChar,
                            100).Value = txtPassword.Text.Trim();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();

                            MessageBox.Show(
                                "Login berhasil sebagai " + role,
                                "Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.Hide();

                            // Role Based Access
                            if (role == "Admin" || role == "Petugas")
                            {
                                FormDashboard dashboard =
                                    new FormDashboard();

                                dashboard.ShowDialog();
                            }
                            else if (role == "Warga" || role == "Muzakki")
                            {
                                FormDonasi donasi =
                                    new FormDonasi();

                                donasi.ShowDialog();
                            }
                            else if (role == "Mustahik")
                            {
                                FormRiwayat riwayat =
                                    new FormRiwayat();

                                riwayat.ShowDialog();
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Username atau Password salah!",
                                "Login Gagal",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        "Kesalahan Database:\n" + ex.Message,
                        "SQL Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Terjadi kesalahan:\n" + ex.Message,
                        "System Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Tombol Keluar
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}