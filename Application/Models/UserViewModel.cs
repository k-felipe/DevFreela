using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class UserViewModel
    {
        public UserViewModel(int id, string fullName, string email, DateTime birthDate, List<string> skills)
        {
            Id = Id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<string> Skills { get; private set; }

        public static UserViewModel FromEntity(User user)
        {
            var skills = user.Skills?
       .Where(u => u.Skill != null)
       .Select(u => u.Skill.Description)
       .ToList() ?? new List<string>();

            return new UserViewModel(user.Id, user.FullName, user.Email, user.BirthDate, skills);
        }
    }
}
