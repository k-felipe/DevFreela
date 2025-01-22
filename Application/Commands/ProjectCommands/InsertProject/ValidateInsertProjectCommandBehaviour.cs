using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class ValidateInsertProjectCommandBehaviour : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertProjectCommandBehaviour(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)

        {
            var clientExists = _context.Users.Any(u => u.Id == request.ClientId);

            var freelancerExists = _context.Users.Any(u => u.Id == request.FreelancerId);

            if (!clientExists || !freelancerExists) return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos.");

            return await next();
        }
    }

}
