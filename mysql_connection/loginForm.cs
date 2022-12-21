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

namespace mysql_connection
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }
        public static string ut;
        //MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=uyeler;Uid=root;Pwd='';");
        MySqlConnection baglanti = new MySqlConnection("Server=134.209.206.170; Port=3306; Uid=mustafa; Pwd=msT1650k; Database=uyeler");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;


        private void button1_Click(object sender, EventArgs e)
        {

            if (txtusername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("BOŞ ALAN BIRKMA", "BOŞ ALAN BIRKMAYIN",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT uyeHesapTuru FROM `uyebilgileri` WHERE uyeKullaniciAdi='" + txtusername.Text+"' and uyeSifre='"+txtPassword.Text+"' ",baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    ut = dt.Rows[0][0].ToString();
                    if (ut == "admin")
                    {
                        adminpaneli admin = new adminpaneli();
                        admin.Show();
                        this.Hide();
                    }
                    else if (ut == "kullanici")
                    {
                        kullanici_arayüzü kullanici = new kullanici_arayüzü();
                        kullanici.Show();
                        kullanici.label2.Text ="Merhaba sayın "+ txtusername.Text;
                        //baslangic
                        cmd = new MySqlCommand("SELECT * FROM `uyebilgileri` WHERE  uyeKullaniciAdi=@kadi AND uyeSifre=@sifre", baglanti);
                        cmd.Parameters.AddWithValue("@kadi",txtusername.Text);
                        cmd.Parameters.AddWithValue("@sifre",txtPassword.Text);
                        baglanti.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string id = dr["id"].ToString();
                            string ad = dr["uyeAd"].ToString();
                            string soyad = dr["uyeSoyad"].ToString();
                            string telefon = dr["uyeTelefon"].ToString();
                            string email = dr["uyeEmail"].ToString();
                            string kadi = dr["uyeKullaniciAdi"].ToString();
                            string sifre = dr["uyeSifre"].ToString();
                            string bitistarihi = dr["uyeBitisTarihi"].ToString();
                            string kayittarihi = dr["uyeKayitTarihi"].ToString();
                            string yetkisi = dr["uyeHesapTuru"].ToString();

                            kullanici.txtid.Text = id;
                            kullanici.txtAd.Text = ad;
                            kullanici.txtSoyad.Text = soyad;
                            kullanici.txtTelefon.Text = telefon;
                            kullanici.txtEmail.Text = email;
                            kullanici.txtusername.Text = kadi;
                            kullanici.txtpassword.Text = sifre;
                            kullanici.dtpBitistarihi.Value = Convert.ToDateTime(bitistarihi);
                            kullanici.dtpKayitTarihi.Value = Convert.ToDateTime(kayittarihi);
                            kullanici.txtyetkisi.Text = yetkisi;
                        }
                        baglanti.Close();
                    }
                    //bitis
                    this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("kullanıcı adı veya şifreni kontrol et", "Kullanıcı adı veya şifre hatalı! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            }

        private void CheckBxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void loginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtPassword.Text = "";
            txtusername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new regForm().Show();
            this.Hide();
        }
    }
}
