using System.Text.Json.Serialization;

namespace FastEInvoice.Models.Common;

/// <summary>
/// Invoice status enumeration
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InvoiceStatus
{
    /// <summary>
    /// Waiting for authentication (Chờ xác thực)
    /// </summary>
    WaitingAuthentication = 1,

    /// <summary>
    /// Authenticated (Đã xác thực)
    /// </summary>
    Authenticated = 2,

    /// <summary>
    /// Cancelled (Hủy)
    /// </summary>
    Cancelled = 8
}

/// <summary>
/// Extension methods for InvoiceStatus
/// </summary>
public static class InvoiceStatusExtensions
{
    /// <summary>
    /// Get Vietnamese description of status
    /// </summary>
    public static string GetDescription(this InvoiceStatus status)
    {
        return status switch
        {
            InvoiceStatus.WaitingAuthentication => "Chờ xác thực",
            InvoiceStatus.Authenticated => "Đã xác thực",
            InvoiceStatus.Cancelled => "Hủy",
            _ => "Unknown status"
        };
    }

    /// <summary>
    /// Check if invoice is editable
    /// </summary>
    public static bool IsEditable(this InvoiceStatus status)
    {
        return status == InvoiceStatus.WaitingAuthentication;
    }

    /// <summary>
    /// Check if invoice is finalized
    /// </summary>
    public static bool IsFinalized(this InvoiceStatus status)
    {
        return status == InvoiceStatus.Authenticated || status == InvoiceStatus.Cancelled;
    }
}