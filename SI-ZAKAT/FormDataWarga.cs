using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDataWarga : Form
    {
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDataWarga()
        {
            InitializeComponent();
        }

        // =========================================================================
        // 1. FUNGSI READ
        // =========================================================================
        BindingSource bsWarga = new BindingSource();

        public void TampilkanData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM vw_DaftarWarga", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        bsWarga.DataSource = dt;
                        dgvDataWarga.DataSource = bsWarga;
                        bindingNavigator1.BindingSource = bsWarga;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error QA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 2. KEYPRESS NIK — hanya angka
        // =========================================================================
        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // =========================================================================
        // 3. KEYPRESS NAMA — hanya huruf, spasi, apostrof (')
        // =========================================================================
        private void txtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool boleh = char.IsLetter(e.KeyChar)
                      || e.KeyChar == ' '
                      || e.KeyChar == '\''
                      || char.IsControl(e.KeyChar);

            if (!boleh)
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // =========================================================================
        // 4. KEYPRESS ALAMAT — huruf, angka, spasi, dan tanda umum (. , - / No. RT RW)
        //    Blokir: %, $, @, #, !, *, (, ), =, &, ^, ~, dll
        // =========================================================================
        private void txtAlamat_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Karakter yang diizinkan untuk alamat Indonesia:
            // huruf, angka, spasi, titik(.), koma(,), strip(-), garis miring(/)
            // Contoh valid: Jl. Melati No. 5, RT 02/RW 03, Bantul
            bool boleh = char.IsLetterOrDigit(e.KeyChar)
                      || e.KeyChar == ' '
                      || e.KeyChar == '.'
                      || e.KeyChar == ','
                      || e.KeyChar == '-'
                      || e.KeyChar == '/'
                      || char.IsControl(e.KeyChar); // Backspace, dll

            if (!boleh)
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // =========================================================================
        // 5. VALIDASI TERPUSAT — dipanggil sebelum Simpan & Update
        // =========================================================================
        private bool ValidasiInput()
        {
            // --- NIK ---
            if (string.IsNullOrWhiteSpace(txtNIK.Text))
            {
                MessageBox.Show("NIK tidak boleh kosong.", "Data Tidak Valid",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIK.Focus(); return false;
            }
            if (!Regex.IsMatch(txtNIK.Text.Trim(), @"^\d{16}$"))
            {
                MessageBox.Show("NIK harus berupa angka saja dan tepat 16 digit.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIK.Focus(); return false;
            }

            // --- NAMA ---
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong.", "Data Tidak Valid",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNama.Focus(); return false;
            }
            // Hanya huruf, spasi, apostrof
            if (!Regex.IsMatch(txtNama.Text.Trim(), @"^[a-zA-Z '\s]+$"))
            {
                MessageBox.Show("Nama hanya boleh mengandung huruf, spasi, dan tanda apostrof (').",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNama.Focus(); return false;
            }

            // --- ALAMAT ---
            if (string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Alamat tidak boleh kosong.", "Data Tidak Valid",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamat.Focus(); return false;
            }
            // Hanya huruf, angka, spasi, titik(.), koma(,), strip(-), garis miring(/)
            if (!Regex.IsMatch(txtAlamat.Text.Trim(), @"^[a-zA-Z0-9 .,\-/\s]+$"))
            {
                MessageBox.Show(
                    "Alamat mengandung karakter tidak valid!\n" +
                    "Karakter yang diizinkan: huruf, angka, spasi, titik (.), koma (,), strip (-), garis miring (/).",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamat.Focus(); return false;
            }
            if (txtAlamat.Text.Length > 200)
            {
                MessageBox.Show("Alamat terlalu panjang! Maksimal 200 karakter.",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamat.Focus(); return false;
            }

            // --- PERAN ---
            if (cbPeran.SelectedIndex == -1)
            {
                MessageBox.Show("Peran warga harus dipilih (Muzakki / Mustahik).",
                    "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPeran.Focus(); return false;
            }

            return true;
        }

        // =========================================================================
        // 6. TOMBOL SIMPAN
        //    FIX: Gunakan INSERT langsung (bukan SP) jika SP tidak return rowsAffected
        //    agar pop-up "berhasil disimpan" selalu muncul
        // =========================================================================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertWarga", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                        cmd.Parameters.Add("@Nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim();
                        cmd.Parameters.Add("@Alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim();
                        cmd.Parameters.Add("@Peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();

                        // FIX: ExecuteNonQuery dipercaya selama tidak throw exception
                        // Pop-up muncul setelah eksekusi berhasil tanpa error
                        cmd.ExecuteNonQuery();
                    }

                    // Pop-up muncul di LUAR blok SqlCommand, dijamin tampil jika tidak ada exception
                    MessageBox.Show("Data warga berhasil disimpan!", "Simpan Berhasil",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    TampilkanData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                        MessageBox.Show("NIK sudah terdaftar di database!", "Duplikasi Data",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Kesalahan database: " + ex.Message, "SQL Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kesalahan sistem: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 7. TOMBOL UPDATE
        // =========================================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateWarga", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                        cmd.Parameters.Add("@Nama", SqlDbType.VarChar, 100).Value = txtNama.Text.Trim();
                        cmd.Parameters.Add("@Alamat", SqlDbType.VarChar, 200).Value = txtAlamat.Text.Trim();
                        cmd.Parameters.Add("@Peran", SqlDbType.VarChar, 20).Value = cbPeran.SelectedItem.ToString();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data warga berhasil diperbarui!", "Update Berhasil",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    TampilkanData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 8. TOMBOL DELETE
        // =========================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIK.Text))
            {
                MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin menghapus data warga dengan NIK ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteWarga", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = txtNIK.Text.Trim();
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Data warga berhasil dihapus!", "Hapus Berhasil",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        TampilkanData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =========================================================================
        // 9. SEARCH
        // =========================================================================
        private void btnCari_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchWarga", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 100).Value = txtCari.Text.Trim();

                        DataTable dt = new DataTable();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                            dt.Load(reader);

                        bsWarga.DataSource = dt;
                        dgvDataWarga.DataSource = bsWarga;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Data warga tidak ditemukan.", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TampilkanData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kesalahan pencarian: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // 10. KLIK BARIS DGV
        // =========================================================================
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

        private void btnTampilkanData_Click(object sender, EventArgs e) => TampilkanData();

        // =========================================================================
        // 11. CLEAR FORM & BACK
        // =========================================================================
        private void ClearForm()
        {
            txtNIK.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
            cbPeran.SelectedIndex = -1;
            txtNIK.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
