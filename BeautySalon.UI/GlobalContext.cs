using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI;

public sealed partial class GlobalContext : ObservableObject
{
    [ObservableProperty] private Salon _salon = null!;

    private Customer? _customer;

    public GlobalContext(IIdentityService identityService)
    {
        identityService.Authorized += user => Customer = user.Customer!;
        identityService.Unauthorized += () => Customer = null;
    }

    public Customer? Customer
    {
        get => _customer;
        private set
        {
            if (_customer == value) return;
            OnPropertyChanging();
            _customer = value;
            OnPropertyChanged();
        }
    }
}