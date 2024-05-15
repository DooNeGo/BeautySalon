using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private Salon _salon = null!;
    [ObservableProperty] private int _servicesCount;
    [ObservableProperty] private int _mastersCount;
    [ObservableProperty] private IReadOnlyList<Service> _firstFiveServices = [];
    [ObservableProperty] private IReadOnlyList<Master> _firstFiveMasters = [];
    [ObservableProperty] private bool _isRefreshing;

    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;

    public MainViewModel(IMediator mediator, GlobalContext globalContext)
    {
        (_mediator, _globalContext) = (mediator, globalContext);
        Task.Run(Update);
    }
    
    [RelayCommand]
    private Task SignUpForServices() => Shell.Current.GoToAsync(nameof(SignUpForServiceStartViewModel));

    [RelayCommand]
    private Task ViewAllServices() => Shell.Current.GoToAsync(nameof(ServicesViewModel));

    [RelayCommand]
    private Task ViewAllMasters() => Shell.Current.GoToAsync(nameof(MastersViewModel));

    [RelayCommand]
    private async Task Update()
    {
        if (!IsRefreshing) IsRefreshing = true;
        try
        {
            Salon = await _mediator.Send(new GetSalonQuery());
            FirstFiveServices = await _mediator.Send(new GetFirstFiveServicesQuery(Salon.Id));
            FirstFiveMasters = await _mediator.Send(new GetFirstFiveMastersQuery(Salon.Id));
            ServicesCount = await _mediator.Send(new GetServicesCountQuery(Salon.Id));
            MastersCount = await _mediator.Send(new GetMastersCountQuery(Salon.Id));
            _globalContext.Salon = Salon;
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}