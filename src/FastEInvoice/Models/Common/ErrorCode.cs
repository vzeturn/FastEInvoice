namespace FastEInvoice.Models.Common;

/// <summary>
/// API Error codes from FAST E-Invoice system
/// </summary>
public static class ErrorCode
{
    /// <summary>
    /// Error code descriptions
    /// </summary>
    public static readonly Dictionary<int, string> Descriptions = new()
    {
        // Common errors
        { 7800, "Tham số đầu vào rỗng" },
        { 7801, "User không đúng" },
        { 7802, "Chuỗi checksum không hợp lệ" },
        { 101, "User không hợp lệ" },
        { 102, "Mã đơn vị không hợp lệ" },
        { 103, "User chưa được phân quyền truy cập đơn vị cơ sở tương ứng" },
        { 812, "Dữ liệu trường vượt quá giới hạn cho phép" },

        // Invoice specific errors
        { 31100, "Trùng khóa hóa đơn trong dữ liệu" },
        { 31101, "Tồn tại hóa đơn đã có sửa đổi dữ liệu hoặc đã phát hành trên portal" },
        { 78306, "Mã số thuế người mua hàng không đúng quy định" },
        { 78307, "Loại hàng hóa trong chi tiết không đúng quy định portal" },
        { 78308, "Loại hàng hóa không đúng" },
        { 78317, "Trường số CMND người mua không đúng quy định" },
        { 78318, "Mã hình thức thanh toán không hợp lệ" },
        { 78320, "Mã khách hàng không đúng" },
        { 78321, "Mã hàng hóa vật tư không đúng" },
        { 78322, "Tên khách hàng hoặc địa chỉ không được trống" },
        { 78035, "Hóa đơn có 1 mặt hàng tồn tại nhiều mức thuế suất khác nhau" },
        { 78036, "Trong đợt gọi api đang truyền cả hóa đơn gốc lẫn hóa đơn điều chỉnh/thay thế" },
        { 78037, "Hóa đơn không phải là hóa đơn điều chỉnh thì chỉ được phép truyền các trường số là số dương" },
        { 78038, "Hóa đơn chiết khấu kỳ chi tiết chỉ được truyền các mặt hàng có loại 05/08" },
        { 78039, "Loại hóa đơn điều chỉnh/thay thế thì thông tin hóa đơn gốc không được bỏ trống" },
        { 78040, "Nếu có truyền lên mã nt hóa đơn gốc thì mã này phải có trong khai báo danh mục tiền tệ trên portal" },
        { 78041, "Trường lý do điều chỉnh/thay thế rỗng" },
        { 78042, "Số biên bản rỗng" },

        // Delete invoice errors
        { 100, "Các hóa đơn cần xóa thuộc nhiều kỳ khác nhau" },
        { 78901, "Dữ liệu rỗng" },
        { 78902, "Hóa đơn không tồn tại" },
        { 78903, "Trạng thái hóa đơn không hợp lệ, Không được phép xóa" }
    };

    /// <summary>
    /// Get error description by code
    /// </summary>
    /// <param name="code">Error code</param>
    /// <returns>Error description</returns>
    public static string GetDescription(int code)
    {
        return Descriptions.TryGetValue(code, out var description) 
            ? description 
            : $"Unknown error code: {code}";
    }

    /// <summary>
    /// Check if error code indicates success
    /// </summary>
    /// <param name="code">Error code</param>
    /// <returns>True if success</returns>
    public static bool IsSuccess(int code)
    {
        return code == 0;
    }
}