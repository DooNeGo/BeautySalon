using BeautySalon.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class LoginViewModel(IIdentityService identityService) : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _username = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public bool IsLoginButtonEnable => Username.Length >= 3
                                    && Password.Length >= 3
                                    && !string.IsNullOrWhiteSpace(Username)
                                    && !string.IsNullOrWhiteSpace(Password);

    [RelayCommand]
    public Task Login()
    {
        return identityService.AuthorizeAsync(Username, Password);
    }
}