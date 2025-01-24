using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _context;
        public SkillRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return skill.Id;   
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Skills.AnyAsync(s => s.Id == id);
        }

        public async Task<List<Skill>> GetAll()
        {
            return  await _context.Skills.ToListAsync();
        }

        public async Task<Skill?> GetById(int id)
        {
            return await _context.Skills.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task Update(Skill skill)
        {
            _context.Skills.Update(skill);

            await _context.SaveChangesAsync();
        }
    }
}
