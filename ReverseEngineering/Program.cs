using ReverseEngineering.Data;
namespace ReverseEngineering
{
    public static class Program
    {
        public static void Main()
        {
            using var context = new TechTalkContext();
            foreach (var item in context.Speakers)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
        }

    }
}

