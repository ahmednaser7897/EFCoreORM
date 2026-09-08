namespace EFDataSaving.Entities
{
    // Dependent (FK)
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; } // FK
        public Author Author { get; set; } = null!;
        public required decimal Price { get; set; }
    }
}
