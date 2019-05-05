using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using StokBarangApp.Reports;

namespace StokBarangApp
{
    public partial class Dashboard : MetroFramework.Forms.MetroForm
    {
        public int id = 0;
        public int id_penjualan = 0;
        #region Table Schema Variable
        public string nama = "";
        public string jenis = "";
        public string merek = "";
        public int harga_beli = 0;
        public int harga_produk = 0;
        public int jumlah_produk = 0;
        public int jumlah_beli = 0;
        public int total_penjualan = 0;
        public string tanggal_beli = "";
        public string tanggal_kadaluarsa = "";
        public string supplier_code = "";
        #endregion
        public Dashboard()
        {
            InitializeComponent();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            NamaUsaha.Text = Properties.Settings.Default.NamaUsaha;
            AlamatUsaha.Text = Properties.Settings.Default.AlamatUsaha;
            TeleponUsaha.Text = Properties.Settings.Default.TeleponUsaha;
            EmailUsaha.Text = Properties.Settings.Default.EmailUsaha;
            KeteranganUsaha.Text = Properties.Settings.Default.KeteranganUsaha;
            NamaLabel.Text = Properties.Settings.Default.NamaUsaha;
            AlamatLabel.Text = Properties.Settings.Default.AlamatUsaha;
            TeleponLabel.Text = Properties.Settings.Default.TeleponUsaha;
            EmailLabel.Text = Properties.Settings.Default.EmailUsaha;
            ListrikTeks.Text = Properties.Settings.Default.listrik.ToString();
            AirTeks.Text = Properties.Settings.Default.air.ToString();
            GajiKaryawanTeks.Text = Properties.Settings.Default.karyawan.ToString();
            PengeluaranTeks.Text = Properties.Settings.Default.lainlain.ToString();
            InternetTeks.Text = Properties.Settings.Default.internet.ToString();
            TotalPengeluaran.Text = Properties.Settings.Default.PengeluaranSebulan.ToString();
            NamaPembeliTeks.WaterMark = "Nama Pembeli";
            TotalHargaTeks.WaterMark = "Total Harga";
            DataTable dt = await new SqlCommand().tampil_barang();
            DataTable dta = await new SqlCommand().tampil_penjualan();
            DataTable datas = await new SqlCommand().tampil_user();
            DataTable dts = await new SqlCommand().tampil_supplier();
            metroGrid1.DataSource = dt;
            metroGrid2.DataSource = dta;
            metroGrid3.DataSource = datas;
            metroGrid4.DataSource = dts;
            chart_pembelian();
            chart_penjualan();
            initSupplierCode();
            timer1.Start();
        }
        private async void initSupplierCode()
        {
            DataTable table = await new SqlCommand().getAllSupplierCodes();
            foreach(DataRow rows in table.Rows)
            {
                KodeSupplierTeks.Items.Clear();
                KodeSupplierTeks.Items.Add(rows["kode_supplier"].ToString());
            }
        }
        private bool validate_data(string data)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
                return true;
            else
                return false;
        }

