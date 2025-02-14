﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public StartProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);
            if (project is null) return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            project.Start();

            _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
