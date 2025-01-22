using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;
        public ProjectService(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3)
        {
            var projects = _context.Projects
                 .Include(p => p.Client)
                 .Include(p => p.Freelancer)
                 .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                 .Skip(page * size)
                 .Take(size)
                 .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _context.Projects
             .Include(p => p.Client)
             .Include(p => p.Freelancer)
             .Include(p => p.Comments)
             .Where(p => !p.IsDeleted)
             .SingleOrDefault(p => p.Id == id);
            if (project is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            var model = ProjectViewModel.FromEntity(project);
            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(project.Id);
        }

        public ResultViewModel Update(UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == model.ProjectId);
            if (project is null) return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");
            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null) ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null) ResultViewModel<ProjectViewModel>.Error("Projeto não existe.");

            var comment = new ProjectComment(model.Content, model.ProjectId, model.UserId);

            _context.ProjectComments.Add(comment);
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
