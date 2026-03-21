namespace store.api._.Common;

public class ApiResponse_Success<T> : ApiResponseBase
{
    public T? Data { get; set; }
}

