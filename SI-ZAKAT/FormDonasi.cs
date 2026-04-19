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


    }
}
