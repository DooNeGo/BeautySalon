using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ServicesViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Service> _services = [];

    public ServicesViewModel(IApplicationContext context)
    {
        Task.Run(() => Services = context.Services);
    }
}