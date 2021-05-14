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
using System.Collections;
using System.Net.NetworkInformation;

namespace HangarGym_v1
{
    public partial class Main : Form
    {
        Sql_kosullar sql_kosul = new Sql_kosullar();
        sql_sorgu Sql_Sorgu = new sql_sorgu();
        string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;

        public string tc_no = null;
        int online_kisi_sayisi = 0;
        string cinsiyet = null;
        int Abone_T = 0;
        string Saglık_Durumu = null;
        string date;

        //FORM EVENTLERİ
        public Main()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Online_Listeleme();
          
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                label4.Text = "Online";
                label4.ForeColor = Color.Green;
            }
            else
            {
                label4.Text = "Offline";
                label4.ForeColor = Color.Red;
            }
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            radioButton7.Checked = true;
            DataTable yen = new DataTable();
            yen = SP_Online_List("OnlineKisiler");
            dataGridView2.DataSource = yen;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
  
      {
            Application.Exit();
        }

        // STRİP MENÜLER
        private void kayıtEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abone kyt = new Abone();
            kyt.ShowDialog();
               
        }

        private void hESKoduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Saglik sgl = new Saglik();
            sgl.ShowDialog();
   
        }

        private void ölçümBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                    Olcum olcum_bilg = new Olcum();
                    olcum_bilg.ShowDialog();
                    
               
        }

        private void supplementlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
                    Supplement splmnt = new Supplement();
                    splmnt.ShowDialog();
                   
        }

        private void eğitmenlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                    Egitmenler egitmen = new Egitmenler();
                    egitmen.ShowDialog();
                    
               
        }

        private void küçültToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void maliDurumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mali malii = new Mali();
            malii.ShowDialog();
            
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "DİKKAT", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                Application.Exit();
            }

        }

        //FONKSİYONLAR
        private void Filter_return()
        {
            if (radioButton1.Checked == true)
                cinsiyet = "Kadın";
            else if (radioButton2.Checked == true)
                cinsiyet = "Erkek";
            if (radioButton3.Checked == true)
                Abone_T = 30;
            else if (radioButton4.Checked == true)
                Abone_T = 60;
            else if (radioButton5.Checked == true)
                Abone_T = 90;
            else if (radioButton6.Checked == true)
                Abone_T = 360;
            if (radioButton7.Checked == true)
                Saglık_Durumu = "Saglikli";
            else if (radioButton8.Checked == true)
                Saglık_Durumu = "Hasta";
        }
        private void Online_Listeleme()
        {
            tc_no = textBox1.Text;
            try
            {
                
                DataTable DataT = new DataTable();
                DataT = Sql_Sorgu.Online_Listeleme(Con_String, tc_no);
                dataGridView1.DataSource = DataT;
                online_kisi_sayisi = (dataGridView1.Rows.Count)-1;
                if (online_kisi_sayisi<=5)
                {
                    label3.ForeColor = Color.Green;
                    label3.Text = online_kisi_sayisi.ToString();
                }
                else if (online_kisi_sayisi > 5 && online_kisi_sayisi <=10)
                {
                    label3.ForeColor = Color.Yellow;
                    label3.Text = online_kisi_sayisi.ToString();
                }
                else if (online_kisi_sayisi > 10 && online_kisi_sayisi <= 15)
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = online_kisi_sayisi.ToString();
                }
            }
            catch (Exception)
            {


            }
        }
        private DataTable SP_Online_List(string pro_name)
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
        private DataTable SP_Main_Filter_Table(string pro_name)
        {
            
            DataTable ds = new DataTable();
            string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(Con_String);
            conn.Open();
            SqlCommand command = new SqlCommand(pro_name, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@cinsiyet", SqlDbType.VarChar, 50);
            command.Parameters["@cinsiyet"].Value = cinsiyet;
            command.Parameters.Add("@sure", SqlDbType.Int);
            command.Parameters["@sure"].Value = Abone_T;
            command.Parameters.Add("@saglik", SqlDbType.VarChar, 50);
            command.Parameters["@saglik"].Value = Saglık_Durumu;
            command.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            da.Fill(ds);

            conn.Close();
            return ds;
        }
        private DataTable SP_Gider_Filter_Table(int ilkdeger,int sondeger)
        {
            DataTable ds = new DataTable();
            try
            {

                string Con_String = ConfigurationManager.ConnectionStrings["HangarGym_v1.Properties.Settings.HangardbConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(Con_String);
                conn.Open();
                SqlCommand command = new SqlCommand("MaliyetFilter", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@date", SqlDbType.NVarChar, 50);
                command.Parameters["@date"].Value = date;
                command.Parameters.Add("@person", SqlDbType.NVarChar, 50);
                command.Parameters["@person"].Value = comboBox1.SelectedItem.ToString();
                command.Parameters.Add("@giderilk", SqlDbType.Int);
                command.Parameters["@giderilk"].Value = ilkdeger;
                command.Parameters.Add("@giderson", SqlDbType.Int);
                command.Parameters["@giderson"].Value = sondeger;
                command.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception)
            {

            }
            return ds;
        }


        // TOOL İTEMLER
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string gelen = null;
                Boolean sagliklimi;
                sagliklimi = sql_kosul.Online_Saglik_Kontrol(Con_String, textBox1.Text);
                Boolean varmi = sql_kosul.Online_Varmi(Con_String, textBox1.Text);
                if(varmi != true)
                {
                    if (sagliklimi == true)
                    {
                        if (online_kisi_sayisi <= 15)
                        {
                            gelen = Sql_Sorgu.Online_Kayit(Con_String, textBox1.Text);
                            label6.ForeColor = Color.Green;
                            label6.Text = "Giriş Yapıldı";
                            Online_Listeleme();
                            textBox1.Text = "";
                        }
                    }
                    else
                    {
                        label6.ForeColor = Color.Yellow;
                        label6.Text = "Sağlıklı Değil";
                    }
                }
                else
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = "İçeride !";
                }
                
                    
            }
            catch (Exception)
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Hata";
            }
            DataTable yen = new DataTable();
            yen = SP_Online_List("OnlineKisiler");
            dataGridView2.DataSource = yen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="")
            {
                try
                {
                    Boolean varmi = sql_kosul.Online_Varmi(Con_String, textBox1.Text);
                    if (varmi == true)
                    {
                        string delete = Sql_Sorgu.Online_Delete(Con_String, textBox1.Text);
                        label6.Text = "Çıkış yapıldı";
                        label6.ForeColor = Color.Green;
                        Online_Listeleme();
                        textBox1.Text = "";
                    }
                    else
                    {
                        label6.Text = "İçeride Değil";
                        label6.ForeColor = Color.Red;
                    }

                }
                catch (Exception)
                {

                    label6.Text = "Tc Yok";
                    label6.ForeColor = Color.Red;
                }

            }
            else
            {
                label6.Text = "Hata";
                label6.ForeColor = Color.Red;
            }
            DataTable yen = new DataTable();
            yen = SP_Online_List("OnlineKisiler");
            dataGridView2.DataSource = yen;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Filter_return();
            DataTable yen = new DataTable();
            yen = SP_Main_Filter_Table("MainFilter");
            dataGridView3.DataSource = yen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            DataTable DataT = new DataTable();
            if (comboBox2.SelectedIndex ==0)
            {//0 ile 100 arasında
                DataT = SP_Gider_Filter_Table(0,100);
                dataGridView4.DataSource = DataT;
            }
            else if (comboBox2.SelectedIndex == 1)
            {//100 ile 200 arasında
                DataT = SP_Gider_Filter_Table(100, 200);
                dataGridView4.DataSource = DataT;
            }
            else if (comboBox2.SelectedIndex == 2)
            {//200 ile 300 arasında
                DataT = SP_Gider_Filter_Table(200, 300);
                dataGridView4.DataSource = DataT;
            }
            else if (comboBox2.SelectedIndex == 3)
            {//300 den büyük
                DataT = SP_Gider_Filter_Table(300, 99999);
                dataGridView4.DataSource = DataT;
            }
            else if (comboBox2.SelectedIndex == 4)
            {//300 den büyük
                DataT = SP_Gider_Filter_Table(0, 99999);
                dataGridView4.DataSource = DataT;
            }

        }
    }

}

//KAYNAKLAR
//https://bidb.itu.edu.tr/seyir-defteri/blog/2013/09/06/sakl%C4%B1-yordamlar-(stored-procedures)
//http://bilisim.kocaeli.edu.tr/dosyalar/Dosyalar/DersNotlari/hafta10_storedprocedure.pdf
//https://docs.microsoft.com/tr-tr/aspnet/web-forms/overview/data-access/accessing-the-database-directly-from-an-aspnet-page/using-parameterized-queries-with-the-sqldatasource-cs
//https://www.yazilimkodlama.com/sql-server-2/sql-server-view-gorunum-olusturma/
//http://kod5.org/sql-in-kullanimi/
//https://www.yusufsezer.com.tr/mysql-stored-procedure/
//http://elvanerdem.blogspot.com/2012/08/stored-procedure-ve-c-ta-stored.html
//https://social.msdn.microsoft.com/Forums/tr-TR/ebd05e9a-ada4-44b7-bb43-3dc4fc74e96e/datagridview-sutun-ad-deitirmek?forum=csharptr