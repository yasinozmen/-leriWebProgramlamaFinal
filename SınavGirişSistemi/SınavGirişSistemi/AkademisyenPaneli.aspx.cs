using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;

namespace SınavGirişSistemi
{
    public partial class AkademisyenPaneli : System.Web.UI.Page
    {
        baglanti bgl = new baglanti();
        SqlCommand cmd;
        SqlDataReader dr;
        double kontejan;
        private void Sınıfları_Getir()
        {
            string sorgu = "SELECT * FROM Sınıflar";
            cmd = new SqlCommand(sorgu, bgl.bagla());
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                liste.Items.Add(dr["SınıfAdı"].ToString() + "," + dr["Kapasite"].ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack == true)
            {
                Sınıfları_Getir();
                bgl.bagla().Close();
            }
        }
        protected void liste_yukle_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                GridView1.DataSource = ÖgrenciTabloGetir();
                GridView1.DataBind();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (VarMi(row.Cells[1].Text) != 0)
                    {
                        Response.Write("<script lang='JavaScript'>alert('Bu kayıtlar zaten mevcut!');</script>");
                        break;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("insert into Öğrenciler (SN,ÖğrenciNo,AdıSoyadı) values (@SN,@ÖğrenciNo,@AdıSoyadı)", bgl.bagla());
                        cmd.Parameters.AddWithValue("@SN", row.Cells[0].Text);
                        cmd.Parameters.AddWithValue("@ÖğrenciNo", row.Cells[1].Text);
                        cmd.Parameters.AddWithValue("@AdıSoyadı", row.Cells[2].Text.ToString());
                        cmd.ExecuteNonQuery();
                        bgl.bagla().Close();
                        SqlConnection.ClearPool(bgl.bagla());
                    }
                }
            }
            sınıfmevcudu.Text = GridView1.Rows.Count.ToString();

        }
        DataTable ÖgrenciTabloGetir()
        {
            string dosyaKonumu = Server.MapPath("Uploads//" + FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(dosyaKonumu);
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dosyaKonumu + ";Extended Properties=Excel 8.0");
            baglanti.Open();
            string query = "select * from [Sayfa1$A1:C800] ";
            OleDbDataAdapter oAdp = new OleDbDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            oAdp.Fill(dt);
            return dt;
        }
        public int VarMi(string aranan)
        {
            int sonuc;
            string sorgu = "Select COUNT(ÖğrenciNo) from Öğrenciler WHERE ÖğrenciNo='" + aranan + "'";
            cmd = new SqlCommand(sorgu, bgl.bagla());
            sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            return sonuc;

        }
        protected void liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 18; i++)
            {
                if (liste.Items[i].Selected)
                {
                    string sorgu = "SELECT * FROM Sınıflar where id=@p1";
                    cmd = new SqlCommand(sorgu, bgl.bagla());
                    cmd.Parameters.AddWithValue("@p1", i + 1);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        kontejan += Convert.ToDouble(dr["Kapasite"]);
                    }
                }
            }
            kontenjan.Text = kontejan.ToString();
        }


        private static Random random = new Random();
        private static HashSet<int> generatedNumbers = new HashSet<int>();
        private int GetUniqueRandomNumber()
        {
            int randomNumber = random.Next(1, 89);
            while (generatedNumbers.Contains(randomNumber))
            {
                randomNumber = random.Next(1, 89);
            }
            generatedNumbers.Add(randomNumber);
            return randomNumber;
        }
        string yerleştirilenSınıf;
        protected void rastgele_dagit_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(kontenjan.Text) < Convert.ToDouble(sınıfmevcudu.Text))
            {
                Response.Write("<script lang='JavaScript'>alert('Kontenjan sınıf mevcudundan az olamaz!');</script>");
            }
            else
            {
                int yerleşenKişiSayısı = 0;
                for (int k = 0; k < 18; k++)
                {
                    if (!liste.Items[k].Selected) continue;
                    string sorgu = "SELECT * FROM Sınıflar where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sorgu, bgl.bagla()))
                    {
                        cmd.Parameters.AddWithValue("@id", k + 1);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int kapasite = Convert.ToInt32(dr["Kapasite"]);
                                yerleştirilenSınıf = dr["SınıfAdı"].ToString();

                                for (int i = 0; i < kapasite; i++)
                                {
                                    int uniqueNumber = GetUniqueRandomNumber();
                                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Öğrenciler set YerleştiğiSınıf = @YerleştiğiSınıf where SN = @SN", bgl.bagla()))
                                    {
                                        cmd2.Parameters.AddWithValue("@SN", uniqueNumber);
                                        cmd2.Parameters.AddWithValue("@YerleştiğiSınıf", yerleştirilenSınıf);
                                        cmd2.ExecuteNonQuery();
                                        yerleşenKişiSayısı++;
                                    }

                                    if (yerleşenKişiSayısı == 88) break;
                                }

                            }
                        }
                    }
                    string query = "SELECT ÖğrenciNo, AdıSoyadı, İmza FROM Öğrenciler where YerleştiğiSınıf='" + yerleştirilenSınıf + "'";
                    using (SqlCommand command = new SqlCommand(query, bgl.bagla()))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // PDF dosyasını oluşturun
                            using (FileStream stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\'" + yerleştirilenSınıf + "'.pdf", FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                int sayac = 1;
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                Paragraph başlık1 = new Paragraph(new Phrase("Kirklareli Üniversitesi - Ileri Web Programlama Final Sınavi ", new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD)));
                                başlık1.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(başlık1);
                                pdfDoc.Add(new Paragraph(" "));
                                Paragraph başlık2 = new Paragraph(new Phrase("13.01.2023 10:00 - Dr.Ögr.Üyesi Bora Aslan ", new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD)));
                                başlık2.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(başlık2);
                                pdfDoc.Add(new Paragraph(" "));
                                Paragraph başlık3 = new Paragraph(new Phrase(yerleştirilenSınıf + " Numarali Sinif", new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD)));
                                başlık3.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(başlık3);
                                pdfDoc.Add(new Paragraph(" "));

                                PdfPTable table = new PdfPTable(reader.FieldCount + 1);
                                table.AddCell(new Phrase("Sira Numarasi"));
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    table.AddCell(new Phrase(reader.GetName(i)));
                                }
                                while (reader.Read())
                                {
                                    table.AddCell(new Phrase(sayac.ToString()));
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        table.AddCell(new Phrase(reader[i].ToString()));
                                    }
                                    sayac++;
                                }
                                pdfDoc.Add(table);
                                pdfDoc.Close();


                            }
                        }
                    }
                }
                Response.Write("<script lang='JavaScript'>alert('Öğrenciler Başarılı Bir Şekilde Sınıflara Dağıtılmıştır.');</script>");
            }
        }
    }
}