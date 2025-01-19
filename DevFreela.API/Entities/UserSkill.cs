namespace DevFreela.API.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int userId, int skillId) :base()
        {
            UserId = userId;
            SkillId = skillId;
        }

        public User User { get; private set; }
        public int UserId { get; private set; }
        public int SkillId { get; private set; }
        public Skill Skill { get; private set; }
    }
}
