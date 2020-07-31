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
using System.Diagnostics;

namespace PACS_Sistemi
{
    public partial class FrmDoktorKayıt : Form
    {
        public FrmDoktorKayıt()
        {
            InitializeComponent();
        }
        public string TC;
        
        SqlBaglanti bgl = new SqlBaglanti();
        private void FrmDoktorKayıt_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Doktor Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad,DoktorBrans From Tbl_Doktor where DoktorTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
                
                
            }
            bgl.baglanti().Close();

            ////ComboBrans Aktarma
            //SqlCommand komut2 = new SqlCommand("Select DoktorBrans From Tbl_Doktor", bgl.baglanti());
            //SqlDataReader dr2 = komut2.ExecuteReader();
            //while (dr2.Read())
            //{
            //    Cmb_Brans.Items.Add(dr2[5]);
            //}
            //bgl.baglanti().Close();


            //Datagridview Gösterme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Hasta ", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand komutkayit = new SqlCommand("insert into Tbl_Hasta (HastaAd,HastaSoyad,HastaTC,HastaCinsiyet,HastaTelefon,HastaBilgi,HastaTarih,HastaSaat,HastaRecete,HastaBrans) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8,@h9,@h10)", bgl.baglanti());
            komutkayit.Parameters.AddWithValue("@h1", TxtAd.Text);
            komutkayit.Parameters.AddWithValue("@h2", TxtSoyad.Text);
            komutkayit.Parameters.AddWithValue("@h3", MskTC.Text);
            komutkayit.Parameters.AddWithValue("@h4", CmbCinsiyet.Text);
            komutkayit.Parameters.AddWithValue("@h5", MskTel.Text);
            komutkayit.Parameters.AddWithValue("@h6", RchBilgi.Text);
            komutkayit.Parameters.AddWithValue("@h7", MskTarih.Text);
            komutkayit.Parameters.AddWithValue("@h8", MskSaat.Text); 
            komutkayit.Parameters.AddWithValue("h9", Rchilaç.Text);
            komutkayit.Parameters.AddWithValue("h10", Cmb_Brans.Text);
            komutkayit.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Hastanız Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutgl = new SqlCommand("Update Tbl_Hasta set HastaAd =@p1,HastaSoyad=@p2,HastaCinsiyet=@p3,HastaTelefon=@p4,HastaBilgi=@p5 where HastaTC=@p6",bgl.baglanti());
            komutgl.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutgl.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komutgl.Parameters.AddWithValue("@p3", CmbCinsiyet.Text);
            komutgl.Parameters.AddWithValue("@p4", MskTel.Text);
            komutgl.Parameters.AddWithValue("@p5", RchBilgi.Text);
            komutgl.Parameters.AddWithValue("@p6", MskTC.Text);
            komutgl.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Hasta Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTel.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            Cmb_Brans.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            CmbCinsiyet.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            RchBilgi.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void Btn_blood_Click(object sender, EventArgs e)
        {
            FrmPdf pf = new FrmPdf();
            pf.Show();
            

        }

        private void Btn_CT_Click(object sender, EventArgs e)
        {
            FrmImage img = new FrmImage();
            img.Show();
        }

        private void FrmDoktorKayıt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;//Bunu yapıyoruz çünkü açmak istediğimiz Dosya yolunu String olarak kaydediyoruz.
                Process.Start(FilePath);//String olarak kaydettiğimiz yolu process.Start komutu ile çalıştırıyoruz.
            }
        }

        private void Cmb_Brans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bu kodda DataGridi Seçilen Branşa göre hasta gösterme yapmış olduk.
            SqlCommand filtre = bgl.baglanti().CreateCommand();
            filtre.CommandType = CommandType.Text;
            filtre.CommandText = "Select * From Tbl_Hasta where HastaBrans like ('"+Cmb_Brans.Text+"%')";
            filtre.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(filtre);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
    }
}
