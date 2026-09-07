using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace EFQueryData;

public static class QueryDataBasics
{
    public static void Run()
    {
        QueryData();
        // ClientVsServerEvaluation();
        // TrackingVsNoTracking();
    }
    public static void QueryData()
    {
        Console.WriteLine("\n=========== Query Data Basics ===========");
        using var context = new AppDbContext();
        //set up the query but will not execute until we iterate over it or call ToListAsync
        // var courses = context.Courses;
        // Console.WriteLine(courses.ToQueryString());
        // foreach (var item in courses)
        //     Console.WriteLine(item);
        //var course = context.Courses.Single(x => x.Id == 1);

        //Console.WriteLine($"course name: {course.CourseName}, {course.HoursToComplete} hrs., {course.Price.ToString("C")}");

        //var course = context.Courses.Single(x => x.HoursToComplete == 25);

        //Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

        // var course = context.Courses.First(x => x.HoursToComplete == 25);

        // Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

        // var course = context.Courses.Single(x => x.HoursToComplete == 999);

        // Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

        //var course = context.Courses.FirstOrDefault(x => x.HoursToComplete == 999);

        //Console.WriteLine($"{course?.CourseName}, {course?.Price.ToString("C")}");

        // var course = context.Courses.SingleOrDefault(x => x.HoursToComplete == 999);

        // Console.WriteLine($"{course?.CourseName}, {course?.Price.ToString("C")}");

        var courses = context.Courses.Where(x => x.Price > 3000);

        Console.WriteLine(courses.ToQueryString());

        foreach (var course in courses)
            Console.WriteLine(course);
        Console.WriteLine("\n=========================================");
    }

    public static void ClientVsServerEvaluation()
    {
        Console.WriteLine("\n=========== Client Vs Server Evaluation ===========");
        using var context = new AppDbContext();
        //    var courseId = 1;

        //    var result = context.Sections
        //        .Where(x => x.CourseId == courseId)
        //        .Select(x => new
        //        {
        //            Id = x.Id,
        //            Section = x.SectionName
        //        });

        //    //DECLARE @__courseId_0 int = 1;
        //    //SELECT[s].[Id], [s].[SectionName] AS[Section]
        //    //FROM[Sections] AS[s]
        //    //WHERE[s].[CourseId] = @__courseId_0

        //    Console.WriteLine(result.ToQueryString());

        //    foreach (var item in result)
        //    {
        //        Console.WriteLine($"{item.Id} {item.Section}");
        //    }
        //==========================================================
        // Entity Framework Core can translate LINQ expressions to SQL, 
        // This called Server Evaluation
        // Anything that can be done in SQL will be done on the Server
        // so it try to converLINQ methods to SQL Server functions as much as possible
        // for example Substring
        const int courseId = 1;
        var result = context.Sections
            .Where(x => x.CourseId == courseId)
            .Select(x => new
            {
                x.Id,
                // Substring will be converted to SUBSTRING() function in SQL Server
                Section = x!.SectionName!.Substring(4),
                // this will be called on the client side after the data is fetched from the server
                // this is called Client Evaluation
                // The  CalculateTotalDays() method is not supported in SQL Server
                TotalDays = CalculateTotalDays(x.DateRange.StartDate, x.DateRange.EndDate)
            });

        // SELECT[s].[Id], SUBSTRING([s].[SectionName], 4 + 1, LEN([s].[SectionName])), [s].[StartDate], [s].[EndDate]
        // FROM[Sections] AS[s]
        // WHERE[s].[CourseId] = 1
        Console.WriteLine(result.ToQueryString());

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Id} {item.Section} ({item.TotalDays})");
        }
        Console.WriteLine("\n=========================================");
    }

    public static void TrackingVsNoTracking()
    {
        Console.WriteLine("\n=========== Tracking Vs No Tracking ===========");
        using var context = new AppDbContext();
        // By default, EF Core tracks all the entities that are retrieved from the database
        // This called Change Tracking
        // so if we change any property of the entity, EF Core will detect the change
        // and update the database when we call SaveChanges()

        var section = context.Sections.FirstOrDefault(x => x.Id == 1);

        Console.WriteLine("before changing tracked object");

        Console.WriteLine(section?.SectionName);

        section!.SectionName = "this is a new section name";

        context.SaveChanges();

        section = context.Sections.FirstOrDefault(x => x.Id == 1);

        Console.WriteLine("after bein changed");

        Console.WriteLine(section?.SectionName);

        Console.WriteLine("\n=========================================");

        // using AsNoTracking() method, we can tell EF Core not to track the entities
        // This called No Change Tracking
        // so if we change any property of the entity, EF Core will not detect the change
        // and will not update the database when we call SaveChanges()
        section = context.Sections.AsNoTracking().FirstOrDefault(x => x.Id == 1);

        Console.WriteLine("before changing tracked object");

        Console.WriteLine(section?.SectionName); // BlaBla

        section?.SectionName = "01A51C05";

        context.SaveChanges();

        section = context.Sections.FirstOrDefault(x => x.Id == 1);

        Console.WriteLine("after bein changed");

        Console.WriteLine(section?.SectionName);
        Console.WriteLine("\n=========================================");
    }

    private static int CalculateTotalDays(DateOnly startDate, DateOnly endDate)
    {
        return endDate.DayNumber - startDate.DayNumber; // 0001-01-01
    }

}