using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SI_ZAKAT
{
    public partial class FormDashboard : Form
    {
        private string connectionString = @"Data Source=AZIZAH\AZIZAH;Initial Catalog=DB_SIZAKAT;Integrated Security=True";
        private List<PieSlice> pieSlices = new List<PieSlice>();

        private class PieSlice
        {
            public string Label { get; set; }
            public double Value { get; set; }
            public float StartAngle { get; set; }
            public float SweepAngle { get; set; }
            public Color Color { get; set; }
        }

        private readonly Color[] pieColors = new Color[]
        {
            Color.FromArgb(70, 130, 180), Color.FromArgb(255, 165, 0),
            Color.FromArgb(220, 50, 50),  Color.FromArgb(60, 179, 113),
            Color.FromArgb(30, 100, 160), Color.FromArgb(169, 169, 169),
            Color.FromArgb(25, 25, 112)
        };

        private ToolTip pieTooltip = new ToolTip();
        private PieSlice lastHoveredSlice = null;

        // Flag: apakah data sudah pernah di-load? Cegah reload berulang saat Back
        private bool isDashboardLoaded = false;

        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            SetupPieChartPanel();

            // Hanya load SEKALI saat pertama kali form dibuka
            if (!isDashboardLoaded)
            {
                RefreshDashboardData();
                isDashboardLoaded = true;
            }

            btnDataWarga.BringToFront();
            btnDonasi.BringToFront();
            btnValidasiDonasi.BringToFront();
            btnStokSembako.BringToFront();
            btnPenyaluran.BringToFront();
            btnLogout.BringToFront();
        }

        // =====================================================================
        // STATISTIK - Error ditampilkan SILENT (tidak pop-up) agar tidak ganggu navigasi
        // =====================================================================
        private void LoadDashboardStatistics()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(jumlah),0) FROM Tabel_Donasi WHERE status='Approved'", conn))
                    {
                        double dana = Convert.ToDouble(cmd.ExecuteScalar());
                        lblTotalDana.Text = "Rp " + dana.ToString("N0");
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tabel_Warga", conn))
                        lblTotalWarga.Text = cmd.ExecuteScalar().ToString() + " Orang";

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(jumlah_stok),0) FROM Tabel_Stok", conn))
                        lblTotalStokBarang.Text = cmd.ExecuteScalar().ToString() + " Item";

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tabel_Penyuluran", conn))
                            lblTotalSalur.Text = cmd.ExecuteScalar().ToString() + " Transaksi";
                    }
                    catch
                    {
                        lblTotalSalur.Text = "0"; // Silent fallback, tidak pop-up
                    }
                }
                catch (Exception ex)
                {

                    if (!isDashboardLoaded)
                        MessageBox.Show("Gagal memuat statistik:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================================
        // PIE CHART
        // =====================================================================
        private void LoadPieChartData()
        {
            pieSlices.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT ISNULL(kategori,'Lainnya') AS Kategori, SUM(jumlah) AS Total
                                     FROM Tabel_Donasi WHERE status='Approved'
                                     GROUP BY kategori ORDER BY Total DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int ci = 0;
                        while (reader.Read())
                            pieSlices.Add(new PieSlice
                            {
                                Label = reader["Kategori"].ToString(),
                                Value = Convert.ToDouble(reader["Total"]),
                                Color = pieColors[ci++ % pieColors.Length]
                            });
                    }
                }
                catch
                {
                    // Fallback dummy — silent, tidak pop-up
                    string[] labs = { "Zakat Fitrah", "Zakat Maal", "Infaq", "Sedekah" };
                    double[] vals = { 400000, 250000, 150000, 100000 };
                    for (int i = 0; i < labs.Length; i++)
                        pieSlices.Add(new PieSlice { Label = labs[i], Value = vals[i], Color = pieColors[i % pieColors.Length] });
                }
            }
            CalculatePieAngles();
            if (panelPieChart != null) panelPieChart.Invalidate();
        }

        private void CalculatePieAngles()
        {
            double total = pieSlices.Sum(s => s.Value);
            if (total == 0) return;
            float angle = -90f;
            foreach (var s in pieSlices)
            {
                s.StartAngle = angle;
                s.SweepAngle = (float)(s.Value / total * 360.0);
                angle += s.SweepAngle;
            }
        }

        private void SetupPieChartPanel()
        {
            if (panelPieChart == null) return;
            panelPieChart.BackColor = Color.White;
            panelPieChart.Paint -= PanelPieChart_Paint;
            panelPieChart.MouseClick -= PanelPieChart_MouseClick;
            panelPieChart.MouseMove -= PanelPieChart_MouseMove;
            panelPieChart.Paint += PanelPieChart_Paint;
            panelPieChart.MouseClick += PanelPieChart_MouseClick;
            panelPieChart.MouseMove += PanelPieChart_MouseMove;
        }

        private void PanelPieChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (pieSlices.Count == 0) return;
            int pieSize = Math.Min(panelPieChart.Width, panelPieChart.Height) - 40;
            if (pieSize < 10) return;
            Rectangle pieRect = new Rectangle(10, (panelPieChart.Height - pieSize) / 2, pieSize, pieSize);
            foreach (var slice in pieSlices)
            {
                using (SolidBrush b = new SolidBrush(slice.Color))
                    g.FillPie(b, pieRect, slice.StartAngle, slice.SweepAngle);
                using (Pen p = new Pen(Color.White, 2))
                    g.DrawPie(p, pieRect, slice.StartAngle, slice.SweepAngle);
            }
            DrawPieLegend(g, pieRect);
        }

        private void DrawPieLegend(Graphics g, Rectangle pieRect)
        {
            int lx = pieRect.Right + 15, ly = 15;
            double total = pieSlices.Sum(s => s.Value);
            Font f = new Font("Segoe UI", 8f);
            foreach (var slice in pieSlices)
            {
                g.FillRectangle(new SolidBrush(slice.Color), lx, ly, 13, 13);
                double pct = total > 0 ? slice.Value / total * 100 : 0;
                g.DrawString($"{slice.Label} ({pct:F1}%)", f, Brushes.Black, lx + 18, ly);
                ly += 20;
            }
            f.Dispose();
        }

        private void PanelPieChart_MouseClick(object sender, MouseEventArgs e)
        {
            var slice = GetSliceAtPoint(e.Location);
            if (slice != null)
            {
                double total = pieSlices.Sum(s => s.Value);
                MessageBox.Show(
                    $"Kategori   : {slice.Label}\nTotal      : Rp {slice.Value:N0}\nPersentase : {(slice.Value / total * 100):F2}%",
                    "Detail Donasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PanelPieChart_MouseMove(object sender, MouseEventArgs e)
        {
            var hovered = GetSliceAtPoint(e.Location);
            if (hovered != lastHoveredSlice)
            {
                lastHoveredSlice = hovered;
                if (hovered != null)
                {
                    double total = pieSlices.Sum(s => s.Value);
                    pieTooltip.SetToolTip(panelPieChart, $"{hovered.Label}: Rp {hovered.Value:N0} ({(hovered.Value / total * 100):F1}%)");
                    panelPieChart.Cursor = Cursors.Hand;
                }
                else
                {
                    pieTooltip.SetToolTip(panelPieChart, "");
                    panelPieChart.Cursor = Cursors.Default;
                }
            }
        }

        private PieSlice GetSliceAtPoint(Point pt)
        {
            if (pieSlices.Count == 0) return null;
            int pieSize = Math.Min(panelPieChart.Width, panelPieChart.Height) - 40;
            if (pieSize < 10) return null;
            float cx = 10 + pieSize / 2f, cy = ((panelPieChart.Height - pieSize) / 2) + pieSize / 2f, radius = pieSize / 2f;
            float dx = pt.X - cx, dy = pt.Y - cy;
            if ((float)Math.Sqrt(dx * dx + dy * dy) > radius) return null;
            float angle = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
            foreach (var slice in pieSlices)
            {
                float start = slice.StartAngle, end = start + slice.SweepAngle, a = angle;
                while (a < start) a += 360;
                while (a >= start + 360) a -= 360;
                if (a >= start && a <= end) return slice;
            }
            return null;
        }


        private void RefreshDashboardData()
        {
            LoadDashboardStatistics();
            LoadPieChartData();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            RefreshDashboardData();
            MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      
        private void NavigateTo(Form form)
        {
            this.Hide();
            form.ShowDialog();
            if (form.DialogResult != DialogResult.OK)
                Application.Exit();
            else
                this.Show();
        }

        private void btnDataWarga_Click(object sender, EventArgs e) => NavigateTo(new FormDataWarga());
        private void btnDonasi_Click(object sender, EventArgs e) => NavigateTo(new FormDonasi());
        private void btnValidasiDonasi_Click(object sender, EventArgs e) => NavigateTo(new FormValidasi());
        private void btnStokSembako_Click(object sender, EventArgs e) => NavigateTo(new FormStokSembako());
        private void btnPenyaluran_Click(object sender, EventArgs e) => NavigateTo(new FormPenyaluran());

        // =====================================================================
        // LOGOUT
        // =====================================================================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show(
                "Apakah Anda yakin ingin keluar dari aplikasi SI-ZAKAT?",
                "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                this.Hide();
                FormLogin fl = new FormLogin();
                fl.ShowDialog();
                this.Close();
            }
        }
    }
}