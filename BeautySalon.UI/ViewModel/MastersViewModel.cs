using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MastersViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Master> _masters = [];

    public MastersViewModel(IApplicationContext applicationContext)
    {
        Task.Run(() => Masters = applicationContext.Masters);
    }
}