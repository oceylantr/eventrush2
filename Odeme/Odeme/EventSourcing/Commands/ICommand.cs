namespace Odeme.EventSourcing
{
    public interface ICommand
    {
        void Handle();
    }
}