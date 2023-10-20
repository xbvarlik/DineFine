using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DineFine.API.Result;

public static class ApiResult
{
    [NonAction] 
    public static IActionResult CreateActionResult<T>(ServiceResult<T> response) 
    { 
        if (response.StatusCode == 204) 
            return new ObjectResult(null) 
            { 
                StatusCode = response.StatusCode 
            };
        return new ObjectResult(response) 
        { 
            StatusCode = response.StatusCode 
        };
    }
}

public class ServiceResult<T>
{
    public T? Data { get; set; }
    [JsonIgnore]
    public int StatusCode { get; set; }
    public List<string>? Errors { get; set; }

    public static ServiceResult<T> Success(int statusCode, T data)
    {
        return new ServiceResult<T> { StatusCode = statusCode, Data = data };
    }

    public static ServiceResult<T> Success(int statusCode)
    {
        return new ServiceResult<T> { StatusCode = statusCode };
    }

    public static ServiceResult<T> Fail(int statusCode, List<string> errors)
    {
        return new ServiceResult<T> { StatusCode = statusCode, Errors = errors };
    }

    public static ServiceResult<T> Fail(int statusCode, string error)
    {
        return new ServiceResult<T> { StatusCode = statusCode, Errors = new List<string> { error } };
    }
}