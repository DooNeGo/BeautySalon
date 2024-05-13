using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MastersViewModel : ObservableObject
{
    [ObservableProperty] private IReadOnlyList<Master> _masters = [];
    
    public MastersViewModel(IMediator mediator, GlobalContext globalContext) =>
        Task.Run(async () => Masters = await mediator.Send(new GetMastersQuery(globalContext.Salon.Id)));
}