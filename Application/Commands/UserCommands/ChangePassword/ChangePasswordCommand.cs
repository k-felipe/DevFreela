using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ResultViewModel>
    {
        public ChangePasswordCommand(int id, string newPassword)
        {
            Id = id;
            NewPassword = newPassword;
        }

        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
