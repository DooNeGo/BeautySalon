using AsyncAwaitBestPractices;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    
    [ObservableProperty] private Salon _salon = null!;
    [ObservableProperty] private int _servicesCount;
    [ObservableProperty] private int _mastersCount;
    [ObservableProperty] private IReadOnlyList<Service> _firstFiveServices = [];
    [ObservableProperty] private IReadOnlyList<Master> _firstFiveMasters = [];
    [ObservableProperty] private bool _isRefreshing;

    public MainViewModel(IMediator mediator, GlobalContext globalContext)
    {
        (_mediator, _globalContext) = (mediator, globalContext);
        Refresh().SafeFireAndForget();
    }

    [RelayCommand]
    private Task ViewAllServices() => Shell.Current.GoToAsync(nameof(ServicesViewModel));

    [RelayCommand]
    private Task ViewAllMasters() => Shell.Current.GoToAsync(nameof(MastersViewModel));

    [RelayCommand]
    private async Task Refresh()
    {
        try
        {
            Salon = await _mediator.Send(new GetSalonQuery()).ConfigureAwait(false);
            FirstFiveServices = await _mediator.Send(new GetFirstFiveServicesQuery(Salon.Id)).ConfigureAwait(false);
            FirstFiveMasters = await _mediator.Send(new GetFirstFiveMastersQuery(Salon.Id)).ConfigureAwait(false);
            ServicesCount = await _mediator.Send(new GetServicesCountQuery(Salon.Id)).ConfigureAwait(false);
            MastersCount = await _mediator.Send(new GetMastersCountQuery(Salon.Id)).ConfigureAwait(false);
            _globalContext.Salon = Salon;
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}