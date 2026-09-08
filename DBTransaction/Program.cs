using DBTransaction.Helpers;
using DBTransaction.Data;
namespace DBTransaction;

public static class Program
{
    public static void Main()
    {
        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        using var context = new AppDbContext();
        foreach (var account in context.Accounts)
        {
            Console.WriteLine($"{account.AccountNumber}: {account.AccountHolder} - {account.Balance}");
        }

    }
}
