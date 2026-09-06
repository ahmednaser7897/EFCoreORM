namespace EFQueryData.Entities
{
    public class Participant : Entity
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public override string ToString()
        {
            return $"Participant ==> Id: {Id} | FirstName: {FName} | LastName: {LName}";
        }
    }
}
