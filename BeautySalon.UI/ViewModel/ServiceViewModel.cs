using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using BeautySalon.UI.View.SignIn;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ServiceViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private IEnumerable<Master> _masters = [];

    private readonly IIdentityService _identityService;
    private readonly IApplicationContext _context;

    public ServiceViewModel(IIdentityService identityService, IApplicationContext context) =>
        (_identityService, _context) = (identityService, context);

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        Task.Run(() => Masters = _context.Positions
            .Where(p => p.Services.Contains(Service))
            .SelectMany(p => p.Masters)
            .ToArray());
    }

    [RelayCommand]
    private async Task SignUp()
    {
        if (_identityService.CurrentUser is not null)
        {
            await Shell.Current.GoToAsync(nameof(ChooseMasterViewModel),
                new Dictionary<string, object> { { "Service", Service } });
        }
        else if (await Shell.Current.CurrentPage.DisplayAlert("Авторизация",
                     "Вы должны войти в аккаунт для записи на услугу", "Войти", "Отмена"))
        {
            await Shell.Current.GoToAsync(nameof(StartView));
        }
    }
}