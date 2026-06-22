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
    public partial class FormRiwayat : Form
    {
        private Label label1;
        private DataGridView dgvRiwayat;
        private Button btnTutup;
        private TextBox txtNamaDonatur;

        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        // NIK donatur — diisi via property setelah konstruktor
        private string _nikDonatur = "";

        public FormRiwayat()
        {
            InitializeComponent();
        }

        // Property untuk kirim NIK dari FormDonasi sebelum ShowDialog()
        public string NikDonatur
        {
            set { _nikDonatur = value; }
        }

        // =========================================================================
        // LOAD
        // =========================================================================
        private void FormRiwayat_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nikDonatur))
            {
                MessageBox.Show("NIK donatur tidak ditemukan.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            LoadNamaDonatur();
            LoadRiwayatDonasi();
        }

        // =========================================================================
        // LOAD NAMA DONATUR
        // =========================================================================
        private void LoadNamaDonatur()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT nama FROM Tabel_Warga WHERE NIK = @NIK", conn))
                    {
                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = _nikDonatur;
                        object hasil = cmd.ExecuteScalar();
                        txtNamaDonatur.Text = hasil != null
                            ? hasil.ToString() + " (" + _nikDonatur.Trim() + ")"
                            : "Donatur tidak ditemukan";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat nama donatur:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // LOAD RIWAYAT DONASI
        // =========================================================================
        private void LoadRiwayatDonasi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            id_donasi AS [ID Donasi],
                            kategori  AS [Kategori],
                            jumlah    AS [Jumlah],
                            tanggal   AS [Tanggal],
                            status    AS [Status]
                        FROM Tabel_Donasi
                        WHERE NIK_Warga = @NIK
                        ORDER BY tanggal DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@NIK", SqlDbType.Char, 16).Value = _nikDonatur;

                        DataTable dt = new DataTable();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                            dt.Load(reader);

                        dgvRiwayat.DataSource = dt;

                        // Format Jumlah → Rp
                        if (dgvRiwayat.Columns["Jumlah"] != null)
                            dgvRiwayat.Columns["Jumlah"].DefaultCellStyle.Format = "Rp #,##0";

                        // Format Tanggal → dd/MM/yyyy
                        if (dgvRiwayat.Columns["Tanggal"] != null)
                            dgvRiwayat.Columns["Tanggal"].DefaultCellStyle.Format = "dd/MM/yyyy";

                        // Warnai kolom Status
                        WarnaiStatusCell();

                        if (dt.Rows.Count == 0)
                            MessageBox.Show("Belum ada riwayat donasi untuk donatur ini.",
                                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat riwayat donasi:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // WARNAI KOLOM STATUS
        // =========================================================================
        private void WarnaiStatusCell()
        {
            foreach (DataGridViewRow row in dgvRiwayat.Rows)
            {
                if (row.IsNewRow) continue;
                var cell = row.Cells["Status"];
                if (cell == null || cell.Value == null) continue;

                switch (cell.Value.ToString().Trim())
                {
                    case "Approved":
                        cell.Style.BackColor = Color.FromArgb(0, 180, 100);
                        cell.Style.ForeColor = Color.White;
                        cell.Style.Font = new Font(dgvRiwayat.Font, FontStyle.Bold);
                        break;
                    case "Pending":
                        cell.Style.BackColor = Color.FromArgb(255, 200, 0);
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.Font = new Font(dgvRiwayat.Font, FontStyle.Bold);
                        break;
                    case "Rejected":
                        cell.Style.BackColor = Color.FromArgb(220, 50, 50);
                        cell.Style.ForeColor = Color.White;
                        cell.Style.Font = new Font(dgvRiwayat.Font, FontStyle.Bold);
                        break;
                }
            }
        }

        // =========================================================================
        // TOMBOL TUTUP
        // =========================================================================
        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNamaDonatur = new System.Windows.Forms.TextBox();
            this.dgvRiwayat = new System.Windows.Forms.DataGridView();
            this.btnTutup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama Donatur :";
            // 
            // txtNamaDonatur
            // 
            this.txtNamaDonatur.Location = new System.Drawing.Point(357, 93);
            this.txtNamaDonatur.Name = "txtNamaDonatur";
            this.txtNamaDonatur.ReadOnly = true;
            this.txtNamaDonatur.Size = new System.Drawing.Size(317, 26);
            this.txtNamaDonatur.TabIndex = 1;
            // 
            // dgvRiwayat
            // 
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayat.Location = new System.Drawing.Point(74, 144);
            this.dgvRiwayat.Name = "dgvRiwayat";
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.RowHeadersWidth = 62;
            this.dgvRiwayat.RowTemplate.Height = 28;
            this.dgvRiwayat.Size = new System.Drawing.Size(748, 258);
            this.dgvRiwayat.TabIndex = 2;
            // 
            // btnTutup
            // 
            this.btnTutup.BackColor = System.Drawing.SystemColors.Info;
            this.btnTutup.Location = new System.Drawing.Point(711, 432);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(112, 39);
            this.btnTutup.TabIndex = 3;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = false;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // FormRiwayat
            // 
            this.ClientSize = new System.Drawing.Size(953, 534);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.dgvRiwayat);
            this.Controls.Add(this.txtNamaDonatur);
            this.Controls.Add(this.label1);
            this.Name = "FormRiwayat";
            this.Text = "Riwayat Donasi";
            this.Load += new System.EventHandler(this.FormRiwayat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}