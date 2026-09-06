using EFMigrationInheritance.Data;
using EFMigrationInheritance.Entities;
namespace EFMigrationInheritance
{

    public static class Program
    {
        public static void Main()
        {
            //TestTPHAndTPT();
            TestTPC();
        }

        private static void TestTPHAndTPT()
        {
            Console.WriteLine("\n------------------ Testing TPH and TPT ------------------");
            /*
            This example demonstrates two common Table-Per-Hierarchy (TPH) patterns:

            TPH (Default Behavior): The BaseParticipant class is mapped using TPH by default. 
            EF Core creates a single table (Participants) with a discriminator column (Type) 
            to distinguish between different entity types. Each row includes all columns 
            from the base and derived classes, with nullable fields for unused properties.

            TPH (Custom Discriminator): The Course class demonstrates a custom TPH mapping where the discriminator column is named "CourseType" 
            instead of the default "Type". This is achieved using the HasDiscriminator<string>("CourseType") method 
            in the configuration.

            To switch to Table-Per-Type (TPT), you can:

            Replace the UseTphMappingStrategy() call with UseTptMappingStrategy() in the AppDbContext configuration.
            Add configuration files for derived entities (e.g., CourseConfiguration) that specify their respective table names using ToTable().
            Remove the HasDiscriminator() call from the base entity configuration.
            When using TPT, EF Core will create a separate table for each entity type (e.g., Courses, OnlineCourses, InPersonCourses), with primary key columns that also serve as foreign key references to the base entity table.
            */
            using var context = new AppDbContext();
            // var part1 = new Participant { Id = 0, FName = "Omar", LName = "Youssef" };
            // var part2 = new Individual { Id = 1, FName = "Abdullah", LName = "Ali", University = "Cairo", YearOfGraduation = 2025, IsIntern = true };
            // var part3 = new Individual { Id = 2, FName = "Reem", LName = "Ahmed", University = "Ain Shams", YearOfGraduation = 2026, IsIntern = false };
            // var part4 = new Coporate { Id = 3, FName = "Noor", LName = "Saleh", Company = "Google", JobTitle = "Software Engineer" };
            // var part5 = new Coporate { Id = 4, FName = "Sara", LName = "Youssef", Company = "Microsoft", JobTitle = "Project Manager" };
            // context.Participants.AddRange(part1, part2, part3, part4, part5);
            // context.SaveChanges();
            var all = context.Participants;
            // it will know each item type by the discriminator column "type"
            // so tostring() will work for derived classes also
            Console.WriteLine("=============== All Participants ===================");
            foreach (var item in all)
                Console.WriteLine(item);
            //We can get data for a spacific type like this:
            //get all individuals
            Console.WriteLine("=============== All Individuals ===================");
            var individuals = context.Individuals;
            foreach (var item in individuals)
                Console.WriteLine(item);
            //or like this:
            Console.WriteLine("=============== All Corporates ===================");
            var coporates = context.Set<Participant>().OfType<Coporate>();
            foreach (var item in coporates)
                Console.WriteLine(item);
        }
        private static void TestTPC()
        {
            Console.WriteLine("------------------ Testing TPC ------------------");
            using var context = new AppDbContext();
            // var quiz1 = new MultipleChoiceQuiz { Id = 0, Title = "Multiple Choice Quiz 1", OptionA = "a", OptionB = "b", OptionC = "c", OptionD = "d", CorrectAnswer = 'a' };
            // var quiz2 = new MultipleChoiceQuiz { Id = 1, Title = "Multiple Choice Quiz 2", OptionA = "a", OptionB = "b", OptionC = "c", OptionD = "d", CorrectAnswer = 'c' };
            // var quiz3 = new TrueAndFalseQuiz { Id = 2, Title = "True and False Quiz 1", CorrectAnswer = true };
            // var quiz4 = new TrueAndFalseQuiz { Id = 3, Title = "True and False Quiz 2", CorrectAnswer = false };
            // context.Quizzes.AddRange(quiz1, quiz2, quiz3, quiz4);
            // context.SaveChanges();
            var all = context.Quizzes;
            // it will know each item type by the discriminator column "type"
            // so tostring() will work for derived classes also
            Console.WriteLine("=============== All Quizzes ===================");
            foreach (var item in all)
                Console.WriteLine(item);
            //We can get data for a spacific type like this:
            //get all m
            Console.WriteLine("=============== All Multiple Choice Quizzes ===================");
            var onlineCourses = context.MultipleChoiceQuizzes;
            foreach (var item in onlineCourses)
                Console.WriteLine(item);
            //or like this:
            Console.WriteLine("=============== All True and False Quizzes ===================");
            var inPersonCourses = context.Set<TrueAndFalseQuiz>();
            foreach (var item in inPersonCourses)
                Console.WriteLine(item);
        }
    }
}

