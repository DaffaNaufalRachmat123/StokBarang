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
    public partial class PrintPembelian : MetroFramework.Forms.MetroForm
    {
        public PrintPembelian()
        {
            InitializeComponent();
        }

        private async void printpembelian()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT nama_produk , jenis_produk , merek_produk , harga_beli , jumlah_beli , tanggal_beli FROM stok_barang",
                    new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "DataBeli");
                        ReportPembelian beli = new ReportPembelian();
                        beli.SetDataSource(dataSet);
                        ReportViewerBeli.ReportSource = beli;
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPembelian_Load(object sender, EventArgs e)
        {
            printpembelian();
        }
    }
}
