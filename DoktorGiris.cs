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
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void DoktorGiris_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Giris_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktor where DoktorTC= @p1 and DoktorSifre = @p2 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                FrmDoktorKayıt fa = new FrmDoktorKayıt();
                fa.TC = MskTC.Text;
                fa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            bgl.baglanti().Close();
        }
    }
}
