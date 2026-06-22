using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormStokSembako : Form
    {
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        // Memanfaatkan Data Binding (Poin 4 UCP 2)
        BindingSource bsStok = new BindingSource();

        public FormStokSembako()
        {
            InitializeComponent();
        }

        private void FormStokSembako_Load(object sender, EventArgs e)
        {
            TampilkanData();
        }

        // --- MENGGUNAKAN VIEW & BINDING (UCP 2) ---
        private void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Menembak objek VIEW, bukan tabel langsung
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_StokSembako", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bsStok.DataSource = dt;
                    dgvStok.DataSource = bsStok;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- TOMBOL SIMPAN (STORED PROCEDURE) ---
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBarang.Text) || string.IsNullOrWhiteSpace(txtNamaBarang.Text) || string.IsNullOrWhiteSpace(txtStok.Text))
            {
                MessageBox.Show("Semua kolom isian wajib diisi!", "Validasi STQA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertStok", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID_Barang", SqlDbType.VarChar, 20).Value = txtIDBarang.Text.Trim();
                        cmd.Parameters.Add("@NamaBarang", SqlDbType.VarChar, 100).Value = txtNamaBarang.Text.Trim();
                        cmd.Parameters.Add("@Stok", SqlDbType.Int).Value = Convert.ToInt32(txtStok.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data logistik sembako berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        TampilkanData();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- TOMBOL UPDATE (STORED PROCEDURE) ---
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBarang.Text)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateStok", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID_Barang", SqlDbType.VarChar, 20).Value = txtIDBarang.Text.Trim();
                        cmd.Parameters.Add("@NamaBarang", SqlDbType.VarChar, 100).Value = txtNamaBarang.Text.Trim();
                        cmd.Parameters.Add("@Stok", SqlDbType.Int).Value = Convert.ToInt32(txtStok.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data logistik sembako berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        TampilkanData();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            }
        }

        // --- TOMBOL HAPUS (STORED PROCEDURE) ---
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBarang.Text)) return;

            if (MessageBox.Show("Hapus barang ini dari gudang?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteStok", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Barang", SqlDbType.VarChar, 20).Value = txtIDBarang.Text.Trim();

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data logistik berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            TampilkanData();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
                }
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        // Klik Baris di DataGridView langsung ngisi TextBox otomatis
        private void dgvStok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStok.Rows[e.RowIndex];
                txtIDBarang.Text = row.Cells["ID Barang"].Value.ToString();
                txtNamaBarang.Text = row.Cells["Nama Barang"].Value.ToString();
                txtStok.Text = row.Cells["Stok"].Value.ToString();
            }
        }

        // Kunci keyboard biar kolom stok hanya bisa diisi angka
        private void txtStok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ClearForm()
        {
            txtIDBarang.Clear();
            txtNamaBarang.Clear();
            txtStok.Clear();
            txtIDBarang.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}