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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            usernameTeks.WaterMark = "Username";
            passwordTeks.WaterMark = "Password";
            
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            new Register().Show();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            int code = new SqlCommand().Login(usernameTeks.Text, new Encryption("StokBarangApp").encrypt(passwordTeks.Text));
            if(code == 1)
            {
                MessageBox.Show("[+]Login Berhasil[+]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            } else
            {
                MessageBox.Show("[-]Login Gagal[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormDatabase().Show();
        }
    }
}
