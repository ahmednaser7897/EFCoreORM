using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;
namespace EFQueryData;

public static class DataJoining
{
    public static void Run()
    {
        //InnerJoin();
        //LeftJoin();
        FullJoin();
    }

    public static void InnerJoin()
    {
        Console.WriteLine("\n=========== Inner Join ==========");
        using var context = new AppDbContext();
        //query syntax
        //var result = from c in context.Courses.AsNoTracking()
        //             join s in context.Sections.AsNoTracking()
        //             on c.Id equals s.CourseId
        //             select new { c.CourseName, s.SectionName };
        //method syntax
        var result = context.Courses.AsNoTracking()
            .Join(
                context.Sections.AsNoTracking(),
                 c => c.Id,
                 s => s.CourseId,
                 (c, s) => new { c.CourseName, s.SectionName });

        foreach (var item in result)
            Console.WriteLine(item);
        Console.WriteLine("\n=========================================");
    }
    public static void LeftJoin()
    {
        Console.WriteLine("\n=========== Left Join ==========");
        using var context = new AppDbContext();
        // Query syntax
        // var ruselt =
        //    (from o in context.Offices.AsNoTracking()
        //     join i in context.Instructors.AsNoTracking()
        //     on o.Id equals i.OfficeId into officeVacancy
        //     from ov in officeVacancy.DefaultIfEmpty()
        //     select new
        //     {
        //         OfficeId = o.Id,
        //         Name = o.OfficeName,
        //         Location = o.OfficeLocation,
        //         Instructor = ov != null ? ov.FName : "<<EMPTY>>"
        //     }).ToList();

        //method syntax
        var ruselt = context.Offices
               .GroupJoin(
                   context.Instructors,
                   o => o.Id,
                   i => i.OfficeId,
                   (office, instructor) => new { office, instructor }
               )
               .SelectMany(
                   ov => ov.instructor.DefaultIfEmpty(),
                   (ov, instructor) => new
                   {
                       OfficeId = ov.office.Id,
                       Name = ov.office.OfficeName,
                       Location = ov.office.OfficeLocation,
                       Instructor = instructor != null ? instructor.FName : "<<EMPTY>>"
                   }
               ).ToList();

        foreach (var office in ruselt)
        {
            Console.WriteLine($"{office.Name} -> {office.Instructor}");
        }
        Console.WriteLine("\n=========================================");
    }
    public static void FullJoin()
    {
        Console.WriteLine("\n=========== Full Join ==========");
        using var context = new AppDbContext();
        // Query syntax

        // ربط كل مدرس مع كل قسم بغض النظر اذا كان يعطيه او لا
        //var sectionInstructorQuerySyntax =
        //        (from s in context.Sections // 200
        //         from i in context.Instructors // 100
        //         select new
        //         {
        //             s.SectionName,
        //             i.FullName
        //         }).ToList();


        //Console.WriteLine(sectionInstructorQuerySyntax.Count()); // 20000

        // method syntax
        var sectionInstructorMethodSyntax = context.Sections
         .SelectMany(
             _ => context.Instructors,
             (s, i) => new { s.SectionName, i.FName }
         ).ToList();

        Console.WriteLine(sectionInstructorMethodSyntax.Count);
        Console.WriteLine("\n=========================================");
    }

}
