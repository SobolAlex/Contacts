using Contacts.Application.Interfaces;
using Contacts.Application.Requests;
using Contacts.Domain.Exceptions;
using Contacts.Infrastructure.Exeptions;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Web.Controllers;

public class ContactsController(IContactService contactService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchPhone, CancellationToken cancellationToken)
    {
        var contacts = await contactService.GetAllContactsAsync(cancellationToken);

        // фильтрация по номеру телефона, если введено
        if (!string.IsNullOrWhiteSpace(searchPhone))
        {
            contacts = contacts
                .Where(c => c.MobilePhone.Contains(searchPhone))
                .ToList();
        }

        ViewBag.SearchPhone = searchPhone; // чтобы текст поиска остался в поле
        return View(contacts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateContactRequest createContactRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var contacts = await contactService.GetAllContactsAsync(cancellationToken);
            return View("Index", contacts);
        }

        try
        {
            await contactService.CreateContactAsync(createContactRequest, cancellationToken);
        }
        catch (ContactValidationException ex)
        {
            ModelState.AddModelError("CreateError", ex.Message);
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError("CreateError", ex.Message);
        }

        if (!ModelState.IsValid)
        {
            ViewBag.ShowCreateModal = true;
            ViewBag.CreateName = createContactRequest.Name;
            ViewBag.CreatePhone = createContactRequest.PhoneNumber;
            ViewBag.CreateJob = createContactRequest.JobTitle;
            ViewBag.CreateBirth = createContactRequest.BirthDay.ToString("yyyy-MM-dd");

            var contacts = await contactService.GetAllContactsAsync(cancellationToken);
            return View("Index", contacts);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateContactRequest updateContactRequest, CancellationToken cancellationToken)
    {
        if (id != updateContactRequest.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            var contacts = await contactService.GetAllContactsAsync(cancellationToken);
            return View("Index", contacts);
        }

        try
        {
            await contactService.UpdateContactAsync(id, updateContactRequest, cancellationToken);
        }
        catch (ContactValidationException ex)
        {
            ModelState.AddModelError("UpdateError", ex.Message);
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError("UpdateError", ex.Message);
        }
        if (!ModelState.IsValid)
        {
            ViewBag.ShowUpdateModal = true;
            ViewBag.UpdateId = id;

            ViewBag.UpdateName = updateContactRequest.Name;
            ViewBag.UpdatePhone = updateContactRequest.PhoneNumber;
            ViewBag.UpdateJob = updateContactRequest.JobTitle;
            ViewBag.UpdateBirth = updateContactRequest.BirthDay.ToString("yyyy-MM-dd");

            var contacts = await contactService.GetAllContactsAsync(cancellationToken);
            return View("Index", contacts);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await contactService.DeleteContactAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}