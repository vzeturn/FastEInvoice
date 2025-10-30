using System.Text.Json.Serialization;

namespace FastEInvoice.Models.DeliveryNote;

/// <summary>
/// Delivery note request data structure for API
/// </summary>
public class DeliveryNoteRequest
{
    /// <summary>
    /// Data wrapper
    /// </summary>
    [JsonPropertyName("data")]
    public DeliveryNoteData Data { get; set; } = new();
}

/// <summary>
/// Delivery note data containing structure and note list
/// </summary>
public class DeliveryNoteData
{
    /// <summary>
    /// Structure definition (field order)
    /// </summary>
    [JsonPropertyName("structure")]
    public DeliveryNoteStructure Structure { get; set; } = new();

    /// <summary>
    /// List of delivery notes
    /// </summary>
    [JsonPropertyName("invoices")]
    public List<DeliveryNoteDataItem> Invoices { get; set; } = new();
}

/// <summary>
/// Structure definition for delivery note fields
/// </summary>
public class DeliveryNoteStructure
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
/// Single delivery note data item
/// </summary>
public class DeliveryNoteDataItem
{
    /// <summary>
    /// Master (header) values matching structure order
    /// </summary>
    [JsonPropertyName("master")]
    public List<object?> Master { get; set; } = new();

    /// <summary>
    /// Detail (line items) values matching structure order
    /// </summary>
    [JsonPropertyName("detail")]
    public List<List<object?>> Detail { get; set; } = new();
}