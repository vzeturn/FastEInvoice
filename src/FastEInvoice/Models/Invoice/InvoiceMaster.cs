using System.ComponentModel.DataAnnotations;

namespace FastEInvoice.Models.Invoice;

/// <summary>
/// Invoice master (header) information
/// </summary>
public class InvoiceMaster
{
    /// <summary>
    /// Unique key for the invoice (max 32 characters) - Required
    /// This key is unique for each invoice of the enterprise
    /// </summary>
    [Required]
    [MaxLength(32)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Reference number / Internal voucher number
    /// </summary>
    public string? VoucherNumber { get; set; }

    /// <summary>
    /// Invoice date (dd/MM/yyyy format) - Required
    /// </summary>
    [Required]
    public string InvoiceDate { get; set; } = string.Empty;

    /// <summary>
    /// Customer code (max 32 characters, no diacritics)
    /// Must be declared in portal if provided
    /// </summary>
    [MaxLength(32)]
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Customer name (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Buyer name (max 100 characters)
    /// </summary>
    [MaxLength(100)]
    public string? Buyer { get; set; }

    /// <summary>
    /// Customer tax code
    /// </summary>
    public string? CustomerTaxCode { get; set; }

    /// <summary>
    /// Customer address (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? Address { get; set; }

    /// <summary>
    /// Phone number (max 20 characters)
    /// </summary>
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// ID card number (9 or 12 digits)
    /// </summary>
    [MaxLength(12)]
    public string? IDCardNo { get; set; }

    /// <summary>
    /// Passport number (max 20 characters)
    /// </summary>
    [MaxLength(20)]
    public string? PassportNo { get; set; }

    /// <summary>
    /// Budget-related unit code (max 7 characters)
    /// </summary>
    [MaxLength(7)]
    public string? BuyerUnit { get; set; }

    /// <summary>
    /// Payment method code (max 32 characters)
    /// Must be declared in portal
    /// </summary>
    [MaxLength(32)]
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// Bank account number (max 30 characters)
    /// </summary>
    [MaxLength(30)]
    public string? BankAccount { get; set; }

    /// <summary>
    /// Bank name (max 400 characters)
    /// </summary>
    [MaxLength(400)]
    public string? BankName { get; set; }

    /// <summary>
    /// Email address for invoice delivery (max 50 characters)
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
    /// Total goods amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Total discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Total tax amount
    /// </summary>
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// Total payment amount
    /// </summary>
    public decimal TotalAmount { get; set; }

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
    /// Custom string field 4 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External4 { get; set; }

    /// <summary>
    /// Custom string field 5 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External5 { get; set; }

    /// <summary>
    /// Custom string field 6 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External6 { get; set; }

    /// <summary>
    /// Custom string field 7 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External7 { get; set; }

    /// <summary>
    /// Custom string field 8 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External8 { get; set; }

    /// <summary>
    /// Custom string field 9 (max 500 characters)
    /// </summary>
    [MaxLength(500)]
    public string? External9 { get; set; }

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
    /// Invoice type
    /// 1 = New (original), 2 = Adjustment, 3 = Replacement, 9 = Period discount adjustment
    /// Default is 1 if empty
    /// </summary>
    public int? VoucherType { get; set; }

    /// <summary>
    /// Original invoice date for adjustment/replacement (dd/MM/yyyy)
    /// Required for adjustment/replacement invoices
    /// </summary>
    public string? OrgInvoiceDate { get; set; }

    /// <summary>
    /// Original invoice number (max 8 characters)
    /// Required for adjustment/replacement invoices
    /// </summary>
    [MaxLength(8)]
    public string? OrgInvoiceNo { get; set; }

    /// <summary>
    /// Original invoice pattern (max 11 characters)
    /// Required for adjustment/replacement invoices
    /// </summary>
    [MaxLength(11)]
    public string? OrgInvoicePattern { get; set; }

    /// <summary>
    /// Original invoice serial (max 8 characters)
    /// Required for adjustment/replacement invoices
    /// </summary>
    [MaxLength(8)]
    public string? OrgInvoiceSerial { get; set; }

    /// <summary>
    /// Reason for adjustment/replacement (max 255 characters)
    /// </summary>
    [MaxLength(255)]
    public string? Reason { get; set; }

    /// <summary>
    /// Original currency code (max 3 characters)
    /// </summary>
    [MaxLength(3)]
    public string? OrgCurrency { get; set; }

    /// <summary>
    /// Original total amount
    /// </summary>
    public decimal? OrgAmount { get; set; }

    /// <summary>
    /// Meeting/minutes date (dd/MM/yyyy)
    /// </summary>
    public string? DateOfMinute { get; set; }

    /// <summary>
    /// Meeting/minutes number (max 32 characters)
    /// </summary>
    [MaxLength(32)]
    public string? NumberOfMinute { get; set; }
}