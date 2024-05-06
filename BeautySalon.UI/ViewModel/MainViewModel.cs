using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries.GetSalon;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private Salon _salon;
    [ObservableProperty] private int _servicesCount;
    [ObservableProperty] private int _mastersCount;
    [ObservableProperty] private IEnumerable<Service> _firstFiveServices = [];
    [ObservableProperty] private IEnumerable<Master> _firstFiveMasters = [];
    [ObservableProperty] private bool _isRefreshing;

    private readonly IMediator _mediator;

    public MainViewModel(IMediator mediator, IApplicationContext context)
    {
        _mediator = mediator;
        Task.Run(() =>
        {
            Salon = context.Salons.First();
            FirstFiveServices = Salon.Services.Take(5);
            FirstFiveMasters = Salon.Masters.Take(5);
            ServicesCount = Salon.Services.Count;
            MastersCount = Salon.Masters.Count;
        });
    }

    [RelayCommand]
    private void SignUpForService()
    {
        
    }

    [RelayCommand]
    private async Task Refresh()
    {
        //if (IsRefreshing) return;
        //IsRefreshing = true;

        try
        {
            Salon = await _mediator.Send(new GetSalonQuery());
            FirstFiveServices = Salon.Services.Take(5);
            FirstFiveMasters = Salon.Masters.Take(5);
            ServicesCount = Salon.Services.Count;
            MastersCount = Salon.Masters.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}