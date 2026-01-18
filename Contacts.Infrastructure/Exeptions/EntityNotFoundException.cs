namespace Contacts.Infrastructure.Exeptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
}