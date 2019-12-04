namespace Konak.EventSourcing
{
    public interface ICommandBus
    {
        void Process(ICommand command);
    }
}