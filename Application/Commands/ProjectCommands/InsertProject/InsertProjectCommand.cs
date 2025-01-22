using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommand : IRequest<ResultViewModel<int>>
    {
        public InsertProjectCommand(string title, string description, int clientId, int freelancerId, decimal totalCost)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int FreelancerId { get; set; }
        public decimal TotalCost { get; set; }

        public Project ToEntity() => new(Title, Description, ClientId, FreelancerId, TotalCost);
    }
}
