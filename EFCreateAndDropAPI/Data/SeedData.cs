
using EFCreateAndDropAPI.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFCreateAndDropAPI.Data;

public static class SeedData
{
    public static List<Participant> LoadParticipants()
    {
        return [
            new Participant { Id = 0, FName = "Omar", LName = "Youssef" },
                new Participant {Id = 1, FName = "Abdullah", LName = "Ali" },
            ];
    }
    public static List<Individual> LoadIndividuals()
    {
        return [
            new Individual { Id = 2, FName = "Omar", LName = "Youssef", University = "Cairo", YearOfGraduation = 2025, IsIntern = true },
                new Individual {Id = 3, FName = "Abdullah", LName = "Ali" , University = "Ain Shams", YearOfGraduation = 2026, IsIntern = false },
            ];
    }
    public static List<Coporate> LoadCoporates()
    {
        return [
            new Coporate { Id = 4, FName = "Omar", LName = "Youssef", Company = "Google", JobTitle = "Software Engineer" },
                new Coporate {Id = 5, FName = "Abdullah", LName = "Ali" , Company = "Microsoft", JobTitle = "Project Manager" },
            ];
    }
    public static List<MultipleChoiceQuiz> LoadMultipleChoiceQuizs()
    {
        return [
            new MultipleChoiceQuiz { Id = 0, Title = "Multiple Choice Quiz 1", OptionA = "a", OptionB = "b", OptionC = "c", OptionD = "d", CorrectAnswer = 'a' },
                new MultipleChoiceQuiz { Id = 1, Title = "Multiple Choice Quiz 2", OptionA = "a", OptionB = "b", OptionC = "c", OptionD = "d", CorrectAnswer = 'c' },
            ];
    }
    public static List<TrueAndFalseQuiz> LoadTrueAndFalseQuizs()
    {
        return [
            new TrueAndFalseQuiz { Id = 2, Title = "True and False Quiz 1", CorrectAnswer = true },
                new TrueAndFalseQuiz { Id = 3, Title = "True and False Quiz 2", CorrectAnswer = false },
            ];
    }
}

