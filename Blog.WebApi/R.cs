using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi;

public class R 
{
    internal R()
    {
    }


    public bool IsSuccess() => Code == 200;

    public int Code { get; set; }

    public string? Msg { get; set; }

    public object? Data { get; set; }
    
    public static R Ok()
    {
        return new R()
        {
            Code = 200,
            Msg = RC.Ok,
            Data = new()
        };
    }

    public static R Ok(string? message)
    {
        return new R()
        {
            Code = 200,
            Msg = message,
            Data = new()
        };
    }

    public static R Ok(object? data)
    {
        return new R()
        {
            Code = 200,
            Msg = RC.Ok,
            Data = data
        };
    }
    
    

    public static R Ok(string message, object data)
    {
        return new R()
        {
            Code = 200,
            Msg = RC.Ok,
            Data = data
        };
    }


    public static R Fail()
    {
        return new R()
        {
            Code = 500,
            Msg = RC.Fail,
            Data = new()
        };
    }

    public static R Fail(string? message)
    {
        return new R()
        {
            Code = 500,
            Msg = message,
            Data = new()
        };
    }

    public static R Fail(object? data)
    {
        return new R()
        {
            Code = 500,
            Msg = RC.Fail,
            Data = data
        };
    }

    public static R Fail(string message, object data)
    {
        return new R()
        {
            Code = 500,
            Msg = RC.Fail,
            Data = data
        };
    }
}

class RC
{
    public const string Ok = "ok";
    public const string Fail = "fail";
}