﻿using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<int> Add(User user);
        Task AddSkill(List<UserSkill> userSkill);
        Task<User?> GetByEmail(string email);
        Task<User?> Login(string email, string password);
        Task Update(User user);
    }
}
