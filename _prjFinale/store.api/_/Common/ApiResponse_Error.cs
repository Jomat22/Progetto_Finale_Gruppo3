namespace store.api._.Common;

public class ApiResponse_Error : ApiResponseBase
{
    public Dictionary<string, string[]>? Error { get; set; }
    public string[]? Warning { get; set; }
    public string DebugInfo { get; set; } = string.Empty;
}

