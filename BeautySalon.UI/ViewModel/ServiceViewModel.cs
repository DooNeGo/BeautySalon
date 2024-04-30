using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ServiceViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private Service _service = null!;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
    }
}