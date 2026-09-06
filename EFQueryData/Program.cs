using EFQueryData.Data;
namespace EFQueryData;

public static class Program
{
    public static void Main()
    {
        using var context = new AppDbContext();
        foreach (var item in context.Courses)
            Console.WriteLine(item);
    }
}


