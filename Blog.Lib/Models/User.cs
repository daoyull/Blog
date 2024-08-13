using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models;

public class UserLoginDto : ObservableObject
{
    private string? _account;

    [MinLength(1, ErrorMessage = "账号不可为空")]
    [MaxLength(255, ErrorMessage = "账号长度超出限制")]
    public string? Account
    {
        get => _account;
        set => SetProperty(ref _account, value);
    }

    private string? _password;

    [MinLength(1, ErrorMessage = "密码不可为空")]
    [MaxLength(16, ErrorMessage = "密码长度不可超出16位")]
    public string? Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
}