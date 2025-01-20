using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        public ResultViewModel<UserViewModel> GetById(int id);
        public ResultViewModel<int> Insert(CreateUserInputModel model);
        public ResultViewModel InsertSkills(int id, UserSkillsInputModel model);

    }
}
