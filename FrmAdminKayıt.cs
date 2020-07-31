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

namespace PACS_Sistemi
{
    public partial class FrmAdminKayıt : Form
    {
        public FrmAdminKayıt()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void label3_Click(object sender, EventArgs e)
        {

        }
        public string TC;
        private void FrmAdminKayıt_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Admin Ad-LblAdSoyad Ekleme
            SqlCommand komut = new SqlCommand("Select AdminAd,AdminSoyad From Tbl_Admin where AdminTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();


            //Datagridview Gösterme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Doktor ", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            Cmb_Brans.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand komutkayit = new SqlCommand("insert into Tbl_Doktor(DoktorAd,DoktorSoyad,DoktorTC,DoktorSifre,DoktorBrans) values (@k1,@k2,@k3,@k4,@k5)", bgl.baglanti());
            komutkayit.Parameters.AddWithValue("@k1", TxtAd.Text);
            komutkayit.Parameters.AddWithValue("@k2", TxtSoyad.Text);
            komutkayit.Parameters.AddWithValue("k3", MskTC.Text);
            komutkayit.Parameters.AddWithValue("k4", TxtSifre.Text);
            komutkayit.Parameters.AddWithValue("k5", Cmb_Brans.Text);
            komutkayit.ExecuteNonQuery();

            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Olundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from Tbl_Doktor where DoktorTC=@s1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@s1", MskTC.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutgl = new SqlCommand("Update Tbl_Doktor set DoktorAd = @g1, DoktorSoyad = @g2, DoktorSifre=@g3,DoktorBrans=@g4 where DoktorTC=@g5 ", bgl.baglanti());
            komutgl.Parameters.AddWithValue("g1", TxtAd.Text);
            komutgl.Parameters.AddWithValue("g2", TxtSoyad.Text);
            komutgl.Parameters.AddWithValue("g3", TxtSifre.Text);
            komutgl.Parameters.AddWithValue("g4", Cmb_Brans.Text);
            komutgl.Parameters.AddWithValue("g5", MskTC.Text);
            komutgl.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAdminKayıt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
