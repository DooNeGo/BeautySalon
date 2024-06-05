using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MasterViewModel(IIdentityService identityService, IMediator mediator)
    : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Master _master = null!;
    [ObservableProperty] private IReadOnlyList<Service> _services = [];
    [ObservableProperty] private bool _isRefreshing;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Master = (Master)query["Master"];
        await Refresh(CancellationToken.None).ConfigureAwait(false);
    }
    
    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        if (!IsRefreshing) IsRefreshing = true;
        Services = await mediator.Send(new GetServicesByMasterIdQuery(Master.Id), cancellationToken)
            .ConfigureAwait(false);
        IsRefreshing = false;
    }
    
    [RelayCommand]
    private async Task SignUp()
    {
        if (identityService.CurrentUser is not null)
        {
           await Shell.Current.GoToAsync(nameof(ChooseServicesViewModel),
               new Dictionary<string, object?> { { "Master", Master } }).ConfigureAwait(false);
        }
        else if (await Shell.Current.CurrentPage.DisplayAlert("Авторизация",
                     "Вы должны войти в аккаунт для записи на услугу", "Войти", "Отмена"))
        {
            await Shell.Current.GoToAsync(nameof(StartViewModel)).ConfigureAwait(false);
        }
    }
}