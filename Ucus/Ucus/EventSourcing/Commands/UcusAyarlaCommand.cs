using Ucus.Services;

namespace Ucus.EventSourcing
{
    public class UcusAyarlaCommand : ICommand
    {
        private readonly IEventBus _eventBus;

        private readonly UcusService _ucusService;

        public UcusAyarlaCommand(Ucus ucus, UcusService ucusService, IEventBus eventBus)
        {
            _ucus = ucus;
            _ucusService = ucusService;
            _eventBus = eventBus;
        }

        public void Handle()
        {
            _ucusService.Create(_ucus);
            _eventBus.Publish(new UcusAyarlandiEvent(_ucus.MusteriAdSoyad, _ucus.UcusSaati, _ucus.OdemeTutari));
        }

        public Ucus _ucus {get; private set;} 
    }
}