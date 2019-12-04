using System;

namespace Arac.EventSourcing
{
    public class AracAyarlandiEvent : Event
    {
        public AracAyarlandiEvent(String musteriAdSoyad, String aracMarka, String teslimSaati)
        {
            MusteriAdSoyad = musteriAdSoyad;
            AracMarka = aracMarka;
            TeslimSaati = teslimSaati;
        }

        public String MusteriAdSoyad {get; set;}

        public String AracMarka {get; set;}

        public String TeslimSaati {get; set;}

        
    }
}