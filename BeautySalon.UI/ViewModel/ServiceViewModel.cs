using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using BeautySalon.UI.View.SignIn;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

[QueryProperty(nameof(Service), nameof(Service))]
public sealed partial class ServiceViewModel : ObservableObject
{
    private readonly IIdentityService _identityService;

    [ObservableProperty]
    private Service _service = null!;

    public ServiceViewModel(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        _identityService = identityService;
    }

    [RelayCommand]
    private async Task SignUp()
    {
        if (_identityService.CurrentUser is null)
        {
            if (await Shell.Current.CurrentPage.DisplayAlert("Авторизация",
                        "Вы должны войти в аккаунт для записи на услугу", "Войти", "Отмена"))
            {
                await Shell.Current.GoToAsync(nameof(StartView));   
            }
        }
    }
}