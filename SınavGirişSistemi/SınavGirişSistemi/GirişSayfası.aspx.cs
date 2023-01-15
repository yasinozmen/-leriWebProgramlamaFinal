using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SınavGirişSistemi
{
    public partial class GirişSayfası : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ÖğrenciGirişYap_Click(object sender, EventArgs e)
        {
            Response.Redirect("ÖğrenciGiriş.aspx");
        }

        protected void AkademisyenButonu_Click(object sender, EventArgs e)
        {
            Response.Redirect("AkademisyenPaneli.aspx");
        }
    }
}