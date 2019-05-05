using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using StokBarangApp.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokBarangApp
{
    public partial class PrintStruk : MetroFramework.Forms.MetroForm
    {
        public string nama = "";
        public PrintStruk(string nama)
        {
            this.nama = nama;
            InitializeComponent();
        }

        private async void printStrukReport()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT id , nama_pembeli , nama_produk , harga_produk , jumlah_penjualan , total_harga , penjualan FROM stok_penjualan WHERE nama_pembeli = ?",
                    new SqlCommand().getConnection()))
                {
                    cmd.Parameters.AddWithValue("nama_pembeli", nama);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "StrukItem");
                        StrukReport strukReport = new StrukReport();
                        strukReport.SetDataSource(dataSet);
                        StrukViewer.ReportSource = strukReport;
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintStruk_Load(object sender, EventArgs e)
        {
            printStrukReport();
        }
    }
}
