using System.ComponentModel.DataAnnotations;

namespace FastEInvoice.Models.DeliveryNote;

/// <summary>
/// Delivery note master (header) information
/// </summary>
public class DeliveryNoteMaster
{
    /// <summary>
    /// Unique key for the delivery note (max 32 characters) - Required
    /// </summary>
    [Required]
    [MaxLength(32)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Delivery note type - Required
    /// 1 = Internal, 2 = Consignment
    /// </summary>
    [Required]
    public int VoucherType { get; set; }

    /// <summary>
    /// Reference number / Internal voucher number
    /// </summary>
    public string? VoucherNumber { get; set; }

    /// <summary>
    /// Delivery note date (dd/MM/yyyy format) - Required
    /// </summary>
    [Required]
    public string InvoiceDate { get; set; } = string.Empty;

    /// <summary>
    /// Customer code (max 32 characters, no diacritics)
    /// </summary>
    [MaxLength(32)]
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Order/contract number (max 100 characters)
    /// Internal: Internal transfer order number
    /// Consignment: Contract number
    /// </summary>
    [MaxLength(100)]
    public string? Order { get; set; }

    /// <summary>
    /// Date of order/contract (dd/MM/yyyy)
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// Partner name (max 400 characters)
    /// Contract of...
    /// </summary>
    [MaxLength(400)]
    public string? Partner { get; set; }

    /// <summary>
    /// Reference/Purpose (max 400 characters)
    /// Regarding/For
    /// </summary>
    [MaxLength(400)]
    public string? Reference { get; set; }

    /// <summary>
    /// Recipient tax code (max 14 characters)
    /// </summary>
    [MaxLength(14)]
    public string? TaxCode { get; set; }

    /// <summary>
    /// Transporter name (max 100 characters)
    /// </summary>
    [MaxLength(100)]
    public string? Deliverer { get; set; }

    /// <summary>
    /// Delivery contract (max 250 characters)
    /// </summary>
    [MaxLength(250)]
    public string? DeliveryContract { get; set; }

    /// <summary>
    /// Transportation method (max 50 characters)
    /// </summary>
    [MaxLength(50)]
    public string? Transportation { get; set; }

    /// <summary>
    /// Issuing warehouse (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? IssuingSite { get; set; }

    /// <summary>
    /// Receiving warehouse (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? ReceivingSite { get; set; }

    /// <summary>
    /// Receiver name (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? Receiver { get; set; }

    /// <summary>
    /// Email address for delivery note (max 50 characters)
    /// </summary>
    [MaxLength(50)]
    public string? EmailDeliver { get; set; }

    /// <summary>
    /// Currency code (3 characters)
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    public decimal ExchangeRate { get; set; } = 1;

    /// <summary>
    /// Total amount
    /// </summary>
    public decimal Amount { get; set; }

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