namespace Konak.EventSourcing
{
    public interface ICommand
    {
        void Handle();
    }
}