namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {
        public string Description { get; private set; }

        public Skill(string description) : base()
        {
            Description = description;
        }
        public List<UserSkill> UserSkills { get; set; }
    }
}
