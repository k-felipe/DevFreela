using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.SkillCommands.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public InsertSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();

            _context.Skills.Add(skill);
           await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
