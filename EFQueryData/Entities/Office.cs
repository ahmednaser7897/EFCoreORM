namespace EFQueryData.Entities
{
    public class Office : Entity
    {
        public string? OfficeName { get; set; }
        public string? OfficeLocation { get; set; }

        public Instructor? Instructor { get; set; }
        public override string ToString()
        {
            return $"Office Name: {OfficeName} | Id: {Id} | Office Location {OfficeLocation}";
        }
    }
}
