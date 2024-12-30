using Blog.Lib.Entity;
using Blog.Lib.Service;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogView.ViewModels;

public partial class LeftNavViewModel : BaseViewModel, IRefresh
{
    public LeftNavViewModel(IInformationService informationService)
    {
        _informationService = informationService;
        
    }


    [ObservableProperty] private LeftInformation? _info;
    private readonly IInformationService _informationService;

    public Action<LeftInformation>? OnLeftInformationChanged;

    public async Task Refresh()
    {
        var leftInformation = await _informationService.GetLeftInformation();
        leftInformation.Handle(info =>
        {
            Info = info;
            OnLeftInformationChanged?.Invoke(info);
        });
    }
}