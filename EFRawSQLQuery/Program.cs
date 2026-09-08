using EFRawSQLQuery.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace EFRawSQLQuery;

public static class Program
{
    public static void Main()
    {
        //RawSQLQueryBasics();
        //SqlQueryParameter();
        //CallingStoredProcedur(); 
        //CallingDbView();
        //CallingUserDefinedFunction();
        //CallingTableValuedFunction();
        GlobalQueryFilter();
    }
    public static void RawSQLQueryBasics()
    {
        Console.WriteLine("\n=========== Raw SQL Query Basics ===========");
        // FromSql +ef 7.0
        // FromSqlInterpolated ef 3.0
        // FromSqlRaw ef 3.0
        using var context = new AppDbContext();
        var courses =
            context.Courses.FromSql($"SELECT * FROM dbo.Courses");

        var coursesv2 =
          context.Courses.FromSqlInterpolated($"SELECT * FROM dbo.Courses");

        var coursesv3 =
         context.Courses.FromSqlRaw("SELECT * FROM dbo.Courses");

        foreach (var c in coursesv3)
        {
            Console.WriteLine($"{c.CourseName} ({c.HoursToComplete})");
        }
        Console.WriteLine("\n=========================================");
    }
    public static void SqlQueryParameter()
    {
        Console.WriteLine("\n=========== Sql Query Parameter ===========");
        // on DbSet.
        // primary key only to search.
        // (local cache) first. if not exist it queries the database.
        // Returns null if the entity is not found.

        //var c1 = context.Courses.Find(1);
        //Console.WriteLine($"{c1.CourseName} ({c1.HoursToComplete})");

        // On IEnumerable or IQueryable.
        // Retrieves the first element of a sequence, or a default value.
        // You can provide a predicate(a condition) to filter the results. 
        //var c2 = context.Courses.FirstOrDefault(x => x.Id == 1);
        //Console.WriteLine($"{c2.CourseName} ({c2.HoursToComplete})");

        // On IEnumerable or IQueryable.
        // Retrieves the single element of a sequence or a default value.
        // more than one element satisfies, exception thrown.
        // Useful when you expect the query to return only one result.

        //var c3 = context.Courses.SingleOrDefault(x => x.Id == 1);
        //Console.WriteLine($"{c3.CourseName} ({c3.HoursToComplete})");
        using var context = new AppDbContext();
        //by defoult FromSql and FromSqlInterpolated are safe
        // becouse its not useing parmeterization in the sql query
        // it use string interpolation to create the query
        // so it is not vulnerable to sql injection
        // but we can make it unsafe by concatenating strings 
        // in the sql query
        var c1 = context.Courses
            .FromSql($"SELECT * FROM dbo.Courses Where Id = {1}")
            .FirstOrDefault();
        Console.WriteLine($"{c1!.CourseName} ({c1.HoursToComplete})");

        var c2 = context.Courses
           .FromSqlInterpolated($"SELECT * FROM dbo.Courses Where Id = {1}")
           .FirstOrDefault();

        Console.WriteLine($"{c2!.CourseName} ({c2.HoursToComplete})");
        // it will not use parameterization in sql (not safe)
        var c3 = context.Courses
       .FromSqlRaw($"SELECT * FROM dbo.Courses Where Id = {1}")
       .FirstOrDefault();
        // var courseId = "1; DELETE FROM dbo.Courses";
        // it will use parameterization in sql (safe)
        var courseIdParam = new SqlParameter("@courseId", 1);
        var c4 = context.Courses
        .FromSqlRaw("SELECT * FROM dbo.Courses Where Id = @courseId", courseIdParam)
        .FirstOrDefault();

        Console.WriteLine($"{c4!.CourseName} ({c4.HoursToComplete})");
        Console.WriteLine("\n=========================================");
    }
    public static void CallingStoredProcedur()
    {
        Console.WriteLine("\n=========== Calling Stored Procedur ===========");
        using var context = new AppDbContext();
        var startDateParam = new SqlParameter("@StartDate", System.Data.SqlDbType.Date)
        {
            Value = new DateTime(2023, 01, 01)
        };
        var endDateParam = new SqlParameter("@EndDate", System.Data.SqlDbType.Date)
        {
            Value = new DateTime(2023, 06, 30)
        };

        var sections = context.SectionWithDetails
            .FromSql($"Exec dbo.sp_GetSectionWithninDateRange {startDateParam}, {endDateParam}")
            .ToList();

        foreach (var s in sections)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine("\n=========================================");
    }

    public static void CallingDbView()
    {
        Console.WriteLine("\n=========== Calling Db View ===========");
        using var context = new AppDbContext();
        var coursesOverviews = context.CourseOverviews.ToList();
        foreach (var courseOverview in coursesOverviews)
        {
            Console.WriteLine(courseOverview);
        }
        Console.WriteLine("\n=========================================");
    }
    public static void CallingUserDefinedFunction()
    {
        Console.WriteLine("\n=========== Calling User DefinedF unction ===========");
        using var context = new AppDbContext();
        var startDate = new DateTime(2023, 09, 24);
        var endDate = new DateTime(2023, 12, 26);
        var startTime = new TimeSpan(08, 00, 00);
        var endTime = new TimeSpan(11, 00, 00);
        var result = context.Instructors.Select(x =>
               new
               {
                   x.Id,
                   FullName = x.FName + " " + x.LName,
                   DateRange = $"{startDate:ToShortDateString()}-{endDate:ToShortDateString()}",
                   TimeRange = $"{startTime:hh\\:mm}-{endTime:hh\\:mm}",
                   Status = AppDbContext
                   .GetInstructorAvailability(x.Id, startDate, endDate, startTime, endTime)
               }).ToList();

        foreach (var item in result)
        {
            Console.WriteLine(
                $"{item.Id}\t{item.FullName,-20}\t{item.DateRange}\t{item.TimeRange}\t{item.Status}");
        }
        Console.WriteLine("\n=========================================");
    }

    public static void CallingTableValuedFunction()
    {
        Console.WriteLine("\n=========== Calling Table Valued Function ===========");
        using var context = new AppDbContext();
        foreach (var section in context.GetSectionsExceedingParticipantCount(21))
        {
            Console.WriteLine($"{section.Id}\t{section.SectionName}\t{section.DateRange}\t{section.TimeSlot}");
        }
        Console.WriteLine("\n=========================================");
    }
    public static void GlobalQueryFilter()
    {
        Console.WriteLine("\n=========== Global Query Filter ===========");
        using var context = new AppDbContext();
        foreach (var section in context.Sections)
        {
            Console.WriteLine($"{section.Id}\t{section.SectionName}\t{section.DateRange}\t{section.TimeSlot}");
        }
        Console.WriteLine("\n=========================================");
    }

}


