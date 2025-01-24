using DevFreela.Application.Commands.SkillCommands.InsertSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
    {
        public InsertSkillValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("É necessário inserir uma habilidade.");
        }
    }
}
