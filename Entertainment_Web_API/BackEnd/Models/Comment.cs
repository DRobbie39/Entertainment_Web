namespace BackEnd.Models
{
    public class Comment
    {
        public string CommentId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateOnly? CommentPostingTime { get; set; }
        public string? VideoId { get; set; }
        public string? Id { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual Video? Video { get; set; }
    }
}
