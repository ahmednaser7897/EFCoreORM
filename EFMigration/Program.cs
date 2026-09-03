using EFMigration.Data;
namespace EFMigration
{

    public static class Program
    {
        public static void Main()
        {
            using AppDbContext context = new();
            Console.WriteLine("EFMigration ");
            //var products = context.Products.ToList();
            //foreach (var p in products)
            //{
            //    Console.WriteLine($"{p.Id} {p.Name} {p.UnitPrice} {p.LastUpdate?.LoadedAt} {p.LastUpdate?.Version}");
            //}
        }

    }
}


