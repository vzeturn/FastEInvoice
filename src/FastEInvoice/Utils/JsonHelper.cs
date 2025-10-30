using System.Reflection;
using System.Text.Json;
using FastEInvoice.Models.DeliveryNote;
using FastEInvoice.Models.Invoice;

namespace FastEInvoice.Utils;

/// <summary>
/// JSON helper for converting objects to API format
/// </summary>
public static class JsonHelper
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Serialize object to JSON string
    /// </summary>
    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, DefaultOptions);
    }

    /// <summary>
    /// Deserialize JSON string to object
    /// </summary>
    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }

    /// <summary>
    /// Convert invoice master and details to API request format
    /// </summary>
    public static InvoiceRequest ConvertToInvoiceRequest(
        InvoiceMaster master,
        List<InvoiceDetail> details)
    {
        var masterProperties = GetPropertyNames<InvoiceMaster>();
        var detailProperties = GetPropertyNames<InvoiceDetail>();

        var masterValues = GetPropertyValues(master, masterProperties);
        var detailValues = details.Select(d => GetPropertyValues(d, detailProperties)).ToList();

        return new InvoiceRequest
        {
            Data = new InvoiceData
            {
                Structure = new InvoiceStructure
                {
                    Master = masterProperties,
                    Detail = detailProperties
                },
                Invoices = new List<InvoiceDataItem>
                {
                    new()
                    {
                        Master = masterValues,
                        Detail = detailValues
                    }
                }
            }
        };
    }

    /// <summary>
    /// Convert multiple invoices to API request format
    /// </summary>
    public static InvoiceRequest ConvertToInvoiceRequest(
        List<(InvoiceMaster Master, List<InvoiceDetail> Details)> invoices)
    {
        if (invoices == null || invoices.Count == 0)
            throw new ArgumentException("Invoices list cannot be null or empty", nameof(invoices));

        var masterProperties = GetPropertyNames<InvoiceMaster>();
        var detailProperties = GetPropertyNames<InvoiceDetail>();

        var invoiceItems = invoices.Select(inv => new InvoiceDataItem
        {
            Master = GetPropertyValues(inv.Master, masterProperties),
            Detail = inv.Details.Select(d => GetPropertyValues(d, detailProperties)).ToList()
        }).ToList();

        return new InvoiceRequest
        {
            Data = new InvoiceData
            {
                Structure = new InvoiceStructure
                {
                    Master = masterProperties,
                    Detail = detailProperties
                },
                Invoices = invoiceItems
            }
        };
    }

    /// <summary>
    /// Convert delivery note master and details to API request format
    /// </summary>
    public static DeliveryNoteRequest ConvertToDeliveryNoteRequest(
        DeliveryNoteMaster master,
        List<DeliveryNoteDetail> details)
    {
        var masterProperties = GetPropertyNames<DeliveryNoteMaster>();
        var detailProperties = GetPropertyNames<DeliveryNoteDetail>();

        var masterValues = GetPropertyValues(master, masterProperties);
        var detailValues = details.Select(d => GetPropertyValues(d, detailProperties)).ToList();

        return new DeliveryNoteRequest
        {
            Data = new DeliveryNoteData
            {
                Structure = new DeliveryNoteStructure
                {
                    Master = masterProperties,
                    Detail = detailProperties
                },
                Invoices = new List<DeliveryNoteDataItem>
                {
                    new()
                    {
                        Master = masterValues,
                        Detail = detailValues
                    }
                }
            }
        };
    }

    /// <summary>
    /// Convert multiple delivery notes to API request format
    /// </summary>
    public static DeliveryNoteRequest ConvertToDeliveryNoteRequest(
        List<(DeliveryNoteMaster Master, List<DeliveryNoteDetail> Details)> deliveryNotes)
    {
        if (deliveryNotes == null || deliveryNotes.Count == 0)
            throw new ArgumentException("Delivery notes list cannot be null or empty", nameof(deliveryNotes));

        var masterProperties = GetPropertyNames<DeliveryNoteMaster>();
        var detailProperties = GetPropertyNames<DeliveryNoteDetail>();

        var noteItems = deliveryNotes.Select(note => new DeliveryNoteDataItem
        {
            Master = GetPropertyValues(note.Master, masterProperties),
            Detail = note.Details.Select(d => GetPropertyValues(d, detailProperties)).ToList()
        }).ToList();

        return new DeliveryNoteRequest
        {
            Data = new DeliveryNoteData
            {
                Structure = new DeliveryNoteStructure
                {
                    Master = masterProperties,
                    Detail = detailProperties
                },
                Invoices = noteItems
            }
        };
    }

    private static List<string> GetPropertyNames<T>()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .Select(p => p.Name)
            .ToList();
    }

    private static List<object?> GetPropertyValues<T>(T obj, List<string> propertyNames)
    {
        var type = typeof(T);
        return propertyNames
            .Select(name => type.GetProperty(name)?.GetValue(obj))
            .ToList();
    }
}