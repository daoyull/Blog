using System.Collections.ObjectModel;
using AvaloniaBlog.Lib.Entity;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogBackManager.Models;

public partial class MenuItem : ObservableObject
{
    [ObservableProperty] private string? _header;

    [ObservableProperty] private Icon? _icon;

    [ObservableProperty] private bool _isSeparator;

    public Type? View { get; set; }


    [ObservableProperty] private ObservableCollection<MenuItem> _children;

    public IEnumerable<MenuItem> GetLeaves()
    {
        if (Children.Count == 0)
        {
            yield return this;
            yield break;
        }

        foreach (var child in Children)
        {
            var items = child.GetLeaves();
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}