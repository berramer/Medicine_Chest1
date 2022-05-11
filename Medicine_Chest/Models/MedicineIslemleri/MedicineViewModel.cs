using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;
using ENTITIES;



namespace Medicine_Chest.Models.MedicineIslemleri
{
    public class MedicineViewModel
    {
        /// <summary>
        /// Kullanıcı Listesindeki Toplam Kayıt Sayısını Getiren Metottur
        /// </summary>
        public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları


        [Required]
        public string NameSorgu { get; set; }


        [Required]
        public DateTime? ExpireDateSorgu { get; set; }

        public decimal? BasisWeightSorgu { get; set; }
        public string ProducerSorgu { get; set; }
     
        public IEnumerable<MEDICINE> MedicineList { get; set; }
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
        public MedicineViewModel()
        {
            ToplamKayitSayisi = 0;
            SorgulandiMi = false;
            SayfaSayisi = 1;
        }
    }
}
