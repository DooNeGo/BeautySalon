using BeautySalon.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class LoginViewModel(IIdentityService identityService) : ObservableObject
{
    [ObservableProperty] private string _errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _password = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _username = string.Empty;

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login(CancellationToken cancellationToken)
    {
        await identityService.AuthorizeAsync(Username.Trim(), Password.Trim(), cancellationToken);
        if (identityService.CurrentUser is null) ErrorMessage = "*Неверный Логин или Пароль";
        else await Shell.Current.GoToAsync("../..");
    }

    private bool CanLogin() =>
        Username.Length >= 3
        && Password.Length >= 3
        && !string.IsNullOrWhiteSpace(Username)
        && !string.IsNullOrWhiteSpace(Password);
}