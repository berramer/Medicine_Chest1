using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Medicine_Chest.Identity;

namespace Medicine_Chest.Models
{
        public class KullaniciViewModel
        {
            /// <summary>
            /// Kullanıcı Listesindeki Toplam Kayıt Sayısını Getiren Metottur
            /// </summary>
            public int ToplamKayitSayisi { get; set; }

        #region Sorgulama Alanları
        public string NameSorgu { get; set; }


        public string SurnameSorgu { get; set; }

      

        public string IdentificationNoSorgu { get; set; }


        public string AddressSorgu { get; set; }

 
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumberSorgu { get; set; }

   
        [DataType(DataType.EmailAddress)]
        public string EmailSorgu { get; set; }
        [Display(Name = "Kullanıcı Adı:")]
        public string UserNameSorgu { get; set; }
        public IEnumerable<User> UserList { get; set; }
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
            public KullaniciViewModel()
            {
                ToplamKayitSayisi = 0;
                SorgulandiMi = false;
                SayfaSayisi = 1;
            }
        }
    }

