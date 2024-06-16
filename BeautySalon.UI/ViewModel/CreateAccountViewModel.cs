using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel.DataAnnotations;
using BeautySalon.Application.Commands.AddUser;

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
    [ObservableProperty] private string _error = string.Empty;

    private User _user = null!;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _user = (User)query["User"];
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
        
        try
        {
            _user.Customer = new Customer(LastName, FirstName, MiddleName, Phone);
            await mediator.Send(new AddUserCommand(_user)).ConfigureAwait(false);
            await Shell.Current.GoToAsync($"../../{nameof(LoginViewModel)}").ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Error = e.InnerException is not null ? e.InnerException.Message : e.Message;
        }
    }

    private void UpdateErrorMessages()
    {
        FirstNameError = GetErrors(nameof(FirstName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LastNameError = GetErrors(nameof(LastName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        MiddleNameError = GetErrors(nameof(MiddleName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PhoneError = GetErrors(nameof(Phone)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}