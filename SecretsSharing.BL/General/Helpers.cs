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
}