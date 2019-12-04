namespace Arac.EventSourcing
{
    public class CommandBus : ICommandBus
    {
        public void Process(ICommand command)
        {
            command.Handle();
        }
    }
}