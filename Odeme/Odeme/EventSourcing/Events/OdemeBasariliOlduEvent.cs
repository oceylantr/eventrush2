namespace Odeme.EventSourcing
{
    public class OdemeBasariliOlduEvent : Event
    {
        public OdemeBasariliOlduEvent(string message, string musteriAdSoyad, string odemeSaati)
        {
            Message = message;
            MusteriAdSoyad = musteriAdSoyad;
            OdemeSaati = odemeSaati;
        }

        public string Message {get; set;}

        public string MusteriAdSoyad {get; set;}

        public string OdemeSaati {get; set;}

        
    }
}