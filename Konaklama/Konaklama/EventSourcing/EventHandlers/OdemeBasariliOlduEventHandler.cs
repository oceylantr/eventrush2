using Konak.Services;

namespace Konak.EventSourcing{

    public class OdemeBasariliOlduEventHandler : IEventHandler<OdemeBasariliOlduEvent>
    {

        private readonly ICommandBus commandBus = new CommandBus();
        private readonly IEventBus _eventBus;
        private readonly KonakService _konakService;

        public OdemeBasariliOlduEventHandler(KonakService konakService, IEventBus eventBus)
        {
            _konakService = konakService;
            _eventBus = eventBus;
        }

        //FIXME bu inject edilecek

        public void Handle(OdemeBasariliOlduEvent @event)
        {
            commandBus.Process(new KonakAyarlaCommand(@event.MusteriAdSoyad, _konakService, _eventBus));
        }
    }

}