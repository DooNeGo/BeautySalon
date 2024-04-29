namespace BeautySalon.UI;

internal static class NavigationExtension
{
    public static void ClearNavigationStack(this INavigation navigation)
    {
        foreach (Page page in navigation.NavigationStack)
        {
            if (page is null) continue;
            navigation.RemovePage(page);
        }
    }
}