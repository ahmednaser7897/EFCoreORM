using EFMigration.Data;
namespace EFMigration
{

    public static class Program
    {
        public static void Main()
        {
            using var context = new AppDbContext();
            foreach (var item in context.Courses)
                Console.WriteLine(item);
            foreach (var item in context.Instructors)
                Console.WriteLine(item);
            foreach (var item in context.Offices)
                Console.WriteLine(item);

        }
    }
}

