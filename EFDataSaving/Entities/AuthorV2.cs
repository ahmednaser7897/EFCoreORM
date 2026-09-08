namespace EFDataSaving.Entities
{
    // Principal
    public class AuthorV2
    {
        public int Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public List<BookV2> BookV2s { get; set; } = [];
    }
}
