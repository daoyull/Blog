using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BlogBackManager.Dialogs;

public partial class MarkPreview : UserControl
{
    // 私有构造函数，防止外部实例化
    public MarkPreview()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => View.Text;
        set => View.Text = value;
    }
}