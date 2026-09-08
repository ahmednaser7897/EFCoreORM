using EFDataSaving.Data;
using EFDataSaving.Entities;
using EFDataSaving.Helpers;
using Microsoft.EntityFrameworkCore;
namespace EFDataSaving;

public static class EfficientUpdating
{
    public static void Run()
    {
        //Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation();
        //Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_Implementation();
        // Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation();
        Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql();
    }
    public static void Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation()
    {
        Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        // this will make update query for each book.
        // In case of large number of books, 
        // it will make multiple updates and it will be slow.
        using (var context = new AppDbContext())
        {
            var author1Books = context.Books.Where(x => x.AuthorId == 1);

            foreach (var book in author1Books) // Deffered Execution
            {
                book.Price *= 1.1m;
            }

            context.SaveChanges();
        }

        Console.ReadKey();
    }
    public static void Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_Implementation()
    {
        Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        // EF Core 7 and up has a feature called execute update.
        // It will make one query to update the books.
        // and no need to use context.SaveChanges();
        // because ExecuteUpdate update data immediately in the database.
        // So there is no need to save the changes.
        using (var context = new AppDbContext())
        {
            context.Books.Where(x => x.AuthorId == 1)
                   .ExecuteUpdate(b => b.SetProperty(p => p.Price, p => p.Price * 1.1m));
        }

        Console.ReadKey();
    }
    public static void Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation()
    {
        Console.WriteLine($">>>> Sample: {nameof(Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        // EF Core 7 and up has a feature called execute delete.
        // It will make one query to delete the books.
        // and no need to use context.SaveChanges();
        // because ExecuteDelete update data immediately in the database.
        // So there is no need to save the changes.
        using (var context = new AppDbContext())
        {
            context.Books.Where(x => x.Title.StartsWith("Book")).ExecuteDelete();
        }

        Console.ReadKey();
    }

    public static void Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql()
    {
        Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        // Raw SQL is used to execute SQL queries directly on the database.
        // It is used to perform operations that cannot be done with Entity Framework Core.
        // This is most efficient way to update data in the database.
        // So there is no need to use context.SaveChanges();
        // because ExecuteUpdate update data immediately in the database.
        // So there is no need to save the changes.
        using (var context = new AppDbContext())
        {
            context.Database.ExecuteSql($"UPDATE dbo.Books SET Price = Price * 2 WHERE AuthorId = 1");
        }

        Console.ReadKey();
    }


}


