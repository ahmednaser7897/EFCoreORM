using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;
namespace EFQueryData;

public static class SplitQueries
{
    public static void Run()
    {
        //by default the query is executed as a single query get all the data
        //with inner join this causes to get the same course multiple times if it has multiple sections or reviews
        // to avoid this we use ProperProjection AND SplitQueries
        // 1- AsSplitQuery => Reduce the number of Rows
        // 2- ProperProjection => Reduce the amount of data transfer between App and DB AND Return the projection of data
        ProperProjection();
        SplitQuery();
    }

    public static void ProperProjection()
    {
        Console.WriteLine("\n=========== Proper Projection ==========");
        // proper projection (select) reduce network traffic
        // and reduce the effect on app performance
        using var context = new AppDbContext();
        var coursesProjection = context.Courses.AsNoTracking()
           .Select(c =>
           new
           {
               CourseId = c.Id,
               CourseName = c.CourseName,
               Hours = c.HoursToComplete,
               Sections = c.Sections.Select(s =>
               new
               {
                   SectionId = s.Id,
                   SectionName = s.SectionName,
                   DateRate = s.DateRange.ToString(),
                   TimeSlot = s.TimeSlot.ToString()
               }),
               Reviews = c.Reviews.Select(r =>
               new
               {
                   FeedBack = r.Feedback,
                   CreateAt = r.CreatedAt
               })
           }).ToList();
        foreach (var course in coursesProjection)
            Console.WriteLine(course);
        Console.WriteLine("\n=========================================");
    }
    public static void SplitQuery()
    {
        // split query (AsSplitQuery) cause EF to use multiple SQL queries
        // each query is executed separately
        // THIS WILL IMPROVE THE PERFORMANCE WHEN YOU HAVE LARGE DATA
        // UNLIKE THE PROJECTION
        // THE SPLIT QUERY WILL RETURN THE FULL OBJECT NOT THE PROJECTION
        Console.WriteLine("\n=========== Split Queries ===========");
        using var context = new AppDbContext();
        // var courses = context.Courses
        //     .AsSplitQuery()// Explicitly tell EF to use split queries
        //     .Include(c => c.Sections)
        //     .Include(c => c.Reviews)
        //     .ToList();
        // will use AsSplitQuery implicity by adding in the AppDbContext.OnConfiguring
        var courses = context.Courses
           .Include(c => c.Sections)
           .Include(c => c.Reviews)
           .ToList();
        foreach (var course in courses)
            Console.WriteLine(course);
        Console.WriteLine("\n=========================================");
    }
}
