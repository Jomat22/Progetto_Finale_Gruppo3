namespace store.api._.Common;

public class ApiResponseBase
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}

