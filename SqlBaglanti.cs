using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PACS_Sistemi
{
    class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-IAFQ17M\\SQLEXPRESS;Initial Catalog=PACS Sistemi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
