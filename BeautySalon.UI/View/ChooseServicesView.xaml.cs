using BeautySalon.UI.CustomControl;
using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ChooseServicesView : ContentPage
{
    private readonly ChooseServicesViewModel _viewModel;

    public ChooseServicesView(ChooseServicesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not ServiceItem serviceItem) return;
        var checkBox = (CheckBox)serviceItem.TailView;
        checkBox.IsChecked = true;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is not Element element) return;
        if (e.Value) _viewModel.AddServiceCommand.Execute(element.Parent.BindingContext);
        else _viewModel.RemoveServiceCommand.Execute(element.Parent.BindingContext);
    }
}