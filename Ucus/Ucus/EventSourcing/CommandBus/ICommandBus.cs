namespace Ucus.EventSourcing
{
    public interface ICommandBus
    {
        void Process(ICommand command);
    }
}