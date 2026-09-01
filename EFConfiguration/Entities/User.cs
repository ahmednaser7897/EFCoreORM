using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFConfiguration.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public override string ToString()
        {
            return $"UserId: {UserId}, Username: {Username}";
        }
    }

    //User class with data annotation
    // we use attributes to configure the model
    // it is more readable than convention but less flexible than fluent api
    // data annotation is a good option for simple configuration
    // but for complex configuration we should use fluent api
    [Table("tblUsers")] // you can change the table name 
    public class UserWithAnnotation
    {
        [Key]
        [Column("UserId")] // you can change the primary key
        public int MyId { get; set; }
        public string Username { get; set; } = null!;
        public override string ToString()
        {
            return $"UserId: {MyId}, Username: {Username}";
        }

    }
}
