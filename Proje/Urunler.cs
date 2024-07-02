using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class Urunler : Form
    {
        Login sql = new Login();
        DataTable dt = new DataTable();
        public Urunler()
        {
            InitializeComponent();
            Tablo();
            combobox();
            openFileDialog1.Filter = "Image files (*.png) | *.png";
        }
        //Tabloya ürünlerin bilgilerini çeken metod
        public void Tablo()
        {
            sql.baglanti.Open();
            string kayit = "SELECT Id, isim, kateg, fiyat FROM Urunler ORDER BY kateg ASC";
            sql.komut = new SqlCommand(kayit, sql.baglanti);
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            sql.baglanti.Close();
        }
        //Ürünler tablosuna veri ekleyen metod
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sql.baglanti.Open();
                string isim = textBox1.Text;
                string yol = string.Format(@"C:\Proje\Resimler\{0}.png", isim);
                File.Copy(openFileDialog1.FileName, yol, true);
                string ekle = "INSERT INTO Urunler (isim, kateg, fiyat, foto) VALUES (@isim,@kateg,@fiyat,@foto)";
                sql.komut = new SqlCommand(ekle, sql.baglanti);
                sql.komut.Parameters.AddWithValue("@isim", textBox1.Text);
                sql.komut.Parameters.AddWithValue("@kateg", comboBox1.Text);
                sql.komut.Parameters.AddWithValue("@fiyat", textBox3.Text);
                sql.komut.Parameters.AddWithValue("@foto", yol);
                sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                MessageBox.Show("Kayıt İşlemi Tamamlandı");
                sql.logtut2("{0} Ürünler Tablosuna {1} Ürününü Ekledi", textBox1.Text);
                Tablo();
            }
            catch(Exception ex) 
            {
                sql.baglanti.Close();
                MessageBox.Show("Resim Seçmeyi Unuttunuz");
            }
        }
        
        //Ürünler tablosundan veri silen metod
        private void button2_Click(object sender, EventArgs e)
        {
            sql.baglanti.Open();
            string ara = "SELECT * FROM Urunler WHERE Id = @Id";
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.komut.Parameters.AddWithValue("@Id", textBox4.Text);
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            sql.read = sql.komut.ExecuteReader();
            if (sql.read.Read())
            {
                sql.read.Close();
                string sil = "DELETE FROM Urunler WHERE Id = @Id";
                sql.komut = new SqlCommand(sil, sql.baglanti);
                sql.komut.Parameters.AddWithValue("@Id", textBox4.Text);
                sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                MessageBox.Show("Ürün Silindi");
                sql.logtut2("{0} Ürünler Tablosundan {1} Ürünü Silindi", textBox1.Text);
                Tablo();
            }
        }
        //Gridwiew de tıklanan veriyi istediğim nesnelere çektiğim metod 
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox4.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
            }
        }
        //Textboxtaki değer değiştiği anda tablo içerisinde arama yapmamı sağlayan metod
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string search = "SELECT Id,isim,kateg,fiyat FROM Urunler WHERE CONCAT(isim,kateg) LIKE '%" + textBox2.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(search, sql.baglanti);
            DataTable tb = new DataTable();
            da.Fill(tb);
            dataGridView2.DataSource = tb;
        }

        private void combobox()
        {
            sql.baglanti.Open();
            string kayit = "SELECT DISTINCT kateg FROM Urunler";
            sql.komut = new SqlCommand();
            sql.komut.Connection = sql.baglanti;
            sql.komut.CommandText = kayit;
            sql.komut.CommandType= CommandType.Text;


            sql.read = sql.komut.ExecuteReader();
            while (sql.read.Read())
            {
                comboBox1.Items.Add(sql.read["kateg"]);
            }
            sql.baglanti.Close();
        }
        private void Urunler_FormClosing(object sender, FormClosingEventArgs e)
        {
            A_Main a_Main = new A_Main();
            this.Hide();
            a_Main.Show();
            e.Cancel = true;
        }
        //Güncelleme metodu 
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sql.baglanti.Open();
                string isim = textBox1.Text;
                string yol = string.Format(@"C:\Proje\Resimler\{0}.png", isim);
                File.Copy(openFileDialog1.FileName, yol, true);
                string güncelle = "UPDATE Urunler SET isim = @isim, kateg = @kateg, fiyat = @fiyat, foto = @foto WHERE Id = @Id";
                sql.komut = new SqlCommand(güncelle, sql.baglanti);
                sql.komut.Parameters.AddWithValue("@Id", textBox4.Text);
                sql.komut.Parameters.AddWithValue("@isim", textBox1.Text);
                sql.komut.Parameters.AddWithValue("@kateg", comboBox1.Text);
                sql.komut.Parameters.AddWithValue("@fiyat", textBox3.Text);
                sql.komut.Parameters.AddWithValue("@foto", yol);
                MessageBox.Show("Güncelleme Başarılı");
                sql.logtut2("{0} Ürünler Tablosundan {1} Ürünü Güncellendi", textBox1.Text);
                int rowsAffected = sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                Tablo();
            }
            catch(Exception ex) 
            {
                sql.baglanti.Close();
                MessageBox.Show("Resim Seçmeyi Unuttunuz");
            }
        } 
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Fiyat değerine harf girilmemesi için gereken metod
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
