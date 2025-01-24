using DevFreela.Application.Commands.ProjectCommands.InsertComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertCommentValidator : AbstractValidator<InsertCommentCommand>
    {
        public InsertCommentValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .WithMessage("Conteúdo do comentário não pode ser vazio");
        }
    }
}
