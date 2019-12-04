using Arac.Services;

namespace Arac.EventSourcing{

    public class OdemeBasariliOlduEventHandler : IEventHandler<OdemeBasariliOlduEvent>
    {

        private readonly ICommandBus commandBus = new CommandBus();
        private readonly IEventBus _eventBus;
        private readonly AracService _aracService;

        public OdemeBasariliOlduEventHandler(AracService aracService, IEventBus eventBus)
        {
            _aracService = aracService;
            _eventBus = eventBus;
        }

        //FIXME bu inject edilecek

        public void Handle(OdemeBasariliOlduEvent @event)
        {
            commandBus.Process(new AracAyarlaCommand(@event.MusteriAdSoyad, _aracService, _eventBus));
        }
    }

}