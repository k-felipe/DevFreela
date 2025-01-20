using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _service;
        public SkillsController(ISkillService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post(int id, CreateSkillInputModel model)
        {
            var result = _service.Insert(id, model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateSkillInputModel model)
        {
            var result = _service.Update(id, model);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }


    }
}
