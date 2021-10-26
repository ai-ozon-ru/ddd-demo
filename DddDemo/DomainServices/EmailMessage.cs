using DddDemo.Entities;
using DddDemo.ValueObjects;

namespace DddDemo.DomainServices
{
    public class EmailMessage : ValueObject<EmailMessage>
    {
        public string Body { get; }
        public Email EmailAddress { get; }

        public EmailMessage(Email emailAddress, string body)
        {
            Body = body;
            EmailAddress = emailAddress;
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool EqualsCore(EmailMessage other)
            => Body == other.Body && EmailAddress == other.EmailAddress;

        /// <inheritdoc />
        protected override int GetHashCodeCore()
            => EmailAddress.GetHashCode() ^ Body.GetHashCode();
    }
}
