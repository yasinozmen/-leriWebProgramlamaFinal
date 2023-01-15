using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
namespace SınavGirişSistemi
{
    public partial class ÖğrenciGiriş : System.Web.UI.Page
    {
        baglanti bgl = new baglanti();
        SqlCommand cmd;
        SqlDataReader dr;
        public int VarMi(string aranan)
        {
            int sonuc;
            string sorgu = "Select COUNT(ÖğrenciNo) from Öğrenciler WHERE ÖğrenciNo='" + aranan + "'";
            cmd = new SqlCommand(sorgu, bgl.bagla());
            sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            return sonuc;

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        string adSoyad;
        string dersAdı;
        string sınavTipi;
        string sınavTarihi;
        string sınavSaati;
        protected void belge_sorgula_Click(object sender, EventArgs e)
        {
            if (VarMi(ÖgrNo.Text) != 0)
            {
                string query = "SELECT * FROM Öğrenciler WHERE ÖğrenciNo = @ÖğrenciNo";
                SqlCommand cmd = new SqlCommand(query, bgl.bagla());
                cmd.Parameters.AddWithValue("@ÖğrenciNo", ÖgrNo.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    adSoyad = reader["AdıSoyadı"].ToString();

                }
                string query2 = "SELECT * FROM SınavBilgileri WHERE id = @id";
                SqlCommand cmd2 = new SqlCommand(query2, bgl.bagla());
                cmd2.Parameters.AddWithValue("@id", 1);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    dersAdı = reader2["DersAdı"].ToString();
                    sınavTipi = reader2["SınavTipi"].ToString();
                    sınavTarihi = reader2["SınavTarihi"].ToString();
                    sınavSaati = reader2["SınavSaati"].ToString();

                }

                System.Drawing.Image imgUniLogo = System.Drawing.Image.FromFile(Server.MapPath("~/Images/logo.jpeg"));
                Bitmap bmp = new Bitmap(500, 500);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(imgUniLogo, new Point(20, 20));
                g.DrawString("Öğrenci No : " + ÖgrNo.Text, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 40));
                g.DrawString("Adı Soyadı : " + adSoyad, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 80));
                g.DrawString("Ders Adı : " + dersAdı, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 120));
                g.DrawString("Sınav Tipi : " + sınavTipi, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 160));
                g.DrawString("Sınav Tarihi : " + sınavTarihi, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 200));
                g.DrawString("Sınav Saati : " + sınavSaati, new Font("Arial", 10), new SolidBrush(Color.Blue), new Point(280, 240));

                bmp.Save("C:\\Users\\Pc\\Desktop\\yakaKartı.png", System.Drawing.Imaging.ImageFormat.Png);
                System.Diagnostics.Process.Start("C:\\Users\\Pc\\Desktop\\yakaKartı.png");
                bgl.bagla().Close();
                Response.Write("<script lang='JavaScript'>alert('Yaka Kartı İndirildi ve Açılıyor...');</script>");

            }

            else
            {
                Response.Write("<script lang='JavaScript'>alert('Böyle bir kayıt bulunmamaktadır.');</script>");
            }
        }
    }
}