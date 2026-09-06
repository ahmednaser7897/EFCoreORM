namespace EFQueryData.Entities
{
    public class Instructor : Entity
    {
        public string? FName { get; set; }
        public string? LName { get; set; }

        public int OfficeId { get; set; }
        public Office? Office { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public override string ToString()
        {
            return $"{Id}  | {FName} {LName}";
        }
    }
}
