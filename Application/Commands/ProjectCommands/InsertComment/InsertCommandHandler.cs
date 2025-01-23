using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommandHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public InsertCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.ProjectId);

            if (project is null) ResultViewModel.Error("Projeto não existe.");

            var comment = new ProjectComment(request.Content, request.ProjectId, request.UserId);

            await _repository.AddComment(comment);

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
