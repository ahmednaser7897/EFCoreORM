namespace EFMigration.Entities
{
    public class Enrollment
    {
        public int SectionId { get; set; }
        public int StudentId { get; set; }

        public Section Section { get; set; } = null!;
        public Student Student { get; set; } = null!;

        public override string ToString()
        {
            return $"Section: {Section.SectionName} , Student: {Student.FName} {Student.LName}";
        }
    }
}
