using Blog.Lib.Service;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogView.ViewModels;

public partial class AboutMeViewModel : BaseViewModel, IRefresh
{
    private readonly IAboutMeService _aboutMeService;
    [ObservableProperty] private string? _desc;

    public AboutMeViewModel(IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
    }

    public async Task Refresh()
    {
        Desc = await _aboutMeService.Desc();
    }
}