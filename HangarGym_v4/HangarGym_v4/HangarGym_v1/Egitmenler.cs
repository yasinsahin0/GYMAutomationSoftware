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
    public partial class Egitmenler : Form
    {
        sql_sorgu Egitmen_Sorgu = new sql_sorgu();
        Sql_kosullar Egitmen_Kosul = new Sql_kosullar();
        static string conString = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;//1
        string egitmen_tc_no = null;
        string ad = null;
        string soyad = null;
        string uzmanlik = null;
        string Durum = null;
        string Tc_Varmi = null;
        string islemNe = null;
        //FORM
        public Egitmenler()
        {
            InitializeComponent();
        }
        private void egitmenler_Load(object sender, EventArgs e)
        {
            Kayıt_listele();
           
            KoseliForm();
        }

        private void egitmenler_FormClosed(object sender, FormClosedEventArgs e)
        {
           
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
        private void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
   
            comboBox1.SelectedItem = null;
        }

        private string NotNull()
        {
            if (textBox1.Text == "")
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Tc No Boş";
            }
                
            else if (textBox2.Text == "")
            {
                label6.ForeColor = Color.Red;
                label6.Text = "İsim Boş";
            }
            else if (textBox3.Text == "")
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Soyisim Boş";
            }
            else
            {
                egitmen_tc_no = textBox1.Text;
                ad = textBox2.Text;
                soyad = textBox3.Text;
                uzmanlik = comboBox1.SelectedItem.ToString();
                Durum = "ok";
            }
            return Durum;
        }

        private void Egitmen_kayit()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum == "ok")
            {
                Egitmen_Sorgu.Egitmen_Kayit(conString, egitmen_tc_no, ad, soyad, uzmanlik);
                islemNe = "kayit";
                timer1.Start();
            }
            else
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Hata";     
            }
        }
        private DataTable SP_Egitmen_Table(string pro_name)
        {
            DataTable ds = new DataTable();
            string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(Con_String);
            conn.Open();
            SqlCommand command = new SqlCommand(pro_name, conn);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            da.Fill(ds);
            command.ExecuteNonQuery();
            conn.Close();
            return ds;
        }
        private void Kayıt_listele()
        { 
            try
            {
                DataTable yen = new DataTable();
                yen = SP_Egitmen_Table("egitmengetir");
                dataGridView1.DataSource = yen;
            }
            catch (Exception ex)
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Hata";
            }
        }
        private void Egitmen_Tc_Sorgulama()
        {
            egitmen_tc_no = null;
            try
            {
                egitmen_tc_no = textBox1.Text;
                Tc_Varmi = Egitmen_Kosul.Egitmenler_Tc_Sorgulama(conString, egitmen_tc_no);
            }
            catch (Exception)
            {

                label6.ForeColor = Color.Red;
                label6.Text = "Hata";
            }
        }
        private void Egitmen_Update()
        {
            progressBar1.Value = 0;
            try
            {
                Egitmen_Tc_Sorgulama();
                if (Tc_Varmi == "True")
                {
                    NotNull();
                    if (Durum == "ok")
                    {
                        Egitmen_Sorgu.Egitmen_Update(conString, egitmen_tc_no, ad, soyad, uzmanlik);
                        islemNe = "update";
                        timer1.Start();
                    }
                    else
                    {
                        label6.ForeColor = Color.Red;
                        label6.Text = "HATA";
                        
                    }
                }
                else
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = "HATA";
                }
            }
            catch (Exception)
            {

                label6.ForeColor = Color.Red;
                label6.Text = "HATA";
            }
        }
        private void Egitmen_Delete()
        {
            progressBar1.Value = 0;
            try
            {
                Egitmen_Tc_Sorgulama();
                if (Tc_Varmi == "True")
                {
                    Egitmen_Sorgu.Egitmen_Delete(conString, egitmen_tc_no);
                    islemNe = "delete";
                    timer1.Start();
                }
                else
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = "Tc Yok";
                }
            }
            catch (Exception)
            {

                label6.ForeColor = Color.Green;
                label6.Text = "Hata";
            }
        }
        //TOOLBAX İTEM
        private void button1_Click(object sender, EventArgs e)
        {
            Egitmen_kayit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Egitmen_Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Egitmen_Delete();
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                if (islemNe == "kayit")
                {
                    label6.ForeColor = Color.Green;
                    label6.Text = "Kayıt Başarılı";
                }
                else if (islemNe == "update")
                {
                    label6.ForeColor = Color.Green;
                    label6.Text = "Güncelleme Başarılı";
                }

                else if (islemNe == "delete")
                {
                    label6.ForeColor = Color.Green;
                    label6.Text = "Silindi";
                }
                Kayıt_listele();
                temizle();
                timer1.Stop();
            }

            else
            {
                label6.Text = "Yükleniyor...";
                label6.ForeColor = Color.Green;
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
    }
}
