namespace Odeme.EventSourcing
{
    public interface IEventBus
    {
        void Publish(Event @event);
        void PublishToExchange(Event @event);
        void Subscribe(string eventName);
        void Unsubscribe(string eventName);
    }
}