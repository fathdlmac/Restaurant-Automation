using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.PortableExecutable;
using System.Configuration;

namespace Proje
{
    public partial class Masalar : Form
    {
        Login sql = new Login();
        private Bitmap _image;

        public Masalar()
        {
            InitializeComponent();
            Tablo_s();
        }
        //ürünleri listwiew'e çektiğim metod gridwiew'e yaptığım çekimlerden daha farklı olmasının sebebi tamamen görsel olarak daha hoş gözükmesini istediğimden
        private void urunGoster(string koşul)
        {
            sql.baglanti.Open();
            string komut = koşul;
            DataTable urun = new DataTable();
            SqlCommand deneme = new SqlCommand(komut, sql.baglanti);
            SqlDataReader read = deneme.ExecuteReader();
            urun.Load(read);
            for (int urunIndex = 0; urunIndex < urun.Rows.Count; ++urunIndex)
                {
                using (Image myImage = Image.FromFile(urun.Rows[urunIndex]["foto"].ToString()))
                {
                    ımageList1.Images.Add(urun.Rows[urunIndex]["Id"].ToString(), myImage);
                }
                ListViewItem item = new ListViewItem(urun.Rows[urunIndex]["isim"].ToString(), urun.Rows[urunIndex]["Id"].ToString());
                item.SubItems.Add(urun.Rows[urunIndex]["fiyat"].ToString());
                listView1.Items.Insert(0, item);
            }
            sql.baglanti.Close();
        }
        //Masa içerisindeki ürünleri çektiğim metod
        public void Tablo_s()
        {
            sql.baglanti.Open();
            string a = comboBox1.Text;
            string kayit = string.Format("SELECT Id, M_adi, M_ürün, M_fiyat FROM Masalar WHERE M_adi = '{0}'", a);
            sql.komut = new SqlCommand(kayit, sql.baglanti);
            sql.komut.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            sql.baglanti.Close();
        }
        private void Masalar_FormClosing(object sender, FormClosingEventArgs e)
        {
            A_Main a_Main = new A_Main();
            sql.baglanti.Close();
            if (label1.Text == "Users")
            {
                a_Main.Controls["button1"].Visible = false;
                a_Main.Controls["button2"].Visible = false;
                a_Main.Controls["label1"].Text = "Users";
            }
            else
            {
                a_Main.Controls["label1"].Text = "Admin";
            }
            this.Hide();
            a_Main.Show();
            e.Cancel = true;
        }
        //Burda menüleri tek tek butonlara tıklayarak çekiyorum urunGoster metoduna koşulu gönderip çalıştırıyorum
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'Menüler'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'KMenüler'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'Burgerler'");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'CLezzetler'");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'Tatlılar'");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            urunGoster("SELECT Id, isim, fiyat, foto FROM Urunler WHERE kateg = 'İçecekler'");
        }
        //Listwiew de bir ürünün üzerine bir ker tıklandığında tıklanan ürünün değerlerini belli yerlere kaydediyoruz daha sonra kaydedilen değerleri Masalar tablosuna aktarıyoruz
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            A_Main a = new A_Main();
            ListViewItem tiklananOge = listView1.SelectedItems[0];
            string b = comboBox1.Text;
            string deger1 = tiklananOge.Text;
            string deger2 = tiklananOge.SubItems[1].Text;
            sql.baglanti.Open();
            string ekle = string.Format("INSERT INTO Masalar (M_adi, M_ürün, M_fiyat) VALUES ('{0}','{1}','{2}')",b ,deger1, deger2 );
            sql.logtut3("{0} {1} e {2} Ürününü Ekledi",  b, deger1);
            sql.komut = new SqlCommand(ekle, sql.baglanti);
            sql.komut.ExecuteNonQuery();
            sql.baglanti.Close();
            Tablo_s();
        }
        //Burda ürünü masadan silmek için ürünün Id sini kaydediyorum
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
            }
        }
        //Ürünü masanın içerisinden silmek için kullandığım metod
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sql.baglanti.Close();
            sql.baglanti.Open();
            string ara = "SELECT * FROM Masalar WHERE Id = @Id";
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.komut.Parameters.AddWithValue("@Id", textBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter(sql.komut);
            sql.read = sql.komut.ExecuteReader();
            if (sql.read.Read())
            {
                string mürün = sql.read.GetString(2);
                sql.read.Close();
                string sil = "DELETE FROM Masalar WHERE Id = @Id";
                sql.komut = new SqlCommand(sil, sql.baglanti);
                sql.komut.Parameters.AddWithValue("@Id", textBox1.Text);
                sql.logtut3("{0} {1} den {2} Ürününü Sildi", comboBox1.Text, mürün);
                sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                Tablo_s();
            }
        }
        //Masadaki ürünleri ödemesi yapıldığında silen metod
        private void button4_Click(object sender, EventArgs e)
        {
            sql.baglanti.Open();
            string ad = comboBox1.Text;
            string ara = string.Format("SELECT M_fiyat FROM Masalar WHERE M_adi = '{0}'", ad);
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.read = sql.komut.ExecuteReader();
            if(sql.read.Read()) 
            {
                int c = 0;
                for (int x = 0; x < dataGridView2.RowCount; x++)
                {
                    int a = sql.read.GetInt32(0);
                    c += + a;
                }
                MessageBox.Show(string.Format("Toplam Tutar {0}₺", c));
                string sil = string.Format("DELETE FROM Masalar WHERE M_adi = '{0}'", ad);
                sql.logtut2("{0} {1} deki Ödemeyi Tamamladı", ad);
                sql.komut = new SqlCommand(sil, sql.baglanti);
                sql.read.Close();
                sql.komut.ExecuteNonQuery();
                sql.baglanti.Close();
                Tablo_s();
            }
            else
            {
                sql.baglanti.Close();
            }
        }
        //Masa aktarması için kullanılan metod
        private void button5_Click(object sender, EventArgs e)
        {
            sql.baglanti.Open();
            string ad = comboBox1.Text;
            string ara = string.Format("SELECT M_adi FROM Masalar WHERE M_adi = '{0}'", ad);
            sql.komut = new SqlCommand(ara, sql.baglanti);
            sql.read = sql.komut.ExecuteReader();
            if (sql.read.Read())
            {
                Aktar aktarma = new Aktar();
                aktarma.Show();
                if (label1.Text == "Users")
                {
                    aktarma.Controls["label1"].Text = "Users";
                }
                else
                {
                    aktarma.Controls["label1"].Text = "Admin";
                }
                this.Hide();
                aktarma.Controls["textbox1"].Text = ad;
                sql.baglanti.Close();
            }
            else
            {
                sql.baglanti.Close();
            }
        }
    }
}
