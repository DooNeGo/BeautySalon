using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Salon _salon;

    public MainViewModel()
    {
            Salon = new Salon("Гомель, Советская 16")
            {
                            Services =
                            [
                                            new Service("Мужская стрижка", 30,
                                                            TimeSpan.FromMinutes(60)),
                                            new Service("Детская стрижка", 15,
                                                            TimeSpan.FromMinutes(45)),
                                            new Service("Маникюр + гель лак", 60,
                                                            TimeSpan.FromMinutes(90))
                            ],
                            Masters =
                            [
                                            new Master("Кострома", "Матвей", null,
                                                            new Position("Парикмахер"),
                                                            "+375447452007"),
                                            new Master("Западнюк", "Анастасия", null,
                                                            new Position("Универсал"),
                                                            "+375447452007"),
                                            new Master("Посредников", "Иван", null,
                                                            new Position("Визажист"),
                                                            "+375447452007")
                            ]
            };
    }
}