namespace Contacts.Infrastructure.Exeptions;

public sealed class ContactValidationException : Exception
{
    public ContactValidationException(string message) : base(message) { }
}