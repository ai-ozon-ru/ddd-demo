namespace DddDemo.DomainServices
{
    public interface IMessageBus
    {
        void Notify(EmailMessage emailMessage);
    }
}
