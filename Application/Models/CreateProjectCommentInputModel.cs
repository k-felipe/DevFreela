namespace DevFreela.Application.Models
{
    public class CreateProjectCommentInputModel
    {
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
