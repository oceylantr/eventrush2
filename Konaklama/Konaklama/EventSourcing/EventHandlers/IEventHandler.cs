namespace Konak.EventSourcing{

    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent: Event
    {
        void Handle(TEvent @event);
    }

    public interface IEventHandler
    {
        
    }

}