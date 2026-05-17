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
    // CATATAN: Pastikan nama kelas ini tetap FormDashboard sesuai nama form-mu
    public partial class FormDashboard : Form
    {
        // SOLUSI ERROR 1: Mendeklarasikan variabel connectionString secara global di dalam kelas
        string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

        public FormDashboard()
        {
            InitializeComponent();
        }

        // Fungsi Load Form: Berjalan otomatis begitu dashboard dibuka oleh user
        private void FormDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        // SOLUSI ERROR 3: Membuat fungsi mandiri yang rapi dan terpisah dari event klik label
        private void LoadDashboard()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Hitung Total Dana (Hanya yang statusnya Approved)
                    SqlCommand cmd1 = new SqlCommand("SELECT SUM(jumlah) FROM Tabel_Donasi WHERE status = 'Approved'", conn);
                    object dana = cmd1.ExecuteScalar();
                    lblTotalDana.Text = "Rp " + (dana != DBNull.Value ? Convert.ToDouble(dana).ToString("N0") : "0");

                    // 2. Hitung Total Warga
                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Tabel_Warga", conn);
                    lblTotalWarga.Text = cmd2.ExecuteScalar().ToString();

                    // 3. Hitung Stok Barang
                    SqlCommand cmd3 = new SqlCommand("SELECT SUM(jumlah_stok) FROM Tabel_Stok", conn);
                    lblStokBarang.Text = cmd3.ExecuteScalar().ToString();

                    // 4. Hitung Total Penyaluran
                    SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Tabel_Penyuluran", conn);
                    lblTotalSalur.Text = cmd4.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data statistik dashboard: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    } // SOLUSI ERROR 2: Kurung kurawal tutup untuk Class FormDashboard
} // Kurung kurawal tutup untuk Namespace SI_ZAKAT