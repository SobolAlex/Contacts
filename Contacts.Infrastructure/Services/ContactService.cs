using Contacts.Application.Interfaces;
using Contacts.Application.Requests;
using Contacts.Domain.Entities;
using Contacts.Infrastructure.Exeptions;

namespace Contacts.Infrastructure.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await contactRepository.ExistsByMobilePhoneAsync(phoneNumber, cancellationToken);
    }

    public async Task<IReadOnlyList<Contact>> GetAllContactsAsync(CancellationToken cancellationToken)
    {
        return await contactRepository.GetAllAsync(cancellationToken);
    }

    public async Task CreateContactAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        if (await contactRepository.ExistsByMobilePhoneAsync(request.PhoneNumber, cancellationToken))
        {
            throw new ContactValidationException("Контакт с таким номером уже существует");
        }

        var contact = new Contact(
            request.Name.Trim(),
            request.PhoneNumber.Trim(),
            request.JobTitle?.Trim(),
            request.BirthDay
        );

        await contactRepository.AddAsync(contact, cancellationToken);
    }

    public async Task UpdateContactAsync(Guid id, UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByIdAsync(id, cancellationToken);

        if (contact is null)
            throw new EntityNotFoundException($"Контакт с идентификатором {id} не существует");

        // Проверяем уникальность нового номера (если он изменился)
        if (contact.MobilePhone != request.PhoneNumber &&
            await contactRepository.ExistsByMobilePhoneAsync(request.PhoneNumber, cancellationToken))
        {
            throw new ContactValidationException("Контакт с таким номером уже существует");
        }

        contact.Update(
            request.Name.Trim(),
            request.PhoneNumber.Trim(),
            request.JobTitle?.Trim(),
            request.BirthDay
        );

        await contactRepository.UpdateAsync(contact, cancellationToken);
    }

    public async Task<Contact?> GetContactByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await contactRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task DeleteContactAsync(Guid id, CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByIdAsync(id, cancellationToken);

        if (contact is null)
            throw new EntityNotFoundException($"Контакт с идентификатором {id} не существует");

        await contactRepository.RemoveAsync(contact, cancellationToken);
    }
}