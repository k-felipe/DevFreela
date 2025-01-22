using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<ResultViewModel>
    {
        public UpdateProjectCommand(int projectId, string title, string description, decimal totalCost)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }

        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}

