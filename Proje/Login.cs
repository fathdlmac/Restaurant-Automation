using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Proje
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            pass.PasswordChar = '*';
        }
        public SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-2V9AK81\\SQLEXPRESS;Initial Catalog=Proje;Integrated Security=True");
        public SqlCommand komut;
        public SqlDataReader read;
        public static string isimdegeri;

        //Giriþ yapan kiþinin admin mi yoksa kullanýcý mý olduðunu sorgulayan metod
        public void giris_b_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand ("SELECT kadi, pass FROM Users WHERE kadi=@kadi AND pass=@pass");
            SqlCommand komut2 = new SqlCommand("SELECT kadi, pass FROM Admin WHERE kadi=@kadi AND pass=@pass");
            komut.Connection = baglanti;
            komut2.Connection = baglanti;
            komut.Parameters.AddWithValue("@kadi", kadi.Text);
            komut.Parameters.AddWithValue("@pass", pass.Text);
            komut2.Parameters.AddWithValue("@kadi", kadi.Text);
            komut2.Parameters.AddWithValue("@pass", pass.Text);
            isimdegeri = kadi.Text;
            baglanti.Open();
            read = komut.ExecuteReader();
            A_Main a_Main = new A_Main();
            if (read.Read())
            {
                logtut1("User Giriþi Yapýldý");
                a_Main.Controls["button1"].Visible = false;
                a_Main.Controls["button2"].Visible = false;
                a_Main.Controls["label1"].Text = "Users";
                a_Main.Show();
                baglanti.Close();
                this.Hide();
            }
            else
            {
                read.Close();
                SqlDataReader read2 = komut2.ExecuteReader();
                if (read2.Read())
                {
                    logtut1("Admin Giriþi Yapýldý");
                    a_Main.Controls["label1"].Text = "Admin";
                    a_Main.Show();
                    baglanti.Close();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanýcý Adý veya Þifreniz Hatalý");
                    baglanti.Close();
                }
            }
        }
        public void logtut1(string value)
        {
            string koþul = string.Format(value, isimdegeri);
            Log a = new Log(koþul);
        }
        public void logtut2(string value, string degerler)
        {
                string koþul = string.Format(value, isimdegeri, degerler);
                Log a = new Log(koþul);
        }
        public void logtut3(string value, string degerler, string degerler2)
        {
            string koþul = string.Format(value, isimdegeri, degerler, degerler2);
            Log a = new Log(koþul);
        }
    }
}