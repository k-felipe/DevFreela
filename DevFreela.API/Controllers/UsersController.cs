using DevFreela.Application.Commands.LoginCommands;
using DevFreela.Application.Commands.UserCommands.ChangePassword;
using DevFreela.Application.Commands.UserCommands.InsertUser;
using DevFreela.Application.Commands.UserCommands.InsertUserSkills;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.UserQueries.GetUserByEmail;
using DevFreela.Application.Queries.UserQueries.GetUserById;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        public UsersController(IMediator mediator, IMemoryCache cache, IEmailService emailService)
        {
            _mediator = mediator;
            _cache = cache;
            _emailService = emailService;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillsCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("password-recovery/request")]
        public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryRequestInputModel model)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(model.Email));

            if (!user.IsSuccess) return BadRequest(user.Message);

            var code = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"RecoveryCode:{model.Email}";

            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(user.Data.Email, "Código de recuperação", $"Seu código de recuperação é: {code}");

            return NoContent();
        }

        [HttpPost("password-recovery/validate")]
        public IActionResult ValidateRecoveryCode(ValidateRecoveryCodeInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";

            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
                return BadRequest();

            return Ok();
        }

        [HttpPost("password-recovery/change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";
            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
                return BadRequest();

            _cache.Remove(cacheKey);

            var user = await _mediator.Send(new GetUserByEmailQuery(model.Email));

            if (!user.IsSuccess) { return BadRequest(user.Message); }

            var result = await _mediator.Send(new ChangePasswordCommand(user.Data.Id, model.NewPassword));
            return Ok();
        }
    }
}
