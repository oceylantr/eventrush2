using System;
using Odeme.Services;

namespace Odeme.EventSourcing
{
    public class OdemeGerceklestirCommand : ICommand
    {
        private readonly IEventBus _eventBus;

        private readonly OdemeService _odemeService;

        public OdemeGerceklestirCommand(string odemeTutari, string musteriAdSoyad, OdemeService odemeService, IEventBus eventBus)
        {
            _odeme = new Odeme();
            _odeme.MusteriAdSoyad = musteriAdSoyad;
            _odeme.OdemeSaati = DateTime.UtcNow.ToString();
            _odeme.OdemeTutari = odemeTutari;

            _odemeService = odemeService;
            _eventBus = eventBus;
        }

        public void Handle()
        {
            _odemeService.Create(_odeme);

            if (Int32.Parse(_odeme.OdemeTutari) < 100){
                _eventBus.Publish(new OdemeHatasiOlustuEvent(_odeme.MusteriAdSoyad+"-"+_odeme.OdemeSaati));    
            }else{
                _eventBus.PublishToExchange(new OdemeBasariliOlduEvent("Odeme" ,_odeme.MusteriAdSoyad, _odeme.OdemeSaati));
            }
            
        }

        public Odeme _odeme {get; private set;} 
    }
}