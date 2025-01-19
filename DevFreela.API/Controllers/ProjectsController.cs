using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
             .Include(p => p.Client)
             .Include(p => p.Freelancer)
             .Include(p => p.Comments)
             .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(project);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted).ToList();

            Console.WriteLine(projects);
            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
            Console.WriteLine(model);

            return Ok(projects);
        }
     
            
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            Console.WriteLine(_context.Projects);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/complete")] 
        public IActionResult Finish(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComement(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            var comment = new ProjectComment(model.Content, model.ProjectId, model.UserId);

            _context.ProjectComments.Add(comment);
            _context.Projects.Update(project);
            _context.SaveChanges();

            return Ok();
        }

    }
}
