using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {
        public InsertCommentCommand(int projectId, string content, int userId)
        {
            ProjectId = projectId;
            Content = content;
            UserId = userId;
        }

        public int ProjectId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
