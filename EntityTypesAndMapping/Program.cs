using EntityTypesAndMapping.Data;
using EntityTypesAndMapping.Entities;
using Microsoft.EntityFrameworkCore;
namespace EntityTypesAndMapping
{

    public static class Program
    {
        public static void Main()
        {
            TestExcludeEntity();
            TestInclude();
            TestView();
            TestViewWithFunction();
        }
        public static void TestExcludeEntity()
        {
            Console.WriteLine("\n\n**************** Test Exclude Entity *****************");
            // we use modelBuilder.Entity<Product>().Ignore(p => p.LastUpdate);
            // we use modelBuilder.Ignore<Snapshot>();
            // we use [NotMapped] in the entity class
            using AppDbContext context = new();
            var products = context.Products.ToList();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} {p.Name} {p.UnitPrice} {p.LastUpdate?.LoadedAt} {p.LastUpdate?.Version}");
            }
        }
        public static void TestInclude()
        {
            Console.WriteLine("\n\n**************** Test Include *****************");
            // if the entity has no data set in the DbContext
            // but if its a parent entity for other entity
            // then it will be included in the database
            // example:
            // if we have a Order entity and OrderDetail entity
            // and we have a collection of OrderDetail in Order entity
            // and we have a data set for Order entity
            // but we don't have a data set for OrderDetail entity
            // then OrderDetail entity will be included in the database
            // but we can't access it directly 
            //  because we don't have a data set for it
            //  so when the query execute it will not select OrderDetail entity
            //  but it will select Order entity and join with OrderDetail entity
            //  if we don't use .Include(o => o.OrderDetails)
            // then it will not select OrderDetail entity at all
            // but if we use .Include(o => o.OrderDetails)
            // then it will select OrderDetail entity and join with Order entity
            using AppDbContext context = new();
            var order = context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefault();
            Console.WriteLine($"{order?.Id} {order?.OrderDate}");
            foreach (var od in order?.OrderDetails)
            {
                Console.WriteLine($"{od.ProductId} {od.Quantity} {od.UnitPrice}");
            }
            // also if i use entity that has no data set and is not exixt also as a part of oter entity then it will not be created in the database
            // but i can get it using .Include() method
            // we can adding it in the DbContext.OnModelCreating() method to make it always included
            // modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders);
            var auditEntry = new AuditEntry { UserName = "issam", Action = "Read order count" };
            context.Set<AuditEntry>().Add(auditEntry);
            // context.SaveChanges(); // Error Invalid object
        }

        public static void TestView()
        {
            Console.WriteLine("\n\n**************** Test View *****************");
            using AppDbContext context = new();
            var orderWithDetailsViews = context.OrderWithDetailsViews.ToList();
            foreach (var orderWithDetailsView in orderWithDetailsViews)
            {
                Console.WriteLine(orderWithDetailsView);
            }
        }

        public static void TestViewWithFunction()
        {
            Console.WriteLine("\n\n**************** Test View With Function *****************");
            // we call it like this because it return a table value function
            // and it has parameter 
            using AppDbContext context = new();
            int id = 1;
            var orderBills = context.OrderBills
            .FromSqlInterpolated($"SELECT * FROM dbo.GetOrderBill({id})")
            .ToList();
            foreach (var orderBill in orderBills)
            {
                Console.WriteLine(orderBill);
            }
        }
    }
}


