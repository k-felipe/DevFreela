﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
               .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
               .Include(u => u.Skills)
               .ThenInclude(u => u.Skill)
               .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> Login(string email, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
