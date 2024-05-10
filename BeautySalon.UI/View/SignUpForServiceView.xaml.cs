using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class SignUpForServiceView
{
    public SignUpForServiceView(SignUpForServiceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void ImageButton_Released(object sender, EventArgs e)
    {
        
    }
}