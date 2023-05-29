using System.Transactions;

namespace SecretsSharing.Test.Helpers
{
	static public class Helper
	{
		public static TransactionScope CreateTransactionScope(int seconds = 99999999)
		{
			return new TransactionScope(
                TransactionScopeOption.Required,
				new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
		}
    }
}

