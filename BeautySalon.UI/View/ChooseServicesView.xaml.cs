using AsyncAwaitBestPractices;
using BeautySalon.Domain;
using BeautySalon.UI.CustomControl;
using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ChooseServicesView
{
    private readonly ChooseServicesViewModel _viewModel;

    public ChooseServicesView(ChooseServicesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        //UpdateCheckBoxesAsync().SafeFireAndForget();
       // _viewModel.PropertyChanged += (_, _) => UpdateCheckBoxesAsync().SafeFireAndForget();
        // _viewModel.SelectedServices.CollectionChanged += (_, _) => UpdateCheckBoxesAsync().SafeFireAndForget();
    }

    /*private Task UpdateCheckBoxesAsync(CancellationToken token = default) => Task.Run(() =>
    {
        foreach (Service service in _viewModel.SelectedServices)
        {
            var element = (ServiceItem?)ServicesVerticalStackLayout
                .FirstOrDefault(item => ((Service)((BindableObject)item).BindingContext).Id == service.Id);
            if (element is null) continue;

            var checkBox = (CheckBox)element.TailView;
            checkBox.IsChecked = true;
        }
    }, token);

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is not Element element) return;
        if (e.Value) _viewModel.AddServiceCommand.Execute(element.Parent.BindingContext);
        else _viewModel.RemoveServiceCommand.Execute(element.Parent.BindingContext);
    }*/
}