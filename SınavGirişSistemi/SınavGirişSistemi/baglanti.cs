using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SınavGirişSistemi
{
    public class baglanti
    {
        public SqlConnection bagla()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-DLFFSN6;Initial Catalog=SınavGirişSistemi;Integrated Security=True;");
            baglan.Open();
            return baglan;
        }
    }
}