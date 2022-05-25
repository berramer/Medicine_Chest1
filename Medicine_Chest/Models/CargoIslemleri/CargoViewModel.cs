using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;
using ENTITIES;

namespace Medicine_Chest.Models.CargoIslemleri
{
    public class CargoViewModel
    {
        public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları


        public string OrderIdSorgu { get; set; }


        public int? IsDeliveredSorgu { get; set; }

        public int ScoreSorgu { get; set; }

        public DateTime? OrderDateCSorgu { get; set; }

        public IEnumerable<CARGO> CargoList { get; set; }
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
        public CargoViewModel()
        {
            ToplamKayitSayisi = 0;
            SorgulandiMi = false;
            SayfaSayisi = 1;
        }
    }
}
