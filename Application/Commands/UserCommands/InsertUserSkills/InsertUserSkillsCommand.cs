using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUserSkills
{
    public class InsertUserSkillsCommand :IRequest<ResultViewModel>
    {
        public InsertUserSkillsCommand(int[] skillsIds, int id)
        {
            SkillsIds = skillsIds;
            Id = id;
        }

        public int[] SkillsIds { get; set; }
        public int Id { get; set; }
    }
}
