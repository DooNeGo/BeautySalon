using BeautySalon.UI.CustomControl;
using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class ChooseMasterView
{
    private readonly ChooseMasterViewModel _viewModel;

    public ChooseMasterView(ChooseMasterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;
        if (sender is not BindableObject bindableObject) return;

        _viewModel.SetMasterCommand.Execute(bindableObject.BindingContext);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not MasterItem masterItem) return;
        var radioButton = (RadioButton)masterItem.TailView;
        radioButton.IsChecked = true;
    }
}