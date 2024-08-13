using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogBackManager.ViewModels;

public partial class MainViewModel : BaseViewModel, IRefresh
{
    [ObservableProperty] private object? _mainView;


    public Task Refresh()
    {
        return Task.CompletedTask;
    }
}