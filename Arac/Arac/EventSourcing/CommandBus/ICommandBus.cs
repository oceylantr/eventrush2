namespace Arac.EventSourcing
{
    public interface ICommandBus
    {
        void Process(ICommand command);
    }
}