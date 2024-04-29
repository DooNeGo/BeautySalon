using System.Collections;

namespace BeautySalon.UI.TemplateSelectors;

public class ParitySelector : DataTemplateSelector
{
    public DataTemplate? DefaultTemplate { get; set; }
    
    public DataTemplate? SpecialTemplate { get; set; }
    
    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (container is not Layout) return DefaultTemplate;
        var itemsSource = (IList)BindableLayout.GetItemsSource(container);
        return itemsSource.IndexOf(item) % 2 is 0 ? SpecialTemplate : DefaultTemplate;
    }
}