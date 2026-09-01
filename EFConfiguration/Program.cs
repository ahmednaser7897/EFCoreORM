using EFConfiguration.Data;

namespace EFCoreORM
{
    public static class Program
    {
        public static void Main()
        {
            //TestConfigurationByConvention();
            //TestConfigurationByAnnotation();
            TestConfigurationByFluentApi();
        }
        public static void TestConfigurationByConvention()
        {
            Console.WriteLine("================== Configuration By Convention ==================");
            using var context = new AppDbContext();
            foreach (var item in context.Users.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Tweets.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Comments.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
        }
        public static void TestConfigurationByAnnotation()
        {
            Console.WriteLine("================== Configuration By Annotation ==================");
            using var context = new AppDbContextWithAnnotation();
            foreach (var item in context.Users.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Tweets.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Comments.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
        }

        public static void TestConfigurationByFluentApi()
        {
            Console.WriteLine("================== Configuration By Fluent Api ==================");
            using var context = new AppDbContextWithFluentApi();
            foreach (var item in context.Users.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Tweets.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
            foreach (var item in context.Comments.ToList())
                Console.WriteLine(item);
            Console.WriteLine("=====================================");
        }
    }
}