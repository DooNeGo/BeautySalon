using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MasterViewModel(IIdentityService identityService, IMediator mediator)
    : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Master _master = null!;
    [ObservableProperty] private IEnumerable<Service> _services = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Master = (Master)query["Master"];
        Task.Run(async () => Services = await mediator.Send(new GetServicesByMasterIdQuery(Master.Id)));
    }
}