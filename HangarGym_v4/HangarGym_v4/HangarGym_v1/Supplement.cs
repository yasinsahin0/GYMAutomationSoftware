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
    public partial class Supplement : Form
    {
        sql_sorgu Supplement_Sorgu = new sql_sorgu();
        Sql_kosullar Supplement_Kosul = new Sql_kosullar();
        static string conString = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;//1
        string tc_no = null;
        string kreatin = null;
        string protein = null;
        string omega = null;
        string bcaa = null;
        string karnitin = null;
        string probiyotik = null;
        string Durum = null;
        string Tc_Varmi = null;
        string islemNe = null;
        //FORM
        public Supplement()
        {
            InitializeComponent();
        }
        private void supplement_Load(object sender, EventArgs e)
        {
            Kayit_listele();
            KoseliForm();
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            radioButton6.Checked = true;
            radioButton8.Checked = true;
            radioButton10.Checked = true;
            radioButton12.Checked = true;
        }
        private void supplement_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        //FONKSİYONLAR
        private void temizle()
        {
            textBox1.Text = "";
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            radioButton6.Checked = true;
            radioButton8.Checked = true;
            radioButton10.Checked = true;
            radioButton12.Checked = true;
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
        private void Secim_Return()
        {
            // Kreatin Durumu
            if (radioButton1.Checked==true)
                kreatin = "EVET";
            else 
                kreatin = "HAYIR";
            // Protein Durumu
            if (radioButton4.Checked == true)
                protein = "EVET";
            else
                protein = "HAYIR";
            // Omega Durumu
            if (radioButton6.Checked == true)
                omega = "EVET";
            else
                omega = "HAYIR";
            // Bcaa Durumu
            if (radioButton8.Checked == true)
                bcaa = "EVET";
            else
                bcaa = "HAYIR";
            // karnitin Durumu
            if (radioButton10.Checked == true)
                karnitin = "EVET";
            else
                karnitin = "HAYIR";
            // probiyotik Durumu
            if (radioButton12.Checked == true)
                probiyotik = "EVET";
            else
                probiyotik = "HAYIR";
            if (textBox1.Text == "")
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Hata ";
            }
            else
            {
                tc_no = textBox1.Text;
                Durum = "ok";
            }
               
            
        }
        private void Supplement_Tc_Sorgulama()
        {
            try
            {
                tc_no = textBox1.Text;
                Tc_Varmi = Supplement_Kosul.Supplement_Tc_Sorgulama(conString, tc_no);
                Kayit_listele();
            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata ";
            }
        }
        private void Suplement_kayit()
        {
            progressBar1.Value = 0;
            Secim_Return();
            try
            {
                if (Durum == "ok")
                {
                    Supplement_Sorgu.Supplement_Kayit(conString, tc_no, kreatin, protein, omega, bcaa, karnitin, probiyotik);
                    islemNe = "kayit";
                    timer1.Start();


                }
                else
                {
                    label8.ForeColor = Color.Red;
                    label8.Text = "Hata ";

                }
            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata ";
            }
            
        }
        private void Kayit_listele()
        { 
            try
            {
                DataTable DataT = new DataTable();
                DataT = Supplement_Sorgu.Supplement_Listeleme(conString);
                dataGridView1.DataSource = DataT;
            }
            catch (Exception ex)
            {
                label8.ForeColor = Color.Red;
                label8.Text = "Hata ";
            }
        }
        private void Supplement_Update()
        {
            progressBar1.Value = 0;
            try
            {
                Supplement_Tc_Sorgulama();
                if (Tc_Varmi == "True")
                {

                    Supplement_Sorgu.Supplement_Update(conString, tc_no, kreatin, protein, omega, bcaa, karnitin, probiyotik);
                    islemNe = "update";
                    timer1.Start();
                }
            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata";
            }
        }
        private void Supplement_Delete()
        {
            progressBar1.Value = 0;
            try
            {
                Supplement_Tc_Sorgulama();
                if (Tc_Varmi == "True")
                {
                    Supplement_Sorgu.Supplement_Delete(conString, tc_no);
                    islemNe = "delete" +
                        "";
                    timer1.Start();
                }
                else
                {
                    label8.ForeColor = Color.Red;
                    label8.Text = "Tc Yok";
                }
            }
            catch (Exception)
            {

                label8.ForeColor = Color.Red;
                label8.Text = "Hata";
            }
        }
        //TOOLBAX İTEM
        private void button1_Click(object sender, EventArgs e)
        {
            Secim_Return();
            Suplement_kayit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Secim_Return();
            Supplement_Update();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Supplement_Delete();
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
                if (islemNe == "kayit")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Kayıt Başarılı";
                }
                else if (islemNe == "update")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Güncelleme Başarılı";
                }

                else if (islemNe == "delete")
                {
                    label8.ForeColor = Color.Green;
                    label8.Text = "Silindi";
                }
                Kayit_listele();
                temizle();
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
    }
}
