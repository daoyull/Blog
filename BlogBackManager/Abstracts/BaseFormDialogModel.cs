using Common.Avalonia.Service;
using Common.Lib.Ioc;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using Microsoft.Extensions.Logging;
using Ursa.Controls;

namespace BlogBackManager.Abstracts;

public abstract class BaseFormDialogModel<T> : BaseViewModel, IDialogContext where T : class, new()
{
    protected BaseFormDialogModel()
    {
        Command = new(Execute);
        CancelCommand = new AsyncRelayCommand(Cancel);
    }

    protected virtual Task Cancel()
    {
        Close();
        return Task.CompletedTask;
    }

    private async Task Execute()
    {
        try
        {
            // 字段校验
            var verifyResult = await Verify();
            if (!verifyResult)
            {
                return;
            }

            // 新增处理
            await HandleCommand();
            Ok();
            Ioc.ResolveOptional<IGlobalNotify>()?.SuccessTip("操作成功");
        }
        catch (Exception e)
        {
            Ioc.ResolveOptional<ILogger<BaseFormDialogModel<T>>>()?.LogError(e, "BaseEditCommand Exec Error");
            Ioc.ResolveOptional<IGlobalNotify>()?.Error("错误", e.Message);
        }
    }

    protected virtual void Ok()
    {
        RequestClose?.Invoke(this, DialogResult.OK);
    }

    protected virtual async Task<bool> Verify()
    {
        if (Model is IDataVerify dataVerify)
        {
            var verifyResult = await dataVerify.Verify();
            if (!verifyResult.Item1)
            {
                var first = verifyResult.Item2.First();
                Ioc.ResolveOptional<IGlobalNotify>()?.Error("错误", first.ErrorMessage ?? first.ToString());
                return false;
            }
        }

        return true;
    }

    protected abstract Task HandleCommand();

    private T _model = new();

    /// <summary>
    /// 新增的模型
    /// </summary>
    public T Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }


    #region Command

    public AsyncRelayCommand Command { get; set; }
    public AsyncRelayCommand CancelCommand { get; set; }

    #endregion

    public void Close()
    {
        RequestClose?.Invoke(this, DialogResult.Cancel);
    }

    public event EventHandler<object?>? RequestClose;
}