namespace Odeme.EventSourcing
{
    public class OdemeHatasiOlustuEvent : Event
    {
        public OdemeHatasiOlustuEvent(string message)
        {
            Message = message;
        }

        public string Message {get; set;}

        
    }
}