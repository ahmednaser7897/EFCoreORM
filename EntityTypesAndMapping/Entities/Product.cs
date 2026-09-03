using System.ComponentModel.DataAnnotations.Schema;

namespace EntityTypesAndMapping.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public string Description { get; set; } = null!;
        //this item is not exist in the database but it is exist in the entity class
        //so it will cause runtime error
        //so we will use [NotMapped] to solve it
        //now it will work fine because [NotMapped] will not create this column in the database
        //but it will not be saved in the database
        //so we need to use [Owned] to solve it
        //[Owned]
        [NotMapped]
        public Snapshot? LastUpdate { get; set; }
    }
}
