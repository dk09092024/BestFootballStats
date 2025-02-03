using Domain.Features.Leagues.Add;
using Domain.Features.Leagues.Delete;
using Domain.Features.Leagues.Get;
using Domain.Features.Leagues.LinkTeam;
using Domain.Features.Leagues.Update;
using FluentValidation;
using FootBallStatsApi.Controllers.DTOs.LeagueDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootBallStatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private IMediator _mediator;

        public LeagueController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLeague([FromRoute] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetLeagueQuery { Id = id });
                return Ok(new GetLeagueDto
                {
                    Id = result.League.Id,
                    Name = result.League.Name,
                    TeamIds = result.League.Teams.Select(team => team.Id).ToArray()
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
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto dto)
        {
            try
            {
                var result = await _mediator.Send(new AddLeagueRequest
                {
                    Name = dto.Name,
                });
                return Ok(new LeagueCreatedDto
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
        public async Task<IActionResult> UpdateLeague([FromBody] UpdateLeagueDto dto)
        {
            try
            {
                await _mediator.Send(new UpdateLeagueRequest
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteLeague([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteLeagueRequest
                {
                    Id = id
                });
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpPatch("link-team")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LinkTeam([FromBody] LinkTeamDto dto)
        {
            try
            {
                await _mediator.Send(new LinkTeamToLeagueRequest()
                {
                    LeagueId = dto.LeagueId,
                    TeamId = dto.TeamId
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
