# FAST E-Invoice Client Library

[![NuGet](https://img.shields.io/nuget/v/FastEInvoice.svg)](https://www.nuget.org/packages/FastEInvoice/)
[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A modern, type-safe .NET 9.0 client library for integrating with FAST E-Invoice API (Vietnam electronic invoice system).

## Features

✨ **Full API Coverage**
- Push invoices to portal (Method 311)
- Push delivery notes to portal (Method 318)
- Query invoice/delivery note status (Method 371/378)
- Delete draft invoices/delivery notes (Method 361/368)

🚀 **Modern .NET 9.0**
- Built with latest C# features
- Async/await throughout
- Nullable reference types
- Dependency injection ready

📦 **Developer Friendly**
- Strongly typed models
- Comprehensive XML documentation
- Fluent API design
- Built-in logging support

🔒 **Production Ready**
- Automatic MD5 checksum generation
- Proper error handling with custom exceptions
- HTTP retry policies support
- Configuration validation

## Installation

### Via .NET CLI
```bash
dotnet add package FastEInvoice
```

### Via Package Manager
```powershell
Install-Package FastEInvoice
```

### Via PackageReference
```xml
<PackageReference Include="FastEInvoice" Version="1.0.0" />
```

## Quick Start

### 1. Configuration

Add to your `appsettings.json`:

```json
{
  "FastEInvoice": {
    "BaseUrl": "https://tcservice.fast.com.vn",
    "ClientCode": "YOUR_CLIENT_CODE",
    "ProxyCode": "YOUR_PROXY_CODE",
    "User": "YOUR_USERNAME",
    "Password": "YOUR_PASSWORD",
    "UnitCode": "YOUR_UNIT_CODE",
    "TimeoutSeconds": 30,
    "EnableDetailedLogging": false
  }
}
```

### 2. Register Services

In your `Program.cs`:

```csharp
using FastEInvoice.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register FAST E-Invoice client
builder.Services.AddFastEInvoice(builder.Configuration);

var app = builder.Build();
app.Run();
```

### 3. Use the Client

```csharp
using FastEInvoice.Client;
using FastEInvoice.Models.Invoice;

public class InvoiceService
{
    private readonly IFastEInvoiceClient _client;

    public InvoiceService(IFastEInvoiceClient client)
    {
        _client = client;
    }

    public async Task CreateInvoiceAsync()
    {
        // Create invoice master (header)
        var master = new InvoiceMaster
        {
            Key = "INV001",
            InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy"),
            CustomerName = "Company ABC",
            CustomerTaxCode = "0123456789",
            Address = "123 Street, City",
            Amount = 1000000,
            TaxAmount = 100000,
            TotalAmount = 1100000
        };

        // Create invoice details (line items)
        var details = new List<InvoiceDetail>
        {
            new()
            {
                ItemName = "Product A",
                ItemType = "01",
                UOM = "Piece",
                Quantity = 10,
                Price = 100000,
                Amount = 1000000,
                TaxRate = 10,
                TaxAmount = 100000
            }
        };

        // Push to portal
        await _client.PushInvoiceAsync(master, details);
        
        Console.WriteLine("Invoice pushed successfully!");
    }
}
```

## Usage Examples

### Push Invoice

```csharp
var master = new InvoiceMaster
{
    Key = "INV20250101001",
    InvoiceDate = "01/01/2025",
    CustomerName = "ABC Company Ltd",
    CustomerTaxCode = "0123456789",
    Address = "123 Main St, Hanoi",
    PhoneNumber = "0912345678",
    EmailDeliver = "customer@example.com",
    PaymentMethod = "TM", // Cash
    Currency = "VND",
    ExchangeRate = 1,
    Amount = 10000000,
    DiscountAmount = 0,
    TaxAmount = 1000000,
    TotalAmount = 11000000
};

var details = new List<InvoiceDetail>
{
    new()
    {
        ItemName = "Laptop Dell XPS 13",
        ItemType = "01", // Goods/Services
        UOM = "Piece",
        Quantity = 1,
        Price = 10000000,
        Amount = 10000000,
        TaxRate = 10,
        TaxAmount = 1000000
    }
};

await _client.PushInvoiceAsync(master, details);
```

### Push Multiple Invoices

```csharp
var invoices = new List<(InvoiceMaster Master, List<InvoiceDetail> Details)>
{
    (invoice1Master, invoice1Details),
    (invoice2Master, invoice2Details),
    (invoice3Master, invoice3Details)
};

await _client.PushInvoicesAsync(invoices);
```

### Query Invoice Status

```csharp
var invoiceInfo = await _client.QueryInvoiceAsync(
    key: "INV20250101001",
    invoiceDate: "01/01/2025"
);

if (invoiceInfo != null)
{
    Console.WriteLine($"Invoice No: {invoiceInfo.InvoiceNo}");
    Console.WriteLine($"Status: {invoiceInfo.StatusDescription}");
    Console.WriteLine($"Pattern: {invoiceInfo.Pattern}");
    Console.WriteLine($"Serial: {invoiceInfo.Serial}");
    Console.WriteLine($"Lookup Code: {invoiceInfo.KeySearch}");
}
```

### Query Multiple Invoices

```csharp
var queries = new List<(string Key, string InvoiceDate)>
{
    ("INV001", "01/01/2025"),
    ("INV002", "02/01/2025"),
    ("INV003", "03/01/2025")
};

var invoices = await _client.QueryInvoicesAsync(queries);

foreach (var invoice in invoices)
{
    Console.WriteLine($"{invoice.Key}: {invoice.StatusDescription}");
}
```

### Delete Draft Invoice

```csharp
await _client.DeleteInvoiceAsync(
    key: "INV20250101001",
    invoiceDate: "01/01/2025"
);
```

### Push Delivery Note

```csharp
var master = new DeliveryNoteMaster
{
    Key = "DN20250101001",
    VoucherType = 1, // Internal
    InvoiceDate = "01/01/2025",
    Receiver = "John Doe",
    TaxCode = "0123456789",
    IssuingSite = "Warehouse A",
    ReceivingSite = "Warehouse B",
    Transportation = "Truck",
    Currency = "VND",
    ExchangeRate = 1,
    Amount = 50000000
};

var details = new List<DeliveryNoteDetail>
{
    new()
    {
        ItemName = "Monitor LG 27inch",
        ItemType = "01",
        UOM = "Piece",
        Quantity = 20,
        Price = 2500000,
        Amount = 50000000
    }
};

await _client.PushDeliveryNoteAsync(master, details);
```

### Adjustment Invoice

```csharp
var adjustmentMaster = new InvoiceMaster
{
    Key = "INVADJ20250101001",
    VoucherType = 2, // Adjustment
    InvoiceDate = "15/01/2025",
    
    // Original invoice info
    OrgInvoiceDate = "01/01/2025",
    OrgInvoiceNo = "00000123",
    OrgInvoicePattern = "1",
    OrgInvoiceSerial = "C24TAA",
    Reason = "Price adjustment",
    NumberOfMinute = "BB2025/01/0001",
    
    // Other fields...
    CustomerName = "ABC Company Ltd",
    Amount = 11000000, // Adjusted amount
    TaxAmount = 1100000,
    TotalAmount = 12100000
};

await _client.PushInvoiceAsync(adjustmentMaster, details);
```

## Configuration Options

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `BaseUrl` | string | Yes | API base URL (test: https://tcservice.fast.com.vn) |
| `ClientCode` | string | Yes | Client code provided by FAST |
| `ProxyCode` | string | Yes | Proxy code provided by FAST |
| `User` | string | Yes | Portal username |
| `Password` | string | Yes | Portal password |
| `UnitCode` | string | Yes | Unit code on portal |
| `TimeoutSeconds` | int | No | HTTP timeout in seconds (default: 30) |
| `EnableDetailedLogging` | bool | No | Enable detailed logging (default: false) |

## Constants and Enums

### Invoice Types (VoucherType)
- `1` - New invoice (original)
- `2` - Adjustment invoice
- `3` - Replacement invoice
- `9` - Period discount adjustment

### Item Types
- `01` - Goods and services
- `02` - Promotional items (non-taxable)
- `03` - Promotional items (taxable)
- `04` - Gifts/donations
- `05` - Discount
- `08` - Note/Comment
- `51` - Special goods - Vehicles
- `52` - Special goods - Transportation service
- `53` - Special goods - Digital platform transportation

### Tax Rates
- `0` - 0%
- `5` - 5%
- `8` - 8%
- `10` - 10%
- `-1` - KCT (Not subject to tax)
- `-2` - KKKNT (Not declared for tax)
- `-9` - Empty (only for note items)

### Invoice Status
- `1` - Waiting for authentication
- `2` - Authenticated
- `8` - Cancelled

## Error Handling

The library throws `FastEInvoiceException` for API errors:

```csharp
try
{
    await _client.PushInvoiceAsync(master, details);
}
catch (FastEInvoiceException ex)
{
    Console.WriteLine($"Error Code: {ex.ErrorCode}");
    Console.WriteLine($"Error Message: {ex.Message}");
    Console.WriteLine($"Raw Message: {ex.RawMessage}");
    
    // Handle specific error codes
    if (ex.ErrorCode == 31101)
    {
        // Invoice already modified or published
        Console.WriteLine("Cannot overwrite: Invoice was modified on portal");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

### Common Error Codes

| Code | Description |
|------|-------------|
| 7800 | Empty input parameters |
| 7801 | Invalid user |
| 7802 | Invalid checksum |
| 31100 | Duplicate invoice key |
| 31101 | Invoice already modified or published |
| 78307 | Invalid item type |
| 78320 | Invalid customer code |
| 78321 | Invalid item code |

[See full error code list](src/FastEInvoice/Models/Common/ErrorCode.cs)

## Advanced Configuration

### Custom HttpClient Configuration

```csharp
builder.Services.AddFastEInvoice(
    builder.Configuration,
    httpClient =>
    {
        httpClient.Timeout = TimeSpan.FromSeconds(60);
        httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "value");
    }
);
```

### Programmatic Configuration

```csharp
builder.Services.AddFastEInvoice(options =>
{
    options.BaseUrl = "https://tcservice.fast.com.vn";
    options.ClientCode = "YOUR_CLIENT_CODE";
    options.ProxyCode = "YOUR_PROXY_CODE";
    options.User = "username";
    options.Password = "password";
    options.UnitCode = "UNIT001";
    options.TimeoutSeconds = 45;
    options.EnableDetailedLogging = true;
});
```

### HTTP Retry Policies with Polly

```csharp
builder.Services.AddHttpClient<IFastEInvoiceClient, FastEInvoiceClient>()
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        )
    );
```

## Testing

See the [samples project](samples/FastEInvoice.Samples) for complete examples.

Run unit tests:
```bash
dotnet test
```

## Documentation

- [API Documentation](https://docs.fast.com.vn) - Official FAST E-Invoice API docs
- [Vietnam E-Invoice Portal](https://tportal.fast.com.vn/) - Test portal

## Requirements

- .NET 9.0 or later
- FAST E-Invoice account (contact FAST for registration)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/fast-einvoice/issues)
- **Email**: support@yourcompany.com
- **FAST Support**: https://support.fast.com.vn

## Changelog

### Version 1.0.0 (2025-01-10)
- Initial release
- Full API support for invoices and delivery notes
- .NET 9.0 implementation
- Comprehensive documentation

## Acknowledgments

- FAST Software Management Company for the E-Invoice API
- Contributors and testers

---

Made with ❤️ for the Vietnamese developer community