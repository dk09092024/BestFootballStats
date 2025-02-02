using Domain.Features.Teams.Add;
using Domain.Features.Teams.Delete;
using Domain.Features.Teams.Get;
using Domain.Features.Teams.LinkPlayer;
using Domain.Features.Teams.Update;
using FluentValidation;
using FootBallStatsApi.Controllers.DTOs.TeamDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootBallStatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private Mediator _mediator;

        public TeamController(Mediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeam([FromRoute] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetTeamQuery { Id = id });
                return Ok(new GetTeamDto
                {
                    Id = result.Team.Id,
                    Name = result.Team.Name,
                    LeagueId = result.Team.LeagueId,
                    PlayerIds = result.Team.Players.Select(player => player.Id).ToArray()
                });
            }
            catch (ValidationException e)
            {
                return NotFound(e);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto dto)
        {
            try
            {
                var result = await _mediator.Send(new AddTeamRequest
                {
                    Name = dto.Name
                });
                return Ok(new TeamCreatedDto
                {
                    Id = result.Id
                });
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto dto)
        {
            try
            {
                await _mediator.Send(new UpdateTeamRequest
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteTeamRequest
                {
                    Id = id
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return NotFound(e);
            }
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LinkPlayerToTeam([FromBody] LinkPlayerToTeamDto dto)
        {
            try
            {
                await _mediator.Send(new LinkPlayerToTeamRequest
                {
                    TeamId = dto.TeamId,
                    PlayerId = dto.PlayerId
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
