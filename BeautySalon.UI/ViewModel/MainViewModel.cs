﻿using AsyncAwaitBestPractices;
using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    private readonly IIdentityService _identityService;

    [ObservableProperty] private Salon _salon = null!;
    [ObservableProperty] private int _servicesCount;
    [ObservableProperty] private int _mastersCount;
    [ObservableProperty] private IReadOnlyList<Service> _firstFiveServices = [];
    [ObservableProperty] private IReadOnlyList<Master> _firstFiveMasters = [];
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private bool _isLogoutButtonVisible;

    public MainViewModel(IMediator mediator, GlobalContext globalContext, IIdentityService identityService)
    {
        (_mediator, _globalContext, _identityService) = (mediator, globalContext, identityService);
        _identityService.Authorized += _ => IsLogoutButtonVisible = true;
        _identityService.Unauthorized += () => IsLogoutButtonVisible = false;
        Refresh().SafeFireAndForget();
    }

    [RelayCommand]
    private Task ViewAllServices(CancellationToken cancellationToken = default) => 
        Shell.Current.GoToAsync(nameof(ServicesViewModel)).WaitAsync(cancellationToken);

    [RelayCommand]
    private Task ViewAllMasters(CancellationToken cancellationToken = default) => 
        Shell.Current.GoToAsync(nameof(MastersViewModel)).WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task Refresh()
    {
        try
        {
            Salon = await _mediator.Send(new GetSalonQuery()).ConfigureAwait(false);
            FirstFiveServices = await _mediator.Send(new GetFirstFiveServicesQuery(Salon.Id)).ConfigureAwait(false);
            FirstFiveMasters = await _mediator.Send(new GetFirstFiveMastersQuery(Salon.Id)).ConfigureAwait(false);
            ServicesCount = await _mediator.Send(new GetServicesCountQuery(Salon.Id)).ConfigureAwait(false);
            MastersCount = await _mediator.Send(new GetMastersCountQuery(Salon.Id)).ConfigureAwait(false);
            _globalContext.Salon = Salon;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task SignUpForService(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        string? answer = await App.Current.MainPage.DisplayActionSheet("Создание записи", "Отмена",
            null, "Услуги", "Мастера").WaitAsync(cancellationToken);
        await GetSignUpAction(answer, cancellationToken).ConfigureAwait(false);
    }

    private Task GetSignUpAction(string answer, CancellationToken cancellationToken) => answer switch
    {
        "Услуги" => ViewAllServices(cancellationToken),
        "Мастера" => ViewAllMasters(cancellationToken),
        _ => Task.CompletedTask
    };

    [RelayCommand]
    private async Task LogoutAsync(CancellationToken cancellationToken)
    {
        if (await App.Current.MainPage
                .DisplayAlert("Выход", 
                    "Вы уверены,что хотите выйти?", 
                    "Да", "Нет")
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false))
        {
            _identityService.Unauthorize();    
        }
        
    }
}