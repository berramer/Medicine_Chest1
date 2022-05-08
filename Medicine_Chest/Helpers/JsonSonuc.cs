using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Helpers
{
        /// <summary>
        /// Metotlarda Json dönüş tipi için kullanılan sınıf
        /// </summary>
        public class JsonSonuc
        {
            public string Id { get; set; }
            public string Baslik { get; set; }
            public string Mesaj { get; set; }
            public bool YeniKayitMi { get; set; }
            public string MesajTuru { get; set; }



            //todo: alttaki alanlan yeni sınıfa atılacak. Sınıf JsonSonucdan türetilecek
            public string Id2 { get; set; }
            public string Ad { get; set; }
            public bool HataMi { get; set; }
            public bool IliskiliKayitVarMi { get; set; }
            public int KayitSayisi { get; set; }
            public bool KayitDegisikligiVarMi { get; set; }
            public bool IhtiyacBilgisi { get; set; }
        }
    }

