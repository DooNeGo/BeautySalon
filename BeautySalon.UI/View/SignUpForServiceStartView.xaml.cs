using BeautySalon.UI.CustomControl;
using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class SignUpForServiceStartView
{
    public SignUpForServiceStartView(SignUpForServiceStartViewModel startViewModel)
    {
        InitializeComponent();
        BindingContext = startViewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not ServiceItem serviceItem) return;
        var checkBox = (CheckBox)serviceItem.TailView;
        checkBox.IsChecked = true;
    }
}