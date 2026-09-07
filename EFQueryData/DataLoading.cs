using EFQueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace EFQueryData;
//EFCore  by default don't load related data (Joins)
// to get the related data we have many technics :
// 1- Eager Loading
// 2- Explicit Loading
// 3- Lazy Loading
public static class DataLoading
{
    public static void Run()
    {

        // RelatedDataEager();
        // RelatedDataExplicitLoading();
        RelatedDataLazyLoading();
    }
    public static void RelatedDataEager()
    {
        Console.WriteLine("\n===========  Eager Loading ===========");
        // 1- Eager Loading:  use Include() method
        //      - to load related data we need to use Include() method
        //      - ThenInclude use with Include() method to load related data of related data (nested related data)
        using var context = new AppDbContext();
        const int sectionId = 1;
        var sections = context.Sections
            .Where(x => x.Id == sectionId);
        // SELECT *
        // FROM[Sections] AS[s]
        // WHERE[s].[Id] = 1
        Console.WriteLine(sections.ToQueryString());
        var section = context.Sections.FirstOrDefault();
        // No Related Data Loaded
        Console.WriteLine((section?.Participants ?? []).Count);//0

        Console.WriteLine("---------------------------");
        sections = context.Sections
        .Include(x => x.Participants)
        .ThenInclude(p => p.Sections)
        .Where(x => x.Id == sectionId);

        section = sections.FirstOrDefault();
        Console.WriteLine(sections.ToQueryString());
        // Related Data Loaded
        foreach (var participant in section?.Participants ?? [])
            Console.WriteLine($"{participant} => Sections count = {(participant.Sections ?? []).Count}");

        Console.WriteLine("\n=========================================");
    }

    public static void RelatedDataExplicitLoading()
    {
        Console.WriteLine("\n=========== Explicit Loading ===========");

        // 2- Explicit Loading :  Explicitly Load Related Data 
        //      - use Load() method on the related entity
        //      - use Query() method on the related entity to build a query
        //      - 

        using var context = new AppDbContext();
        const int sectionId = 1;
        var section = context.Sections.FirstOrDefault(x => x.Id == sectionId);
        // no related data loaded by default

        // Build a query to load the related data
        var query = context.Entry(section!)
            .Collection(x => x.Participants)
            .Query();
        Console.WriteLine(query.ToQueryString());
        // Load the related data
        query.Load();
        // Related Data Loaded
        foreach (var participant in section?.Participants ?? [])
            Console.WriteLine($"{participant} => Sections count = {(participant.Sections ?? []).Count}");

        Console.WriteLine("\n=========================================");
    }
    public static void RelatedDataLazyLoading()
    {
        Console.WriteLine("\n=========== Lazy Loading ===========");

        // 3- Lazy Loading :  Lazy Load Related Data 
        //      - use Lazy Loading Proxy Objects
        //      - must Enable Lazy Loading in DbContext (virtual properties)
        //      - properties must be virtual
        //      - when iterate over the related data, it will load the data 

        using var context = new AppDbContext();
        const int sectionId = 1;
        var sections = context.Sections.Where(x => x.Id == sectionId);
        Console.WriteLine(sections.ToQueryString());
        foreach (var section in sections)
            Console.WriteLine($"{section} => Sections count = {(section.Participants ?? []).Count}");

        Console.WriteLine("\n=========================================");
    }


}
