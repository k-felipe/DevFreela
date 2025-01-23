using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    internal class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Freelancer: Projeto criado {notification.Title}");

            return Task.CompletedTask;
        }
    }
}
