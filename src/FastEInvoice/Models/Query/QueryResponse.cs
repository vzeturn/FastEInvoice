using FastEInvoice.Models.Invoice;

namespace FastEInvoice.Models.Query;

/// <summary>
/// Query response containing list of invoice information
/// </summary>
public class QueryResponse
{
    /// <summary>
    /// List of invoice information
    /// </summary>
    public List<InvoiceInfo> Invoices { get; set; } = new();
}