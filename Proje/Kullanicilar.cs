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
    public partial class Kullanicilar : Form
    {
        Login sql = new Login();
        public Kullanicilar()
        {
            InitializeComponent();
            Tablo();
        }
        //Tabloya veri çeken metod
        public void Tablo()
        {
            sql.baglanti.Open();
            string kayit = "SELECT kadi, pass FROM Users";
            sql.komut = new SqlCommand(kayit, sql.baglanti);
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            sql.baglanti.Close();
        }
        //Tabloya veri ekleyen metod
        private void button1_Click(object sender, EventArgs e)
        {
            sql.baglanti.Open();
            string ekle = "INSERT INTO Users (kadi, pass) VALUES (@kadi,@pass)";
            sql.komut = new SqlCommand(ekle, sql.baglanti);
            sql.komut.Parameters.AddWithValue("@kadi", textBox1.Text);
            sql.komut.Parameters.AddWithValue("@pass", textBox2.Text);
            sql.komut.ExecuteNonQuery();
            sql.baglanti.Close();
            MessageBox.Show("Kayıt İşlemi Tamamlandı");
            sql.logtut2("{0} {1} Kullanıcısını Ekledi", textBox1.Text);
            Tablo();
        }
        //Tablodan veri silen metod
        private void button2_Click(object sender, EventArgs e)
        {
            sql.baglanti.Open();
            string ara = "SELECT * FROM Users WHERE kadi=@kadi AND pass=@pass";
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.komut.Parameters.AddWithValue("@kadi", textBox1.Text);
            sql.komut.Parameters.AddWithValue("@pass", textBox2.Text);
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            sql.read = sql.komut.ExecuteReader();
            if (sql.read.Read())
            {
                sql.read.Close();
                string sil = "DELETE FROM Users WHERE kadi=@kadi AND pass=@pass";
                sql.komut = new SqlCommand(sil, sql.baglanti);
                sql.komut.Parameters.AddWithValue("@kadi", textBox1.Text);
                sql.komut.Parameters.AddWithValue("@pass", textBox2.Text);
                sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                MessageBox.Show("Kullanıcı Silindi");
                sql.logtut2("{0} {1} Kullanıcısını Sildi", textBox1.Text);
                Tablo();
            }
        }
        //Form kapatıldığında ana sayfaya dönmek için formclosing eventi kullanıyorum
        private void Kullanicilar_FormClosing(object sender, FormClosingEventArgs e)
        { 
            A_Main a_Main = new A_Main();
            this.Hide();
            a_Main.Show();
            e.Cancel= true;
        }
        //Gridwiew üzerinde tıklanan hücrenin verilerini istediğim textboxlara çekmek için kullandığım metod
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
            }
        }
    }
}
