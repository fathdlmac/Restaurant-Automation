using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class Aktar : Form
    {
        Login sql = new Login();
        public Aktar()
        {
            InitializeComponent();
        }
        //Masadaki ürünleri başka masaya aktaran metod 
        private void button1_Click(object sender, EventArgs e)
        {
            Masalar tablo = new Masalar();
            sql.baglanti.Open();
            string degisecek = comboBox1.Text;
            string degisen = textBox1.Text;
            string ara = string.Format("UPDATE Masalar SET M_adi = '{0}' WHERE M_adi = '{1}'", degisecek, degisen);
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.komut.ExecuteNonQuery();
            MessageBox.Show("Aktarma Tamamlandı");
            sql.logtut3("{0} {1} deki Ürünleri {2} e Aktardı", degisen, degisecek);
            tablo.Controls["comboBox1"].Text = degisecek;
            if (label1.Text == "Users")
            {
                tablo.Controls["label1"].Text = "Users";
            }
            else
            {
                tablo.Controls["label1"].Text = "Admin";
            }
            this.Hide();
            tablo.Show();
            tablo.Tablo_s();
        }
    }
}
