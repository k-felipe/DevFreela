using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;
        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task AddSkill(List<UserSkill> userSkill)
        {
            await _context.UserSkills.AddRangeAsync(userSkill);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
               .Include(u => u.Skills)
               .ThenInclude(u => u.Skill)
               .SingleOrDefaultAsync(u => u.Id ==id);
        }
    }
}
