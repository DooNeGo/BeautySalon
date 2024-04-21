using BeautySalon.Application.Commands.AddUserCommand;
using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateUserViewModel(IMediator mediator) : ObservableValidator
{
    [Required(ErrorMessage = "*Required")]
    [LatinOnly("*Must contain only a-z, A-Z or 0-9")]
    [MinLength(4, ErrorMessage = "*Minimum length is 4")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _username = string.Empty;

    [Required(ErrorMessage = "*Required")]
    [LatinOnly("*Must contain only a-z, A-Z or 0-9")]
    [MinLength(8, ErrorMessage = "*Minimum length is 8")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _password = string.Empty;

    [Required(ErrorMessage = "*Required")]
    [LatinOnly("*Must contain only a-z, A-Z or 0-9")]
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

    [RelayCommand]
    private async Task CreateUser()
    {
        Username = Username.Trim();
        Password = Password.Trim();
        ConfirmedPassword = ConfirmedPassword.Trim();
        Email = Email.Trim();

        ValidateAllProperties();
        UpdateErrorMessages();

        if (HasErrors)
        {
            return;
        }

        Guid id = await mediator.Send(new AddUserCommand(new User(Username, Password, Email)));
        await Shell.Current.GoToAsync(nameof(CreateAccountViewModel), new ShellNavigationQueryParameters { { "UserId", id } });
    }

    private void UpdateErrorMessages()
    {
        UsernameError = GetErrors(nameof(Username)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PasswordError = GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        ConfirmedPasswordError = GetErrors(nameof(ConfirmedPassword)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        EmailError = GetErrors(nameof(Email)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}