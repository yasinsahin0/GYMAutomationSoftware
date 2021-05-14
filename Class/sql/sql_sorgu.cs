using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace sql
{

    public class sql_sorgu
    {
        // ABONE İŞLEMLERİ
        //Kaynak 4
        public void Abone_Kayit(string ConString,string Tc_no,string Ad,string Soyad,string Tel_no,string Cinsiyet,int Abone_Time,int Ucret,string Egitmen_Tc)
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
        //Kaynak 3
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
        //Kaynak 1
        public void Abone_Delete(string ConString,string tc)
        {
            SqlConnection abone_baglanti = new SqlConnection(ConString);
            abone_baglanti.Open();
            string abone_kayit = "DELETE from [Abone] where tc_no IN(" + tc + ")";
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
        
        //SAĞLIK İŞLEMLERİ
        public void Saglik_Kayit(string ConString,string TcNo,string HesKodu,string SaglikDurumu,string TakviyeDurumu)
        {
            SqlConnection Saglik_con = new SqlConnection(ConString);
            Saglik_con.Open();
            string Saglik_Sorgu = "insert into Saglik_Bilgileri(hes_kodu,saglik_durumu,takviye_kullanimi,tc_no) values (@hes,@sglk,@tkvy,@tc)";
            SqlCommand komut_saglik = new SqlCommand(Saglik_Sorgu, Saglik_con);
            komut_saglik.Parameters.AddWithValue("@hes", HesKodu);
            komut_saglik.Parameters.AddWithValue("@sglk", SaglikDurumu);
            komut_saglik.Parameters.AddWithValue("@tkvy", TakviyeDurumu);
            komut_saglik.Parameters.AddWithValue("@tc", TcNo);
            komut_saglik.ExecuteNonQuery();
            Saglik_con.Close();
        }
        public void Saglik_Update(string ConString, string TcNo, string HesKodu, string SaglikDurumu, string TakviyeDurumu)
        {

            SqlConnection Saglik_baglanti = new SqlConnection(ConString);
            Saglik_baglanti.Open();
            string Saglik_kayit = "UPDATE Saglik_Bilgileri SET hes_kodu=@hes,saglik_durumu=@saglik, takviye_kullanimi=@takviye ,tc_no=@tc WHERE tc_no=@tc";
            SqlCommand command_abone = new SqlCommand(Saglik_kayit, Saglik_baglanti);
            command_abone.Parameters.AddWithValue("@tc", TcNo);
            command_abone.Parameters.AddWithValue("@hes", HesKodu);
            command_abone.Parameters.AddWithValue("@saglik", SaglikDurumu);
            command_abone.Parameters.AddWithValue("@takviye", TakviyeDurumu);
            command_abone.ExecuteNonQuery();
            Saglik_baglanti.Close();

        }
        public void Saglik_Delete(string ConString, string tc)
        {
            SqlConnection Saglik_baglanti = new SqlConnection(ConString);
            Saglik_baglanti.Open();
            string Saglik_kayit = "DELETE from [Saglik_Bilgileri] where tc_no IN(" + tc + ")";
            SqlCommand command_abone = new SqlCommand(Saglik_kayit, Saglik_baglanti);
            command_abone.ExecuteNonQuery();
            Saglik_baglanti.Close();
        }
        public DataTable Saglik_Listeleme(string ConString)
        {
            SqlConnection Saglik_con = new SqlConnection(ConString);
            Saglik_con.Open();
            string Saglik_Sorgu = "SELECT * from Saglik_Bilgileri";
            SqlCommand komut = new SqlCommand(Saglik_Sorgu, Saglik_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Saglik_con.Close();
            return DataTab;
        }


        //OLCÜM İŞLEMLERİ
        public void Olcum_Bilgi_Kayit(string ConString,string TcNo,string Kg,string Boy,string Yag,string Yas,string Vki)
        {
            SqlConnection Olcum_baglanti = new SqlConnection(ConString);
            Olcum_baglanti.Open();
            string Olcum_con = "insert into Olcum_Bilgileri(tc_no,kg,boy,yag_orani,yas,vki) values (@tcno,@kg,@boy,@yag,@yas,@vki)";
            SqlCommand komut_olcum = new SqlCommand(Olcum_con, Olcum_baglanti);
            komut_olcum.Parameters.AddWithValue("@tcno", TcNo);
            komut_olcum.Parameters.AddWithValue("@kg", Kg);
            komut_olcum.Parameters.AddWithValue("@boy", Boy);
            komut_olcum.Parameters.AddWithValue("@yag", Yag);
            komut_olcum.Parameters.AddWithValue("@yas", Yas);
            komut_olcum.Parameters.AddWithValue("@vki", Vki);
            komut_olcum.ExecuteNonQuery();
            Olcum_baglanti.Close();
        }
        public void Olcum_Update(string ConString, string TcNo, string Kg, string Boy, string Yag, string Yas, string Vki)
        {
            SqlConnection Olcum_baglanti = new SqlConnection(ConString);
            Olcum_baglanti.Open();
            string Olcum_kayit = "UPDATE Olcum_Bilgileri SET tc_no=@tc,kg=@kg, boy=@boy ,yag_orani=@yag,yas=@yas,vki=@vki WHERE tc_no=@tc";
            SqlCommand command_abone = new SqlCommand(Olcum_kayit, Olcum_baglanti);
            command_abone.Parameters.AddWithValue("@tc", TcNo);
            command_abone.Parameters.AddWithValue("@kg", Kg);
            command_abone.Parameters.AddWithValue("@boy", Boy);
            command_abone.Parameters.AddWithValue("@yag", Yag);
            command_abone.Parameters.AddWithValue("@yas", Yas);
            command_abone.Parameters.AddWithValue("@vki", Vki);
            command_abone.ExecuteNonQuery();
            Olcum_baglanti.Close();
        }
        public void Olcum_Delete(string ConString, string tc)
        {
            SqlConnection Olcum_baglanti = new SqlConnection(ConString);
            Olcum_baglanti.Open();
            string Olcum_kayit = "DELETE from [Olcum_Bilgileri] where tc_no IN(" + tc + ")";
            SqlCommand command_abone = new SqlCommand(Olcum_kayit, Olcum_baglanti);
            command_abone.ExecuteNonQuery();
            Olcum_baglanti.Close();
        }
        public DataTable Olcum_Listeleme(string ConString)
        {
            SqlConnection Olcum_con = new SqlConnection(ConString);
            Olcum_con.Open();
            string Olcum_Sorgu = "SELECT * from Olcum_Bilgileri";
            SqlCommand komut = new SqlCommand(Olcum_Sorgu, Olcum_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Olcum_con.Close();
            return DataTab;
        }

        //SUPPLEMENT İŞLEMLERİ
        public void Supplement_Update(string ConString,string TcNo,string kreatin,string protein,string omega,string bcaa,string karnitin,string probiyotik)
        {
            SqlConnection Supplement_baglanti = new SqlConnection(ConString);
            Supplement_baglanti.Open();
            string Supplement_con = "UPDATE Supplementler SET tc_no=@tcno,kreatin=@kreatin, protein=@protein ,omega_3=@omega,bcaa=@bcaa,Lkarnitin=@lkarnitin,probiyotik=@probiyotik WHERE tc_no=@tcno";
            SqlCommand command_suplement = new SqlCommand(Supplement_con, Supplement_baglanti);
            command_suplement.Parameters.AddWithValue("@tcno", TcNo);
            command_suplement.Parameters.AddWithValue("@kreatin", kreatin);
            command_suplement.Parameters.AddWithValue("@protein", protein);
            command_suplement.Parameters.AddWithValue("@omega", omega);
            command_suplement.Parameters.AddWithValue("@bcaa", bcaa);
            command_suplement.Parameters.AddWithValue("@lkarnitin", karnitin);
            command_suplement.Parameters.AddWithValue("@probiyotik", probiyotik);
            command_suplement.ExecuteNonQuery();
            Supplement_baglanti.Close();
        }
        public void Supplement_Kayit(string ConString, string TcNo, string kreatin, string protein, string omega, string bcaa, string karnitin, string probiyotik)
        {
            SqlConnection Supplement_baglanti = new SqlConnection(ConString);
            Supplement_baglanti.Open();
            string Supplement_con = "insert into Supplementler(tc_no,kreatin,protein,omega_3,bcaa,Lkarnitin,probiyotik) values (@tcno,@kreatin,@protein,@omega,@bcaa,@lkarnitin,@probiyotik)";
            SqlCommand command_suplement = new SqlCommand(Supplement_con, Supplement_baglanti);
            command_suplement.Parameters.AddWithValue("@tcno", TcNo);
            command_suplement.Parameters.AddWithValue("@kreatin", kreatin);
            command_suplement.Parameters.AddWithValue("@protein", protein);
            command_suplement.Parameters.AddWithValue("@omega", omega);
            command_suplement.Parameters.AddWithValue("@bcaa", bcaa);
            command_suplement.Parameters.AddWithValue("@lkarnitin", karnitin);
            command_suplement.Parameters.AddWithValue("@probiyotik", probiyotik);
            command_suplement.ExecuteNonQuery();
            Supplement_baglanti.Close();
        }
        public void Supplement_Delete(string ConString, string tc)
        {
            SqlConnection Supplement_baglanti = new SqlConnection(ConString);
            Supplement_baglanti.Open();
            string Supplement_con = "DELETE from [Supplementler] where tc_no IN(" + tc + ")";
            SqlCommand command_suplement = new SqlCommand(Supplement_con, Supplement_baglanti);

            command_suplement.ExecuteNonQuery();
            Supplement_baglanti.Close();
        }
        public DataTable Supplement_Listeleme(string ConString)
        {
            SqlConnection Supplement_con = new SqlConnection(ConString);
            Supplement_con.Open();
            string Supplement_Sorgu = "SELECT * from Supplementler";
            SqlCommand komut = new SqlCommand(Supplement_Sorgu, Supplement_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Supplement_con.Close();
            return DataTab;
        }


        //EGİTMEN İŞLEMLERİ
        public void Egitmen_Update(string ConString,string EgtTcNo,string ad,string soyad,string uzmanlik)
        {
            SqlConnection Egitmen_baglanti = new SqlConnection(ConString);
            Egitmen_baglanti.Open();
            string Egitmen_con = "UPDATE egitmenler SET egt_tc_no=@egittcno,ad=@ad, soyad=@soyad ,uzmanlik=@uzmanlik WHERE egt_tc_no=@egittcno";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Egitmen_baglanti);
            command_egitmen.Parameters.AddWithValue("@egittcno", EgtTcNo);
            command_egitmen.Parameters.AddWithValue("@ad", ad);
            command_egitmen.Parameters.AddWithValue("@soyad", soyad);
            command_egitmen.Parameters.AddWithValue("@uzmanlik", uzmanlik);

            command_egitmen.ExecuteNonQuery();
            Egitmen_baglanti.Close();
        }
        public void Egitmen_Delete(string ConString,string tc)
        {
            SqlConnection Egitmen_baglanti = new SqlConnection(ConString);
            Egitmen_baglanti.Open();
            string Egitmen_con = "DELETE from [egitmenler] where egt_tc_no IN(" + tc + ")";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Egitmen_baglanti);
            command_egitmen.ExecuteNonQuery();
            Egitmen_baglanti.Close();
        }
        public void Egitmen_Kayit(string ConString, string EgtTcNo, string ad, string soyad, string uzmanlik)
        {
            SqlConnection Egitmen_baglanti = new SqlConnection(ConString);
            Egitmen_baglanti.Open();
            string Egitmen_con = "insert into egitmenler(egt_tc_no,ad,soyad,uzmanlik) values (@egittcno,@ad,@soyad,@uzmanlık)";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Egitmen_baglanti);
            command_egitmen.Parameters.AddWithValue("@egittcno", EgtTcNo);
            command_egitmen.Parameters.AddWithValue("@ad", ad);
            command_egitmen.Parameters.AddWithValue("@soyad", soyad);
            command_egitmen.Parameters.AddWithValue("@uzmanlık", uzmanlik);

            command_egitmen.ExecuteNonQuery();
            Egitmen_baglanti.Close();
        }
        public DataTable Egitmen_Listeleme(string ConString)
        {
            SqlConnection Egitmen_con = new SqlConnection(ConString);
            Egitmen_con.Open();
            string Egitmen_Sorgu = "SELECT * from egitmenler";
            SqlCommand komut = new SqlCommand(Egitmen_Sorgu, Egitmen_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Egitmen_con.Close();
            return DataTab;
        }

        //MALİYE İŞLEMLERİ
        public void Maliye_Kayit(string ConString,string date,string fatura_no,string aciklama,string personel,int gider)
            {
            SqlConnection Maliye_baglanti = new SqlConnection(ConString);
            Maliye_baglanti.Open();
            string Egitmen_con = "insert into Giderler(tarih,fatura_no,aciklama,islem_yapan,gider) values (@date,@fatura,@aciklama,@personel,@gider)";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Maliye_baglanti);
            command_egitmen.Parameters.AddWithValue("@date", date);
            command_egitmen.Parameters.AddWithValue("@fatura", fatura_no.Trim());
            command_egitmen.Parameters.AddWithValue("@aciklama", aciklama);
            command_egitmen.Parameters.AddWithValue("@personel", personel);
            command_egitmen.Parameters.AddWithValue("@gider", gider);
            command_egitmen.ExecuteNonQuery();
            Maliye_baglanti.Close();
        }
        public void Maliye_Update(string ConString, string date, string fatura_no, string aciklama, string personel, int gider)
        {
            SqlConnection Maliye_baglanti = new SqlConnection(ConString);
            Maliye_baglanti.Open();
            string Egitmen_con = "UPDATE Giderler SET tarih=@date,fatura_no=@fatura, aciklama=@aciklama ,islem_yapan=@personel,gider=@gider WHERE fatura_no=@fatura";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Maliye_baglanti);
            command_egitmen.Parameters.AddWithValue("@date", date);
            command_egitmen.Parameters.AddWithValue("@fatura", fatura_no);
            command_egitmen.Parameters.AddWithValue("@aciklama", aciklama);
            command_egitmen.Parameters.AddWithValue("@personel", personel);
            command_egitmen.Parameters.AddWithValue("@gider", gider);
            command_egitmen.ExecuteNonQuery();
            Maliye_baglanti.Close();
        }
        public void Maliye_Delete(string ConString, string fatura_no)
        {
            SqlConnection Maliye_baglanti = new SqlConnection(ConString);
            Maliye_baglanti.Open();
            string Egitmen_con = "DELETE from [Giderler] where fatura_no IN(" + fatura_no + ")";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Maliye_baglanti);
            command_egitmen.ExecuteNonQuery();
            Maliye_baglanti.Close();
        }
        public DataTable Maliye_Listeleme(string ConString)
        {
            SqlConnection Maliye_con = new SqlConnection(ConString);
            Maliye_con.Open();
            string Maliye_Sorgu = "SELECT * from Giderler";
            SqlCommand komut = new SqlCommand(Maliye_Sorgu, Maliye_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Maliye_con.Close();
            return DataTab;
        }

        //ONLİNE İŞLEMLERİ
        public DataTable Online_Listeleme(string ConString,string tc)
        {
            SqlConnection Online_baglanti = new SqlConnection(ConString);
            Online_baglanti.Open();
            DataTable dTable = new DataTable();
            using (SqlDataAdapter dAdapter = new SqlDataAdapter("SELECT ad,soyad FROM Online", Online_baglanti))
            {
                dAdapter.Fill(dTable);
            }
            Online_baglanti.Close();
            return dTable;
        } 
        public string Online_Kayit(string ConString,string tc)
        {
            string adi = null;
            string soyadi = null;
            SqlConnection Online_baglanti = new SqlConnection(ConString);
            Online_baglanti.Open();
            SqlCommand Komut = new SqlCommand();
            Komut.Connection = Online_baglanti;
            Komut.CommandText = "SELECT ad,soyad FROM Abone where tc_no IN(" + tc + ")";
            
            SqlDataReader dr = Komut.ExecuteReader();

            while (dr.Read())
            {
                adi = dr["ad"].ToString();
                soyadi = dr["soyad"].ToString();
            }
            dr.Close();
            Online_baglanti.Close();
            Online_baglanti.Open();

            string Egitmen_con = "insert into Online(ad,soyad) values (@ad,@soyad)";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Online_baglanti);
            command_egitmen.Parameters.AddWithValue("@ad", adi);
            command_egitmen.Parameters.AddWithValue("@soyad", soyadi);
            command_egitmen.ExecuteNonQuery();
            Online_baglanti.Close();
            string TamText = "Ekleme Başarılı : " + adi + " " + soyadi;
            return TamText;
        }
        public string Online_Delete(string ConString,string tc)
        {
            string adi = null;
            string soyadi = null;
            SqlConnection Online_baglanti = new SqlConnection(ConString);
            Online_baglanti.Open();
            SqlCommand Komut = new SqlCommand();
            Komut.Connection = Online_baglanti;
            Komut.CommandText = "SELECT ad,soyad FROM Abone where tc_no IN(" + tc + ")";

            SqlDataReader dr = Komut.ExecuteReader();

            while (dr.Read())
            {
                adi = dr["ad"].ToString();
                soyadi = dr["soyad"].ToString();
            }
            dr.Close();
            Online_baglanti.Close();
            Online_baglanti.Open();
            //Silme
            string Egitmen_con = "DELETE from [Online] where ad =('" + adi + "') and soyad =('" + soyadi + "')";
            SqlCommand command_egitmen = new SqlCommand(Egitmen_con, Online_baglanti);
            command_egitmen.ExecuteNonQuery();
            Online_baglanti.Close();
            string TamText = "Çıkış yapıldı : " + adi + " " + soyadi;
            return TamText;
        }
    }
    public class Sql_kosullar
    {
        // ONLİNE SORGULAR
        public DataTable Online_Kisiler(string ConString)
        {
            SqlConnection Egitmen_con = new SqlConnection(ConString);
            Egitmen_con.Open();
            string Egitmen_Sorgu = "SELECT * from Online";
            SqlCommand komut = new SqlCommand(Egitmen_Sorgu, Egitmen_con);
            SqlDataAdapter DataAdap = new SqlDataAdapter(komut);
            DataTable DataTab = new DataTable();
            DataAdap.Fill(DataTab);
            Egitmen_con.Close();
            return DataTab;
        }
        public Boolean Online_Saglik_Kontrol(string ConString, string tc)
        {
            string saglikDurumu = null;
            Boolean SaglikliMi = false;
            SqlConnection Online_baglanti = new SqlConnection(ConString);
            Online_baglanti.Open();
            SqlCommand Komut = new SqlCommand();
            Komut.Connection = Online_baglanti;
            Komut.CommandText = "SELECT saglik_durumu FROM Saglik_Bilgileri where tc_no IN(" + tc + ")";
            SqlDataReader dr = Komut.ExecuteReader();
            while (dr.Read())
            {
                saglikDurumu = dr["saglik_durumu"].ToString();
            }
            dr.Close();
            if (saglikDurumu.Trim() == "Saglikli")
            {
                SaglikliMi = true;
            }
            Online_baglanti.Close();
            return SaglikliMi;
        }
        public Boolean Online_Varmi(string ConString, string tc)
        {
            string adi = null;
            string soyadi = null;
            Boolean varmi = false;
            SqlConnection Online_baglanti = new SqlConnection(ConString);
            Online_baglanti.Open();
            SqlCommand Komut = new SqlCommand();
            Komut.Connection = Online_baglanti;
            Komut.CommandText = "SELECT ad,soyad FROM Abone where tc_no =('" + tc + "')";
            SqlDataReader dr = Komut.ExecuteReader();
            while (dr.Read())
            {
                adi = dr["ad"].ToString();
                soyadi = dr["soyad"].ToString();
            }
            dr.Close();
            Online_baglanti.Close();
            Online_baglanti.Open();
            Komut.Connection = Online_baglanti;
            Komut.CommandText = "SELECT ad,soyad FROM Online where ad =('" + adi.Trim() + "') and soyad =('" + soyadi.Trim() + "')";
            SqlDataReader dr1 = Komut.ExecuteReader();
            while (dr1.Read())
            {
                if (adi.Trim() == dr1["ad"].ToString())
                {
                    if (soyadi.Trim() == dr1["soyad"].ToString())
                    {
                        varmi = true;
                    }
                    else
                        varmi = false;
                }

            }
            Online_baglanti.Close();
            return varmi;
        }

        // ABONE SORGULAR
        public string Abone_Tc_Sorgulama(string ConString,string tc)
        {
            string cevap=null;
            SqlConnection Egitmen_con = new SqlConnection(ConString);
            Egitmen_con.Open();
            string Egitmen_Sorgu = "SELECT tc_no from Abone where tc_no IN(" + tc + ")";
            SqlCommand komut = new SqlCommand(Egitmen_Sorgu, Egitmen_con);
            SqlDataReader DataRead = komut.ExecuteReader();
 
            cevap = DataRead.Read().ToString();

            return cevap;
        }

        //Kaynak : 2
        public DataTable Abone_Ad_Soyad_Sorgulama(string ConString, string ad,string soyad)
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
        } // trim yapman lazım
        
        //SAGLIK SORGULAR
        public string Saglik_Tc_Sorgulama(string ConString, string tc)
        {
            string cevap = null;
            SqlConnection Saglik_con = new SqlConnection(ConString);
            Saglik_con.Open();
            string Saglik_Sorgu = "SELECT tc_no from Saglik_Bilgileri where tc_no IN(" + tc + ")";
            SqlCommand komut = new SqlCommand(Saglik_Sorgu, Saglik_con);
            SqlDataReader DataRead = komut.ExecuteReader();

            cevap = DataRead.Read().ToString();

            return cevap;
        }
        public string Olcum_Tc_Sorgulama(string ConString, string tc)
        {
            string cevap = null;
            SqlConnection Olcum_con = new SqlConnection(ConString);
            Olcum_con.Open();
            string Olcum_Sorgu = "SELECT tc_no from Olcum_Bilgileri where tc_no IN(" + tc + ")";
            SqlCommand komut = new SqlCommand(Olcum_Sorgu, Olcum_con);
            SqlDataReader DataRead = komut.ExecuteReader();

            cevap = DataRead.Read().ToString();

            return cevap;
        }
        public string Supplement_Tc_Sorgulama(string ConString,string tc)
        {
            string cevap = null;
            SqlConnection Supplement_con = new SqlConnection(ConString);
            Supplement_con.Open();
            string Supplement_Sorgu = "SELECT tc_no from Supplementler where tc_no IN(" + tc + ")";
            SqlCommand komut = new SqlCommand(Supplement_Sorgu, Supplement_con);
            SqlDataReader DataRead = komut.ExecuteReader();

            cevap = DataRead.Read().ToString();

            return cevap;
        }
        public string Egitmenler_Tc_Sorgulama(string ConString,string tc)
        {
            string cevap = null;
            SqlConnection Egitmen_con = new SqlConnection(ConString);
            Egitmen_con.Open();
            string Egitmen_Sorgu = "SELECT egt_tc_no from egitmenler where egt_tc_no IN(" + tc + ")";
            SqlCommand komut = new SqlCommand(Egitmen_Sorgu, Egitmen_con);
            SqlDataReader DataRead = komut.ExecuteReader();

            cevap = DataRead.Read().ToString();

            return cevap;
        }
        public string Mali_Fatura_Sorgulama(string ConString, string fatura)
        {
            string cevap = null;
            SqlConnection Mali_con = new SqlConnection(ConString);
            Mali_con.Open();
            string Mali_Sorgu = "SELECT fatura_no from Giderler where fatura_no =(" + fatura.Trim() + ")";
            SqlCommand komut = new SqlCommand(Mali_Sorgu, Mali_con);
            SqlDataReader DataRead = komut.ExecuteReader();
            cevap = DataRead.Read().ToString();
            return cevap;
        }

    }
    //KAYNAKALAR
    //https://www.tutorialsteacher.com/csharp/csharp-arraylist
    //https://mustafabukulmez.com/2018/02/16/c-sharp-string-uzerindeki-islemler/
    //Kaynak 2 : https://www.hikmetokumus.com/makale/51/csharp-ile-sqldatareader-kullanimi
    //Kaynak 1 : https://stackoverflow.com/questions/22061568/deleting-row-from-database-using-c-sharp
    //Kaynak 3 : https://stackoverflow.com/questions/20492019/update-statement-in-mysql-using-c-sharp
    //Kaynak 4 : http://ozguryaman.com/sqlde-insert-into-komutu/
}
