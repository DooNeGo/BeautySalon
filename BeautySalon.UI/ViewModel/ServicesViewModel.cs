using System.Collections.ObjectModel;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public partial class ServicesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Service> _services;

    public ServicesViewModel()
    {
        Services = 
        [
                new Service("Мужская стрижка", 30,
                        TimeSpan.FromMinutes(60)),
                new Service("Детская стрижка", 15,
                        TimeSpan.FromMinutes(45)),
                new Service("Маникюр + гель лак", 60,
                        TimeSpan.FromMinutes(90))];
    }
}