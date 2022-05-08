using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Helpers
{
 
    

        public static class MesajSabitler
        {
            public static string IslemBasarili = "İşlem Başarılı";
            public static string TarihKarsilastirmasi = "Başlangıç tarihini Bitiş tarihinden küçük seçiniz";
            public static string GerekliVeriGirisi = "Hesaplama için girilmesi gerekli alanları giriniz";
            public static string IslemBasarisiz = "İşlem Başarısız";
            public static string KayitIcerikBasarili = "Kayıt işlemi başarılıyla gerçekleştirildi";
            public static string KayitIcerikBasarisiz = "Kayıt işlemi başarısız oldu";
            public static string GuncellemeIcerikBasarili = "Güncelleme işlemi başarılıyla gerçekleştirildi";
            public static string GuncellemeIcerikBasarisiz = "Güncelleme işlemi başarısız oldu";
            public static string SilmeIcerikBasarili = "Silme işlemi başarılıyla gerçekleştirildi";
            public static string SilmeIcerikBasarisiz = "Silme İşlemi işlemi başarısız oldu";
            public static string SorgulamaIcerikBasarili = "Sorgulama işlemi başarılıyla gerçekleştirildi";
            public static string SorgulamaIcerikBasarisiz = "Sorgulama işlemi başarısız oldu";
            public static string IptalIcerikBasarili = "İptal işlemi başarılıyla gerçekleştirildi";
            public static string IptalIcerikBasarilisiz = "İptal işlemi başarısız oldu";
            public static string MukerrerKayitEkleme = "Eklemek İstediğiniz Özelliklere Sahip Kayıt Zaten Sistemde Mevcut";
            public static string MukerrerKayitGuncelleme = "Güncellediğiniz Özelliklere Sahip Kayıt Zaten Sistemde Mevcut";
            public static string AktiflestirmeIcerikBasarili = "Aktifleştirme işlemi başarılıyla gerçekleştirildi";
            public static string RafineridenYillikPlanlananAkaryakit = "Kontenjan Miktarı, Rafineride Yıllık Planlanan Miktarı Geçemez";
            public static string AktiflestirmeIcerikBasarisiz = "Aktifleştirme işlemi başarısız oldu";
            public static string GecerliBirlikSecimi = "Seçilen Birliğe ailt detay kaydı bulunamadı";
            public static string KayitBulunamadi = "Kayıt Bulunamadı";

        }

        public static class YuklenecekAnaKlasorAdi
        {
            public static string EvrakKlasor = "Evraklar";
            public static string FotografKlasor = "Fotograflar";
            public static string FirmaLogo = "FirmaLogo";
            public static string EkEvrak = "BelgeEkEvrak";
            public static string BelgeEvrak = "BelgeEvrak";
            public static string FirmaBelge = "FirmaBelge";
            public static string SunucuFizikselAdres = "C://Sutem";
        }

        public static class DosyaSabitler
        {
            public const string ExportExcel = ".xls";
            public const string ExportHtml = ".html";
            public const string ExportCsv = ".csv";
            public const string ExportPdf = ".pdf";
            public static string[] GoruntulenecekDosyaUzantiListesi = { ".png", ".bmp", ".jpg", ".pdf" };
            public const int AzamiDosyaBoyutu = 5242880;
            public const string ShapeUzanti = "Shape";
        }

        public static class IslemTurSabitler
        {
            public static string IslemTuruKayitEkleme = "Ekleme";
            public static string IslemTuruKayitGuncelleme = "Guncelleme";
            public static string IslemTuruKayitSilme = "Silme";
            public static string IslemTuruKayitIptal = "Iptal";
            public static string IslemTuruDetayGorme = "Detay";
        }

        /// <summary>
        /// Sabit kodlardan tablodaki id'yi getirebilmek için yazılan metotta
        /// nesne türünün adlarının bulunduğu sabitler
        /// </summary>
        public static class NesneTurKodSabitler
        {
            public static string NesneTuruLogTuruAna = "Log Türü Ana";
            public static string NesneTuruLogTuruAlt = "Log Türü Alt";
            public static string NesneTuruLogTuruIslem = "Log Türü İşlem";
            public static string NesneTuruBirim = "Birim";
            public static string NesneTuruHak = "Hak";
            public static string NesneTuruIletisimTuru = "İletişim Türü";
            public static string NesneTuruTalepDurum = "Talep Durum";
            public static string NesneTuruGorevUnvan = "Görev Unvanı";
        }

        public static class SistemSabitler
        {
            public const int SmtpPort = 25;
            public const int SistemdeOlmayanKullanici = -1;
        }

        public static class ComboAlanSabitler
        {
            public const string Ad = "Ad";
            public const string Id = "Id";
            public const string TakbisRef = "TakbisRef";
        }

        public static class KodMalikTuruSabitler
        {
            public const string Malik = "1";
            public const string Icarci = "2";

        }

      
        public static class CiktiTuruSabitleri
        {
            public const string Pdf = "PDF";
            public const string Xls = "XLS";
        }
       
    }

