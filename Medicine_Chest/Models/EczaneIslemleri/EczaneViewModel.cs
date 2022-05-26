using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;
using ENTITIES;

namespace Medicine_Chest.Models
{
    public class EczaneViewModel
    {
        /// <summary>
        /// Kullanıcı Listesindeki Toplam Kayıt Sayısını Getiren Metottur
        /// </summary>
        public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları
      public string EczaneAdiSorgu {get;set;}
      public string AdresiSorgu {get;set;}
      public string SemtSorgu {get;set;}
      public string YolTarifiSorgu {get;set;}
      public string TelefonSorgu {get;set;}
      public string SehirSorgu {get;set;}
      public string ilceSorgu {get;set;}
        public bool SorgulandiMi { get; set; }
        /// <summary>
        /// Kaçıncı Sayfanın Grid üzerinde gösterileceği bilgisi
        /// </summary>
        public int SayfaSayisi { get; set; }

        /// <summary>
        /// Silinmiş kayıtların getirilip getirilmeyeceği bilgisi
        /// </summary>
        public bool SilinmisKayitlarGelsin { get; set; }
        public IEnumerable<PHARMACIES> PharmaciesList { get; internal set; }
        #endregion

        /// <summary>
        /// Kullanıcı Ana Sayfasında Gösterilecek Verilerin Ayarlandığı
        /// Model Yapıcı Metodudur.
        /// </summary>
        public EczaneViewModel()
        {
            ToplamKayitSayisi = 0;
            SorgulandiMi = false;
            SayfaSayisi = 1;
        }
    }
}

