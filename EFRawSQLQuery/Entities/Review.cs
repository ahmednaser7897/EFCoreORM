namespace EFRawSQLQuery.Entities
{
    public class Review : Entity
    {
        public string Feedback { get; set; } = null!;

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public override string ToString()
        {
            return $"Review ==> Feedback: {Feedback} | Id: {Id} | CourseId: {CourseId} | CreatedAt: {CreatedAt}";
        }
    }
}
