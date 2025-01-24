using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.UpdateSkill
{
    public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, ResultViewModel>
    {
        private readonly ISkillRepository _repository;
        public UpdateSkillHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetById(request.Id);
            if (skill is null)
                return ResultViewModel<SkillViewModel>.Error("Habilidade não existe.");

            await _repository.Update(skill);

            return ResultViewModel.Success();
        }
    }
}
