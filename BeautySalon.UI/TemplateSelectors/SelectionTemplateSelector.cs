using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.TemplateSelectors;

[ObservableObject]
public sealed partial class SelectionTemplateSelector : DataTemplateSelector
{
    [ObservableProperty] private object? _selectedItem;
    
    public DataTemplate? DefaultTemplate { get; set; }
    
    public DataTemplate? SelectedTemplate { get; set; }
    
    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        DataTemplate? res;
        if (ReferenceEquals(item, _selectedItem))
            res = SelectedTemplate;
        else
            res = DefaultTemplate;
        return res;
    }
}