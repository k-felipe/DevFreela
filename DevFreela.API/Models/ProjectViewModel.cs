using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string description, int clientId, int freelancerId, string clientName, string freelancerName, decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = comments.Select(c => c.Content).ToList();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ClientId { get; private set; }
        public int FreelancerId { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<String> Comments { get; private set; }

        public static ProjectViewModel FromEntity(Project entity)=> new (entity.Id, entity.Title,entity.Description, entity.ClientId, entity.FreelancerId, entity.Client.FullName, entity.Freelancer.FullName, entity.TotalCost, entity.Comments);
    }
}
