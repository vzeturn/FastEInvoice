using System.Text.Json.Serialization;

namespace FastEInvoice.Models.Invoice;

/// <summary>
/// Invoice request data structure for API
/// </summary>
public class InvoiceRequest
{
    /// <summary>
    /// Data wrapper
    /// </summary>
    [JsonPropertyName("data")]
    public InvoiceData Data { get; set; } = new();
}

/// <summary>
/// Invoice data containing structure and invoice list
/// </summary>
public class InvoiceData
{
    /// <summary>
    /// Structure definition (field order)
    /// </summary>
    [JsonPropertyName("structure")]
    public InvoiceStructure Structure { get; set; } = new();

    /// <summary>
    /// List of invoices
    /// </summary>
    [JsonPropertyName("invoices")]
    public List<InvoiceDataItem> Invoices { get; set; } = new();
}

/// <summary>
/// Structure definition for invoice fields
/// </summary>
public class InvoiceStructure
{
    /// <summary>
    /// Master (header) field names in order
    /// </summary>
    [JsonPropertyName("master")]
    public List<string> Master { get; set; } = new();

    /// <summary>
    /// Detail (line item) field names in order
    /// </summary>
    [JsonPropertyName("detail")]
    public List<string> Detail { get; set; } = new();
}

/// <summary>
/// Single invoice data item
/// </summary>
public class InvoiceDataItem
{
    /// <summary>
    /// Master (header) values matching structure order
    /// </summary>
    [JsonPropertyName("master")]
    public List<object?> Master { get; set; } = new();

    /// <summary>
    /// Detail (line items) values matching structure order
    /// Each item is an array of values
    /// </summary>
    [JsonPropertyName("detail")]
    public List<List<object?>> Detail { get; set; } = new();
}