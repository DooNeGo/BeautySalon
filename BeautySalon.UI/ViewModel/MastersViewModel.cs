using System.Collections.ObjectModel;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MastersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Master> _masters;

    public MastersViewModel()
    {
        Masters =
        [
                new Master("Кострома", "Матвей", null, new Position("Парикмахер"), "+375447452007"),
                new Master("Западнюк", "Анастасия", null, new Position("Универсал"), "+375447452007"),
                new Master("Посредников", "Иван", null, new Position("Визажист"), "+375447452007")
        ];
    }
}