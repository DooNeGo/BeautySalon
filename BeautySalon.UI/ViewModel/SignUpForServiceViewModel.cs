using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class SignUpForServiceViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Service> _services;

    public SignUpForServiceViewModel(IApplicationContext applicationContext)
    {
        Services = applicationContext.Services;
    }

    [RelayCommand]
    private Task GoToServiceView(Service service) =>
        Shell.Current.GoToAsync(nameof(ServiceViewModel),
            new Dictionary<string, object> { { "Service", service } });
}