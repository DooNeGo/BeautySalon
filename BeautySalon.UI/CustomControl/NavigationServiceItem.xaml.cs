using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.CustomControl;

public sealed partial class NavigationServiceItem : ServiceItem
{
	public NavigationServiceItem()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not BindableObject bindableObject) return;
        Shell.Current.GoToAsync(nameof(ServiceViewModel),
                new Dictionary<string, object> { { "Service", bindableObject.BindingContext } });
    }
}