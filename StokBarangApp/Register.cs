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
    public partial class Register : MetroFramework.Forms.MetroForm
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            userTeks.WaterMark = "Username";
            pwdTeks.WaterMark = "Password";
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string password = new Encryption("StokBarangApp").encrypt(pwdTeks.Text);
            new SqlCommand().Register(userTeks.Text, password);
        }
    }
}
