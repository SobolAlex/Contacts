using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Persistence;

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}