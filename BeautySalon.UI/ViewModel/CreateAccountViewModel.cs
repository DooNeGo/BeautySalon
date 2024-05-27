using BeautySalon.Application.Commands.AddCustomer;
using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateAccountViewModel(IMediator mediator) : ObservableValidator, IQueryAttributable
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Обязательное поле")]
    [MinLength(2, ErrorMessage = "*Минимальная длина поля 2")]
    private string _firstName = string.Empty;

    [ObservableProperty] private string _firstNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Обязательное поле")]
    [MinLength(2, ErrorMessage = "*Минимальная длина поля 2")]
    private string _lastName = string.Empty;

    [ObservableProperty] private string _lastNameError = string.Empty;

    [ObservableProperty] private string _middleName = string.Empty;
    [ObservableProperty] private string _middleNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Обязательное поле")]
    [PhoneNumber("*Неверный номер телефон")]
    private string _phone = string.Empty;

    [ObservableProperty] private string _phoneError = string.Empty;

    private Guid _userId;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _userId = (Guid)query["UserId"];
    }

    [RelayCommand]
    private async Task CreateCustomerAccount()
    {
        FirstName = FirstName.Trim();
        LastName = LastName.Trim();
        MiddleName = MiddleName.Trim();
        Phone = Phone.Trim();

        ValidateAllProperties();
        UpdateErrorMessages();

        if (HasErrors) return;
        if (_userId == Guid.Empty) ThrowHelper.ThrowInvalidDataException("The user id was empty");

        Customer customer = new(LastName, FirstName, MiddleName, Phone, _userId);
        await mediator.Send(new AddCustomerCommand(customer)).ConfigureAwait(false);
        await Shell.Current.GoToAsync($"../../{nameof(LoginViewModel)}").ConfigureAwait(false);
    }

    private void UpdateErrorMessages()
    {
        FirstNameError = GetErrors(nameof(FirstName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LastNameError = GetErrors(nameof(LastName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        MiddleNameError = GetErrors(nameof(MiddleName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PhoneError = GetErrors(nameof(Phone)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}