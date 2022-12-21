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
using MySql.Data;
namespace mysql_connection
{
    public partial class kullanici_arayüzü : Form
    {
        public kullanici_arayüzü()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=serverip; Port=port; Uid=your-username; Pwd=your-password; Database=db_name");
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void kullanici_arayüzü_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void CheckBxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE `uyebilgileri` SET uyeTelefon=@telefon, uyeEmail=@email, uyeKullaniciAdi=@kadi, uyeSifre=@sifre WHERE id=@id";
            cmd = new MySqlCommand(sql,baglanti);
            cmd.Parameters.AddWithValue("@id",txtid.Text);
            cmd.Parameters.AddWithValue("@ad",txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad",txtSoyad.Text);;
            cmd.Parameters.AddWithValue("@telefon",txtTelefon.Text);
            cmd.Parameters.AddWithValue("@email",txtEmail.Text);
            cmd.Parameters.AddWithValue("@hesapturu",txtyetkisi.Text);
            cmd.Parameters.AddWithValue("@kadi",txtusername.Text);
            cmd.Parameters.AddWithValue("@sifre",txtpassword.Text);
            cmd.Parameters.AddWithValue("@bitistarihi",dtpBitistarihi.Value);
            cmd.Parameters.AddWithValue("@kayittarihi",dtpKayitTarihi.Value);
            baglanti.Open();
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Değişiklik başarıyla kaydedildi","Bilgler güncellendi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
