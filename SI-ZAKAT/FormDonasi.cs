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
    public partial class FormDonasi : Form
    {
        public FormDonasi()
        {
            InitializeComponent();
        }

        public void LoadDonatur()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT NIK, nama FROM Tabel_Warga";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Menampilkan NIK - Nama di dropdown
                    cbDonatur.Items.Add(reader["NIK"].ToString() + " - " + reader["nama"].ToString());
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ambil NIK saja dari ComboBox (ambil string sebelum tanda "-")
                    string nikDonatur = cbDonatur.SelectedItem.ToString().Split('-')[0].Trim();

                    string query = "INSERT INTO Tabel_Donasi (nik_warga, kategori, jumlah, tanggal, status) " +
                                   "VALUES (@nik, @kat, @jml, @tgl, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nik", nikDonatur);
                    cmd.Parameters.AddWithValue("@kat", cbKategori.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@jml", decimal.Parse(txtJumlah.Text));
                    cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value);
                    cmd.Parameters.AddWithValue("@status", "Pending"); // Status default

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donasi Berhasil Disubmit! Menunggu validasi admin.", "Sukses");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Submit: " + ex.Message);
                }
            }
        }




    }
}
