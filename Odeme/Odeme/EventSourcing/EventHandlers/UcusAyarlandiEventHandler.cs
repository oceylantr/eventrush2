using Odeme.Services;

namespace Odeme.EventSourcing{

    public class UcusAyarlandiEventHandler : IEventHandler<UcusAyarlandiEvent>
    {

        private readonly ICommandBus commandBus = new CommandBus();
        private readonly IEventBus _eventBus;
        private readonly OdemeService _odemeService;

        public UcusAyarlandiEventHandler(OdemeService odemeService, IEventBus eventBus)
        {
            _odemeService = odemeService;
            _eventBus = eventBus;
        }

        //FIXME bu inject edilecek

        public void Handle(UcusAyarlandiEvent @event)
        {
            commandBus.Process(new OdemeGerceklestirCommand(@event.OdemeTutari, @event.MusteriAdSoyad, _odemeService, _eventBus));
        }
    }

}