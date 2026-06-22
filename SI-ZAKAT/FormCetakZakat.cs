using Microsoft.Reporting.WinForms;
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
    public partial class FormCetakZakat : Form
    {
        public FormCetakZakat()
        {
            InitializeComponent();
        }

        private void FormCetakZakat_Load(object sender, EventArgs e)
        {
            try
            {
                // Gunakan connection string database SI-ZAKAT milikmu
                string connString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Mengambil rekapan data donasi dari view database[cite: 5]
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_LaporanDonasiZakat", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt); // Mengisi data secara disconnected[cite: 4]

                    // Bersihkan data lama di viewer sebelum memuat yang baru
                    this.reportViewer1.LocalReport.DataSources.Clear();

                    // Nama "DataSet1" harus sama dengan nama DataSet saat membuat rdlc kemarin
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Add(rds);

                    // Segarkan dan render halaman cetak laporannya
                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat halaman cetak laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
