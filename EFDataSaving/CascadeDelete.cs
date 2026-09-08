using EFDataSaving.Data;
using EFDataSaving.Entities;
using EFDataSaving.Helpers;
using Microsoft.EntityFrameworkCore;
namespace EFDataSaving;
//By Default, EF Core uses CASCADE 
// delete its dependents
//ON DELETE
// - RESTRICT - prevent deleting principal if has dependent
// - CASCADE - delete principal and its dependents
// - NO ACTION - same as RESTRICT
// - SET NULL - set principal to null if dependent has optional FK
// 
public static class CascadeDelete
{
    public static void Run()
    {
        DeletePrincipalAuthor_With_Dependent_Book_FK_Required();

        // DeletePrincipalAuthor_With_Dependent_Book_FK_Optional();

        // SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional();

        //SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional();
    }
    public static void DeletePrincipalAuthor_With_Dependent_Book_FK_Required()
    {
        Console.WriteLine($">>>> Sample: {nameof(DeletePrincipalAuthor_With_Dependent_Book_FK_Required)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            var author = context.Authors.First();
            // the relationship between Author and Book is one to many
            // the book must have an author, this is why FK is required
            // So, if we delete author, then books will be deleted
            // because without author, book cannot exist
            context.Authors.Remove(author);

            context.SaveChanges();
        }

        Console.ReadKey();
    }

    public static void DeletePrincipalAuthor_With_Dependent_Book_FK_Optional()
    {
        Console.WriteLine($">>>> Sample: {nameof(DeletePrincipalAuthor_With_Dependent_Book_FK_Optional)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {

            var author = context.AuthorV2s.Include(x => x.BookV2s).First();

            // the relationship between Author and Book is one to many
            // the book can have an author, this is why FK is optional
            // So, if we delete author, the relationship is removed
            // so the autherid in the book will be set to null
            context.AuthorV2s.Remove(author);

            context.SaveChanges();
        }

        Console.ReadKey();
    }

    public static void SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional()
    {
        Console.WriteLine($">>>> Sample: {nameof(SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            //this will not delete the book because we are not get it from db we must incloude it to track
            // var author = context.AuthorV2s.First();
            // author.BookV2s.Clear();
            var author = context.AuthorV2s.Include(x => x.BookV2s).First();

            author.BookV2s.Clear();

            context.SaveChanges();
        }

        Console.ReadKey();
    }

    public static void SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional()
    {
        Console.WriteLine($">>>> Sample: {nameof(SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional)}");
        Console.WriteLine();

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            var author = context.AuthorV2s.Include(x => x.BookV2s).First();

            foreach (var book in author.BookV2s)
            {
                book.AuthorV2 = null;
            }
            context.SaveChanges();
        }

        Console.ReadKey();
    }


}


