using AsyncAwaitBestPractices;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ServicesViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    
    [ObservableProperty] private IReadOnlyList<Service> _services = [];
    [ObservableProperty] private bool _isRefreshing;
    
    public ServicesViewModel(IMediator mediator, GlobalContext globalContext)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        Refresh(CancellationToken.None).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        Services = await _mediator
            .Send(new GetServicesQuery(_globalContext.Salon.Id), cancellationToken)
            .ConfigureAwait(false);
        IsRefreshing = false;
    }
}