namespace EFQueryData.Entities
{
    public class Course : Entity
    {
        public string? CourseName { get; set; }
        public decimal Price { get; set; }
        public int HoursToComplete { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public override string ToString()
        {
            return $"Course Name: {CourseName} | Id: {Id} | Price {Price} | Hours To Complete {HoursToComplete}";
        }
    }
}
