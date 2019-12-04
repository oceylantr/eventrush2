using System;

namespace Odeme.EventSourcing
{
    public class UcusAyarlandiEvent : Event
    {
        public UcusAyarlandiEvent(String musteriAdSoyad, String ucusSaati, String odemeTutari)
        {
            MusteriAdSoyad = musteriAdSoyad;
            UcusSaati = ucusSaati;
            OdemeTutari = odemeTutari;
        }

        public String MusteriAdSoyad {get; set;}

        public String UcusSaati {get; set;}

        public String OdemeTutari {get; set;}

        
    }
}