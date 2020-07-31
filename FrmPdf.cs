using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PACS_Sistemi
{
    public partial class FrmPdf : Form
    {
        public FrmPdf()
        {
            InitializeComponent();
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmPdf_Load(object sender, EventArgs e)
        {
            OpenFileDialog ac = new OpenFileDialog();
            if (ac.ShowDialog() == DialogResult.OK) ;
            {
                axAcroPDF1.LoadFile(ac.FileName);
            }
        }
    }
}
