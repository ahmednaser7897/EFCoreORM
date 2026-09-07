using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace EFQueryData;

public static class SelectManyClass
{

    public static void Run()
    {
        Console.WriteLine("\n=========== Select Many ===========");
        using var context = new AppDbContext();
        // Query syntax

        // front end (angular, react)
        //  اسماء الطلاب اللي بيدرسوا فرونت اند

        // var frontendParticipants =
        //    from c in context.Courses
        //    where c.CourseName!.Contains("frontend") // angular, react
        //    from s in c.Sections
        //    from p in s.Participants
        //    select new
        //    {
        //        ParticipantName = p.FName + " " + p.LName
        //    };


        // method syntax

        var frontendParticipants =
            context.Courses
            .Where(x => x.CourseName!.Contains("frontend")) //  angular, react
            .SelectMany(x => x.Sections) // s1, s2, s...
            .SelectMany(x => x.Participants)
            .Select(p =>
                new
                {
                    ParticipantName = p.FName + " " + p.LName
                });

        foreach (var pName in frontendParticipants)
            Console.WriteLine(pName);
        Console.WriteLine("\n=========================================");
    }



}