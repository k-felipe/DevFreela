using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.LoginCommands
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginViewModel>>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        public LoginHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }
        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _repository.Login(request.Email, hash);

            if (user is null)
                return ResultViewModel<LoginViewModel>.Error("Usuário ou senha incorreto(a).");

            var token = _authService.GenerateToken(user.Email, user.Role);

            var model = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel>.Success(model);
        }
    }
}
