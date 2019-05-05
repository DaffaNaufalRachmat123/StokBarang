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
    public partial class PrintPenjualan : MetroFramework.Forms.MetroForm
    {
        public PrintPenjualan()
        {
            InitializeComponent();
        }

        private async void printPenjualan()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT nama_pembeli , nama_produk , harga_produk , jumlah_penjualan , total_harga , penjualan FROM stok_penjualan",
                    new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "DataJual");
                        ReportJual jual = new ReportJual();
                        jual.SetDataSource(dataSet);
                        ReportViewerJual.ReportSource = jual;
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPenjualan_Load(object sender, EventArgs e)
        {
            printPenjualan();
        }
    }
}
