using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class StartViewModel : ObservableObject
{
    [RelayCommand]
    private Task GoToLoginView() => Shell.Current.GoToAsync(nameof(LoginViewModel));

    [RelayCommand]
    private Task GoToCreateView() => Shell.Current.GoToAsync(nameof(CreateUserViewModel));
}