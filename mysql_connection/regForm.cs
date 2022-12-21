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
using MySql.Data.MySqlClient;

namespace mysql_connection
{
    public partial class regForm : Form
    {
        public regForm()
        {
            InitializeComponent();
        }

        //MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=uyeler;Uid=root;Pwd='';");
        MySqlConnection baglanti = new MySqlConnection("Server=134.209.206.170; Port=3306; Uid=mustafa; Pwd=msT1650k; Database=uyeler");
        MySqlCommand cmd = new MySqlCommand();

        private void textboxtemizle()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void CheckBxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtTelefon.Text == "" || txtusername.Text == "" ||txtpassword.Text == "" || txtpassword.Text == "" ||txtEmail.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayın", "Kayıt Olma Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtpassword.Text == txtComPassword.Text)
            {

            string sql = "INSERT INTO `uyebilgileri` (`uyeAd`, `uyeSoyad`, `uyeTelefon`, `uyeEmail`,`uyeKullaniciAdi`, `uyeSifre`) VALUES (@ad,@soyad,@telefon,@email,@kullaniciadi, @sifre)";
            cmd = new MySqlCommand(sql,baglanti);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@kullaniciadi", txtusername.Text);
            cmd.Parameters.AddWithValue("@sifre", txtpassword.Text);
            baglanti.Open();
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Başarılı Bir Şekilde Kayıt Olundu", "Kayıt Olundu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textboxtemizle();
            }
            else
            {
                MessageBox.Show("Şifreler Eşleşmiyor Lütfen Tekrar Deneyin", "Kayıt Olma Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Text = "";
                txtpassword.Text = "";
                txtComPassword.Text = "";
                txtusername.Focus();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textboxtemizle();
        }

        private void regForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
