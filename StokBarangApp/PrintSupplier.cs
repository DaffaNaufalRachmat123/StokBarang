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
    public partial class PrintSupplier : MetroFramework.Forms.MetroForm
    {
        public PrintSupplier()
        {
            InitializeComponent();
        }

        private async void initSupplierData()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM supplier", new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "DataSupplier");
                        ReportSupplier supplier = new ReportSupplier();
                        supplier.SetDataSource(dataSet);
                        SupplierReport.ReportSource = supplier;
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintSupplier_Load(object sender, EventArgs e)
        {
            initSupplierData();
        }
    }
}
