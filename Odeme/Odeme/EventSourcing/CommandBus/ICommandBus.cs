namespace Odeme.EventSourcing
{
    public interface ICommandBus
    {
        void Process(ICommand command);
    }
}