using DevFreela.Application.Commands.SkillCommands.InsertSkill;
using DevFreela.Application.Commands.SkillCommands.UpdateSkill;
using DevFreela.Application.Queries.SkillQueries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSkillsQuery());

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(int id, InsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }


    }
}
