using EFInterceptors.Entities.Contract;

namespace EFInterceptors.Entities
{
    public class Book : ISoftDeleteable
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
