namespace Contacts.Application.Requests;

public sealed class UpdateContactRequest : BaseContactRequest
{
    public Guid Id { get; set; }
}