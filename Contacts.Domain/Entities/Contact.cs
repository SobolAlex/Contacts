using Contacts.Domain.Exceptions;

namespace Contacts.Domain.Entities;

public sealed class Contact
{
    private Contact()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string MobilePhone { get; private set; }

    public string? JobTitle { get; private set; }

    public DateOnly BirthDay { get; private set; }

    public Contact(string name, string mobilePhone, string? jobTitle, DateOnly birthDay)
    {
        Validate(name, mobilePhone, birthDay);

        Id = Guid.NewGuid();
        Name = name;
        MobilePhone = mobilePhone;
        JobTitle = jobTitle;
        BirthDay = birthDay;
    }

    public void Update(string name, string mobilePhone, string? jobTitle, DateOnly birthDay)
    {
        Validate(name, mobilePhone, birthDay);

        Name = name;
        MobilePhone = mobilePhone;
        JobTitle = jobTitle;
        BirthDay = birthDay;
    }
    private static void Validate(string name, string mobilePhone, DateOnly birthDay)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        if (string.IsNullOrWhiteSpace(mobilePhone))
            throw new DomainException("Mobile phone cannot be empty");

        if (birthDay > DateOnly.FromDateTime(DateTime.Today))
            throw new DomainException("BirthDay cannot be in the future");
    }
}