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
    public partial class Abone : Form
    {
        // Değişkenler
        sql_sorgu sqlSorgu = new sql_sorgu();
        Sql_kosullar Sql_kosul = new Sql_kosullar();
        string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
        string tc_no = null;
        string ad = null;
        string soyad = null;
        string tel_no = null;
        string cinsiyet = null;
        int abone_time = 0;
        int ucret = 0;
        string egitmen_tc = null;
        string Durum = null;
        string Tc_Varmi = null;
        string İslemNe = null;
        //Değişken Son

        //FORM
        private void kayıt_ekle_Load(object sender, EventArgs e)
        {
            sql_sorgu Abone_Listele = new sql_sorgu();
            DataTable Data_T = new DataTable();
            Data_T = Abone_Listele.Abone_Listeleme(Con_String);
            dataGridView1.DataSource = Data_T;
           
            KoseliForm();
            comboBox1.SelectedItem = "30";
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
        }


        // SQL SORGU SINIFI
        public void Abone_Kayit(string ConString, string Tc_no, string Ad, string Soyad, string Tel_no, string Cinsiyet, int Abone_Time, int Ucret, string Egitmen_Tc)
        {

            SqlConnection abone_baglanti = new SqlConnection(ConString);
            abone_baglanti.Open();
            string abone_kayit = "insert into abone(tc_no,ad,soyad,tel_no,cinsiyet,abone_suresi,ucret,egitmen_tc) values (@tcno,@isim,@soyisim,@telefon,@cinsiyet,@abonet,@ucret,@egt_tc)";
            SqlCommand command_abone = new SqlCommand(abone_kayit, abone_baglanti);
            command_abone.Parameters.AddWithValue("@tcno", Tc_no);
            command_abone.Parameters.AddWithValue("@isim", Ad);
            command_abone.Parameters.AddWithValue("@soyisim", Soyad);
            command_abone.Parameters.AddWithValue("@telefon", Tel_no);
            command_abone.Parameters.AddWithValue("@cinsiyet", Cinsiyet);
            command_abone.Parameters.AddWithValue("@abonet", Abone_Time);
            command_abone.Parameters.AddWithValue("@ucret", Ucret);
            command_abone.Parameters.AddWithValue("@egt_tc", Egitmen_Tc);
            command_abone.ExecuteNonQuery();
            abone_baglanti.Close();

        }
        public void Abone_Update(string ConString, string Tc_no, string Ad, string Soyad, string Tel_no, string Cinsiyet, int Abone_Time, int Ucret, string Egitmen_Tc)
        {

            SqlConnection abone_baglanti = new SqlConnection(ConString);
            abone_baglanti.Open();
            string abone_kayit = "UPDATE Abone SET tc_no=@tcno,ad=@isim, soyad=@soyisim ,tel_no=@telefon,cinsiyet=@cinsiyet,abone_suresi=@abonet,ucret=@ucret,egitmen_tc=@egt_tc WHERE tc_no=@tcno";
            SqlCommand command_abone = new SqlCommand(abone_kayit, abone_baglanti);
            command_abone.Parameters.AddWithValue("@tcno", Tc_no);
            command_abone.Parameters.AddWithValue("@isim", Ad);
            command_abone.Parameters.AddWithValue("@soyisim", Soyad);
            command_abone.Parameters.AddWithValue("@telefon", Tel_no);
            command_abone.Parameters.AddWithValue("@cinsiyet", Cinsiyet);
            command_abone.Parameters.AddWithValue("@abonet", Abone_Time);
            command_abone.Parameters.AddWithValue("@ucret", Ucret);
            command_abone.Parameters.AddWithValue("@egt_tc", Egitmen_Tc);
            command_abone.ExecuteNonQuery();
            abone_baglanti.Close();

        }
        public void Abone_Delete(string ConString, string tc)
        {
            SqlConnection abone_baglanti = new SqlConnection(ConString);
            abone_baglanti.Open();
            string abone_kayit = "DELETE from Abone where tc_no =('" + tc + "')";
            SqlCommand command_abone = new SqlCommand(abone_kayit, abone_baglanti);
            command_abone.ExecuteNonQuery();
            abone_baglanti.Close();
        }
        public DataTable Abone_Listeleme(string ConString)
        {
            SqlConnection abone_baglanti = new SqlConnection(ConString);
            abone_baglanti.Open();
            DataTable dTable = new DataTable();
            using (SqlDataAdapter dAdapter = new SqlDataAdapter("SELECT * FROM abone", abone_baglanti))
            {
                dAdapter.Fill(dTable);
            }
            return dTable;
        }
        // SQL KOSUL SINIFI
        public string Abone_Tc_Sorgulama(string ConString, string tc)
        {
            string cevap = null;
            SqlConnection Egitmen_con = new SqlConnection(ConString);
            Egitmen_con.Open();
            string Egitmen_Sorgu = "SELECT tc_no from Abone where tc_no =('" + tc + "')";
            SqlCommand komut = new SqlCommand(Egitmen_Sorgu, Egitmen_con);
            SqlDataReader DataRead = komut.ExecuteReader();
            cevap = DataRead.Read().ToString();

            return cevap;
        }
        public DataTable Abone_Ad_Soyad_Sorgulama(string ConString, string ad, string soyad)
        {
            SqlConnection Abone_con = new SqlConnection(ConString);
            Abone_con.Open();
            string Abone_Sorgu = "SELECT tc_no,ad,soyad,tel_no from Abone where ad ='" + ad.Trim() + "' and soyad ='" + soyad.Trim() + "'";
            SqlCommand komut = new SqlCommand(Abone_Sorgu, Abone_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Abone_con.Close();
            return DataTab;
        }
        // SINIF SON


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
        private string NotNull()
        {
            if (textBox1.Text == "")
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Tc No Boş !";
            }  
            else if(textBox2.Text =="")
            {
                label11.ForeColor = Color.Red;
                label11.Text = "İsim Boş !";
            }
            else if (textBox3.Text == "")
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Soyisim Boş !";
            }
            else if (textBox4.Text == "")
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Tel no Boş !";
            }
            else
            {
                tc_no = textBox1.Text;
                ad = textBox2.Text;
                soyad = textBox3.Text;
                tel_no = textBox4.Text;
                if (radioButton1.Checked == true)
                {
                    cinsiyet = "Erkek";
                }
                else
                {
                    cinsiyet = "Kadın";
                }

                abone_time = Convert.ToInt32(comboBox1.SelectedItem);
                egitmen_tc = textBox5.Text;
                Durum = "ok";
            }
            return Durum;
        }
        public Abone()
        {
            InitializeComponent();
        }
        private void Girdi_temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedItem = null;
            Durum = null;
            tc_no = null;
            ad = null;
            soyad = null;
            tel_no = null;
            cinsiyet = null;
            abone_time = 0;
            ucret = 0;
            egitmen_tc = null;
        }
        private void Abone_kayit()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum == "ok")
            {
                try
                    
                {
                    Abone_Sorgulama();
                    if (Tc_Varmi!="True")
                    {
                        if (textBox5.Text != "")
                            ucret += 600;

                        sqlSorgu.Abone_Kayit(Con_String, tc_no, ad, soyad, tel_no, cinsiyet, abone_time, ucret, egitmen_tc);
                        İslemNe = "kayit";
                        Girdi_temizle();
                        timer1.Start();
                    }
                    else
                    {
                        label11.ForeColor = Color.Red;
                        label11.Text = "Tc var";
                    }

                }
                catch (Exception ex)
                {
                    label11.ForeColor = Color.Red;
                    label11.Text = "Hata - 1";
                }
            }     
            else
            {
                timer1.Stop();
            }       
        }
        private void Abone_Update()
        {
            progressBar1.Value = 0;
            NotNull();
            if (Durum == "ok")
            {
                try
                {
                    Abone_Sorgulama();
                    if (Tc_Varmi=="True")
                    {
                        if (textBox5.Text != "")
                            ucret += 600;
                        sqlSorgu.Abone_Update(Con_String, tc_no, ad, soyad, tel_no, cinsiyet, abone_time, ucret, egitmen_tc);
                        İslemNe = "update";
                        Girdi_temizle();
                        timer1.Start();
                    }
                    else
                    {
                        label11.ForeColor = Color.Red;
                        label11.Text = "Tc no yok !";
                    }   
                }
                catch (Exception ex)
                {
                    timer1.Stop();
                    label11.ForeColor = Color.Red;
                    label11.Text = "Hata - 2";
                }
            }
            else
            {
                timer1.Stop();
            }
        }
        private void Abone_Delete()
        {
            progressBar1.Value = 0;
            try
            {
                tc_no = textBox1.Text;
                Abone_Sorgulama();
                
                if (Tc_Varmi=="True")
                {
                    sqlSorgu.Abone_Delete(Con_String, tc_no);
                    İslemNe = "delete";
                    timer1.Start();
                }
                else
                {
                    label11.ForeColor = Color.Red;
                    label11.Text = "Tc no yok !";
                }
                    

            }
            catch (Exception ex)
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Hata - 3";
                
            }
           
        }
        private void Abone_Sorgulama()
        {
            try
            {
                tc_no = textBox1.Text;
                Tc_Varmi = Sql_kosul.Abone_Tc_Sorgulama(Con_String, tc_no);
                
                
            }
            catch (Exception)
            {

                label11.ForeColor = Color.Red;
                label11.Text = "Hata - 4";
            }
        }
        private void Kayıt_listele()
        {
            try
            {
                sql_sorgu Abone_Listele = new sql_sorgu();
                DataTable Data_T = new DataTable();
                Data_T = Abone_Listele.Abone_Listeleme(Con_String);
                dataGridView1.DataSource = Data_T;
            }
            catch (Exception ex)
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Hata - 5";
            }
        }
        private void Abone_Ad_Soyad_Sorgulama()
        {
            ad = textBox2.Text;
            soyad = textBox3.Text;
            if (ad != "")
            { 
                if(soyad != "")
                {
                    DataTable DataT = new DataTable();
                    DataT = Sql_kosul.Abone_Ad_Soyad_Sorgulama(Con_String, ad, soyad);
                    dataGridView1.DataSource=DataT;
                }
                else
                {
                    label11.ForeColor = Color.Red;
                    label11.Text = "Soyisim Boş";
                }
                    

            }
            else
            {
                label11.ForeColor = Color.Red;
                label11.Text = "İsim Boş";
            }
        }
        private void SP_Egitmen_Sorgu()
        {
            string adi = null;
            DataTable ds = new DataTable();
            string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(Con_String);
            conn.Open();
            SqlCommand command = new SqlCommand(Convert.ToString("AboneEgitmenGetir"), conn);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                adi = dr["ad"].ToString();
            }
            dr.Close();
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show(adi);
        } // ÇALIŞMIYOR
        private DataTable SP_Abone_Filter_Table(string pro_name)
        {
            DataTable ds = new DataTable();
            try
            {
                
                string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(Con_String);
                conn.Open();
                SqlCommand command = new SqlCommand(pro_name, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@cinsiyet", SqlDbType.NVarChar, 50);
                command.Parameters["@cinsiyet"].Value = comboBox2.SelectedItem.ToString();
                command.Parameters.Add("@sure", SqlDbType.Int);
                command.Parameters["@sure"].Value = Convert.ToInt32(comboBox3.SelectedItem);
                command.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception)
            {

                label11.ForeColor = Color.Red;
                label11.Text = "Hata - 6";
            }
            return ds;
        }



        // TOOLBAX İTEM
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                if (textBox5.Text != "")
                {
                    label7.Text = "620 TL";
                    ucret = 620;
                }
                else
                {
                    label7.Text = "120 TL";
                    ucret = 120;
                }
                
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (textBox5.Text != "")
                {
                    label7.Text = "700 TL";
                    ucret = 700;
                }
                else
                {
                    label7.Text = "200 TL";
                    ucret = 200;
                }

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (textBox5.Text != "")
                {
                    label7.Text = "850 TL";
                    ucret = 850;
                }
                else
                {
                    label7.Text = "350 TL";
                    ucret = 350;
                }

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                if(textBox5.Text!="")
                {
                    label7.Text = "1600 TL";
                    ucret = 1600;
                }
                else
                {
                    label7.Text = "1100 TL";
                    ucret = 1100;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //if (abone_baglanti.State == ConnectionState.Closed)
            Abone_Update();
        } // GÜNCELLE
        private void button3_Click(object sender, EventArgs e)
        {
            Abone_Delete();
        } // SİL
        private void button4_Click(object sender, EventArgs e)
        {
            Abone_Ad_Soyad_Sorgulama();
        } // İSİM SOYİSİM SORGUSU
        private void button4_MouseHover(object sender, EventArgs e)
        {
            ToolTip T1 = new ToolTip();
            T1.SetToolTip(button4, "Tc no bilmiyorsanız isim soyisim girerek aratınız");
            T1.ToolTipTitle = "Bilgilendirme";
            T1.ToolTipIcon = ToolTipIcon.Info;
            T1.IsBalloon = true;
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {

            ToolTip T1 = new ToolTip();
            T1.SetToolTip(button1, "Tüm alanları Eksiksiz Doldurduysanız Kayıt Yapabilirsiniz.");
            T1.ToolTipTitle = "Bilgilendirme";
            T1.ToolTipIcon = ToolTipIcon.Info;
            T1.IsBalloon = true;


                
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "DİKKAT", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        } // ÇIKIŞ
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                if(İslemNe=="kayit")
                    label11.Text = "Kayıt Başarılı...";
                if(İslemNe=="update")
                    label11.Text = "Güncelleme Başarılı...";
                if(İslemNe=="delete")
                    label11.Text = "Kayıt Silindi...";
                Kayıt_listele();
                Girdi_temizle();
                timer1.Stop();
            }

            else
            {
                label11.Text = "Yükleniyor...";
                label11.ForeColor = Color.Green;
                progressBar1.Value += 5;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //if (abone_baglanti.State == ConnectionState.Closed)
            Abone_kayit();

        }// KAYDET
        private void button6_Click(object sender, EventArgs e)
        {
            SP_Egitmen_Sorgu();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            DataTable DataT = new DataTable();
            DataT = SP_Abone_Filter_Table("AboneFilter");
            dataGridView1.DataSource = DataT;
        } // FİLTRELE
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
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string deger = textBox5.Text;
            for (int i = 0; i < deger.Length; i++)
                if (!char.IsDigit(deger[i]))
                {

                    textBox5.Text = textBox5.Text.Substring(0, textBox5.Text.Length - 1);
                    ToolTip T1 = new ToolTip();
                    T1.SetToolTip(textBox5, "Sadece Sayı girişi yapınız !!");
                    T1.ToolTipTitle = "Bilgilendirme";
                    T1.ToolTipIcon = ToolTipIcon.Warning;
                    T1.IsBalloon = true;
                }
        }
    }
}

//KAYNNAKLAR
//https://www.yazilimkodlama.com/programlama/c-stored-procedure-ile-baglanti-islemleri/
//https://tr.coredump.biz/questions/24645587/returning-values-from-a-sql-server-stored-procedure
//https://stackoverflow.com/questions/13685226/c-sharp-sqldatareader-for-int
//https://stackoverflow.com/questions/14119133/conversion-failed-when-converting-date-and-or-time-from-character-string-while-i
//https://caglartelef.com/sql-server-komutlari/
//https://www.bilisimogretmeni.com/programlama/mssql-komutlari-kullanimlari-ve-aciklamlari.html
//