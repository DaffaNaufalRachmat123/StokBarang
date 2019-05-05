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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            metroGrid1.DataSource = await new SqlCommand().tampil_barang();
        }

        private void metroGrid1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(metroGrid1.SelectedRows[0].Cells[0].Value.ToString()))
            {
                MessageBox.Show("is null or empty");
            } else if(string.IsNullOrWhiteSpace(metroGrid1.SelectedRows[0].Cells[0].Value.ToString()))
            {
                MessageBox.Show("is null or white space");
            }
        }
    }
}
