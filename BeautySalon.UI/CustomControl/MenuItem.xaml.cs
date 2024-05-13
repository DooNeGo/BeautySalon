namespace BeautySalon.UI.CustomControl;

public partial class MenuItem
{
    #region BindableProperties
    public static readonly BindableProperty HeadViewProperty = BindableProperty.Create(nameof(HeadView),
        typeof(Microsoft.Maui.Controls.View), typeof(MenuItem), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (MenuItem)bindable;
            control.Head.Content = (Microsoft.Maui.Controls.View)newValue;
        });

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
        typeof(string), typeof(MenuItem), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (MenuItem)bindable;
            control.TitleLabel.Text = (string)newValue;
        });

    public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail),
        typeof(string), typeof(MenuItem), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (MenuItem)bindable;
            control.DetailLabel.Text = (string)newValue;
        });

    public static readonly BindableProperty TailViewProperty = BindableProperty.Create(nameof(TailView),
        typeof(Microsoft.Maui.Controls.View), typeof(MenuItem), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (MenuItem)bindable;
            control.Tail.Content = (Microsoft.Maui.Controls.View)newValue;
        });
    #endregion

    public MenuItem()
    {
        InitializeComponent();
    }

    public Microsoft.Maui.Controls.View HeadView
    {
        get => (Microsoft.Maui.Controls.View)GetValue(HeadViewProperty);
        set => SetValue(HeadViewProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(HeadViewProperty);
        set => SetValue(HeadViewProperty, value);
    }

    public string Detail
    {
        get => (string)GetValue(HeadViewProperty);
        set => SetValue(HeadViewProperty, value);
    }

    public Microsoft.Maui.Controls.View TailView
    {
        get => (Microsoft.Maui.Controls.View)GetValue(TailViewProperty);
        set => SetValue(TailViewProperty, value);
    }
}