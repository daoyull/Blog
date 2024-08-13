using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBlog.Lib.Models;

public partial class PageModel : ObservableObject
{
    [ObservableProperty] private int _pageSize;
    [ObservableProperty] private int _pageCount;
    [ObservableProperty] private int _pageNum;
    [ObservableProperty] private long _total;
}
