using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //criar skillsviewmodel
            var skills = _context.Skills.ToList();
            return Ok(skills);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Post(int id, CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateSkillInputModel model)
        {
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return NoContent();
        //}

    }
}
