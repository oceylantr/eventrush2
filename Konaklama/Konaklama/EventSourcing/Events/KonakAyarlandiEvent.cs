using System;

namespace Konak.EventSourcing
{
    public class KonakAyarlandiEvent : Event
    {
        public KonakAyarlandiEvent(String musteriAdSoyad, String otel, String girisSaati)
        {
            MusteriAdSoyad = musteriAdSoyad;
            Otel = otel;
            GirisSaati = girisSaati;
        }

        public String MusteriAdSoyad {get; set;}

        public String Otel {get; set;}

        public String GirisSaati {get; set;}

        
    }
}