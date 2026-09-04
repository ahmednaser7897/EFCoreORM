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
    //-----------------------------------------------------------------------
    // Instructor and section has one to many relationship
    // where instructor may has many sections (Optional) 
    // where section may has an instructor (Optional)
    public class Instructor
    {
        public int Id { get; set; }
        //public string? Name { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        // where instructor must has an office (Required)
        // it must not be null because we will create a migration and 
        // EF Core will add a default value to the office id
        public int OfficeId { get; set; }
        public Office Office { get; set; } = null!;

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public override string ToString()
        {
            //return $"Instructor Name: {Name} | Id: {Id}";
            return $"Instructor Name: {FName + " " + LName} | Id: {Id} ";
        }
    }
}
