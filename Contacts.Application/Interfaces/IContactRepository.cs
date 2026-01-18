using Contacts.Domain.Entities;

namespace Contacts.Application.Interfaces;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Contact>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> ExistsByMobilePhoneAsync(string mobilePhone, CancellationToken cancellationToken);

    Task AddAsync(Contact contact, CancellationToken cancellationToken);

    Task UpdateAsync(Contact contact, CancellationToken cancellationToken);

    Task RemoveAsync(Contact contact, CancellationToken cancellationToken);
}