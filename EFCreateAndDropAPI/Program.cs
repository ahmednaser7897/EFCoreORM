using EFCreateAndDropAPI.Data;
using EFCreateAndDropAPI.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFCreateAndDropAPI;


public static class Program
{
    public static async Task Main()
    {
        await TestCreateAndDropAPI();
        await TestSeedData();
        await using var context = new AppDbContext();
        Console.WriteLine("=============== All Participants ===================");
        foreach (var item in context.Participants)
            Console.WriteLine(item);
        Console.WriteLine("=============== All Quizzes ===================");
        foreach (var item in context.Quizzes)
            Console.WriteLine(item);
        Console.ReadKey();
    }

    public static async Task TestCreateAndDropAPI()
    {
        Console.WriteLine("\n------------------ Test Create And Drop API ------------------");
        await using var context = new AppDbContext();
        // Database will be created if it does not exist
        // without any migrations applied, and the model will be used to create the database schema.
        await context.Database.EnsureCreatedAsync();

        var sqlScript = context.Database.GenerateCreateScript();
        Console.WriteLine(sqlScript);
        //await Task.Delay(30000);

        // Database will be deleted if it does exist
        //await context.Database.EnsureDeletedAsync();
    }
    public static async Task TestSeedData()
    {
        Console.WriteLine("\n------------------ Testing Seed Data ------------------");
        //WE can add init data using this way that is better then using builder.hasData() method
        // becouse of owned tables limitation that builder.hasData() cannot handle it.
        // and it is more flexible.
        await using var context = new AppDbContext();
        await context.Database.EnsureCreatedAsync();
        // WE Can also create the database using SeedData.EnsurePopulatedAsync(context); which is more cleaner.
        // await SeedData.EnsurePopulatedAsync(context);
        if (!await context.Participants.AnyAsync())
        {
            await context.Participants.AddRangeAsync(SeedData.LoadParticipants());
            await context.SaveChangesAsync();
        }
        if (!await context.Individuals.AnyAsync())
        {
            await context.Individuals.AddRangeAsync(SeedData.LoadIndividuals());
            await context.SaveChangesAsync();
        }
        if (!await context.Coporates.AnyAsync())
        {
            await context.Coporates.AddRangeAsync(SeedData.LoadCoporates());
            await context.SaveChangesAsync();
        }
        if (!await context.MultipleChoiceQuizzes.AnyAsync())
        {
            await context.MultipleChoiceQuizzes.AddRangeAsync(SeedData.LoadMultipleChoiceQuizs());
            await context.SaveChangesAsync();
        }
        if (!await context.TrueAndFalseQuizzes.AnyAsync())
        {
            await context.TrueAndFalseQuizzes.AddRangeAsync(SeedData.LoadTrueAndFalseQuizs());
            await context.SaveChangesAsync();
        }
    }
}


