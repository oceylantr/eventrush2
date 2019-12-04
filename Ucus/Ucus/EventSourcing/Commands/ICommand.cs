namespace Ucus.EventSourcing
{
    public interface ICommand
    {
        void Handle();
    }
}