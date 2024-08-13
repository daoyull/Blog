using Avalonia.Controls;
using Common.Avalonia.Abstracts;
using Common.Avalonia.Service;
using Common.Lib.Ioc;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using Ursa.Controls;

namespace BlogBackManager.Helpers;

public static class DialogHelper
{
    public static async Task ShowDialogAsync<TV, TM>(DialogOptions<TV, TM>? options = null)
        where TV : UserComponent<TM>, new() where TM : BaseViewModel
    {
        if (options == null)
        {
            options = new();
        }

        var dialog = new TV();
        if (options.InitCommand != null)
        {
            await options.InitCommand.ExecuteAsync(dialog);
        }

        var dialogResult = await Dialog.ShowModal(dialog, dialog.DataContext, options: new()
        {
            StartupLocation = options.StartupLocation,
            Title = options.Title,
            Mode = options.DialogMode,
            Button = options.DialogButton,
            IsCloseButtonVisible = options.IsCloseButtonVisible
        });

        if (dialogResult == DialogResult.OK && options.OkCommand != null)
        {
            await options.OkCommand.ExecuteAsync(dialog);
        }

        if ((dialogResult == DialogResult.Cancel || dialogResult == DialogResult.None) && options.CancelCommand != null)
        {
            await options.CancelCommand.ExecuteAsync(dialog);
        }
    }

    public static async Task<DialogResult> ShowDialogAsync(Control control, DialogOptions? dialogOptions = null)
    {
        if (dialogOptions == null)
        {
            dialogOptions = DefaultDialogOptions;
        }

        return await Dialog.ShowModal(control, control.DataContext, options: dialogOptions);
    }

    public static DialogOptions? DefaultDialogOptions { get; } = new()
    {
        StartupLocation = WindowStartupLocation.CenterScreen,
        Title = string.Empty,
        Mode = DialogMode.None,
        Button = DialogButton.None,
        IsCloseButtonVisible = false,
        ShowInTaskBar = false
    };
}

public class DialogOptions<T, TM> where T : UserComponent<TM> where TM : BaseViewModel
{
    public string Title { get; set; } = "Title";

    public bool IsCloseButtonVisible { get; set; } = false;

    public AsyncRelayCommand<T>? InitCommand { get; set; }

    public AsyncRelayCommand<T>? OkCommand { get; set; }

    public AsyncRelayCommand<T>? CancelCommand { get; set; } = new(DefaultCancel);


    private static Task DefaultCancel(T? arg)
    {
        Ioc.ResolveOptional<IGlobalNotify>()?.Info("提示", "操作取消");
        return Task.CompletedTask;
    }

    public DialogMode DialogMode { get; set; } = DialogMode.Info;

    public DialogButton DialogButton { get; set; } = Ursa.Controls.DialogButton.None;

    public WindowStartupLocation StartupLocation { get; set; } = WindowStartupLocation.CenterOwner;
}