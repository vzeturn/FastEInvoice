using FastEInvoice.Models.Common;

namespace FastEInvoice.Exceptions;

/// <summary>
/// Exception thrown by FAST E-Invoice API client
/// </summary>
public class FastEInvoiceException : Exception
{
    /// <summary>
    /// Error code from API
    /// </summary>
    public int ErrorCode { get; }

    /// <summary>
    /// Raw error message from API
    /// </summary>
    public string? RawMessage { get; }

    /// <summary>
    /// Initializes a new instance of FastEInvoiceException
    /// </summary>
    /// <param name="message">Exception message</param>
    public FastEInvoiceException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of FastEInvoiceException
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public FastEInvoiceException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of FastEInvoiceException with error code
    /// </summary>
    /// <param name="errorCode">Error code from API</param>
    /// <param name="message">Error message</param>
    /// <param name="rawMessage">Raw message from API</param>
    public FastEInvoiceException(int errorCode, string message, string? rawMessage = null) 
        : base(message)
    {
        ErrorCode = errorCode;
        RawMessage = rawMessage;
    }

    /// <summary>
    /// Create exception from API response
    /// </summary>
    /// <param name="response">API response</param>
    /// <returns>FastEInvoiceException</returns>
    public static FastEInvoiceException FromApiResponse(ApiResponse response)
    {
        var description = Models.Common.ErrorCode.GetDescription(response.Code);
        var message = string.IsNullOrWhiteSpace(response.Message)
            ? description
            : $"{description} - {response.Message}";

        return new FastEInvoiceException(response.Code, message, response.Message);
    }

    /// <summary>
    /// Create exception from API response with data
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    /// <param name="response">API response</param>
    /// <returns>FastEInvoiceException</returns>
    public static FastEInvoiceException FromApiResponse<T>(ApiResponse<T> response)
    {
        var description = Models.Common.ErrorCode.GetDescription(response.Code);
        var message = string.IsNullOrWhiteSpace(response.Message)
            ? description
            : $"{description} - {response.Message}";

        return new FastEInvoiceException(response.Code, message, response.Message);
    }
}