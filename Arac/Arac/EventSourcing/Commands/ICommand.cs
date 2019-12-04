namespace Arac.EventSourcing
{
    public interface ICommand
    {
        void Handle();
    }
}