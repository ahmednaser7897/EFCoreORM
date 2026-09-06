namespace EFCreateAndDropAPI.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public override string ToString()
        {
            return $"Participant Name: {FName} {LName} | Id: {Id} ";
        }

    }

    public class Individual : Participant
    {
        public string University { get; set; } = null!;
        public int YearOfGraduation { get; set; }
        public bool IsIntern { get; set; }

        public override string ToString()
        {
            return $"Individual Name: {FName} {LName} | Id: {Id} | University: {University} | Year Of Graduation: {YearOfGraduation} | Is Intern: {IsIntern} ";
        }
    }
    public class Coporate : Participant
    {
        public string Company { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public override string ToString()
        {
            return $"Coporate Name: {FName} {LName} | Id: {Id} | Company: {Company} | Job Title: {JobTitle} ";
        }
    }
}
