using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace SecretsSharing.BL.General;

public static class Helpers
{
    public static int? StringToIntDef(string str, int? def)
    {
        return int.TryParse(str, out var value) 
            ? value 
            : def;
    }

    public static Guid? StringToGuidDef(string str, Guid? def = null)
    {
        return Guid.TryParse(str, out var value) 
            ? value 
            : def;
    }
    
    public static TransactionScope CreateTransactionScope(int seconds = 600)
    {
        return new TransactionScope(TransactionScopeOption.Required, 
            new TimeSpan(0, 0, seconds), 
            TransactionScopeAsyncFlowOption.Enabled);
    }

    public static string StringToHashString(this string str)
    {
        var md5Hash = MD5.Create();
        var hashBytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(str));
        return Convert.ToHexString(hashBytes);
    }
}