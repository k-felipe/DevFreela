using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.SkillCommands.UpdateSkill
{
    public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public UpdateSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Id == request.SkillId);
            if (skill is null)
                return ResultViewModel<SkillViewModel>.Error("Habilidade não existe.");

            skill.Update(request.Description);

            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
