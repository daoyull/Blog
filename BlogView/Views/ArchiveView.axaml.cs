using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Views;

public partial class ArchiveView :UserControl
{
    public ArchiveViewModel ViewModel { get; } = Ioc.Resolve<ArchiveViewModel>();
    public ArchiveView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}
