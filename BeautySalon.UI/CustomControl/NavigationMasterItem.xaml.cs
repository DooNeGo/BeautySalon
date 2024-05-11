using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.CustomControl;

public sealed partial class NavigationMasterItem : MasterItem
{
    public NavigationMasterItem()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not BindableObject bindableObject) return;
        Shell.Current.GoToAsync(nameof(MasterViewModel),
            new Dictionary<string, object> { { "Master", bindableObject.BindingContext } });
    }
}