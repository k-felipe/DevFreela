using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillViewModel>> GetAll();
        ResultViewModel<int> Insert(int id, CreateSkillInputModel model);
        ResultViewModel Update(int id, UpdateSkillInputModel model);
    }
}
