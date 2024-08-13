using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LanguageExt;
using LanguageExt.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;


namespace BlogBackManager.ViewModels;

public partial class IndexViewModel : BaseViewModel, IRefresh
{
    private readonly ITagService _tagService;

    private readonly ICategoryService _categoryService;
    // private readonly IIndexService _indexService;

    public IndexViewModel()
    {
        // Design
    }

    public IndexViewModel(ITagService tagService, ICategoryService categoryService)
    {
        _tagService = tagService;
        _categoryService = categoryService;
        // _indexService = indexService;
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }


    [ObservableProperty] private List<ISeries>? _tagSeries;
    [ObservableProperty] private List<ISeries>? _categorySeries;


    public async Task Refresh()
    {
        TagSeries = null;
        CategorySeries = null;
        var tagResult = await _tagService.GetTagBlogNum();
        var categoryResult = await _categoryService.GetTagBlogNum();

        tagResult.Handle(tags => TagSeries = HandleSerialList(tags));
        categoryResult.Handle(cat => CategorySeries = HandleSerialList(cat));
    }


    private List<ISeries> HandleSerialList(List<CountChart> list)
    {
        int outerRadiusOffset = 5;
        var series = new List<ISeries>();
        foreach (var it in list)
        {
            var pieSeries = new PieSeries<int>()
            {
                Values = new List<int>() { it.Count },
                Name = it.Name,
                DataLabelsSize = 22,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => it.Name ?? "",
                InnerRadius = 40,
                OuterRadiusOffset = outerRadiusOffset,
                Fill = new SolidColorPaint(SKColor.Parse(it.Color))
            };
            outerRadiusOffset = Math.Min(outerRadiusOffset + 5, 100);
            series.Add(pieSeries);
        }

        return series;
    }
}