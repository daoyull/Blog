using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Blog.Lib.Entity;
using BlogView.Helpers;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Components;

public partial class LeftNavView : UserComponent<LeftNavViewModel>
{
    private string[] _rollText = { "" };
    private int _index = 0;
    private int _length = 0;
    private bool _isAdd = true;
    private bool _isPause = false;

    private DispatcherTimer _timer = new();

    private int _pauseCount = 0;

    private const int RandomCharCount = 5;
    private readonly List<Run> _runs = new();

    public LeftNavView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.OnLeftInformationChanged += HandleOnLeftInformationChanged;
        }
        _timer.Interval = TimeSpan.FromMilliseconds(100);
        _timer.Tick += (sender, args) => { ChangeDescText(); };
        base.OnLoaded(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        if (ViewModel != null)
        {
            ViewModel.OnLeftInformationChanged -= HandleOnLeftInformationChanged;
        }

        _timer.Stop();
    }


    private void HandleOnLeftInformationChanged(LeftInformation obj)
    {
        _timer.Stop();
        if (obj.RollText != null && obj.RollText.Length > 0)
        {
            _rollText = obj.RollText;
            var maxLength = 1 + RandomCharCount;
            Dispatcher.UIThread.Invoke(() =>
            {
                for (int i = 0; i < maxLength; i++)
                {
                    var run = new Run();
                    _runs.Add(run);
                }
            });
            _timer.Start();
        }
        else
        {
            _timer.Stop();
        }
    }


    private async void ChangeDescText()
    {
        if (_isPause)
        {
            if (_pauseCount <= 10)
            {
                // 不处理，模拟暂停
                _pauseCount++;
                return;
            }

            _pauseCount = 0;
            _isPause = false;
        }

        DescTextBlock.Inlines!.Clear();
        if (_rollText.Length == _index)
        {
            _index = 0;
        }

        var str = _rollText[_index];
        _length += _isAdd ? 1 : -1;
        var showText = str.Substring(0, _length);
        var textRun = _runs[0];
        textRun.Text = showText;
        DescTextBlock.Inlines.Add(textRun);
        if (_length >= str.Length)
        {
            _isAdd = false;
            _isPause = true;
            return;
        }

        if (_length <= 0)
        {
            _index++;
            _length = 0;
            _isAdd = true;
        }

        foreach (var run in GetRandomRunText())
        {
            DescTextBlock.Inlines.Add(run);
        }
    }

    private List<Run> GetRandomRunText()
    {
        var runs = new List<Run>();
        for (int i = 1; i <= RandomCharCount; i++)
        {
            var charValue = AllCharacters[_random.Next(AllCharacters.Length)];
            _runs[i].Text = charValue.ToString();
            _runs[i].Foreground = RandomColorHelper.GetRandomColor();
            runs.Add(_runs[i]);
        }

        return runs;
    }


    private static Random _random = new Random();


    private const string AllCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
}