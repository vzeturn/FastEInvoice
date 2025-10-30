using System.Text;
using System.Text.Json;
using FastEInvoice.Configuration;
using FastEInvoice.Constants;
using FastEInvoice.Exceptions;
using FastEInvoice.Models.Common;
using FastEInvoice.Models.DeliveryNote;
using FastEInvoice.Models.Invoice;
using FastEInvoice.Models.Query;
using FastEInvoice.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FastEInvoice.Client;

/// <summary>
/// FAST E-Invoice API client implementation
/// </summary>
public class FastEInvoiceClient : IFastEInvoiceClient
{
    private readonly HttpClient _httpClient;
    private readonly FastEInvoiceOptions _options;
    private readonly ILogger<FastEInvoiceClient> _logger;
    private readonly string _checkSum;

    /// <summary>
    /// Initializes a new instance of FastEInvoiceClient
    /// </summary>
    public FastEInvoiceClient(
        HttpClient httpClient,
        IOptions<FastEInvoiceOptions> options,
        ILogger<FastEInvoiceClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Validate configuration
        _options.Validate();

        // Configure HttpClient
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);

        // Generate checksum once during initialization
        _checkSum = MD5Helper.GenerateCheckSum(_options.Password, _options.ClientCode);

        _logger.LogInformation("FastEInvoiceClient initialized for {ClientCode}", _options.ClientCode);
    }

    #region Invoice Operations

    /// <inheritdoc/>
    public async Task PushInvoiceAsync(
        InvoiceMaster master,
        List<InvoiceDetail> details,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(master);
        ArgumentNullException.ThrowIfNull(details);

        _logger.LogInformation("Pushing invoice with key: {Key}", master.Key);

        var request = JsonHelper.ConvertToInvoiceRequest(master, details);
        await ExecuteCommandAsync(ApiConstants.Methods.PushInvoice, request, cancellationToken);

        _logger.LogInformation("Successfully pushed invoice with key: {Key}", master.Key);
    }

    /// <inheritdoc/>
    public async Task PushInvoicesAsync(
        List<(InvoiceMaster Master, List<InvoiceDetail> Details)> invoices,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(invoices);

        if (invoices.Count == 0)
            throw new ArgumentException("Invoices list cannot be empty", nameof(invoices));

        _logger.LogInformation("Pushing {Count} invoices", invoices.Count);

        var request = JsonHelper.ConvertToInvoiceRequest(invoices);
        await ExecuteCommandAsync(ApiConstants.Methods.PushInvoice, request, cancellationToken);

        _logger.LogInformation("Successfully pushed {Count} invoices", invoices.Count);
    }

    /// <inheritdoc/>
    public async Task<InvoiceInfo?> QueryInvoiceAsync(
        string key,
        string invoiceDate,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(invoiceDate);

        _logger.LogInformation("Querying invoice with key: {Key}", key);

        var invoices = await QueryInvoicesAsync(
            new List<(string Key, string InvoiceDate)> { (key, invoiceDate) },
            cancellationToken);

        return invoices.FirstOrDefault();
    }

    /// <inheritdoc/>
    public async Task<List<InvoiceInfo>> QueryInvoicesAsync(
        List<(string Key, string InvoiceDate)> queries,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queries);

        if (queries.Count == 0)
            throw new ArgumentException("Queries list cannot be empty", nameof(queries));

        _logger.LogInformation("Querying {Count} invoices", queries.Count);

        var request = new QueryRequest
        {
            Data = queries.Select(q => new QueryItem
            {
                Key = q.Key,
                InvoiceDate = q.InvoiceDate
            }).ToList()
        };

        var response = await ExecuteCommandAsync(
            ApiConstants.Methods.QueryInvoice,
            request,
            cancellationToken);

        // Parse the Message field which contains JSON array of invoice info
        if (string.IsNullOrWhiteSpace(response.Message))
        {
            _logger.LogWarning("Query returned empty message");
            return new List<InvoiceInfo>();
        }

        try
        {
            var invoices = JsonSerializer.Deserialize<List<InvoiceInfo>>(response.Message);
            _logger.LogInformation("Successfully queried {Count} invoices", invoices?.Count ?? 0);
            return invoices ?? new List<InvoiceInfo>();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse query response message");
            throw new FastEInvoiceException("Failed to parse query response", ex);
        }
    }

    /// <inheritdoc/>
    public async Task DeleteInvoiceAsync(
        string key,
        string invoiceDate,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(invoiceDate);

        _logger.LogInformation("Deleting invoice with key: {Key}", key);

        await DeleteInvoicesAsync(
            new List<(string Key, string InvoiceDate)> { (key, invoiceDate) },
            cancellationToken);

        _logger.LogInformation("Successfully deleted invoice with key: {Key}", key);
    }

    /// <inheritdoc/>
    public async Task DeleteInvoicesAsync(
        List<(string Key, string InvoiceDate)> invoices,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(invoices);

        if (invoices.Count == 0)
            throw new ArgumentException("Invoices list cannot be empty", nameof(invoices));

        _logger.LogInformation("Deleting {Count} invoices", invoices.Count);

        var request = new QueryRequest
        {
            Data = invoices.Select(inv => new QueryItem
            {
                Key = inv.Key,
                InvoiceDate = inv.InvoiceDate
            }).ToList()
        };

        await ExecuteCommandAsync(ApiConstants.Methods.DeleteInvoice, request, cancellationToken);

        _logger.LogInformation("Successfully deleted {Count} invoices", invoices.Count);
    }

    #endregion

    #region Delivery Note Operations

    /// <inheritdoc/>
    public async Task PushDeliveryNoteAsync(
        DeliveryNoteMaster master,
        List<DeliveryNoteDetail> details,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(master);
        ArgumentNullException.ThrowIfNull(details);

        _logger.LogInformation("Pushing delivery note with key: {Key}", master.Key);

        var request = JsonHelper.ConvertToDeliveryNoteRequest(master, details);
        await ExecuteCommandAsync(ApiConstants.Methods.PushDeliveryNote, request, cancellationToken);

        _logger.LogInformation("Successfully pushed delivery note with key: {Key}", master.Key);
    }

    /// <inheritdoc/>
    public async Task PushDeliveryNotesAsync(
        List<(DeliveryNoteMaster Master, List<DeliveryNoteDetail> Details)> deliveryNotes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(deliveryNotes);

        if (deliveryNotes.Count == 0)
            throw new ArgumentException("Delivery notes list cannot be empty", nameof(deliveryNotes));

        _logger.LogInformation("Pushing {Count} delivery notes", deliveryNotes.Count);

        var request = JsonHelper.ConvertToDeliveryNoteRequest(deliveryNotes);
        await ExecuteCommandAsync(ApiConstants.Methods.PushDeliveryNote, request, cancellationToken);

        _logger.LogInformation("Successfully pushed {Count} delivery notes", deliveryNotes.Count);
    }

    /// <inheritdoc/>
    public async Task<InvoiceInfo?> QueryDeliveryNoteAsync(
        string key,
        string deliveryNoteDate,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(deliveryNoteDate);

        _logger.LogInformation("Querying delivery note with key: {Key}", key);

        var notes = await QueryDeliveryNotesAsync(
            new List<(string Key, string DeliveryNoteDate)> { (key, deliveryNoteDate) },
            cancellationToken);

        return notes.FirstOrDefault();
    }

    /// <inheritdoc/>
    public async Task<List<InvoiceInfo>> QueryDeliveryNotesAsync(
        List<(string Key, string DeliveryNoteDate)> queries,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queries);

        if (queries.Count == 0)
            throw new ArgumentException("Queries list cannot be empty", nameof(queries));

        _logger.LogInformation("Querying {Count} delivery notes", queries.Count);

        var request = new QueryRequest
        {
            Data = queries.Select(q => new QueryItem
            {
                Key = q.Key,
                InvoiceDate = q.DeliveryNoteDate
            }).ToList()
        };

        var response = await ExecuteCommandAsync(
            ApiConstants.Methods.QueryDeliveryNote,
            request,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(response.Message))
        {
            _logger.LogWarning("Query returned empty message");
            return new List<InvoiceInfo>();
        }

        try
        {
            var notes = JsonSerializer.Deserialize<List<InvoiceInfo>>(response.Message);
            _logger.LogInformation("Successfully queried {Count} delivery notes", notes?.Count ?? 0);
            return notes ?? new List<InvoiceInfo>();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse query response message");
            throw new FastEInvoiceException("Failed to parse query response", ex);
        }
    }

    /// <inheritdoc/>
    public async Task DeleteDeliveryNoteAsync(
        string key,
        string deliveryNoteDate,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(deliveryNoteDate);

        _logger.LogInformation("Deleting delivery note with key: {Key}", key);

        await DeleteDeliveryNotesAsync(
            new List<(string Key, string DeliveryNoteDate)> { (key, deliveryNoteDate) },
            cancellationToken);

        _logger.LogInformation("Successfully deleted delivery note with key: {Key}", key);
    }

    /// <inheritdoc/>
    public async Task DeleteDeliveryNotesAsync(
        List<(string Key, string DeliveryNoteDate)> deliveryNotes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(deliveryNotes);

        if (deliveryNotes.Count == 0)
            throw new ArgumentException("Delivery notes list cannot be empty", nameof(deliveryNotes));

        _logger.LogInformation("Deleting {Count} delivery notes", deliveryNotes.Count);

        var request = new QueryRequest
        {
            Data = deliveryNotes.Select(note => new QueryItem
            {
                Key = note.Key,
                InvoiceDate = note.DeliveryNoteDate
            }).ToList()
        };

        await ExecuteCommandAsync(ApiConstants.Methods.DeleteDeliveryNote, request, cancellationToken);

        _logger.LogInformation("Successfully deleted {Count} delivery notes", deliveryNotes.Count);
    }

    #endregion

    #region Private Helper Methods

    private async Task<ApiResponse> ExecuteCommandAsync<TRequest>(
        string method,
        TRequest request,
        CancellationToken cancellationToken)
    {
        var url = BuildApiUrl(method);
        var requestJson = JsonHelper.Serialize(request);

        if (_options.EnableDetailedLogging)
        {
            _logger.LogDebug("API Request - Method: {Method}, URL: {Url}, Body: {Body}",
                method, url, requestJson);
        }

        var httpRequest = CreateHttpRequest(url, requestJson);

        try
        {
            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
            var responseJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (_options.EnableDetailedLogging)
            {
                _logger.LogDebug("API Response - Status: {StatusCode}, Body: {Body}",
                    httpResponse.StatusCode, responseJson);
            }

            httpResponse.EnsureSuccessStatusCode();

            var apiResponse = JsonHelper.Deserialize<ApiResponse>(responseJson);

            if (apiResponse == null)
            {
                throw new FastEInvoiceException("Failed to deserialize API response");
            }

            if (!apiResponse.IsSuccess)
            {
                _logger.LogError("API returned error - Code: {Code}, Message: {Message}",
                    apiResponse.Code, apiResponse.Message);
                throw FastEInvoiceException.FromApiResponse(apiResponse);
            }

            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed");
            throw new FastEInvoiceException("HTTP request failed", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Request timeout");
            throw new FastEInvoiceException("Request timeout", ex);
        }
    }

    private string BuildApiUrl(string method)
    {
        var queryParams = new Dictionary<string, string>
        {
            [ApiConstants.QueryParams.Action] = ApiConstants.Actions.Execute,
            [ApiConstants.QueryParams.Method] = method,
            [ApiConstants.QueryParams.ClientCode] = _options.ClientCode,
            [ApiConstants.QueryParams.ProxyCode] = _options.ProxyCode
        };

        var queryString = string.Join("&",
            queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        return $"{ApiConstants.ApiPath}?{queryString}";
    }

    private HttpRequestMessage CreateHttpRequest(string url, string body)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        // Set headers
        request.Headers.Add(ApiConstants.Headers.User, _options.User);
        request.Headers.Add(ApiConstants.Headers.UnitCode, _options.UnitCode);
        request.Headers.Add(ApiConstants.Headers.CheckSum, _checkSum);

        // Set content
        request.Content = new StringContent(
            body,
            Encoding.UTF8,
            ApiConstants.ContentTypes.TextPlain);

        return request;
    }

    #endregion
}