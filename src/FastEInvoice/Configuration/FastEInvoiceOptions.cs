using System.ComponentModel.DataAnnotations;

namespace FastEInvoice.Configuration;

/// <summary>
/// Configuration options for FAST E-Invoice API client
/// </summary>
public class FastEInvoiceOptions
{
    /// <summary>
    /// Configuration section name
    /// </summary>
    public const string SectionName = "FastEInvoice";

    /// <summary>
    /// API base URL - Required
    /// Test: https://tcservice.fast.com.vn
    /// Production: (provided by FAST)
    /// </summary>
    [Required]
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Client code (Mã doanh nghiệp) - Required
    /// Provided by FAST
    /// </summary>
    [Required]
    public string ClientCode { get; set; } = string.Empty;

    /// <summary>
    /// Proxy code (Mã nhóm dịch vụ) - Required
    /// Provided by FAST
    /// </summary>
    [Required]
    public string ProxyCode { get; set; } = string.Empty;

    /// <summary>
    /// Portal username - Required
    /// </summary>
    [Required]
    public string User { get; set; } = string.Empty;

    /// <summary>
    /// Portal password - Required
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Unit code (Mã đơn vị) - Required
    /// </summary>
    [Required]
    public string UnitCode { get; set; } = string.Empty;

    /// <summary>
    /// HTTP request timeout in seconds
    /// Default: 30 seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Enable detailed logging
    /// Default: false
    /// </summary>
    public bool EnableDetailedLogging { get; set; } = false;

    /// <summary>
    /// Validate configuration
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
            throw new ArgumentException("BaseUrl is required", nameof(BaseUrl));

        if (string.IsNullOrWhiteSpace(ClientCode))
            throw new ArgumentException("ClientCode is required", nameof(ClientCode));

        if (string.IsNullOrWhiteSpace(ProxyCode))
            throw new ArgumentException("ProxyCode is required", nameof(ProxyCode));

        if (string.IsNullOrWhiteSpace(User))
            throw new ArgumentException("User is required", nameof(User));

        if (string.IsNullOrWhiteSpace(Password))
            throw new ArgumentException("Password is required", nameof(Password));

        if (string.IsNullOrWhiteSpace(UnitCode))
            throw new ArgumentException("UnitCode is required", nameof(UnitCode));

        if (TimeoutSeconds <= 0)
            throw new ArgumentException("TimeoutSeconds must be greater than 0", nameof(TimeoutSeconds));

        if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _))
            throw new ArgumentException("BaseUrl must be a valid absolute URL", nameof(BaseUrl));
    }
}