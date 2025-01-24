﻿
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill?> GetById(int id);
        Task <bool> Exists(int id);
        Task<int> Add(Skill skill);
        Task Update(Skill skill);
    }
}
