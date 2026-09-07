using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace EFQueryData;

public static class GroupByClass
{

    public static void Run()
    {
        Console.WriteLine("\n=========== Group By ===========");
        using var context = new AppDbContext();
        // •  Ins#1
        //         S1, S2, s3
        // •  Ins#2
        //         S5, S2, s1

        // Query syntax

        // var instructorSections =
        //    from s in context.Sections
        //    group s by s.Instructor
        //    into g
        //    select new
        //    {
        //        Key = g.Key,
        //        Sections = g.ToList()
        //    };

        // foreach (var item in instructorSections)
        // {
        //     Console.WriteLine($"{item.Key!.FName} {item.Key!.LName} ");
        //     foreach (var section in item.Sections)
        //     {
        //         Console.WriteLine(section.SectionName);
        //     }
        // }


        // var instructorSections =
        //    from s in context.Sections
        //    group s by s.Instructor
        //    into g
        //    select new
        //    {
        //        Key = g.Key,
        //        TotalSections = g.Count()
        //    };



        //method syntax
        var instructorSections =
            context.Sections.GroupBy(x => x.Instructor)
            .Select(x => new
            {
                Key = x.Key,
                TotalSections = x.Count()
            });


        foreach (var item in instructorSections)
        {
            Console.WriteLine($"{item.Key!.FName} {item.Key!.LName} " +
                $"==> Total Sections #[{item.TotalSections}]");
        }
        Console.WriteLine("\n=========================================");
    }



}