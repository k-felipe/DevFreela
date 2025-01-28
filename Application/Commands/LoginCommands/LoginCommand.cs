using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.LoginCommands
{
    public class LoginCommand : IRequest<ResultViewModel<LoginViewModel>>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
