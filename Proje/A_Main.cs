using Microsoft.VisualBasic.Logging;
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
    public partial class A_Main : Form
    {
        Login sql = new Login();
        DataTable dt = new DataTable();
        public A_Main()
        {
            InitializeComponent();
            //Butonların özelliklerini for içerisinde değiştirebilmek için diziye atıyorum
            Button[] butonlar = { button5, Masa1, Masa2, Masa3, Masa4, Masa5, Masa6, Masa7, Masa8 };
            //Buton sayısı kadar döngü yapan for döngüsü
            for (int x = 1; x < butonlar.Length; x++)
            {
                sql.baglanti.Open();
                //Masanın içerisinde ürün olup olmadığını kontrol ediyorum
                string koşul = string.Format("SELECT * FROM Masalar WHERE M_adi = 'Masa{0}'", x);
                sql.komut = new SqlCommand(koşul, sql.baglanti);
                sql.read = sql.komut.ExecuteReader();
                //Varsa rengini kırmızı yoksa yeşil yapıyorum
                if (sql.read.Read())
                {
                    butonlar[x].BackColor = Color.Red;
                    sql.baglanti.Close();
                }
                else
                {
                    butonlar[x].BackColor = Color.Green;
                    sql.baglanti.Close();
                }
            }
        }
        //Butonlara tıklandığında gerekli formu açmasını sağlayan click metodları
        public void button1_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Ürünler Tablosu Açıldı");
            Urunler urun = new Urunler();
            urun.Show();   
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Kullanıcı Ekleme Tablosu Açıldı");
            Kullanicilar kullanici = new Kullanicilar();
            kullanici.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 1 Açıldı");
            Masalar masa = new Masalar();
            //Masalar class'ındaki combobox1 nesnesine erişerek text değerini istediğim gibi değişiyorum
            masa.Controls["comboBox1"].Text = "Masa1";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 2 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa2";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 3 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa3";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 4 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa4";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 5 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa5";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 6 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa6";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 7 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa7";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            sql.logtut1("{0} Masa 8 Açıldı");
            Masalar masa = new Masalar();
            masa.Controls["comboBox1"].Text = "Masa8";
            masa.Controls["label1"].Text = label1.Text;
            masa.Tablo_s();
            masa.Show();
            this.Hide();
        }

        private void A_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
