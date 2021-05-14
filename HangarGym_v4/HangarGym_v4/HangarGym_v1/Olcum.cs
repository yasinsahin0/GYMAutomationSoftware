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
    public partial class Olcum : Form
    {
        //DEĞİŞKENLER
        sql_sorgu Olcum_Sorgu = new sql_sorgu();
        Sql_kosullar SqlKosul = new Sql_kosullar();
        string conString = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
        string tc_no = null;
        string kg = null;
        string boy = null;
        string yag = null;
        string yas = null;
        string vki = null;
        string Durum = null;
        string Tc_Varmi = null;
        string islemNe = null;

        //FORM
        public Olcum()
        {
            InitializeComponent();
        }
        private void olcum_bilgileri_Load(object sender, EventArgs e)
        {
            Olcum_Kayit_Listele();
           
            KoseliForm();
        }

        //FONKSİYONLAR
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
        private void Girdi_Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private string NotNull()
        {
            if (textBox1.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Tc Boş";
            }
            else if (textBox2.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Kilo Boş";
            }
            else if (textBox3.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Boy Boş";
            }
            else if (textBox4.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Yağ Boş";
            }
            else if (textBox5.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Yaş Boş";
            }
            else if (textBox6.Text == "")
            {
                label7.ForeColor = Color.Red;
                label7.Text = "VKİ Boş";

            }
            else
            {
                tc_no = textBox1.Text;
                kg = textBox2.Text;
                boy = textBox3.Text;
                yag = textBox4.Text;
                yas = textBox5.Text;
                vki = textBox6.Text;
                Durum = "ok";

            }
            return Durum;
        }
        private void Olcum_kayit()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum == "ok")
            {
                Olcum_Sorgu.Olcum_Bilgi_Kayit(conString, tc_no, kg, boy, yag, yas, vki);
                islemNe = "kayit";
                timer1.Start();
            }
            else
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Hata";
            }
        }
        private void Olcum_Kayit_Listele()
        {
            try
            {
                DataTable DataT = new DataTable();
                DataT = Olcum_Sorgu.Olcum_Listeleme(conString);
                dataGridView1.DataSource = DataT;

            }
            catch (Exception ex)
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Listeleme Hatası";
            }
        }
        private void Olcum_Tc_Sorgulama()
        {
            try
            {
                tc_no = textBox1.Text;
                Tc_Varmi = SqlKosul.Olcum_Tc_Sorgulama(conString, tc_no);

            }
            catch (Exception)
            {
                tc_no = null;
                label7.ForeColor = Color.Red;
                label7.Text = "Hata";
            }
        }
        private void Olcum_Update()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum=="ok")
            {
                tc_no = textBox1.Text;
                Olcum_Tc_Sorgulama();
                if (Tc_Varmi=="True")
                {
                    Olcum_Sorgu.Olcum_Update(conString,tc_no,kg,boy,yag,yas,vki);
                    islemNe = "update";
                    timer1.Start();
                }
                else
                {
                    label7.ForeColor = Color.Red;
                    label7.Text = "Tc Yok";
                }
            }
        }
        private void Olcum_Delete()
        {
            progressBar1.Value = 0;
            Olcum_Tc_Sorgulama();
            if (Tc_Varmi == "True")
            {
                Olcum_Sorgu.Olcum_Delete(conString, tc_no);
                islemNe = "delete";
                timer1.Start();
                
            }
            else
            {
                label7.ForeColor = Color.Red;
                label7.Text = "Tc Yok";
            }
                
            
        }
    
        //TOOLBAX İTEM
  
        private void button2_Click(object sender, EventArgs e)
        {
            Olcum_Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Olcum_Delete();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "DİKKAT", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            Olcum_kayit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (progressBar1.Value == 100)
            {
                if (islemNe == "kayit")
                {
                    label7.ForeColor = Color.Green;
                    label7.Text = "Kayıt Başarılı";
                }
                else if (islemNe == "update")
                {
                    label7.ForeColor = Color.Green;
                    label7.Text = "Güncelleme Başarılı";
                }
                    
                else if (islemNe == "delete")
                    label7.Text = "Kayıt Silindi";
                Olcum_Kayit_Listele();
                Girdi_Temizle();
                timer1.Stop();
            }

            else
            {
                label7.Text = "Yükleniyor...";
                label7.ForeColor = Color.Green;
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

                }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox2.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);

                }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox3.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);

                }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox4.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 1);

                }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox5.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox5.Text = textBox5.Text.Substring(0, textBox5.Text.Length - 1);

                }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox6.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox6.Text = textBox6.Text.Substring(0, textBox6.Text.Length - 1);

                }
        }
    }
}
//