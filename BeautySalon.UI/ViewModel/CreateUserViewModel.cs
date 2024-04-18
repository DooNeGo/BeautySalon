using BeautySalon.Application.Commands.AddUserCommand;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateUserViewModel(IMediator mediator) : ObservableValidator, IQueryAttributable
{
    private Customer _customer = null!;

    [Required(ErrorMessage = "*Required")]
    //[Base64String]
    [MinLength(3, ErrorMessage = "*Minimum length is 3")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _username = string.Empty;

    [Required(ErrorMessage = "*Required")]
    //[Base64String]
    [MinLength(3, ErrorMessage = "*Minimum length is 3")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _password = string.Empty;

    [Required(ErrorMessage = "*Required")]
    //[Base64String]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _confirmedPassword = string.Empty;

    [Required(ErrorMessage = "*Required")]
    [EmailAddress]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _usernameError = string.Empty;

    [ObservableProperty]
    private string _passwordError = string.Empty;

    [ObservableProperty]
    private string _confirmedPasswordError = string.Empty;

    [ObservableProperty]
    private string _emailError = string.Empty;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _customer = (Customer)query["Customer"];
    }

    [RelayCommand]
    private async Task CreateUser()
    {
        ValidateAllProperties();
        UpdateErrorMessages();

        if (HasErrors)
        {
            return;
        }

        await mediator.Send(new AddUserCommand(new User(Username, Password, Email, _customer)));
        await Shell.Current.GoToAsync(nameof(MainViewModel));
    }

    private void UpdateErrorMessages()
    {
        UsernameError = GetErrors(nameof(Username)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PasswordError = GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        ConfirmedPasswordError = GetErrors(nameof(ConfirmedPassword)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        EmailError = GetErrors(nameof(Email)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}