using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MasterViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Master _master = null!;
    [ObservableProperty] private IEnumerable<Service> _services = [];
    
    private readonly IIdentityService _identityService;
    private readonly IApplicationContext _context;

    public MasterViewModel(IIdentityService identityService, IApplicationContext context) =>
        (_identityService, _context) = (identityService, context);
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Master = (Master)query["Master"];
        Task.Run(() => Services = Master.Position.Services);
    }
}