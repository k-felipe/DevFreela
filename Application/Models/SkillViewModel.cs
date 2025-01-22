using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillViewModel
    {
        public SkillViewModel(string description, List<string> userSkills)
        {
            Description = description;
            UserSkills = userSkills;
        }

        public string Description { get; set; }
        public List<string> UserSkills { get; set; }

        public static SkillViewModel FromEntity(Skill skill)
        {
            var userSkills = skill.UserSkills?
                    .Where(u => u.Skill != null)
                    .Select(u => u.Skill.Description)
                    .ToList() ?? new List<string>();
            return new SkillViewModel(skill.Description, userSkills);
        }
    }
}
