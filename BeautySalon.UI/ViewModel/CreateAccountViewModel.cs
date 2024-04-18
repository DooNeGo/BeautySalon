using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateAccountViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [MinLength(2, ErrorMessage = "*Minimum length is 2")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [MinLength(2, ErrorMessage = "*Minimum length is 2")]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _middleName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "*Required")]
    [PhoneNumber("*Is not valid phone number")]
    private string _phone = string.Empty;

    [ObservableProperty]
    private string _firstNameError = string.Empty;

    [ObservableProperty]
    private string _lastNameError = string.Empty;

    [ObservableProperty]
    private string _middleNameError = string.Empty;

    [ObservableProperty]
    private string _phoneError = string.Empty;

    [RelayCommand]
    private void GoNext()
    {
        ValidateAllProperties();
        UpdateErrorMessages();

        if (HasErrors)
        {
            return;
        }

        var customer = new Customer(LastName, FirstName, MiddleName, Phone);
        Shell.Current.GoToAsync(nameof(CreateUserViewModel), new ShellNavigationQueryParameters { {"Customer", customer} });
    }

    private void UpdateErrorMessages()
    {
        FirstNameError = GetErrors(nameof(FirstName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LastNameError = GetErrors(nameof(LastName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        MiddleNameError = GetErrors(nameof(MiddleName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PhoneError = GetErrors(nameof(Phone)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}