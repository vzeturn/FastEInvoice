using System.ComponentModel.DataAnnotations;

namespace FastEInvoice.Models.Invoice;

/// <summary>
/// Invoice detail (line item) information
/// </summary>
public class InvoiceDetail
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
    /// 01 = Goods/Services, 02 = Promotional (non-tax), 03 = Promotional (tax)
    /// 04 = Gift, 05 = Discount, 08 = Note
    /// 51 = Vehicle (car/motorcycle), 52 = Transport service, 53 = Digital transport/E-commerce
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
    /// Amount (before discount) = Quantity × Price
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Discount rate (%)
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Tax rate (%)
    /// 0, 5, 8, 10, -1 (KCT), -2 (KKKNT), -9 (Empty for note items)
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Tax amount = (Amount - Discount) × TaxRate
    /// </summary>
    public decimal TaxAmount { get; set; }

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

    /// <summary>
    /// Chassis number (for vehicles - ItemType 51) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T1 { get; set; }

    /// <summary>
    /// Engine number (for vehicles - ItemType 51) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T2 { get; set; }

    /// <summary>
    /// License plate (for transport - ItemType 52) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T3 { get; set; }

    /// <summary>
    /// Sender name (for digital transport - ItemType 53) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T4 { get; set; }

    /// <summary>
    /// Sender address (for digital transport - ItemType 53) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T5 { get; set; }

    /// <summary>
    /// Sender tax code (for digital transport - ItemType 53) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T6 { get; set; }

    /// <summary>
    /// Sender identification (for digital transport - ItemType 53) - max 200 characters
    /// </summary>
    [MaxLength(200)]
    public string? T7 { get; set; }
}