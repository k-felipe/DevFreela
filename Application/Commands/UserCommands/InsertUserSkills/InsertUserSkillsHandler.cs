using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUserSkills
{
    public class InsertUserSkillsHandler : IRequestHandler<InsertUserSkillsCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        public InsertUserSkillsHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertUserSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillsIds.Select(s => new UserSkill(request.Id, s))
                .ToList();

            if (userSkills is null)
                return ResultViewModel.Error("Habilidade(s) não encontrada(s).");

          await _repository.AddSkill(userSkills);

            return ResultViewModel.Success();
        }
    }
}
