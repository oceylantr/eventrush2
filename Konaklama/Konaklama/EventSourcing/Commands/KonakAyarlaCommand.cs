using System;
using System.Collections.Generic;
using Konak.Services;

namespace Konak.EventSourcing
{
    public class KonakAyarlaCommand : ICommand
    {
        private readonly IEventBus _eventBus;

        private readonly KonakService _konakService;

        private readonly List<string> otelListesi = new List<string>{"Hilton", "Movenpick", "Reyiz Otel", "Otel Tombik", "Lesh Otel"};

        public KonakAyarlaCommand(string musteriAdSoyad, KonakService konakService, IEventBus eventBus)
        {
            _konak = new Konak();
            _konak.MusteriAdSoyad = musteriAdSoyad;
            _konak.Otel = otelListesi[new Random().Next(otelListesi.Count)];
            _konak.GirisSaati = DateTime.UtcNow.ToString();

            _konakService = konakService;
            _eventBus = eventBus;
        }

        public void Handle()
        {
            _konakService.Create(_konak);
            _eventBus.Publish(new KonakAyarlandiEvent(_konak.MusteriAdSoyad, _konak.Otel, _konak.GirisSaati));
            
        }

        public Konak _konak {get; private set;} 
    }
}