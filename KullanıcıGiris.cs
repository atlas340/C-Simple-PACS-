using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PACS_Sistemi
{
    public partial class KullanıcGiris : Form
    {
        public KullanıcGiris()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminGiris adm = new AdminGiris();
            adm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoktorGiris dg = new DoktorGiris();
            dg.Show();
            this.Hide();
        }

        private void KullanıcGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
