using System.Collections.ObjectModel;
using Avalonia.Controls;
using AvaloniaBlog.Lib.Entity;
using BlogBackManager.Views;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using MenuItem = BlogBackManager.Models.MenuItem;

namespace BlogBackManager.ViewModels;

public partial class LeftNavViewModel : BaseViewModel, IRefresh
{
    private readonly MainViewModel _mainViewModel;

    public LeftNavViewModel()
    {
        // Design
        InitMenu();
    }

    public LeftNavViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }

    public ObservableCollection<MenuItem> MenuItemSource { get; } = new();

    [ObservableProperty] private MenuItem? _selectMenuItem;


    partial void OnSelectMenuItemChanged(MenuItem? value)
    {
        _mainViewModel.MainView = null;
        if (value?.View != null)
        {
            var instance = Activator.CreateInstance(value.View);
            _mainViewModel.MainView = instance;
        }
        else
        {
            _mainViewModel.MainView = new TextBlock() { Text = "Not Found Page" };
        }
    }

    private void InitMenu()
    {
        // Index
        MenuItemSource.Add(new MenuItem
        {
            Header = "主页",
            Icon = Icon.Home,
            IsSeparator = false,
            Children = new ObservableCollection<MenuItem>(),
            View = typeof(IndexView)
        });

        // Tag
        MenuItemSource.Add(new MenuItem
        {
            Header = "标签",
            Icon = Icon.Tags,
            IsSeparator = false,
            View = typeof(TagView),
        });

        // Category
        MenuItemSource.Add(new MenuItem
        {
            Header = "分类",
            Icon = Icon.Category,
            IsSeparator = false,
            Children = new ObservableCollection<MenuItem>(),
            View = typeof(CategoryView)
        });

        // Blog
        MenuItemSource.Add(new MenuItem
        {
            Header = "文章",
            Icon = Icon.BiliBili,
            IsSeparator = false,
            Children = new ObservableCollection<MenuItem>(),
            View = typeof(BlogView)
        });

        MenuItemSource.Add(new MenuItem
        {
            Header = "Empty",
            Icon = Icon.Null,
            IsSeparator = false,
        });
        SelectMenuItem = MenuItemSource.First(it => it.IsSeparator == false);
    }

    public Task Refresh()
    {
        InitMenu();

        return Task.CompletedTask;
    }
}