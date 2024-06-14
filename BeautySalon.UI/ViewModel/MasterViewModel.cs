using AsyncAwaitBestPractices;
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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Master = (Master)query["Master"];
        Refresh(CancellationToken.None).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        try
        {
            Services = await mediator
             .Send(new GetServicesByMasterIdQuery(Master.Id), cancellationToken)
             .ConfigureAwait(false);
        }
        finally
        {
            IsRefreshing = false;
        }
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