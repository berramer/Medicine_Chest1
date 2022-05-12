using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;
using ENTITIES;



namespace Medicine_Chest.Models.PrescriptionIslemleri
{
    public class PrescriptionViewModel
    {
        /// <summary>
        /// Kullanıcı Listesindeki Toplam Kayıt Sayısını Getiren Metottur
        /// </summary>
        public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları


        [Required]
        public string PrescriptionCodeSorgu { get; set; }


        [Required]
        public string UserIDSorgu { get; set; }


        [Required]
        public DateTime? ValidityDateSorgu { get; set; }
        [Required]
        public string MedicineIDSorgu { get; set; }

        public IEnumerable<PRESCRIPTION> PrescriptionList { get; set; }
        public bool SorgulandiMi { get; set; }
        /// <summary>
        /// Kaçıncı Sayfanın Grid üzerinde gösterileceği bilgisi
        /// </summary>
        public int SayfaSayisi { get; set; }

        /// <summary>
        /// Silinmiş kayıtların getirilip getirilmeyeceği bilgisi
        /// </summary>
        public bool SilinmisKayitlarGelsin { get; set; }
        #endregion

        /// <summary>
        /// Kullanıcı Ana Sayfasında Gösterilecek Verilerin Ayarlandığı
        /// Model Yapıcı Metodudur.
        /// </summary>
        public PrescriptionViewModel()
        {
            ToplamKayitSayisi = 0;
            SorgulandiMi = false;
            SayfaSayisi = 1;
        }
    }
}
