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
    public partial class FormDatabase : MetroFramework.Forms.MetroForm
    {
        public FormDatabase()
        {
            InitializeComponent();
        }

        private void FormDatabase_Load(object sender, EventArgs e)
        {
            DatabaseHost.Text = Properties.Settings.Default.database_server;
            DatabaseName.Text = Properties.Settings.Default.database_name;
            DatabasePort.Text = Properties.Settings.Default.mysql_port;
            DatabaseUsername.Text = Properties.Settings.Default.username_db;
            DatabasePassword.Text = Properties.Settings.Default.password_db;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Anda Yakin Pengaturan Ini Benar ? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                Properties.Settings.Default.database_server = DatabaseHost.Text;
                Properties.Settings.Default.database_name = DatabaseName.Text;
                Properties.Settings.Default.mysql_port = DatabasePort.Text;
                Properties.Settings.Default.username_db = DatabaseUsername.Text;
                Properties.Settings.Default.password_db = DatabasePassword.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Perubahan Disimpan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            } else
            {
                MessageBox.Show("Silahkan perbaiki pengaturan lagi", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            DatabaseHost.Text = "";
            DatabaseName.Text = "";
            DatabaseUsername.Text = "";
            DatabasePort.Text = "";
            DatabasePassword.Text = "";
        }
    }
}
