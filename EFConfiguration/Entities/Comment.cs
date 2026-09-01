using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFConfiguration.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public override string ToString() => $"CommentId: {CommentId}, CommentText: {CommentText}, CreatedAt: {CreatedAt}";
    }
    [Table("tblComments")]
    public class CommentWithAnnotation
    {
        [Key]
        public int CommentId { get; set; }
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public override string ToString() => $"CommentId: {CommentId}, CommentText: {CommentText}, CreatedAt: {CreatedAt}";
    }
}



