﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly IUserRepository _repository;
        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null) return ResultViewModel<UserViewModel>.Error("Usuário não encontrado.");

            var model = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}
