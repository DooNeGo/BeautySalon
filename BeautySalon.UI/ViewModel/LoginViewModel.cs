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
    private async Task LoginAsync(CancellationToken cancellationToken)
    {
        await identityService.AuthorizeAsync(Username.Trim(), Password.Trim(), cancellationToken).ConfigureAwait(false);
        if (identityService.CurrentUser is null) ErrorMessage = "*Неверный Логин или Пароль";
        else await Shell.Current.GoToAsync("../..", true).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    private bool CanLogin() =>
        Username.Trim().Length >= 3
        && Password.Trim().Length >= 3
        && !string.IsNullOrWhiteSpace(Username)
        && !string.IsNullOrWhiteSpace(Password);
}