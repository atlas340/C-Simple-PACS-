using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace PACS_Sistemi
{
    public partial class FrmImage : Form
    {
        public FrmImage()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        string imgLoc = "";
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                //Görüntü Dosyalarını Filtreleme Kodu
                dlg.Filter = "JPG Files ( *.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|All Files(*.*)|*.* ";
                dlg.Title = "Görüntü Seçme";
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    
                    pictureBox1.ImageLocation = imgLoc;
                 
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void FrmImage_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                
                SqlCommand komutkayit = new SqlCommand( "insert into Tbl_Image(HastaAd,HastaSoyad,HastaTC,HastaMRI,HastaBrans,HastaTarih) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                komutkayit.Parameters.AddWithValue("@p1", TxtAd.Text);
                komutkayit.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komutkayit.Parameters.AddWithValue("@p3", MskTC.Text);
                komutkayit.Parameters.AddWithValue("@p5", Cmb_Brans.Text);
                komutkayit.Parameters.AddWithValue("@p4", img);
                komutkayit.Parameters.AddWithValue("@p6", MskTarih.Text);
                

                int x = komutkayit.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show(x.ToString() + "Resim Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtAd.Text = "";
                TxtSoyad.Text = "";
                MskTC.Text = "";
                pictureBox1.Image = null;
                

            }
            catch (Exception hata)
            {
                bgl.baglanti().Close();
                MessageBox.Show(hata.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komutgoster = new SqlCommand("Select HastaAd,HastaSoyad,HastaMRI From Tbl_Image where HastaTC = @c1 and  HastaBrans = @c2 and HastaTarih = @c3", bgl.baglanti());
                komutgoster.Parameters.AddWithValue("@c1", MskTC.Text);
                komutgoster.Parameters.AddWithValue("@c2", Cmb_Brans.Text);
                komutgoster.Parameters.AddWithValue("@c3", MskTarih.Text);
                SqlDataReader rd = komutgoster.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    TxtAd.Text = rd[0].ToString();
                    TxtSoyad.Text = rd[1].ToString();
                    byte[] img = (byte[])(rd[2]);
                    if (img == null)
                        pictureBox1.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }

                }
                else
                {
                    MessageBox.Show("Böyle Bir Hasta Görüntüsü Yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            }
            catch (Exception hata)
            {
                bgl.baglanti().Close();
                MessageBox.Show(hata.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Secim = new DialogResult();

                Secim = MessageBox.Show("Cikmak istediginizden emin misiniz?", "Cikis", MessageBoxButtons.YesNo);

                if (Secim == DialogResult.Yes)

                {

                    SqlCommand komutgl = new SqlCommand("Update Tbl_Image set HastaAd = @p1, HastaSoyad = @p2, HastaBrans = @p3,HastaTarih = @p4 where HastaTC = @p5", bgl.baglanti());
                    komutgl.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komutgl.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    komutgl.Parameters.AddWithValue("@p3", Cmb_Brans.Text);
                    komutgl.Parameters.AddWithValue("@p4", MskTarih.Text);
                    komutgl.Parameters.AddWithValue("@p5", MskTC.Text);
                    komutgl.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Güncelleme Tamamlandı!");

                }
                else
                {
                    MessageBox.Show("Güncelleme Yapılmadı!");
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        
            

        }
    }
}