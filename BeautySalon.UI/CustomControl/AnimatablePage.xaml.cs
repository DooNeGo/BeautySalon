namespace BeautySalon.UI.CustomControl;

public partial class AnimatablePage : ContentPage
{
    public AnimatablePage()
    {
        InitializeComponent();

        this.TranslateTo(Width, 0, easing: Easing.SpringOut);
    }

    public async Task GoToAnimated(string url)
    {
        await this.TranslateTo(-Width, 0, easing: Easing.SpringOut);
        await Shell.Current.GoToAsync(url, true);
    }
}