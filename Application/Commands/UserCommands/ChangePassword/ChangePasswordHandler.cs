using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        public ChangePasswordHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }
        public async Task<ResultViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _repository.GetById(request.Id);

            if (user is null)
                return ResultViewModel.Error("User not found.");

            var hash = _authService.ComputeHash(request.NewPassword);

            user.UpdatePassword(hash);

            await _repository.Update(user);

            return ResultViewModel.Success();
        }
    }
}
