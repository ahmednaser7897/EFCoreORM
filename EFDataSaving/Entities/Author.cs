namespace EFDataSaving.Entities
{
    // Principal
    public class Author
    {
        public int Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public List<Book> Books { get; set; } = [];
    }
}
