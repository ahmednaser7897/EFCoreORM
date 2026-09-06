namespace EFCreateAndDropAPI.Entities
{
    public abstract class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public override string ToString()
        {
            return $"Quiz : Id: {Id} , Title : {Title}";
        }
    }

    public class MultipleChoiceQuiz : Quiz
    {
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        public char CorrectAnswer { get; set; }
        public override string ToString()
        {
            return $"MultipleChoiceQuiz : Id: {Id} , Title : {Title} , OptionA: {OptionA} , OptionB: {OptionB} , OptionC: {OptionC} , OptionD: {OptionD} , CorrectAnswer: {CorrectAnswer}";
        }
    }
    public class TrueAndFalseQuiz : Quiz
    {
        public bool CorrectAnswer { get; set; }
        public override string ToString()
        {
            return $"TrueAndFalseQuiz : Id: {Id} , Title : {Title} , CorrectAnswer: {CorrectAnswer}";
        }
    }
}