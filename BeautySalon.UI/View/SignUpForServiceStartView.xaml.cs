using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class SignUpForServiceStartView
{
    public SignUpForServiceStartView(SignUpForServiceStartViewModel startViewModel)
    {
        InitializeComponent();
        BindingContext = startViewModel;
    }

    private void ImageButton_Released(object sender, EventArgs e)
    {
        
    }
}