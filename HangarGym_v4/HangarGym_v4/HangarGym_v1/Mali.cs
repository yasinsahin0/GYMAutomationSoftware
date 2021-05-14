using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using sql;
using System.Drawing.Drawing2D;

namespace HangarGym_v1
{
    public partial class Mali : Form
    {
        static string conString = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
        sql_sorgu MaliyeSorgu = new sql_sorgu();
        Sql_kosullar MaliyeKosul = new Sql_kosullar();
        string date;
        string fatura = null;
        string aciklama = null;
        string personel = null;
        int gider = 0;
        string Durum = null;
        Boolean Fatura_Varmi ;
        string islenNe = null;

        public Mali()
        {
            InitializeComponent();
        }
        private string NotNull()
        {
            if (textBox1.Text == "")
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Fatura No Boş";
            }
            
            else if (textBox3.Text == "")
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Gider Boş";
            }
            else if (richTextBox1.Text == "")
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Açıklama Boş";
            }
            else
            {
                date = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                fatura = textBox1.Text;
                aciklama = richTextBox1.Text;
                personel = comboBox1.SelectedItem.ToString();
                gider = Convert.ToInt32(textBox3.Text);
                Durum = "ok";
            }
            return Durum;
        }
        private void Temizle()
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            textBox3.Text = "";
            richTextBox1.Text = "";

            
        }
        public void KoseliForm()
        {
            GraphicsPath graphicpath = new GraphicsPath();
            graphicpath.StartFigure();
            graphicpath.AddArc(0, 0, 25, 25, 180, 90);
            graphicpath.AddLine(25, 0, this.Width - 25, 0);
            graphicpath.AddArc(this.Width - 25, 0, 25, 25, 270, 90);
            graphicpath.AddLine(this.Width, 25, this.Width, this.Height - 25);
            graphicpath.AddArc(this.Width - 25, this.Height - 25, 25, 25, 0, 90);
            graphicpath.AddLine(this.Width - 25, this.Height, 25, this.Height);
            graphicpath.AddArc(0, this.Height - 25, 25, 25, 90, 90);
            graphicpath.CloseFigure();
            this.Region = new Region(graphicpath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            Maliye_Kayit();
        }
        private void Maliye_Fatura_Varmi()
        {
            try
            {
                DataTable ds = new DataTable();
                string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(Con_String);
                conn.Open();
                SqlCommand command = new SqlCommand("MaliFaturaSorgulama", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@fatura", SqlDbType.VarChar, 50);
                command.Parameters["@fatura"].Value = textBox1.Text;
                command.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);

                conn.Close();
                if (ds.Rows.Count > 0)
                {
                    Fatura_Varmi = true;
                }
                    
                else if (ds.Rows.Count == 0)
                {
                    Fatura_Varmi = false;
                }
                    
            }
            catch (Exception)
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Hata - 1";
            }

        }
        private void Maliye_Update()
        {
            progressBar1.Value = 0;
            Maliye_Fatura_Varmi();
            NotNull();
            try
            {
                if (Fatura_Varmi == true)
                {
                    MaliyeSorgu.Maliye_Update(conString,date, fatura, aciklama, personel, gider);
                    islenNe = "update";
                    timer1.Start();
                }
                else
                {
                    label8.ForeColor = Color.Red;
                    label8.Text = "Fatura Yok";
                }

            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata - 2";
            }
        }
        private void Maliye_Kayit()
        {
            progressBar1.Value = 0;
            NotNull();
            try
            {
                if (Durum =="ok")
                {
                    if (Fatura_Varmi == true)
                    {
                        label8.ForeColor = Color.Red;
                        label8.Text = "Fatura Mevcut";
                    }
                    else
                    {
                        MaliyeSorgu.Maliye_Kayit(conString, date, fatura, aciklama, personel, gider);
                        islenNe = "kayit";
                        comboBox1.SelectedIndex = 0;
                        timer1.Start();
                    }
                }

            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata - 4";
            }
        }
        private void Maliye_Delete()
        {
            progressBar1.Value = 0;
            fatura = textBox1.Text;
            Maliye_Fatura_Varmi();
            try
            {
                if (Fatura_Varmi == true)
                {
                    MaliyeSorgu.Maliye_Delete(conString, fatura);
                    islenNe = "delete";
                    timer1.Start();
                }
                else
                {
                    label8.ForeColor = Color.Red;
                    label8.Text = "Fatura Yok";
                }
            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata - 3";
            }
        }
        private void Maliye_Listele()
        {
            DataTable DataT = new DataTable();
            DataT = MaliyeSorgu.Maliye_Listeleme(conString);
            dataGridView1.DataSource = DataT;
        }
        bool SayiMi(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }
        private void Mali_Load(object sender, EventArgs e)
        {
            Maliye_Listele();
            KoseliForm();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Maliye_Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Maliye_Delete();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "DİKKAT", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                if (islenNe == "kayit")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Kayıt Başarılı";
                }
                else if (islenNe == "update")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Güncelleme Başarılı";
                }

                else if (islenNe == "delete")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Silindi";
                }
                Maliye_Listele();
                Temizle();
                timer1.Stop();
            }

            else
            {
                label8.Text = "Yükleniyor...";
                label8.ForeColor = Color.Green;
                progressBar1.Value += 5;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox1.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {
                    
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    ToolTip T1 = new ToolTip();
                    T1.SetToolTip(textBox1, "Sadece Sayı girişi yapınız !!");
                    T1.ToolTipTitle = "Bilgilendirme";
                    T1.ToolTipIcon = ToolTipIcon.Warning;
                    T1.IsBalloon = true;
                }   
         
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox3.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);
                    ToolTip T1 = new ToolTip();
                    T1.SetToolTip(textBox3, "Sadece Sayı girişi yapınız !!");
                    T1.ToolTipTitle = "Bilgilendirme";
                    T1.ToolTipIcon = ToolTipIcon.Warning;
                    T1.IsBalloon = true;
                }
        }
    }
}
//KAYNAKLAR
//https://ridvanozbilen.files.wordpress.com/2016/12/bc3b6lc3bcm-2-sql-programlama-veri-tipleri-ve-dec49fic59fkenler-5-sayfa.pdf
//http://www.oguzhantas.com/sql-server/357-parametre-alan-ve-deger-donduren-stored-procedure-yazmak.html
//https://www.kodmatik.com/sql-server-tum-veri-tipleri/
//https://stackoverflow.com/questions/6210027/calling-stored-procedure-with-return-value