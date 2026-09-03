using System.ComponentModel.DataAnnotations.Schema;

namespace EntityTypesAndMapping.Entities
{
    //this is a Value Object
    //it will not be created as a table
    //but it will be created as a column in the parent entity table
    [NotMapped] // <--- this will not be created as a table
    public class Snapshot
    {
        public DateTime LoadedAt => DateTime.UtcNow;
        public String Version =>
            Guid.NewGuid().ToString().Substring(0, 8); // 81604D1D
    }
}