using Contacts.Application.Requests;
using Contacts.Domain.Entities;

namespace Contacts.Application.Interfaces;

public interface IContactService
{
    Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken);
    Task CreateContactAsync(CreateContactRequest request, CancellationToken cancellationToken);
    Task UpdateContactAsync(Guid id, UpdateContactRequest request, CancellationToken cancellationToken);
    Task DeleteContactAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Contact>> GetAllContactsAsync(CancellationToken cancellationToken);
    Task<Contact?> GetContactByIdAsync(Guid id, CancellationToken cancellationToken);
}