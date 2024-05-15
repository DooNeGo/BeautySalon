namespace BeautySalon.UI.Services;

public interface INavigationService
{
    public Task GoToAsync(string url);
}

public sealed class NavigationService : INavigationService
{
    public Task GoToAsync(string url)
    {
        throw new NotImplementedException();
    }
}