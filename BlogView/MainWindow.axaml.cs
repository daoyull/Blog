using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;

namespace BlogView;

public partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; } = Ioc.Resolve<MainViewModel>();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModel;
    }
    
}
