using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastEInvoice.Models.Query;

/// <summary>
/// Query request wrapper
/// </summary>
public class QueryRequest
{
    /// <summary>
    /// List of invoices to query
    /// </summary>
    [JsonPropertyName("data")]
    public List<QueryItem> Data { get; set; } = new();
}

/// <summary>
/// Single query item
/// </summary>
public class QueryItem
{
    /// <summary>
    /// Invoice key - Required
    /// </summary>
    [Required]
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Invoice date (dd/MM/yyyy) - Required
    /// </summary>
    [Required]
    [JsonPropertyName("invoiceDate")]
    public string InvoiceDate { get; set; } = string.Empty;
}