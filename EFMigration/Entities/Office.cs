namespace EFMigration.Entities
{
    // Instructor and office has one to one relationship
    // where instructor must has an office (Required)
    // where office may has an instructor (Optional)
    // so we will create a foreign key in the instructor table for the office id
    // so we will add OfficeId and Office propertys in Instructor class (table) and  
    // and the instructor id will be required (becuase instructor must has an office)
    // but the office id will be nullable (becuase office may has an instructor)
    // and in the office we just will add Instructor not id
    // the will refare to the  Instructor if the office has an instructor
    public class Office
    {
        public int Id { get; set; }
        public string? OfficeName { get; set; }
        public string? OfficeLocation { get; set; }
        public Instructor? Instructor { get; set; }
        public override string ToString()
        {
            return $"Office Name: {OfficeName} | Id: {Id} OfficeLocation: {OfficeLocation}";
        }
    }
}
