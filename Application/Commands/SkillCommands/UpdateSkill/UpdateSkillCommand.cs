using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest<ResultViewModel>
    {

        public UpdateSkillCommand(int id, string description)
        {
            SkillId = id;
            Description = description;
        }

        public int SkillId { get; set; }
        public string Description { get; set; }
    }
}
