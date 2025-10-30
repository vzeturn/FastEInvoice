using System.Security.Cryptography;
using System.Text;

namespace FastEInvoice.Utils;

/// <summary>
/// MD5 hash helper for generating checksum
/// </summary>
public static class MD5Helper
{
    /// <summary>
    /// Compute MD5 hash of input string
    /// </summary>
    /// <param name="input">Input string</param>
    /// <returns>MD5 hash in lowercase hexadecimal</returns>
    public static string ComputeHash(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input cannot be null or empty", nameof(input));

        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Generate checksum for FAST API authentication
    /// Formula: MD5(MD5(password) + clientCode)
    /// </summary>
    /// <param name="password">Portal password</param>
    /// <param name="clientCode">Client code</param>
    /// <returns>Checksum string</returns>
    public static string GenerateCheckSum(string password, string clientCode)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));

        if (string.IsNullOrEmpty(clientCode))
            throw new ArgumentException("ClientCode cannot be null or empty", nameof(clientCode));

        // Step 1: Hash the password
        var passwordHash = ComputeHash(password);

        // Step 2: Concatenate with client code and hash again
        var combined = passwordHash + clientCode;
        var checksum = ComputeHash(combined);

        return checksum;
    }
}