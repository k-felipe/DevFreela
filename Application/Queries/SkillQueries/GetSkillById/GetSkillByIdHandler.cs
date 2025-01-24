using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetSkillById
{
    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, ResultViewModel<SkillViewModel>>
    {
        private readonly ISkillRepository _repository;
        public GetSkillByIdHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<SkillViewModel>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetById(request.Id);
            if (skill is null)
                return ResultViewModel<SkillViewModel>.Error("Habilidade não existe.");

            var model = SkillViewModel.FromEntity(skill);
            return ResultViewModel<SkillViewModel>.Success(model);
        }
    }
}
