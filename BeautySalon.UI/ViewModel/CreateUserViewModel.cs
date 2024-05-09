using System.ComponentModel.DataAnnotations;
using BeautySalon.Application.Commands.AddUser;
using BeautySalon.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class CreateUserViewModel(IMediator mediator) : ObservableValidator
{
    [Required(ErrorMessage = "*Обязательное поле")]
    [LatinOnly("*Поле должно состоять только из латиницы, 0-9 и _")]
    [MinLength(4, ErrorMessage = "*Минимальная длина поля 4")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _username = string.Empty;
    
    [Required(ErrorMessage = "*Обязательное поле")]
    [LatinOnly("*Поле должно состоять только из латиницы, 0-9 и _")]
    [MinLength(8, ErrorMessage = "*Минимальная длина поля 8")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _password = string.Empty;
    
    //[Compare(nameof(Password), ErrorMessage = "Passwords must match")]
    [Required(ErrorMessage = "*Обязательное поле")]
    [LatinOnly("*Поле должно состоять только из латиницы, 0-9 и _")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _confirmedPassword = string.Empty;

    [Required(ErrorMessage = "*Обязательное поле")]
    [EmailAddress(ErrorMessage = "*Неверный адрес электронной почты")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty] private string _usernameError = string.Empty;
    [ObservableProperty] private string _passwordError = string.Empty;
    [ObservableProperty] private string _confirmedPasswordError = string.Empty;
    [ObservableProperty] private string _emailError = string.Empty;
    [ObservableProperty] private string _error = string.Empty;

    [RelayCommand]
    private async Task CreateUser()
    {
        Username = Username.Trim();
        Password = Password.Trim();
        ConfirmedPassword = ConfirmedPassword.Trim();
        Email = Email.Trim();

        ValidateAllProperties();
        UpdateErrorMessages();

        if (HasErrors) return;

        try
        {
            Guid id = await mediator.Send(new AddUserCommand(new User(Username, Password, Email)));
            await Shell.Current.GoToAsync(nameof(CreateAccountViewModel),
                    new ShellNavigationQueryParameters { { "UserId", id } });
        }
        catch (Exception e)
        {
            Error = e.InnerException?.Message ?? string.Empty;
        }
    }

    private void UpdateErrorMessages()
    {
        UsernameError = GetErrors(nameof(Username)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PasswordError = GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        ConfirmedPasswordError = GetErrors(nameof(ConfirmedPassword)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        EmailError = GetErrors(nameof(Email)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}