﻿
namespace SharedData.Wrapper;
public class ApiResponse<T>

{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message = "")
    {
        return new ApiResponse<T> { Success = true, Message = message, Data = data };
    }

    public static ApiResponse<T> FailResponse(string message)
    {
        return new ApiResponse<T> { Success = false, Message = message };
    }
}
