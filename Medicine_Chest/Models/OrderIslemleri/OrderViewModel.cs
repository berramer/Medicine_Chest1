using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;
using ENTITIES;



namespace Medicine_Chest.Models.OrderIslemleri
{
    public class OrderViewModel
    {

        /// <summary>
        /// Kullanıcı Listesindeki Toplam Kayıt Sayısını Getiren Metottur
        /// </summary>
        public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları


        [Required]
        public string UserIDSorgu { get; set; }
        public string UserNameSorgu { get; set; }
        public string UserSurnameSorgu { get; set; }
        public string AddressSorgu { get; set; }
        public string PhonenumberSorgu { get; set; }
        public string MailAddressSorgu { get; set; }
        public string PharmaciesIDSorgu { get; set; }
        public string MedicineIDSorgu { get; set; }
        public decimal? PriceSorgu { get; set; }


        public IEnumerable<ORDER> OrderList { get; set; }
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
        public OrderViewModel()
        {
            ToplamKayitSayisi = 0;
            SorgulandiMi = false;
            SayfaSayisi = 1;
        }
    }
}
