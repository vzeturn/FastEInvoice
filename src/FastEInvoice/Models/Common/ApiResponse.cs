using System.Text.Json.Serialization;

namespace FastEInvoice.Models.Common;

/// <summary>
/// Standard API response from FAST E-Invoice system
/// </summary>
/// <typeparam name="T">Type of data returned</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Success status (1 = Success, 0 = Error)
    /// </summary>
    [JsonPropertyName("Success")]
    public int Success { get; set; }

    /// <summary>
    /// Error code (0 = No error)
    /// </summary>
    [JsonPropertyName("Code")]
    public int Code { get; set; }

    /// <summary>
    /// Message or error description
    /// </summary>
    [JsonPropertyName("Message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Response data
    /// </summary>
    [JsonPropertyName("Data")]
    public T? Data { get; set; }

    /// <summary>
    /// Check if response is successful
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Success == 1 && Code == 0;

    /// <summary>
    /// Get error description
    /// </summary>
    [JsonIgnore]
    public string ErrorDescription => !IsSuccess 
        ? ErrorCode.GetDescription(Code) 
        : string.Empty;
}

/// <summary>
/// API response without data
/// </summary>
public class ApiResponse : ApiResponse<object>
{
}