using BeautySalon.UI.ViewModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace BeautySalon.UI.View;

public sealed partial class AppointmentsView
{
    public AppointmentsView(AppointmentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void Expander_OnExpandedChanged(object? sender, ExpandedChangedEventArgs e)
    {
        if (sender is not Expander expander) return;
        var chevron = expander.FindByName<Image>("Chevron");
        chevron.RotateTo(e.IsExpanded ? 180 : 0, easing: Easing.Default);
    }
}