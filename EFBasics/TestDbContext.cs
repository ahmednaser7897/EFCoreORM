using EFBasics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFCoreORM
{
    public static class TestDbContext
    {
        public static void Run()
        {
            // TestInternalConfig();
            // TestExternalConfig();
            // TestServiceCollectionConfig();
            // TestFactories();
            // TestDbContextLifeTime();
            // TestOtherConfigurations();
            // TestConcurrency();
            TestDbContextPool();

        }

        public static void TestInternalConfig()
        {
            Console.WriteLine("\n--- TestInternalConfig ---");
            // Create a new DbContext to start a new session with the database.
            // using AppDbContext that has its own configuration
            using var context = new AppDbContext();
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
            Console.WriteLine("----------------------------\n");
        }

        public static void TestExternalConfig()
        {
            Console.WriteLine("\n--- TestExternalConfig ---");
            // Create a new DbContext to start a new session with the database.
            // using AppDbContextExternall that will receive the configuration from outside
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContextExternall>();
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionString());
            Console.WriteLine(optionsBuilder.Options.ToString());
            using var context = new AppDbContextExternall(optionsBuilder.Options);
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
            Console.WriteLine("----------------------------\n");
        }
        public static void TestServiceCollectionConfig()
        {
            Console.WriteLine("\n--- TestServiceCollectionConfig ---");
            // Create a new DbContext to start a new session with the database.
            // using AppDbContextExternall that has its own configuration
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContextExternall>(option =>
                option.UseSqlServer(ConnectionString.LoadConnectionString()));
            // Build a service provider from the service collection.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Get the DbContext from the service provider.
            using var context = serviceProvider.GetRequiredService<AppDbContextExternall>();
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
            Console.WriteLine("----------------------------\n");
        }

        public static void TestFactories()
        {
            Console.WriteLine("\n--- TestFactories ---");
            // Create a new DbContext to start a new session with the database.
            // using AppDbContextExternall that has its own configuration
            var services = new ServiceCollection();
            services.AddDbContextFactory<AppDbContextExternall>(option =>
                option.UseSqlServer(ConnectionString.LoadConnectionString()));
            // Build a service provider from the service collection.
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            // get the factory
            var contextFactory = serviceProvider.GetRequiredService<IDbContextFactory<AppDbContextExternall>>();
            // Get the DbContext from the factory.
            using var context = contextFactory.CreateDbContext();
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
            Console.WriteLine("----------------------------\n");
        }

        public static void TestDbContextLifeTime()
        {
            Console.WriteLine("\n--- TestDbContextLifeTime ---");

            using (var context = new AppDbContext())
            {
                // creation -> tracking -> save changes -> dispose
                // DbContext works like a unit of work , 
                // it tracks all the changes that happen to its entities
                // inside this session (using block)
                // it does not execute queries on the database until SaveChanges is called
                var wallet1 = new Wallet() { Holder = "sayed", Balance = 2343434 };
                // Add() method tells the context that this entity is new and should be added to the database
                context.Wallets.Add(wallet1);
                var wallet2 = new Wallet() { Holder = "ALI", Balance = 23243 };
                context.Wallets.Add(wallet2);
                Console.WriteLine(wallet1);
                Console.WriteLine(wallet2);
                // SaveChanges() method executes all the changes that have been made to the context
                // in a single transaction
                // All or nothing principle: 
                // if any of the changes fail, the entire transaction is rolled back
                // and the database is left unchanged
                context.SaveChanges();
                // the id is generated by the database after SaveChanges
                Console.WriteLine(wallet1);
                Console.WriteLine(wallet2);
            }

            Console.WriteLine("----------------------------\n");
        }

        public static void TestOtherConfigurations()
        {
            Console.WriteLine("\n--- TestOtherConfigurations ---");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContextExternall>();
            optionsBuilder
            .UseSqlServer(ConnectionString.LoadConnectionString())
            .LogTo(Console.WriteLine, LogLevel.Information)
            ;
            Console.WriteLine(optionsBuilder.Options.ToString());
            using var context = new AppDbContextExternall(optionsBuilder.Options);
            var wallets = context.Wallets.ToList();
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);


            Console.WriteLine("----------------------------\n");
        }

        public static void TestConcurrency()
        {
            Console.WriteLine("\n--- TestConcurrency ---");
            // to enable entity framework to catch the concurrency exception
            // we need to use async and await pattern
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContextExternall>();
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionString());
            Console.WriteLine(optionsBuilder.Options.ToString());
            using var context = new AppDbContextExternall(optionsBuilder.Options);
            var list = new[]{
                Task.Factory.StartNew(()=>Task1(context)),
                Task.Factory.StartNew(()=>Task2(context))
            };
            Task.WhenAll(list).ContinueWith(_ => Console.WriteLine("All tasks completed"));

            Console.WriteLine("----------------------------\n");
            Console.ReadKey();
        }
        private static async Task Task1(AppDbContextExternall context)
        {
            context.Wallets.Add(new Wallet { Holder = "Task1", Balance = 100 });
            await context.SaveChangesAsync();
        }
        private static async Task Task2(AppDbContextExternall context)
        {
            context.Wallets.Add(new Wallet { Holder = "Task2", Balance = 200 });
            await context.SaveChangesAsync();
        }
        public static void TestDbContextPool()
        {
            Console.WriteLine("\n--- TestDbContextPool ---");
            // DbContext Pooling improves the performance of Entity Framework applications
            // by reusing DbContext instances that are no longer in use.
            // DbContext Pooling is only supported for DbContext types that have a parameterless constructor.
            // DbContext Pooling is not supported for DbContext types that use DbContextFactory.
            var services = new ServiceCollection();
            services.AddDbContextPool<AppDbContextExternall>(option =>
                option.UseSqlServer(ConnectionString.LoadConnectionString()));
            // Build a service provider from the service collection.
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            // get the factory
            var contextFactory = serviceProvider.GetRequiredService<IDbContextFactory<AppDbContextExternall>>();
            // Get the DbContext from the factory.
            using var context = contextFactory.CreateDbContext();
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
            Console.WriteLine("----------------------------\n");
        }


    }
}