using System.ComponentModel.DataAnnotations;
using BeautySalon.Application.Commands.AddCustomer;
using BeautySalon.Application.Commands.UpdateUser;
using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries.GetUserById;
using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateAccountViewModel(IMediator mediator, IIdentityService identityService) : ObservableValidator, IQueryAttributable
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [MinLength(2, ErrorMessage = "*Minimum length is 2")]
    private string _firstName = string.Empty;

    [ObservableProperty] private string _firstNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [MinLength(2, ErrorMessage = "*Minimum length is 2")]
    private string _lastName = string.Empty;

    [ObservableProperty] private string _lastNameError = string.Empty;

    [ObservableProperty] private string _middleName = string.Empty;
    [ObservableProperty] private string _middleNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [PhoneNumber("*Is not valid phone number")]
    private string _phone = string.Empty;

    [ObservableProperty] private string _phoneError = string.Empty;

    private Guid _userId = Guid.Empty;

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
        await mediator.Send(new AddCustomerCommand(customer));
        await Shell.Current.Navigation.PopToRootAsync();
    }

    private void UpdateErrorMessages()
    {
        FirstNameError = GetErrors(nameof(FirstName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LastNameError = GetErrors(nameof(LastName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        MiddleNameError = GetErrors(nameof(MiddleName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PhoneError = GetErrors(nameof(Phone)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}