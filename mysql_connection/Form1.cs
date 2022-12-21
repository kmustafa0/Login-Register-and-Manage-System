using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace mysql_connection
{
    public partial class adminpaneli : Form
    {
        public adminpaneli()
        {
            InitializeComponent();
        }
        //MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=uyeler;Uid=root;Pwd='';");
        MySqlConnection baglanti = new MySqlConnection("Server=134.209.206.170; Port=3306; Uid=mustafa; Pwd=msT1650k; Database=uyeler");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt;
        private void uyelerigetir()
        {
            //baglanti.Open();
            //string komut = "SELECT * FROM uyebilgileri";
            //da = new MySqlDataAdapter(komut, baglanti);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //baglanti.Close();

            dt = new DataTable();
            baglanti.Open();
            da = new MySqlDataAdapter("SELECT * FROM `uyebilgileri` ", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void temizle()
        {
            txtID.Text = txtAd.Text = txtSoyad.Text = txtTelefon.Text = txtMail.Text = cboxHesapTuru.Text = txtkadi.Text = txtSifre.Text = txtArama.Text = null;
            uyelerigetir();
            dateTimePickerBitis.Value = dateTimePickerKayit.Value =  DateTime.Now.Date;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            uyelerigetir();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cboxHesapTuru.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtkadi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtSifre.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            dateTimePickerBitis.Value = (DateTime)dataGridView1.CurrentRow.Cells[8].Value;
            dateTimePickerKayit.Value = (DateTime)dataGridView1.CurrentRow.Cells[9].Value;

            DateTime date = DateTime.Now.Date;
            double kalangun = (dateTimePickerBitis.Value - date).TotalDays;
            lblKalanGun.Visible = true;
            lblKalanGun.Text = kalangun.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtTelefon.Text == "" || txtMail.Text == "" || txtkadi.Text == "" || txtSifre.Text == "" || dateTimePickerBitis.Value != DateTime.Now.Date)
            {
                MessageBox.Show("Boş alan bırakmadan doldurun.", "Boş Bırakılamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "insert into `uyebilgileri`        (uyeAd,uyeSoyad,uyeTelefon,uyeEmail,uyeHesapTuru,uyeKullaniciAdi,uyeSifre,uyeBitisTarihi,uyeKayitTarihi) values(@ad,@soyad,@telefon,@mail,@hesapturu,@kullaniciadi,@sifre,@bitistarihi,@kayittarihi);";
            cmd = new MySqlCommand(sql, baglanti);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@mail", txtMail.Text);
            cmd.Parameters.AddWithValue("@hesapturu", cboxHesapTuru.Text);
            cmd.Parameters.AddWithValue("@kullaniciadi", txtkadi.Text);
            cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
            cmd.Parameters.AddWithValue("@bitistarihi", dateTimePickerBitis.Value);
            cmd.Parameters.AddWithValue("@kayittarihi", dateTimePickerKayit.Value);
            baglanti.Open();
            cmd.ExecuteNonQuery();
            baglanti.Close();
            uyelerigetir();
            temizle();
            MessageBox.Show("Kayıt Başarılı Bir Şekilde Eklendi", "Kayıt Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
            if(txtID.Text == "")
            {
                MessageBox.Show("Kullanıcı seçilmeden silme işlemi yapılamaz.", "Kullanıcı Seçilmemiş",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {

            DialogResult onay = MessageBox.Show($@"ID'si {this.txtID.Text} olan  {this.txtAd.Text} {this.txtSoyad.Text} kalıcı olarak silinecek. Bu işlemi yapmak istediğine emin misin?", "Kalıcı Olarak Silinecek", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay == DialogResult.Yes)
            {
                String sql = "DELETE FROM uyebilgileri WHERE id=@uyeno";
                cmd = new MySqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@uyeno", txtID.Text);
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();
                uyelerigetir();
                temizle();
            }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Kullanıcı seçilmeden bu işlem yapılamaz", "Kullanıcı Seçilmemiş!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                string sql = "UPDATE uyebilgileri SET uyeAd=@ad,uyeSoyad=@soyad,uyeTelefon=@telefon,uyeEmail=@mail,uyeHesapTuru=@hesapturu,uyeKullaniciAdi=@kullaniciadi,uyeSifre=@sifre,uyeBitisTarihi=@bitistarihi WHERE id=@uyeno";
                cmd = new MySqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@hesapturu", cboxHesapTuru.Text);
                cmd.Parameters.AddWithValue("@kullaniciadi", txtkadi.Text);
                cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                cmd.Parameters.AddWithValue("@bitistarihi", dateTimePickerBitis.Value);
                cmd.Parameters.AddWithValue("@uyeno", txtID.Text);
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();
                uyelerigetir();
                temizle();
                MessageBox.Show("Kayıt başarılı bir şekilde güncellendi", "Kayıt Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            temizle();
            uyelerigetir();
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "uyeAd LIKE '" + txtArama.Text + "%'" +
                "OR uyeSoyad LIKE '" + txtArama.Text + "%'" +
                "OR uyeTelefon LIKE '" + txtArama.Text + "%'" +
                "OR uyeEmail LIKE '" + txtArama.Text + "%'"; 
            dataGridView1.DataSource = dv;
        }

        private void adminpaneli_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
