using System.ComponentModel.DataAnnotations;

namespace FastEInvoice.Models.DeliveryNote;

/// <summary>
/// Delivery note detail (line item) information
/// </summary>
public class DeliveryNoteDetail
{
    /// <summary>
    /// Item code (max 32 characters, no diacritics)
    /// Must be declared in portal if provided
    /// </summary>
    [MaxLength(32)]
    public string? ItemCode { get; set; }

    /// <summary>
    /// Item name (max 500 characters) - Required
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// Item type - Required
    /// 01 = Goods/Services, 08 = Note
    /// </summary>
    [Required]
    public string ItemType { get; set; } = "01";

    /// <summary>
    /// Unit of measurement (max 16 characters)
    /// </summary>
    [MaxLength(16)]
    public string? UOM { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Unit price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Amount = Quantity × Price
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Don't show ordinal number (0/1)
    /// Only applicable for note items (ItemType = 08)
    /// </summary>
    public int NotShowOrdinalNumber { get; set; }

    /// <summary>
    /// Custom string field 1 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External1 { get; set; }

    /// <summary>
    /// Custom string field 2 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External2 { get; set; }

    /// <summary>
    /// Custom string field 3 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External3 { get; set; }

    /// <summary>
    /// Custom numeric field 1
    /// </summary>
    public decimal? NumberExternal1 { get; set; }

    /// <summary>
    /// Custom numeric field 2
    /// </summary>
    public decimal? NumberExternal2 { get; set; }

    /// <summary>
    /// Custom numeric field 3
    /// </summary>
    public decimal? NumberExternal3 { get; set; }
}