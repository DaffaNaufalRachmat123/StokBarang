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
    public partial class PrintStokBarang : MetroFramework.Forms.MetroForm
    {
        public PrintStokBarang()
        {
            InitializeComponent();
        }

        private async void InitBarangPrintData()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT id , nama_produk , merek_produk , harga_beli , harga_produk , jumlah_produk , tanggal_beli , nama_supplier FROM stok_barang stok INNER JOIN supplier supp ON stok.kode_supplier = supp.kode_supplier ",
                    new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "DataBarang");
                        ReportBarang barang = new ReportBarang();
                        barang.SetDataSource(dataSet);
                        crystalReportViewer1.ReportSource = barang;
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPembelian_Load(object sender, EventArgs e)
        {
            InitBarangPrintData();
        }
    }
}