        private void metroGrid1_Click(object sender, EventArgs e)
        {
            Jumlah.Items.Clear();
            if (validate_data(metroGrid1.SelectedRows[0].Cells[0].Value.ToString()))
                id = 0;
            else
                id = int.Parse(metroGrid1.SelectedRows[0].Cells[0].Value.ToString());
            if (validate_data(metroGrid1.SelectedRows[0].Cells[1].Value.ToString()))
                NamaProdukTeks.Text = "";
            else
            {
                NamaProdukTeks.Text = metroGrid1.SelectedRows[0].Cells[1].Value.ToString();
                nama  = metroGrid1.SelectedRows[0].Cells[1].Value.ToString();
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[2].Value.ToString()))
                JenisProdukTeks.Text = "";
            else {
                JenisProdukTeks.Text = metroGrid1.SelectedRows[0].Cells[2].Value.ToString();
                jenis = metroGrid1.SelectedRows[0].Cells[2].Value.ToString();
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[3].Value.ToString()))
                MerekProdukTeks.Text = "";
            else
            {
                MerekProdukTeks.Text = metroGrid1.SelectedRows[0].Cells[3].Value.ToString();
                merek = metroGrid1.SelectedRows[0].Cells[3].Value.ToString();
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[4].Value.ToString()))
                HargaBeliTeks.Text = 0.ToString();
            else
            {
                HargaBeliTeks.Text = metroGrid1.SelectedRows[0].Cells[4].Value.ToString();
                harga_beli = int.Parse(metroGrid1.SelectedRows[0].Cells[4].Value.ToString());
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[5].Value.ToString()))
                HargaProdukTeks.Text = 0.ToString();
            else
                harga_produk = int.Parse(metroGrid1.SelectedRows[0].Cells[5].Value.ToString());
                HargaProdukTeks.Text = metroGrid1.SelectedRows[0].Cells[5].Value.ToString();
            if (validate_data(metroGrid1.SelectedRows[0].Cells[6].Value.ToString()))
                JumlahProdukTeks.Text = 0.ToString();
            else
            {
                JumlahProdukTeks.Text = metroGrid1.SelectedRows[0].Cells[6].Value.ToString();
                jumlah_produk = int.Parse(metroGrid1.SelectedRows[0].Cells[6].Value.ToString());
                if(int.Parse(metroGrid1.SelectedRows[0].Cells[6].Value.ToString()) == 0)
                {
                    NamaPembeliTeks.Enabled = false;
                    Jumlah.Enabled = false;
                    TotalHargaTeks.Enabled = false;
                    JualButton.Enabled = false;
                }
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[6].Value.ToString()))
                Jumlah.Items.Add("0");
            else
            {
                int jumlah = int.Parse(metroGrid1.SelectedRows[0].Cells[6].Value.ToString());
                for(int i = 0; i <= jumlah; i++)
                {
                    Jumlah.Items.Add(i.ToString());
                }
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[7].Value.ToString()))
                JumlahBeliTeks.Text = "";
            else
            {
                JumlahBeliTeks.Text = metroGrid1.SelectedRows[0].Cells[7].Value.ToString();
                jumlah_beli = int.Parse(metroGrid1.SelectedRows[0].Cells[7].Value.ToString());
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[8].Value.ToString()))
                total_penjualan = 0;
            else
            {
                total_penjualan = int.Parse(metroGrid1.SelectedRows[0].Cells[8].Value.ToString());
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[9].Value.ToString()) == false)
            {
                TanggalBeliTeks.Text = metroGrid1.SelectedRows[0].Cells[9].Value.ToString();
                tanggal_beli = metroGrid1.SelectedRows[0].Cells[9].Value.ToString();
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[10].Value.ToString()) == false)
            {
                TanggalKadaluarsaTeks.Text = metroGrid1.SelectedRows[0].Cells[10].Value.ToString();
                tanggal_kadaluarsa = metroGrid1.SelectedRows[0].Cells[10].Value.ToString();
            }
            if (validate_data(metroGrid1.SelectedRows[0].Cells[11].Value.ToString()))
            {
                supplier_code = "";
            } else
            {
                supplier_code = metroGrid1.SelectedRows[0].Cells[11].Value.ToString();
                KodeSupplierTeks.SelectedItem = metroGrid1.SelectedRows[0].Cells[11].Value.ToString();
            }
            Jumlah.Enabled = true;
            UbahButton.Enabled = true;
            HapusButton.Enabled = true;
            JualButton.Enabled = true;
            NamaPembeliTeks.Enabled = true;
        }
        

        private void JumlahProdukTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void HargaProdukTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            if(validate_data(KodeSupplierTeks.SelectedItem.ToString()) == false && validate_data(NamaProdukTeks.Text) == false &&
                validate_data(JenisProdukTeks.Text) == false && validate_data(MerekProdukTeks.Text) == false &&
                validate_data(HargaBeliTeks.Text) == false && validate_data(HargaProdukTeks.Text) == false &&
                validate_data(JumlahProdukTeks.Text) == false)
            {
                new SqlCommand().save_barang(NamaProdukTeks.Text, JenisProdukTeks.Text, MerekProdukTeks.Text, int.Parse(HargaBeliTeks.Text),
                    int.Parse(HargaProdukTeks.Text), int.Parse(JumlahProdukTeks.Text), int.Parse(JumlahBeliTeks.Text),
                    0, TanggalKadaluarsaTeks.Text, TanggalBeliTeks.Text, KodeSupplierTeks.SelectedItem.ToString());
                DeselectAll();
                load_data_to_barang();
            } else
            {
                MessageBox.Show("Tolong Isi Semua Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Jumlah_SelectedValueChanged(object sender, EventArgs e)
        {
            int result = harga_produk * int.Parse(Jumlah.SelectedItem.ToString());
            TotalHargaTeks.Text = result.ToString();
        }

        private void UbahButton_Click(object sender, EventArgs e)
        {
            if (validate_data(KodeSupplierTeks.SelectedItem.ToString()) == false && validate_data(NamaProdukTeks.Text) == false &&
                validate_data(JenisProdukTeks.Text) == false && validate_data(MerekProdukTeks.Text) == false &&
                validate_data(HargaBeliTeks.Text) == false && validate_data(HargaProdukTeks.Text) == false &&
                validate_data(JumlahProdukTeks.Text) == false)
            {
                new SqlCommand().update_barang(NamaProdukTeks.Text, JenisProdukTeks.Text, MerekProdukTeks.Text,
                int.Parse(HargaBeliTeks.Text), int.Parse(HargaProdukTeks.Text), int.Parse(JumlahProdukTeks.Text),
                int.Parse(JumlahBeliTeks.Text), total_penjualan, TanggalBeliTeks.Text, TanggalKadaluarsaTeks.Text, KodeSupplierTeks.SelectedItem.ToString(), id);
                DeselectAll();
                load_data_to_barang();
            } else
            {
                MessageBox.Show("Tolong Isi Semua Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HapusButton_Click(object sender, EventArgs e)
        {
            if(id != 0)
                await Task.Run(() => new SqlCommand().Delete(id));
            load_data_to_barang();
            DeselectAll();
        }

        private void JualButton_Click(object sender, EventArgs e)
        {
            if (id == 0)
                MessageBox.Show("Harap Pilih Dulu Produk nya", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(id != 0)
            {
                if(validate_data(NamaPembeliTeks.Text) == false && validate_data(TotalHargaTeks.Text) == false && int.Parse(Jumlah.SelectedItem.ToString()) != 0)
                {
                    new SqlCommand().save_penjualan(NamaPembeliTeks.Text, int.Parse(TotalHargaTeks.Text),
                        nama, jenis, merek, int.Parse(Jumlah.SelectedItem.ToString()), harga_produk, DateTime.Now.ToString("MM/dd/yyyy"),int.Parse(TotalHargaTeks.Text) - (harga_beli * int.Parse(Jumlah.SelectedItem.ToString())));
                    new SqlCommand().update_barang(nama, jenis, merek, harga_beli, harga_produk, jumlah_produk - int.Parse(Jumlah.SelectedItem.ToString()),
                        jumlah_beli, total_penjualan + int.Parse(Jumlah.SelectedItem.ToString()), tanggal_beli, tanggal_kadaluarsa, supplier_code, id);
                    DialogResult result = MessageBox.Show("Ingin Cetak Struk ? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if(result == DialogResult.Yes)
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject teks = new StrukReport().ReportDefinition.ReportObjects["TeksHeaderStruk"] as TextObject;
                        teks.Text = Properties.Settings.Default.NamaUsaha;
                        new PrintStruk(NamaPembeliTeks.Text).Show();
                    } else
                    {
                        DeselectAll();
                        load_data_to_barang();
                    }
                } else
                {
                    MessageBox.Show("Mohon Isi Nama Pembeli dan Jumlah", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
            DataTable result = await new SqlCommand().tampil_barang();
            metroGrid1.DataSource = result;
        }


        private async void TombolHapus_Click(object sender, EventArgs e)
        {
            if (id_penjualan != 0)
            {
                await Task.Run(() => new SqlCommand().delete_penjualan(id_penjualan));
                metroGrid2.DataSource = await new SqlCommand().tampil_penjualan();
                DeselectPenjualan();
            } else
            {
                MessageBox.Show("[!]Silahkan Pilih Data nya Dulu[!]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tabPage2_Click(object sender, EventArgs e)
        {
            DataTable result = await new SqlCommand().tampil_penjualan();
            metroGrid2.DataSource = result;
        }

        private void HargaBeliTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            metroGrid2.DataSource = await new SqlCommand().tampil_penjualan();
            DeselectPenjualan();
        }

        private void NamaPembeli_Click(object sender, EventArgs e)
        {

        }
        public string nama_pembeli = "";

        private void metroGrid2_Click(object sender, EventArgs e)
        {
            if (validate_data(metroGrid2.SelectedRows[0].Cells[0].Value.ToString()))
                id_penjualan = 0;
            else
                id_penjualan = int.Parse(metroGrid2.SelectedRows[0].Cells[0].Value.ToString());
            if (validate_data(metroGrid2.SelectedRows[0].Cells[1].Value.ToString()))
                NamaPembeli.Text = "";
            else
            {
                NamaPembeli.Text = metroGrid2.SelectedRows[0].Cells[1].Value.ToString();
                nama_pembeli = metroGrid2.SelectedRows[0].Cells[1].Value.ToString();
            }
            if (validate_data(metroGrid2.SelectedRows[0].Cells[2].Value.ToString()))
                TotalHarga.Text = "";
            else
                TotalHarga.Text = metroGrid2.SelectedRows[0].Cells[2].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[3].Value.ToString()))
                NamaProduk.Text = "";
            else
                NamaProduk.Text = metroGrid2.SelectedRows[0].Cells[3].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[4].Value.ToString()))
                JenisProduk.Text = "";
            else
                JenisProduk.Text = metroGrid2.SelectedRows[0].Cells[4].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[5].Value.ToString()))
                MerekProduk.Text = "";
            else
                MerekProduk.Text = metroGrid2.SelectedRows[0].Cells[5].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[6].Value.ToString()))
                JumlahTerjual.Text = "";
            else
                JumlahTerjual.Text = metroGrid2.SelectedRows[0].Cells[6].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[7].Value.ToString()))
                HargaProduk.Text = "0";
            else
                HargaProduk.Text = metroGrid2.SelectedRows[0].Cells[7].Value.ToString();
            if (validate_data(metroGrid2.SelectedRows[0].Cells[8].Value.ToString()) == false)
                TanggalPenjualan.Text = metroGrid2.SelectedRows[0].Cells[8].Value.ToString();
            TombolHapus.Enabled = true;
            PrintBtnJual.Enabled = true;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            DeselectAll();
        }
        private async void load_data_to_barang()
        {
            metroGrid1.DataSource = await new SqlCommand().tampil_barang();
        }
        private void DeselectAll()
        {
            NamaProdukTeks.Text = "";
            JenisProdukTeks.Text = "";
            MerekProdukTeks.Text = "";
            HargaBeliTeks.Text = "";
            HargaProdukTeks.Text = "";
            JumlahProdukTeks.Text = "";
            JumlahBeliTeks.Text = "";
            TanggalKadaluarsaTeks.Text = DateTime.Now.ToString("MM/dd/yyyy");
            metroGrid1.ClearSelection();
            NamaPembeliTeks.Text = "";
            TotalHargaTeks.Text = "";
            UbahButton.Enabled = false;
            HapusButton.Enabled = false;
            JualButton.Enabled = false;
            Jumlah.Items.Clear();
            TanggalBeliTeks.Text = DateTime.Now.ToString("MM/dd/yyyy");
            TotalHargaTeks.Enabled = true;
            Jumlah.Enabled = true;
            NamaPembeliTeks.Enabled = true;
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            DeselectPenjualan();
        }
        private void DeselectPenjualan()
        {
            TombolHapus.Enabled = false;
            id_penjualan = 0;
            NamaPembeli.Text = "";
            TotalHarga.Text = "";
            NamaProduk.Text = "";
            JenisProduk.Text = "";
            MerekProduk.Text = "";
            JumlahTerjual.Text = "";
            HargaProduk.Text = "";
            TanggalPenjualan.Text = DateTime.Now.ToString("MM/dd/yyyy");
            NamaPembeli.Enabled = false;
            TotalHarga.Enabled = false;
            NamaProduk.Enabled = false;
            JenisProduk.Enabled = false;
            MerekProduk.Enabled = false;
            JumlahTerjual.Enabled = false;
            HargaProduk.Enabled = false;
            TanggalPenjualan.Enabled = false;
            PrintBtnJual.Enabled = false;
            metroGrid2.ClearSelection();
        }

        private void JumlahBeliTeks_Click(object sender, EventArgs e)
        {
            
        }

        private void JumlahBeliTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private async void TambahUser_Click(object sender, EventArgs e)
        {
            if(validate_data(UsernameTeks.Text) == false && validate_data(PasswordTeks.Text) == false)
            {
                await Task.Run(() => new SqlCommand().save_user(UsernameTeks.Text, PasswordTeks.Text));
                deselectall();
                metroGrid3.DataSource = await new SqlCommand().tampil_user();
            } else
            {
                MessageBox.Show("Tolong Isi Semua Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int id_user = 0;

        private void metroGrid3_Click(object sender, EventArgs e)
        {
            if (validate_data(metroGrid3.SelectedRows[0].Cells[0].Value.ToString()))
                id_user = 0;
            else
                id_user = int.Parse(metroGrid3.SelectedRows[0].Cells[0].Value.ToString());
            if (validate_data(metroGrid3.SelectedRows[0].Cells[1].Value.ToString()))
                UsernameTeks.Text = "";
            else
                UsernameTeks.Text = metroGrid3.SelectedRows[0].Cells[1].Value.ToString();
            if (validate_data(metroGrid3.SelectedRows[0].Cells[2].Value.ToString()))
                PasswordTeks.Text = "";
            else
                PasswordTeks.Text = new Encryption("StokBarangApp").decrypt(metroGrid3.SelectedRows[0].Cells[2].Value.ToString());
            UsernameTeks.Enabled = true;
            PasswordTeks.Enabled = true;
            UbahUser.Enabled = true;
            HapusUser.Enabled = true;
        }

        private async void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            metroGrid3.DataSource = await new SqlCommand().tampil_user();
        }

        private void deselectall()
        {
            UbahUser.Enabled = false;
            HapusUser.Enabled = false;
            UsernameTeks.Text = "";
            PasswordTeks.Text = "";
            metroGrid3.ClearSelection();
        }

        private async void UbahUser_Click(object sender, EventArgs e)
        {
            if (validate_data(UsernameTeks.Text) == false && validate_data(PasswordTeks.Text) == false)
            {
                await Task.Run(() => new SqlCommand().update_user(UsernameTeks.Text, PasswordTeks.Text, id_user));
                deselectall();
                metroGrid3.DataSource = await new SqlCommand().tampil_user();
            } else
            {
                MessageBox.Show("Tolong Isi Semua Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HapusUser_Click(object sender, EventArgs e)
        {
            if (id_user != 0)
                await Task.Run(() => new SqlCommand().delete_user(id_user));
            else
                MessageBox.Show("Silahkan Pilih Datanya", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            deselectall();
            metroGrid3.DataSource = await new SqlCommand().tampil_user();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            deselectall();
        }
        #region charts
        private async void chart_pembelian()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) , tanggal_beli FROM stok_barang GROUP by tanggal_beli" , new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        foreach(DataRow rows in table.Rows)
                        {
                            GrafikBeli.Series["Pembelian"].Points.AddXY(rows["tanggal_beli"].ToString(), int.Parse(rows["COUNT(*)"].ToString()));
                            BeliCombo.Items.Add(rows["tanggal_beli"].ToString());
                            BeliComboSecond.Items.Add(rows["tanggal_beli"].ToString());
                        }
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show("Chart Failed To Load : " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void chart_penjualan()
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) , penjualan FROM stok_penjualan GROUP BY penjualan", new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable tables = new DataTable();
                        adapter.Fill(tables);
                        foreach (DataRow rows in tables.Rows)
                        {
                            GrafikJual.Series["Penjualan"].Points.AddXY(rows["penjualan"].ToString(), int.Parse(rows["COUNT(*)"].ToString()));
                            JualCombo.Items.Add(rows["penjualan"].ToString());
                            JualComboSecond.Items.Add(rows["penjualan"].ToString());
                        }
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show("Chart Load failed : " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void chart_pembelian_tanggal(string firstDate , string secondDate)
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) , tanggal_beli FROM stok_barang GROUP BY tanggal_beli HAVING tanggal_beli BETWEEN '" + firstDate + "' AND '" + secondDate + "';",
                    new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        foreach(DataRow rows in table.Rows)
                        {
                            GrafikBeli.Series["Pembelian"].Points.AddXY(rows["tanggal_beli"].ToString(), int.Parse(rows["COUNT(*)"].ToString()));
                        }
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show("Chart failed load : " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void chart_penjualan_tanggal(string firstDate , string secondDate)
        {
            try
            {
                await new SqlCommand().getConnection().OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) , penjualan FROM stok_penjualan GROUP BY penjualan HAVING penjualan BETWEEN '" + firstDate + "' AND '" + secondDate + "';",
                        new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        foreach(DataRow rows in table.Rows)
                        {
                            GrafikJual.Series["Penjualan"].Points.AddXY(rows["penjualan"].ToString(), int.Parse(rows["COUNT(*)"].ToString()));
                        }
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            GrafikBeli.Series["Pembelian"].Points.Clear();
            BeliCombo.Items.Clear();
            BeliComboSecond.Items.Clear();
            chart_pembelian();
        }

        private void RefreshChartJualBtn_Click(object sender, EventArgs e)
        {
            GrafikJual.Series["Penjualan"].Points.Clear();
            JualCombo.Items.Clear();
            JualComboSecond.Items.Clear();
            chart_penjualan();
        }

        private void FindRangeBeli_Click(object sender, EventArgs e)
        {
            GrafikBeli.Series["Pembelian"].Points.Clear();
            chart_pembelian_tanggal(BeliCombo.SelectedItem.ToString(), BeliComboSecond.SelectedItem.ToString());
        }

        private void FindRangeJual_Click(object sender, EventArgs e)
        {
            GrafikJual.Series["Penjualan"].Points.Clear();
            chart_penjualan_tanggal(JualCombo.SelectedItem.ToString(), JualComboSecond.SelectedItem.ToString());
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            TextObject teksView = new ReportBarang().ReportDefinition.ReportObjects["TeksHeaderBarang"] as TextObject;
            teksView.Text = Properties.Settings.Default.NamaUsaha;
            new PrintStokBarang().Show();
        }
        public string kode_supplier = "";

        private void metroGrid4_Click(object sender, EventArgs e)
        {
            if (validate_data(metroGrid4.SelectedRows[0].Cells[0].Value.ToString()))
                KodeSupplier.Text = "";
            else
            {
                KodeSupplier.Text = metroGrid4.SelectedRows[0].Cells[0].Value.ToString();
                supplier_code = metroGrid4.SelectedRows[0].Cells[0].Value.ToString();
            }
            if (validate_data(metroGrid4.SelectedRows[0].Cells[1].Value.ToString()))
                NamaSupplier.Text = "";
            else
                NamaSupplier.Text = metroGrid4.SelectedRows[0].Cells[1].Value.ToString();
            if (validate_data(metroGrid4.SelectedRows[0].Cells[2].Value.ToString()))
                AlamatSupplier.Text = "";
            else
                AlamatSupplier.Text = metroGrid4.SelectedRows[0].Cells[2].Value.ToString();
            if (validate_data(metroGrid4.SelectedRows[0].Cells[3].Value.ToString()))
                TeleponSupplier.Text = "";
            else
                TeleponSupplier.Text = metroGrid4.SelectedRows[0].Cells[3].Value.ToString();
            if (validate_data(metroGrid4.SelectedRows[0].Cells[4].Value.ToString()))
                EmailSupplier.Text = "";
            else
                EmailSupplier.Text = metroGrid4.SelectedRows[0].Cells[4].Value.ToString();
            if (validate_data(metroGrid4.SelectedRows[0].Cells[5].Value.ToString()))
                TanggalDaftar.Text = DateTime.Now.ToString("MM/dd/yyyy");
            else
                TanggalDaftar.Text = metroGrid4.SelectedRows[0].Cells[5].Value.ToString();
            kode_supplier = metroGrid4.SelectedRows[0].Cells[0].Value.ToString();
            KodeSupplier.Enabled = false;
            UbahSup.Enabled = true;
            HapusSup.Enabled = true;
        }

        private async void TambahSup_Click(object sender, EventArgs e)
        {
            if(validate_data(KodeSupplier.Text) == false && validate_data(NamaSupplier.Text) == false &&
                validate_data(AlamatSupplier.Text) == false && validate_data(TeleponSupplier.Text) == false &&
                validate_data(EmailSupplier.Text) == false)
            {
                await Task.Run(() => new SqlCommand().save_supplier(KodeSupplier.Text, NamaSupplier.Text, AlamatSupplier.Text, TeleponSupplier.Text,
                    EmailSupplier.Text, TanggalDaftar.Text));
                load_data_supplier();
                initSupplierCode();
                clearall();
            } else
            {
                MessageBox.Show("Tolong Isi Semua Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void load_data_supplier()
        {
            metroGrid4.DataSource = await new SqlCommand().tampil_supplier();
        }

        private void clearall()
        {
            HapusSup.Enabled = false;
            UbahSup.Enabled = false;
            KodeSupplier.ResetText();
            NamaSupplier.ResetText();
            AlamatSupplier.ResetText();
            TeleponSupplier.ResetText();
            EmailSupplier.ResetText();
            TanggalDaftar.Text = DateTime.Now.ToString("MM/dd/yyyy");
            KodeSupplier.Enabled = true;
            metroGrid4.ClearSelection();
        }

        private async void UbahSup_Click(object sender, EventArgs e)
        {
            if (validate_data(KodeSupplier.Text) == false && validate_data(NamaSupplier.Text) == false &&
                validate_data(AlamatSupplier.Text) == false && validate_data(TeleponSupplier.Text) == false &&
                validate_data(EmailSupplier.Text) == false)
            {
                await Task.Run(() => new SqlCommand().update_supplier(NamaSupplier.Text, AlamatSupplier.Text,
                    TeleponSupplier.Text, EmailSupplier.Text, TanggalDaftar.Text, supplier_code));
                load_data_supplier();
                initSupplierCode();
                clearall();
            } else
            {
                MessageBox.Show("Tolong isi semua data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HapusSup_Click(object sender, EventArgs e)
        {
            if (kode_supplier != "")
                await Task.Run(() => new SqlCommand().delete_supplier(kode_supplier));
            else
                MessageBox.Show("Silahkan Pilih Datanya Dulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            load_data_supplier();
            initSupplierCode();
            clearall();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            clearall();
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            load_data_supplier();
            initSupplierCode();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            new PrintPenjualan().Show();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            new PrintPembelian().Show();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            if (validate_data(nama_pembeli))
                MessageBox.Show("Silahkan Pilih Data Yang Mau Di Print", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                new PrintStruk(nama_pembeli).Show();
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            new PrintSupplier().Show();
        }
        #region ChartLaporan
        private void initUntungHarian()
        {
            try
            {
                new SqlCommand().getConnection().Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT SUM(jumlah_penjualan) , SUM(total_untung) FROM stok_penjualan WHERE penjualan = '" + DateTime.Now.ToString("MM/dd/yyyy") + "'",
                    new SqlCommand().getConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        DataRow rows = table.Rows[0];
                        int total = int.Parse(rows["SUM(total_untung)"].ToString());
                        int jumlah = int.Parse(rows["SUM(jumlah_penjualan)"].ToString());
                        LabelUntungHari.Text = "Rp " + total.ToString() + " dari " + jumlah.ToString() + " penjualan";
                        LabelUntungHari.ForeColor = Color.Green;
                    }
                }
            } catch
            {
                LabelUntungHari.Text = "Tak Ada Penjualan Hari Ini";
                LabelUntungHari.ForeColor = Color.Red;
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void HitungTotalButton_Click(object sender, EventArgs e)
        {
            int listrik = 0;
            int air = 0;
            int karyawan = 0;
            int lainlain = 0;
            int internet = 0;
            if (validate_data(ListrikTeks.Text) == true)
            {
                listrik = 0;
                ListrikTeks.Text = "0";
            } else
            {
                listrik = int.Parse(ListrikTeks.Text);
                Properties.Settings.Default.listrik = listrik;
            }
            if(validate_data(AirTeks.Text) == true)
            {
                air = 0;
                AirTeks.Text = "0";
            } else
            {
                air = int.Parse(AirTeks.Text);
                Properties.Settings.Default.air = air;
            }
            if (validate_data(GajiKaryawanTeks.Text))
            {
                karyawan = 0;
                GajiKaryawanTeks.Text = "0";
            } else
            {
                karyawan = int.Parse(GajiKaryawanTeks.Text);
                Properties.Settings.Default.karyawan = karyawan;
            }
            if (validate_data(PengeluaranTeks.Text))
            {
                lainlain = 0;
                PengeluaranTeks.Text = "0";
            } else
            {
                lainlain = int.Parse(PengeluaranTeks.Text);
                Properties.Settings.Default.lainlain = lainlain;
            }
            if (validate_data(InternetTeks.Text))
            {
                InternetTeks.Text = "0";
                internet = 0;
            } else
            {
                internet = int.Parse(InternetTeks.Text);
                Properties.Settings.Default.internet = internet;
            }
            int result = listrik + air + karyawan + lainlain + internet;
            TotalPengeluaran.Text = result.ToString();
            Properties.Settings.Default.PengeluaranSebulan = result;
            Properties.Settings.Default.Save();
        }

        private void metroButton10_Click_1(object sender, EventArgs e)
        {
            ChartUntungRugi.Series["Laporan"].Points.Clear();
            loadDataUntungToChart();
        }
        public string hasil = null;

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void JumlahProdukTeks_Click(object sender, EventArgs e)
        {

        }

        private void ListrikTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void AirTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GajiKaryawanTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void InternetTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void PengeluaranTeks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TotalPengeluaran_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            initUntungHarian();
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            if(DateTime.Now.Month == DateTime.Now.AddDays(1).Month == false)
            {
                initUntungHarian();
                showStatusAndSaveData();
                loadDataUntungToChart();
            } else
            {
                MessageBox.Show("Belum Akhir Bulan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void showStatusAndSaveData()
        {
            try
            {
                if(DateTime.Now.Month == DateTime.Now.AddDays(1).Month == false)
                {
                    int results = 0;
                    string stats = null;
                    new SqlCommand().getConnection().Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM stok_penjualan ORDER BY penjualan ASC", new SqlCommand().getConnection()))
                    {
                        int totalUntung = 0;
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            totalUntung = 0;
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            foreach(DataRow rows in table.Rows)
                            {
                                if(DateTime.Now.Month == DateTime.ParseExact(rows["penjualan"].ToString() , "MM/dd/yyyy" , null).Month)
                                {
                                    totalUntung += int.Parse(rows["total_untung"].ToString());
                                }
                            }
                        }
                        int result = totalUntung - Properties.Settings.Default.PengeluaranSebulan;
                        if(result < 0)
                        {
                            results = result;
                            stats = "rugi";
                            StatusBulan.Text = "Rp " + result.ToString() + " rugi ";
                            StatusBulan.ForeColor = Color.Red;
                        } else if(result == 0)
                        {
                            results = result;
                            stats = "tidak untung dan rugi";
                            StatusBulan.Text = "Rp " + result.ToString() + " tidak untung dan rugi ";
                            StatusBulan.ForeColor = Color.Blue;
                        } else if(result > 0)
                        {
                            results = result;
                            stats = "untung";
                            StatusBulan.Text = "Rp " + result.ToString() + " untung ";
                            StatusBulan.ForeColor = Color.Green;
                        }
                    }
                    new SqlCommand().connection.Close();
                    save_laporan(DateTime.Now.Month.ToString(), results, stats);
                }
            } catch(Exception e)
            {
                StatusBulan.Text = e.Message;
            }
        }
        public static string strConnect = "Server = " + Properties.Settings.Default.database_server + "; port = " + Properties.Settings.Default.mysql_port + "; database = " + Properties.Settings.Default.database_name + "; Uid = " + Properties.Settings.Default.username_db + "; Pwd = " + Properties.Settings.Default.password_db + ";";
        public MySqlConnection connections = new MySqlConnection(strConnect);
        private void save_laporan(string bulan , int total_semua , string status)
        {
            try
            {
                connections.Open();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO untung_rugi_bulanan (bulan , total_semua , status) VALUES (?,?,?)",connections))
                {
                    cmd.Parameters.AddWithValue("bulan", bulan);
                    cmd.Parameters.AddWithValue("total_semua", total_semua);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.ExecuteNonQuery();
                }
                connections.Close();
            } catch (Exception e)
            {
                label43.Text = e.Message.ToString();
            }
        }
        private void delete_laporan(string bulan)
        {
            try
            {
                connections.Open();
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM untung_rugi_bulanan WHERE bulan = '" + bulan + "'", connections))
                {
                    cmd.ExecuteNonQuery();
                }
                connections.Close();
            } catch (Exception e)
            {
                label43.Text = e.Message;
            }
        }
        private void loadDataUntungToChart()
        {
            ChartUntungRugi.Series["Laporan"].Points.Clear();
            try
            {
                if(DateTime.Now.Month == DateTime.Now.AddDays(1).Month == false)
                {
                    new SqlCommand().getConnection().Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM untung_rugi_bulanan", new SqlCommand().getConnection()))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            foreach(DataRow rows in table.Rows)
                            {
                                ChartUntungRugi.Series["Laporan"].Points.AddXY(rows["bulan"].ToString(), int.Parse(rows["total_semua"].ToString()));
                            }
                        }
                    }
                    new SqlCommand().getConnection().Close();
                    delete_laporan(DateTime.Now.Month.ToString());
                } else
                {
                    ChartUntungRugi.Series["Laporan"].Points.AddXY("Belom Ada Laporan", 0);
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            initUntungHarian();
            showStatusAndSaveData();
            loadDataUntungToChart();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            string nama = null;
            string alamat = null;
            string telepon = null;
            string email = null;
            string keterangan = null;
            if (validate_data(NamaUsaha.Text))
                nama = "";
            else
                nama = NamaUsaha.Text;
            if (validate_data(AlamatUsaha.Text))
                alamat = null;
            else
                alamat = AlamatUsaha.Text;
            if (validate_data(TeleponUsaha.Text))
                telepon = "";
            else
                telepon = TeleponUsaha.Text;
            if (validate_data(EmailUsaha.Text))
                email = "";
            else
                email = EmailUsaha.Text;
            if (validate_data(KeteranganUsaha.Text))
                keterangan = "";
            else
                keterangan = KeteranganUsaha.Text;
            Properties.Settings.Default.NamaUsaha = nama;
            Properties.Settings.Default.AlamatUsaha = alamat;
            Properties.Settings.Default.TeleponUsaha = telepon;
            Properties.Settings.Default.EmailUsaha = email;
            Properties.Settings.Default.KeteranganUsaha = keterangan;
            Properties.Settings.Default.Save();
            NamaLabel.Text = Properties.Settings.Default.NamaUsaha;
            AlamatLabel.Text = Properties.Settings.Default.AlamatUsaha;
            TeleponLabel.Text = Properties.Settings.Default.TeleponUsaha;
            EmailLabel.Text = Properties.Settings.Default.EmailUsaha;
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
