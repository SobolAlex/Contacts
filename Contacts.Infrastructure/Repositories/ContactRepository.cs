using Contacts.Application.Interfaces;
using Contacts.Domain.Entities;
using Contacts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Repositories;

public sealed class ContactRepository(ContactsDbContext dbContext) : IContactRepository

{
    public async Task<Contact?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Contacts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Contacts
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Contact contact, CancellationToken cancellationToken)
    {
        dbContext.Contacts.Add(contact);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken)
    {
        dbContext.Contacts.Update(contact);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Contact contact, CancellationToken cancellationToken)
    {
        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByMobilePhoneAsync(string mobilePhone, CancellationToken cancellationToken)
    {
        return await dbContext.Contacts
            .AsNoTracking()
            .AnyAsync(x => x.MobilePhone == mobilePhone, cancellationToken);
    }
}