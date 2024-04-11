using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _username = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoginButtonEnable))]
    private string _password = string.Empty;

    public bool IsLoginButtonEnable => Username.Length >= 3
                                    && Password.Length >= 3
                                    && !string.IsNullOrWhiteSpace(Username)
                                    && !string.IsNullOrWhiteSpace(Password);

    [RelayCommand]
    public void Login()
    {

    }
}