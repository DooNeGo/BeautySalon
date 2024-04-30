using BeautySalon.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class LoginViewModel(IIdentityService identityService) : ObservableObject
{
    [ObservableProperty] private string _errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _username = string.Empty;

    public bool IsLoginButtonEnable =>
            Username.Length >= 3 && Password.Length >= 3 && !string.IsNullOrWhiteSpace(Username)
            && !string.IsNullOrWhiteSpace(Password);

    [RelayCommand]
    private async Task Login()
    {
        if (!await identityService.AuthorizeAsync(Username.Trim(), Password.Trim()))
        {
            ErrorMessage = "*Wrong username or password";
        }
    }
}