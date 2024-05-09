using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.CustomControl;

public partial class MasterItem
{
    public MasterItem()
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