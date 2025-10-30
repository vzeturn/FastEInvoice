namespace FastEInvoice.Constants;

/// <summary>
/// Constants for FAST E-Invoice API
/// </summary>
public static class ApiConstants
{
    /// <summary>
    /// API Methods
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Push invoice data to portal (Method 311)
        /// </summary>
        public const string PushInvoice = "311";

        /// <summary>
        /// Push delivery note data to portal (Method 318)
        /// </summary>
        public const string PushDeliveryNote = "318";

        /// <summary>
        /// Query invoice status (Method 371)
        /// </summary>
        public const string QueryInvoice = "371";

        /// <summary>
        /// Query delivery note status (Method 378)
        /// </summary>
        public const string QueryDeliveryNote = "378";

        /// <summary>
        /// Delete draft invoice (Method 361)
        /// </summary>
        public const string DeleteInvoice = "361";

        /// <summary>
        /// Delete draft delivery note (Method 368)
        /// </summary>
        public const string DeleteDeliveryNote = "368";
    }

    /// <summary>
    /// API Action values
    /// </summary>
    public static class Actions
    {
        public const string Execute = "0";
    }

    /// <summary>
    /// HTTP Headers
    /// </summary>
    public static class Headers
    {
        public const string ContentType = "Content-Type";
        public const string User = "user";
        public const string UnitCode = "unitCode";
        public const string CheckSum = "checkSum";
    }

    /// <summary>
    /// Content Types
    /// </summary>
    public static class ContentTypes
    {
        public const string TextPlain = "text/plain";
    }

    /// <summary>
    /// Query Parameters
    /// </summary>
    public static class QueryParams
    {
        public const string Action = "action";
        public const string Method = "method";
        public const string ClientCode = "clientCode";
        public const string ProxyCode = "proxyCode";
    }

    /// <summary>
    /// Date format
    /// </summary>
    public const string DateFormat = "dd/MM/yyyy";

    /// <summary>
    /// API Endpoint path
    /// </summary>
    public const string ApiPath = "/api/Command/ExecuteCommand";

    /// <summary>
    /// Invoice types
    /// </summary>
    public static class VoucherTypes
    {
        /// <summary>
        /// New invoice (original)
        /// </summary>
        public const int New = 1;

        /// <summary>
        /// Adjustment invoice
        /// </summary>
        public const int Adjustment = 2;

        /// <summary>
        /// Replacement invoice
        /// </summary>
        public const int Replacement = 3;

        /// <summary>
        /// Period discount adjustment
        /// </summary>
        public const int PeriodDiscount = 9;
    }

    /// <summary>
    /// Item types (Product/Service types)
    /// </summary>
    public static class ItemTypes
    {
        /// <summary>
        /// Goods and services
        /// </summary>
        public const string GoodsAndServices = "01";

        /// <summary>
        /// Promotional items (non-taxable)
        /// </summary>
        public const string PromotionalNonTax = "02";

        /// <summary>
        /// Promotional items (taxable)
        /// </summary>
        public const string PromotionalTax = "03";

        /// <summary>
        /// Gifts/donations
        /// </summary>
        public const string Gift = "04";

        /// <summary>
        /// Discount
        /// </summary>
        public const string Discount = "05";

        /// <summary>
        /// Note/Comment
        /// </summary>
        public const string Note = "08";

        /// <summary>
        /// Special goods - Vehicles (car, motorcycle)
        /// </summary>
        public const string SpecialVehicle = "51";

        /// <summary>
        /// Special goods - Transportation service
        /// </summary>
        public const string SpecialTransport = "52";

        /// <summary>
        /// Special goods - Digital platform transportation/E-commerce
        /// </summary>
        public const string SpecialDigitalTransport = "53";
    }

    /// <summary>
    /// Tax rates
    /// </summary>
    public static class TaxRates
    {
        public const decimal Rate0 = 0m;
        public const decimal Rate5 = 5m;
        public const decimal Rate8 = 8m;
        public const decimal Rate10 = 10m;
        public const decimal NotSubject = -1m; // KCT: Không chịu thuế
        public const decimal NotDeclared = -2m; // KKKNT: Không kê khai kê khai thuế
        public const decimal Empty = -9m; // Only for note items
    }

    /// <summary>
    /// Delivery note types
    /// </summary>
    public static class DeliveryNoteTypes
    {
        /// <summary>
        /// Internal delivery note
        /// </summary>
        public const int Internal = 1;

        /// <summary>
        /// Consignment delivery note
        /// </summary>
        public const int Consignment = 2;
    }
}