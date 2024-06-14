using AsyncAwaitBestPractices;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MastersViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;

    [ObservableProperty] private IReadOnlyList<Master> _masters = [];
    [ObservableProperty] private bool _isRefreshing;

    public MastersViewModel(IMediator mediator, GlobalContext globalContext)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        Refresh(CancellationToken.None).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        Guard.IsNotNull(_globalContext.Salon, nameof(_globalContext.Salon));
        try
        {
            Masters = await _mediator
                .Send(new GetMastersQuery(_globalContext.Salon.Id), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}