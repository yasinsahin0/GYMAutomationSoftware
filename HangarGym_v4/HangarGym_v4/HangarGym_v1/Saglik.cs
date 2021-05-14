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
    public partial class Saglik : Form
    {
        sql_sorgu SqlSorgu = new sql_sorgu();
        Sql_kosullar SqlKosul = new Sql_kosullar();
        string conString = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
        string tc_no = null;
        string hes_kodu = null;
        string saglik_durumu = null;
        string takviye_durumu = null;
        string Durum = null;
        string Tc_Varmi = null;
        string İslemNe = null;
        //FORM

        public Saglik()
        {
            InitializeComponent();
        }
        private void saglik_Load(object sender, EventArgs e)
        {
            Saglik_Bilgisi_Sorgulama();
            KoseliForm();
            dataGridView1.Columns[0].HeaderText = "Hes Kodu";
            dataGridView1.Columns[1].HeaderText = "Sağlık D.";
            dataGridView1.Columns[2].HeaderText = "Takviye";
            dataGridView1.Columns[3].HeaderText = "TC No";
            progressBar1.ForeColor = Color.DarkBlue;
        }
        //FONKSİYONLAR
        private string NotNull()
        {
            if (textBox1.Text == "")
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Tc No Boş";
            }

            else if (textBox2.Text == "")
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Hes Kodu Boş";
            }
                
            else if (comboBox1.SelectedItem==null)
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Sağlık Durumu Boş";
            }
            else if (comboBox2.SelectedItem == null)
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Takviye Durumu Boş";
            }
            else
            {
                tc_no = textBox1.Text;
                hes_kodu = textBox2.Text;
                saglik_durumu = comboBox1.SelectedItem.ToString();
                takviye_durumu = comboBox2.SelectedItem.ToString();
                Durum = "ok";
            }
            return Durum;
        }
        private void Girdi_Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            Durum = null;
            tc_no = null;
            hes_kodu = null;
            saglik_durumu = null;
            takviye_durumu = null;
            Durum = null;
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
        private void Saglik_Bilgisi_Ekle()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum=="ok")
            {
                try
                {
                    sql_sorgu Saglik_s = new sql_sorgu();
                    Saglik_s.Saglik_Kayit(conString, tc_no, hes_kodu, saglik_durumu, takviye_durumu);
                    
                    İslemNe = "kayit";
                    timer1.Start();
                }
                catch (Exception)
                {

                    label5.ForeColor = Color.Red;
                    label5.Text = "Yanlış Bilgi";
                }
            }
        }
        private void Saglik_Bilgisi_Sorgulama()
        {
            try
            {
                sql_sorgu Saglik_Sorgu = new sql_sorgu();
                DataTable DataT = new DataTable();
                DataT = Saglik_Sorgu.Saglik_Listeleme(conString);
                dataGridView1.DataSource = DataT;  
            }
            catch (Exception ex)
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Hata";
            }
            
        }
        private void Saglik_Update()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum == "ok")
            {
                try
                {
                    if (Tc_Varmi == "True")
                    {
                        SqlSorgu.Saglik_Update(conString, tc_no, hes_kodu, saglik_durumu, takviye_durumu);
                        İslemNe = "update";
                        timer1.Start();
                    }
                    else
                    {
                        label5.ForeColor = Color.Red;
                        label5.Text = "Tc No Hatası";
                    }

                }
                catch (Exception ex)
                {

                    label5.ForeColor = Color.Red;
                    label5.Text = "Bağlantı Hatası";
                }
            }
            else
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Eksik Alan Var";
            }
        } 
        private void Saglik_Delete()
        {
            progressBar1.Value = 0;
            try
            {
                tc_no = textBox1.Text;
                Saglik_Tc_Sorgulama();
                if (Tc_Varmi == "True")
                {
                    SqlSorgu.Saglik_Delete(conString, tc_no);
                    İslemNe = "delete";
                    timer1.Start();
                }
                else
                {
                    label5.ForeColor = Color.Red;
                    label5.Text = "Tc No Hatası";
                }
            }
            catch (Exception)
            {

                label5.ForeColor = Color.Red;
                label5.Text = "Bağlantı Hatası";
            }
        }
        private void Saglik_Tc_Sorgulama()
        {
            try
            {
                tc_no = textBox1.Text;
                Tc_Varmi = SqlKosul.Saglik_Tc_Sorgulama(conString, tc_no);
                Saglik_Bilgisi_Sorgulama();
            }
            catch (Exception)
            {

                label5.ForeColor = Color.Red;
                label5.Text = "Hata";
            }
        }
        
        // TOOLBAX İTEMS
        private void button1_Click(object sender, EventArgs e)
        {
            Saglik_Bilgisi_Ekle();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Saglik_Tc_Sorgulama();
            Saglik_Update();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Saglik_Delete();
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                if (İslemNe == "kayit")
                {
                    label5.ForeColor = Color.Green;
                    label5.Text = "Kayıt Başarılı";
                }
                    
                if (İslemNe == "update")
                {
                    label5.ForeColor = Color.Green;
                    label5.Text = "Güncelleme Başarılı";
                }
                if (İslemNe == "delete")
                {
                    label5.ForeColor = Color.Green;
                    label5.Text = "Silme Başarılı";
                }
                Saglik_Bilgisi_Sorgulama();
                Girdi_Temizle();
                timer1.Stop();
            }

            else
            {
                label5.Text = "Yükleniyor";
                label5.ForeColor = Color.Green;
                progressBar1.Value += 5;
            }
        }
        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip T1 = new ToolTip();
            T1.SetToolTip(textBox1, "Tc no Bilginizi buraya girin boşluk bırakmayın 11 hane olmasına dikkat edin");
            T1.ToolTipTitle = "Bilgilendirme";
            T1.ToolTipIcon = ToolTipIcon.Info;
            T1.IsBalloon = true;
        }
        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip T1 = new ToolTip();
            T1.SetToolTip(textBox2, "Hes Kodu Bilginizi girin aralara - işareti koymayı unutmayın");
            T1.ToolTipTitle = "Bilgilendirme";
            T1.ToolTipIcon = ToolTipIcon.Info;
            T1.IsBalloon = true;
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
