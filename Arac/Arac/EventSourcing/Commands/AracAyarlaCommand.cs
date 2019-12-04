using System;
using System.Collections.Generic;
using Arac.Services;

namespace Arac.EventSourcing
{
    public class AracAyarlaCommand : ICommand
    {
        private readonly IEventBus _eventBus;

        private readonly AracService _aracService;

        private readonly List<string> markaListesi = new List<string>{"Toyota Corolla", "Hyundai Accent", "Mercedes CLK GTR XYSZPS", "Audi A6", "Mustang Shelby"};

        public AracAyarlaCommand(string musteriAdSoyad, AracService aracService, IEventBus eventBus)
        {
            _arac = new Arac();
            _arac.MusteriAdSoyad = musteriAdSoyad;
            _arac.AracMarka = markaListesi[new Random().Next(markaListesi.Count)];
            _arac.TeslimSaati = DateTime.UtcNow.ToString();

            _aracService = aracService;
            _eventBus = eventBus;
        }

        public void Handle()
        {
            _aracService.Create(_arac);
            _eventBus.Publish(new AracAyarlandiEvent(_arac.MusteriAdSoyad, _arac.AracMarka, _arac.TeslimSaati));
            
        }

        public Arac _arac {get; private set;} 
    }
}