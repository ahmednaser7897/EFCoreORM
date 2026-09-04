namespace EFMigration.Entities
{
    // Course and section has one to many relationship
    // where course may has many sections (Optional) 
    // where section must has a course (Required)
    public class Course
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public decimal Price { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public override string ToString()
        {
            return $"Course Name: {CourseName} | Id: {Id} | Price {Price}";
        }
    }
}
