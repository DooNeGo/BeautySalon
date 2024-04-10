using BeautySalon.Application;

namespace BeautySalon.UI;

public partial class MainPage : ContentPage
{
    private int count;

    private readonly IApplicationContext _context;

    public MainPage(IApplicationContext context)
    {
        InitializeComponent();
        _context = context;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

    }
}