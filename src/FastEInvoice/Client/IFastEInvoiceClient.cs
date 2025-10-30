using FastEInvoice.Models.DeliveryNote;
using FastEInvoice.Models.Invoice;
using FastEInvoice.Models.Query;

namespace FastEInvoice.Client;

/// <summary>
/// Interface for FAST E-Invoice API client
/// </summary>
public interface IFastEInvoiceClient
{
    // Invoice operations

    /// <summary>
    /// Push invoice data to portal (Method 311)
    /// </summary>
    /// <param name="master">Invoice master data</param>
    /// <param name="details">Invoice detail data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task PushInvoiceAsync(
        InvoiceMaster master,
        List<InvoiceDetail> details,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Push multiple invoices to portal (Method 311)
    /// </summary>
    /// <param name="invoices">List of invoices with master and details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task PushInvoicesAsync(
        List<(InvoiceMaster Master, List<InvoiceDetail> Details)> invoices,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query invoice status (Method 371)
    /// </summary>
    /// <param name="key">Invoice key</param>
    /// <param name="invoiceDate">Invoice date (dd/MM/yyyy)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Invoice information</returns>
    Task<InvoiceInfo?> QueryInvoiceAsync(
        string key,
        string invoiceDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query multiple invoices status (Method 371)
    /// </summary>
    /// <param name="queries">List of invoice keys and dates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of invoice information</returns>
    Task<List<InvoiceInfo>> QueryInvoicesAsync(
        List<(string Key, string InvoiceDate)> queries,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete draft invoice (Method 361)
    /// </summary>
    /// <param name="key">Invoice key</param>
    /// <param name="invoiceDate">Invoice date (dd/MM/yyyy)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteInvoiceAsync(
        string key,
        string invoiceDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete multiple draft invoices (Method 361)
    /// </summary>
    /// <param name="invoices">List of invoice keys and dates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteInvoicesAsync(
        List<(string Key, string InvoiceDate)> invoices,
        CancellationToken cancellationToken = default);

    // Delivery Note operations

    /// <summary>
    /// Push delivery note data to portal (Method 318)
    /// </summary>
    /// <param name="master">Delivery note master data</param>
    /// <param name="details">Delivery note detail data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task PushDeliveryNoteAsync(
        DeliveryNoteMaster master,
        List<DeliveryNoteDetail> details,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Push multiple delivery notes to portal (Method 318)
    /// </summary>
    /// <param name="deliveryNotes">List of delivery notes with master and details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task PushDeliveryNotesAsync(
        List<(DeliveryNoteMaster Master, List<DeliveryNoteDetail> Details)> deliveryNotes,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query delivery note status (Method 378)
    /// </summary>
    /// <param name="key">Delivery note key</param>
    /// <param name="deliveryNoteDate">Delivery note date (dd/MM/yyyy)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Delivery note information</returns>
    Task<InvoiceInfo?> QueryDeliveryNoteAsync(
        string key,
        string deliveryNoteDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query multiple delivery notes status (Method 378)
    /// </summary>
    /// <param name="queries">List of delivery note keys and dates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of delivery note information</returns>
    Task<List<InvoiceInfo>> QueryDeliveryNotesAsync(
        List<(string Key, string DeliveryNoteDate)> queries,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete draft delivery note (Method 368)
    /// </summary>
    /// <param name="key">Delivery note key</param>
    /// <param name="deliveryNoteDate">Delivery note date (dd/MM/yyyy)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteDeliveryNoteAsync(
        string key,
        string deliveryNoteDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete multiple draft delivery notes (Method 368)
    /// </summary>
    /// <param name="deliveryNotes">List of delivery note keys and dates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteDeliveryNotesAsync(
        List<(string Key, string DeliveryNoteDate)> deliveryNotes,
        CancellationToken cancellationToken = default);
}