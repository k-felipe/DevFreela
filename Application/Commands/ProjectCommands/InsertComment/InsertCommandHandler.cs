using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommandHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId);

            if (project is null) ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            var comment = new ProjectComment(request.Content, request.ProjectId, request.UserId);

            await _context.ProjectComments.AddAsync(comment);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
