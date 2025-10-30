using FastEInvoice.Client;
using FastEInvoice.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastEInvoice.Extensions;

/// <summary>
/// Extension methods for IServiceCollection to register FAST E-Invoice services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add FAST E-Invoice client to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="sectionName">Configuration section name (default: "FastEInvoice")</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddFastEInvoice(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = FastEInvoiceOptions.SectionName)
    {
        // Bind configuration
        services.Configure<FastEInvoiceOptions>(
            configuration.GetSection(sectionName));

        // Register HttpClient with typed client
        services.AddHttpClient<IFastEInvoiceClient, FastEInvoiceClient>();

        return services;
    }

    /// <summary>
    /// Add FAST E-Invoice client to the service collection with action configuration
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configureOptions">Action to configure options</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddFastEInvoice(
        this IServiceCollection services,
        Action<FastEInvoiceOptions> configureOptions)
    {
        // Configure options
        services.Configure(configureOptions);

        // Register HttpClient with typed client
        services.AddHttpClient<IFastEInvoiceClient, FastEInvoiceClient>();

        return services;
    }

    /// <summary>
    /// Add FAST E-Invoice client to the service collection with custom HttpClient configuration
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="configureHttpClient">Action to configure HttpClient</param>
    /// <param name="sectionName">Configuration section name (default: "FastEInvoice")</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddFastEInvoice(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient> configureHttpClient,
        string sectionName = FastEInvoiceOptions.SectionName)
    {
        // Bind configuration
        services.Configure<FastEInvoiceOptions>(
            configuration.GetSection(sectionName));

        // Register HttpClient with typed client and custom configuration
        services.AddHttpClient<IFastEInvoiceClient, FastEInvoiceClient>(configureHttpClient);

        return services;
    }
}