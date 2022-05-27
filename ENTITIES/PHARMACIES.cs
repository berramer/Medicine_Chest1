using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ENTITIES
{
    public class PHARMACIES : EntityBase
    {
        [JsonPropertyName("EczaneAdi")]
        public string EczaneAdi {get;set;}
        [JsonPropertyName("Adresi")]
        public string Adresi {get;set;}
        [JsonPropertyName("Semt")]
        public string Semt {get;set;}
        [JsonPropertyName("YolTarifi")]
        public string YolTarifi {get;set;}
        [JsonPropertyName("Telefon")]
        public string Telefon {get;set;}
        [JsonPropertyName("Sehir")]
        public string Sehir {get;set;}
        [JsonPropertyName("ilce")]
        public string ilce {get;set;}
    // public Double latitude=0.0;
   //  public Double longitude=0.0;

    }
}
