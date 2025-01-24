using DevFreela.Application.Commands.ProjectCommands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Title)
                .Length(5, 50)
                .WithMessage("Tamanho do título inválido.");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("É necessário inserir uma descrição no projeto.");
        }
    }
}
