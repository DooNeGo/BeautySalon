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
        
        /*Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Grid
                    {
                        ColumnDefinitions = 
                            [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
                        RowDefinitions =
                            [new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto)],
                        
                        Children =
                        {
                            new Label()
                                .Text("Дата", (Color)App.Current.Resources["Gray400"])
                                .Margins(5)
                                .Row(0).Column(0),
                            new Label()
                                .Text("Время", (Color)App.Current.Resources["Gray400"])
                                .Margins(5)
                                .Row(0).Column(1),
                            new DatePicker { Format = "dd.MM.yyyy", MinimumDate = viewModel.MinimumDateTime}
                                .Bind(DatePicker.DateProperty, nameof(viewModel.SelectedDate))
                                .FontSize(15)
                                .Row(1).Column(0),
                            new Picker()
                                .Bind(Picker.SelectedItemProperty, nameof(viewModel.SelectedTime))
                                .Bind(Picker.ItemsSourceProperty, nameof(viewModel.MasterFreeTime))
                                .FontSize(15)
                                .Row(1).Column(1)
                        }
                    },
                    new Label()
                        .Text("Мастера", (Color)App.Current.Resources["Gray400"])
                        .Margins(5, 15, 0, 5),
                    new VerticalStackLayout()
                        .Bind(BindableLayout.ItemsSourceProperty, nameof(viewModel.Masters))
                        .ItemTemplate(new DataTemplate(() =>
                        {
                            RadioButton radioButton = new() { GroupName = "Masters" };
                            radioButton.Bind(RadioButton.IsCheckedProperty,
                                nameof(SelectableObject<Master>.IsSelected), BindingMode.TwoWay);
                            radioButton.CheckedChanged += RadioButton_CheckedChanged;

                            TapGestureRecognizer tapGestureRecognizer = new();
                            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                            
                            var item = new MenuItem
                                { TailView = radioButton, GestureRecognizers = { tapGestureRecognizer } };
                            item.Bind(MenuItem.TitleProperty, "Value.FirstName")
                                .Bind(MenuItem.DetailProperty, "Value.Position.Title");
                            
                            return item;
                        }))
                }
            }
        }.Padding(10);*/
    }

    private void RadioButton_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;
        if (sender is not RadioButton radioButton) return;
        _viewModel.SetMasterCommand.Execute(radioButton.Parent.BindingContext);
    }

    private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        if (sender is not MasterItem masterItem) return;
        var radioButton = (RadioButton)masterItem.TailView;
        _viewModel.SetMasterCommand.Execute(radioButton.Parent.BindingContext);
        //radioButton.IsChecked = true;
    }
}