using System.ComponentModel.DataAnnotations;

namespace Contacts.Application.Requests;

public abstract class BaseContactRequest
{
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(25, ErrorMessage = "Имя не должно превышать 25 символов")]
    public string Name { get; init; } = null!;

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [RegularExpression(
        @"^375(29|33|44|25)\d{7}$",
        ErrorMessage = "Номер телефона должен быть в формате РБ")]
    public string PhoneNumber { get; init; } = null!;

    [StringLength(30, ErrorMessage = "Должность не должна превышать 30 символов")]
    public string? JobTitle { get; init; }

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateOnly BirthDay { get; init; }
}