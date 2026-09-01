using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFConfiguration.Entities
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string TweetText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public override string ToString() => $"TweetId: {TweetId}, TweetText: {TweetText}, CreatedAt: {CreatedAt}";
    }
    [Table("tblTweets")]
    public class TweetWithAnnotation
    {
        [Key]
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string TweetText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public override string ToString() => $"TweetId: {TweetId}, TweetText: {TweetText}, CreatedAt: {CreatedAt}";
    }

}


