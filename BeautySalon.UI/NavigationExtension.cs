namespace BeautySalon.UI;

internal static class NavigationExtension
{
    public static void ClearNavigationStack(this INavigation navigation)
    {
        for (var i = 0; i < navigation.NavigationStack.Count - 1; i++)
        {
            Page? page = navigation.NavigationStack[i];
            if (page is null) continue;
            navigation.RemovePage(page);
        }
    }
}