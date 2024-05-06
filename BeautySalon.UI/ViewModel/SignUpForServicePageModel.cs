using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class SignUpForServicePageModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Service> _services;

    public SignUpForServicePageModel(IApplicationContext applicationContext)
    {
        Services = applicationContext.Services;
    }
}