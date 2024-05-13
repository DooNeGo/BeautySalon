using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class SignUpForServiceStartViewModel : ObservableObject
{
    [ObservableProperty] private IReadOnlyList<Service> _services = [];
    
    public SignUpForServiceStartViewModel(IMediator mediator, GlobalContext globalContext) =>
        Task.Run(async () => Services = await mediator.Send(new GetServicesQuery(globalContext.Salon.Id)));
}