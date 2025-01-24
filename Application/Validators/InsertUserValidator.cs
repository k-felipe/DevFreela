using DevFreela.Application.Commands.UserCommands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(u => u.FullName)
                .NotEmpty()
                .WithMessage("Nome inválido.");

            RuleFor(U => U.BirthDate)
                .Must(b => b < DateTime.Now.AddYears(-18))
                .WithMessage("É necessário ter mais de 18 anos.");


        }
    }
}
